using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace Pairs {
    public partial class MainWindow : Window {
        public static MainWindow main; //ca sa primeasca update de la AddNewUser

        public static string DirBaza = Directory.GetCurrentDirectory();
        public static string NumeFisier = @".\utilizatori.txt";
        public static string NumeFolderImagini = @"Imagini";
        public static string NumeFolderSalvari = @"Salvari";

        public MainWindow() {
            InitializeComponent();
            main = this;//ca sa primeasca update de la AddNewUser
            Creare_Foldere();
            Fisier_utilizatoriTXT();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            About aboutWindow = new About();
            aboutWindow.Show();
        }

        private void Citeste_utilizatoriTXT() {
            using (StreamReader useri_streamer = new StreamReader(NumeFisier)) {
                while (useri_streamer.Peek() >= 0) {
                    string linie_citita;
                    string[] string_citit = { "", "", "", "" };
                    linie_citita = useri_streamer.ReadLine();
                    string_citit = linie_citita.Split('\t'); // caracterul dupa care se face extragerea informatiilor din fisierul utilizatori.txt este TAB
                    //Console.WriteLine("linie citita = " + linie_citita);

                    if (linie_citita != "") { // daca linia citita din utilizatori.txt nu este goala atunci scrie informatiile in ListView=lista_useri
                        lista_useri.Items.Add(new User() { UserName = string_citit[0], UserImg = string_citit[1], UserScor = string_citit[2] });
                    }
                }
                useri_streamer.Close();
            }
        }

        public class User {
            public string UserName { get; set; }
            public string UserImg { get; set; }
            public string UserScor { get; set; }
        }

        private void Creare_Foldere() {
            //Console.WriteLine();
            //Console.WriteLine("Directorul curent este " + DirBaza);

            if (Directory.Exists(NumeFolderImagini)) {
                Console.WriteLine("Folderul " + NumeFolderImagini + " exista.");
            }
            else {
                Console.WriteLine("Folderul " + NumeFolderImagini + " nu exista.");
                Directory.CreateDirectory(NumeFolderImagini);
                Console.WriteLine("Folderul " + NumeFolderImagini + " s-a creat in directorul curent.");
            }

            if (Directory.Exists(NumeFolderSalvari)) {
                Console.WriteLine("Folderul " + NumeFolderSalvari + " exista.");
            }
            else {
                Console.WriteLine("Folderul " + NumeFolderSalvari + " nu exista.");
                Directory.CreateDirectory(NumeFolderSalvari);
                Console.WriteLine("Folderul " + NumeFolderSalvari + " s-a creat in directorul curent.");
            }

            Console.WriteLine();
        }

        public void Fisier_utilizatoriTXT() {
            lista_useri.Items.Clear();
            //Console.WriteLine();

            if (File.Exists(NumeFisier)) {
                //Console.WriteLine("Fisierul exista.");
                Citeste_utilizatoriTXT();
            }
            else {
                //Console.WriteLine("Fisierul " + NumeFisier + " nu exista.");
                File.CreateText(NumeFisier);
                //Console.WriteLine("Fisierul " + NumeFisier + " s-a creat in directorul curent.");
            }

            Console.WriteLine();
        }

        public void Update_Scor_utilizator(string user_curent, string scor_actualizat) {
            for (int i = 0; i < lista_useri.Items.Count; i++) {
                User item = (lista_useri.Items[i] as User);
                if (item.UserName == user_curent && Int16.Parse(scor_actualizat) > Int16.Parse(item.UserScor)) { // daca este userul_curent si noul scor este mai mare decat cel din lista
                    lista_useri.SelectedIndex = i;//selecteaza randul unde a gasit userul
                    if (System.Windows.MessageBox.Show("Ati obtinut " + scor_actualizat + " puncte.\rDoriti sa actualizati scorul pentru userul " + item.UserName + "?", "Actualizare scor", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes) {  // afiseaza o ferestra daca doreste sa actualizeze scorul
                        item.UserScor = scor_actualizat; // actualizeaza scorul pentru userul ce a terminat jocul cu succes
                        //Console.WriteLine("\tindex = " + i);
                    }
                }
            }
            lista_useri.Items.Refresh();
            Salveaza_utilizatoriTXT();
        }

        public bool Verifica_utilizator(string user_nou) {
            foreach (User item in lista_useri.Items) {
                if (item.UserName == user_nou) {
                    return true;    // daca nume user exista in lista returneaza true
                }
            }
            return false;
        }

        private void lista_useri_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) {
            User selectedUser = lista_useri.SelectedItem as User;
            //Console.WriteLine("lista_useri.SelectedItem = " + lista_useri.SelectedItem.ToString());

            if (selectedUser != null) {
                string selectedName = selectedUser.UserName;
                string selectedImg = selectedUser.UserImg;
                string selectedScor = selectedUser.UserScor;
                //string selectedSave = selectedUser.UserSave;
                Console.WriteLine("name = " + selectedName);
                Console.WriteLine("img = " + DirBaza + selectedImg);
                Console.WriteLine("scor = " + selectedScor);
                //Console.WriteLine("save = " + selectedSave);

                poza.Source = new BitmapImage(new Uri(DirBaza + "\\" + NumeFolderImagini + "\\" + selectedImg));//compune calea catre fisierul poza

                txtBox_detalii.Document.Blocks.Clear();
                txtBox_detalii.AppendText("Nume:\t" + selectedName + "\rScor:\t" + selectedScor + "\rImagine:\t" + selectedImg);

                btnDeleteUser.IsEnabled = true; // face butoanele Delete si Play selectabile
                btnPlay.IsEnabled = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();//inchide fereastra curenta
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e) {
            AddNewUser newUserWindow = new AddNewUser();
            newUserWindow.Show();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e) {
            if (btnDeleteUser.IsEnabled) {// daca butonul este vizibil
                User user_selectat = lista_useri.SelectedItem as User;
                //Console.WriteLine("Sterge user index = " + lista_useri.SelectedIndex + "\tuser_selectat = " + user_selectat.UserName);

                lista_useri.Items.RemoveAt(lista_useri.SelectedIndex);
                lista_useri.UnselectAll();

                btnDeleteUser.IsEnabled = false; // face butoanele Delete si Play NEselectabile
                btnPlay.IsEnabled = false;

                poza.Source = null; // sterge imaginea user-ului din poza

                txtBox_detalii.Document.Blocks.Clear(); //sterge detaliile din txtBox_detalii

                Salveaza_utilizatoriTXT();

                //stergerea oricarui joc salvat de catre user sters mai sus user_selectat.UserName
                string[] filesToDelete = Directory.GetFiles(DirBaza + "\\" + NumeFolderSalvari + "\\", user_selectat.UserName + "*.*");
                filesToDelete.ToList().ForEach(file => File.Delete(file));
            }
        }

        private void Salveaza_utilizatoriTXT() {
            //salveaza integral ListView lista_useri in fisierul utilizatori.txt
            File.Delete(NumeFisier);// sterge fisierul utilizatori.txt
            StringBuilder string_linie = new StringBuilder();
            if (lista_useri.Items.Count > 0) { //construieste fisierul utilizatori.txt cu date din ListView=lista_useri
                using (StreamWriter file_out = new StreamWriter(NumeFisier)) {
                    foreach (User item in lista_useri.Items) {
                        file_out.WriteLine("{0}{1}{2}{3}{4}", item.UserName.ToString(), "\t", item.UserImg.ToString(), "\t", item.UserScor.ToString());
                    }
                    file_out.Flush();
                    file_out.Close();
                }
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e) {
            if (btnPlay.IsEnabled) {// daca butonul este vizibil
                User user_selectat = lista_useri.SelectedItem as User;

                Pairs_game newPairsGame = new Pairs_game();
                newPairsGame.Show();
                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.lblUserName.Text = user_selectat.UserName; }));//apeleaza Fisier_utilizatoriTXT() din MainWindow pentu a actualiza ListView=lista_useri
                Pairs_game.PairsGame.Dispatcher.Invoke(new Action(delegate() { Pairs_game.PairsGame.lblUserNameImg.Source = poza.Source; }));

                this.Close();
            }
        }
    }
}
