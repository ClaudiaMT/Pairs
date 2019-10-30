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
using System.IO;
using Microsoft.Win32;

namespace Pairs {
    public partial class AddNewUser : Window {
        public static string DirBaza = Directory.GetCurrentDirectory();
        public static string NumeFisier = @".\utilizatori.txt";
        public static string NumeFolderImagini = @"Imagini";
        public static string NumeFolderSalvari = @"Salvari";

        public AddNewUser() {
            InitializeComponent();
        }

        private void btnSelectPoza_Click(object sender, RoutedEventArgs e) {
                OpenFileDialog openFilePozaDialog = new OpenFileDialog();
                openFilePozaDialog.Filter = "Image files (*.jpg;*.gif)|*.jpg;*.gif";//setaza filtrele pentru a arata doar fisierele cu extensia dorita JPG sau GIF
                openFilePozaDialog.InitialDirectory = (DirBaza + "\\" + NumeFolderImagini + "\\");//Open dialog deschide in folderul Imagini

                if (openFilePozaDialog.ShowDialog() == true) {
                    SelectieImagine.Text = openFilePozaDialog.FileName;// pune calea si numele fisierului in fereastra SelectieImagine de tip TextBox

                    SelectieImagine.CaretIndex = SelectieImagine.Text.Length;//afiseaza doar ultima parte daca nu incape
                    SelectieImagine.ScrollToEnd();
                    SelectieImagine.Focus();
                    SelectieImagine.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;//nu mai afiseaza scroll bar orizontal

                    PrevizualizarePoza.Source = new BitmapImage(new Uri(SelectieImagine.Text));// previzualizare poza selectata
                }
        }

        private void btnNewUserCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();// daca se selecteaza Cancel atunci inchide fereastra curenta
        }

        private void btnNewUserOK_Click(object sender, RoutedEventArgs e) {
            bool user_existent = false;

                if (NumeUser.Text == "") MessageBox.Show("Atentie\rNume user necompletat.", "Atentie!", MessageBoxButton.OK, MessageBoxImage.Warning);
                if (SelectieImagine.Text == "") MessageBox.Show("Atentie!\rImagine neselectata.", "Atentie!", MessageBoxButton.OK, MessageBoxImage.Warning);

                MainWindow.main.Dispatcher.Invoke(new Action(delegate() { user_existent = MainWindow.main.Verifica_utilizator(NumeUser.Text); })); // verifica daca numele de user ce se doreste a fi introdus este deja in lista_useri.
                if (user_existent) {
                    MessageBox.Show("Atentie\rNume user \"" + NumeUser.Text + "\" existent.", "Atentie!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (NumeUser.Text != "" && SelectieImagine.Text != "" && !user_existent) {
                    string NumeFisierNou = Path.GetFileName(SelectieImagine.Text);
                    //Console.WriteLine("\r\nNume fisier de copiat in folderul Imagini = " + NumeFisierNou);
                    string FisierNouImagine = DirBaza + "\\" + NumeFolderImagini + "\\" + NumeFisierNou;

                    if (!File.Exists(FisierNouImagine)) {
                        //Console.WriteLine("Fisierul " + NumeFisierNou + " nu exista, se va copia in folderul Imagini. " + FisierNouImagine);
                        File.Copy(SelectieImagine.Text, FisierNouImagine);//copiaza fisierul imagine ales in subdirectorul Imagini
                    }
                    else {
                        Console.WriteLine("Fisierul " + NumeFisierNou + " exista.");
                    }

                    using (StreamWriter file_out = File.AppendText(NumeFisier)) {
                        file_out.WriteLine(NumeUser.Text + "\t" + NumeFisierNou + "\t0\t"); // adauga in fisierul utilizatori.txt noul user cu: "NumeUser TAB NumeFisierNou TAB 0 TAB"
                        file_out.Flush();
                        file_out.Close();
                    }
                    MainWindow.main.Dispatcher.Invoke(new Action(delegate() { MainWindow.main.Fisier_utilizatoriTXT(); }));//apeleaza Fisier_utilizatoriTXT() din MainWindow pentu a actualiza ListView=lista_useri

                    this.Close();//inchide fereastra curenta
                }
        }
    }
}
