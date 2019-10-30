using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pairs {
    public partial class NewGame : Window {
        public static NewGame newGame; //ca sa primeasca update de la Pairs_gameWindow
        int valoare_min = 1, valoare_max = 60 * 24, valoare_initiala = 1; //timp de joc in minute

        public NewGame() {
            InitializeComponent();
            newGame = this; //ca sa primeasca update de la Pairs_gameWindow

            TimpSelectat.Text = valoare_initiala.ToString();
            UpDown.Minimum = valoare_min;
            UpDown.Maximum = valoare_max;
            UpDown.SmallChange = 1;
        }

        private void btnNewPairGame_OK_Click(object sender, RoutedEventArgs e) {
            if (btnRtimp.IsChecked == true) { // daca butonul radio este selectat pe Timp atunci copiaza timpul presetat din TimpSelectat in status bar din fereastra PairsGame in campul lblCowntDownTimer in dreapta jos
                uint timp = Convert.ToUInt32(TimpSelectat.Text, 10);// converteste timpul selectat TimpSelectat.Text in minute in int fara semn in baza 10

                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.TimerCowntDown(timp*60); })); // face update in lblCowntDownTimer.Text din PairsWindow pentu a actualiza timpul presetat
                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.lblCowntDownTimerText.Visibility = System.Windows.Visibility.Visible; }));// face vizibil textul Timp Pairs_game in taskbar

                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.lblCowntDownTimer.Visibility = System.Windows.Visibility.Visible; }));    // face vizibil Timpul presetat in Pairs_game in taskbar

                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.generare_carti(); })); // genereaza tabela de carti in fereastra PairsWindow

                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.timer.IsEnabled = true; })); // activeaza timerul CountDown
            }
            else {//altfel afiseaza doar scorul
                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.lblCowntDownTimerText.Visibility = System.Windows.Visibility.Hidden; }));// face invizibil textul Timp Pairs_game in taskbar
                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.lblCowntDownTimer.Visibility = System.Windows.Visibility.Hidden; }));    // face invizibil Timpul presetat in Pairs_game in taskbar
                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.lblCowntDownTimer.Text = "0"; }));
                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.generare_carti(); })); // genereaza tabela de carti in fereastra PairsWindow
                
                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.timer.IsEnabled = false; })); // dezactiveaza timerul CountDown
            }
            this.Close();
        }

        private void btnRScor_Click(object sender, RoutedEventArgs e) {
            TimpSelectat.IsEnabled = false;
        }

        private void btnRtimp_Click(object sender, RoutedEventArgs e) {
            TimpSelectat.IsEnabled = true;
            //Console.WriteLine("timpi minute = " + valoare_min + " " + valoare_max + " " + valoare_initiala);
        }

        private void UpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            TimpSelectat.Text = UpDown.Value.ToString();
        }
    }
}
