using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class Custom_format : Window {
        public Custom_format() {
            InitializeComponent();
        }

        private void btnCustom_OK_Click(object sender, RoutedEventArgs e) {
            var regex = new Regex("[^0-9]+"); // copnstruieste regex pentru verificare daca sunt numere intregi pozitive

            if (txtM.Text != "" && txtN.Text != "" && !regex.IsMatch(txtM.Text) && !regex.IsMatch(txtN.Text)) {// verifica daca M si N exista si sunt numere!!!
                //Console.WriteLine("txtM = " + txtM.Text);
                //Console.WriteLine("txtN = " + txtN.Text);
                int M = Convert.ToInt16(txtM.Text, 10);// converteste text in intreg pentru a calcula daca MxN=numar_par
                int N = Convert.ToInt16(txtN.Text, 10);

                if (M == 0 || N == 0) {
                    //Console.WriteLine("numar zero " + M + "\t" + N);
                    MessageBox.Show("Unul dintre numere este 0!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                    Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.Beginner.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent)); }));
                    // selecteaza din Meniu / Option / Beginner
                }

                else {
                    if ((M * N) % 2 != 0) {
                        //Console.WriteLine("numar impar " + (M * N).ToString());
                        MessageBox.Show("(linii x coloane) este numar impar\rNumarul jetoanelor de pe tabla trebuie sa fie numar par", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                        Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.Beginner.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent)); }));
                        // selecteaza din Meniu / Option / Beginner
                    }

                    else {
                        //Console.WriteLine("numar par " + (M * N).ToString());
                        Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() {
                            Pairs_game.PairsGame.lblOptionsM.Text = txtM.Text;
                            Pairs_game.PairsGame.lblOptionsN.Text = txtN.Text;
                        }));
                    }
                }
            }
            else {
                MessageBox.Show("Unul dintre numere nu a fost introdus\rsau nu este numar intreg pozitiv", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.Beginner.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent)); }));
                // selecteaza din Meniu / Option / Beginner
            }
            this.Close();
        }

        private void btnCustom_Cancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

    }
}
