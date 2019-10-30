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
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Collections; // folosit pentru timer

namespace Pairs {
    public partial class Pairs_game : Window {
        public static Pairs_game PairsGame; //ca sa primeasca update de la MainWindow

        public static string DirBaza = Directory.GetCurrentDirectory();
        public static string NumeFisier = @".\utilizatori.txt";
        public static string NumeFolderImagini = @"Imagini";
        public static string NumeFolderSalvari = @"Salvari";

        public static string carte1 = "";//simbol prima carte intoarsa
        public static string carte2 = "";//simbol a 2a carte intoarsa
        public System.Windows.Controls.Button carte1_obj;
        public System.Windows.Controls.Button carte2_obj;

        public static int numar_total_de_perechi = 0;

        public static SolidColorBrush Background_card = new SolidColorBrush(Colors.DarkRed); // culori carti si tabla
        public static SolidColorBrush Foreground_card = new SolidColorBrush(Colors.Black);
        public static SolidColorBrush Bkgrd_view_card = new SolidColorBrush(Colors.Orange);
        public static SolidColorBrush Background_table = new SolidColorBrush(Colors.DarkSlateGray);

        public int l_card = 50;
        public int H_card = 75;
        public int L_intre_carduri = 20;
        public int fontsize_simbol = 45;


        public DispatcherTimer timer;//necesar pentru timer couwnt down, este public ca sa-l poata vedea si fereastra NewGame
        TimeSpan timp;

        DispatcherTimer timer2;//necesar pentru timer intoarce cartile si sa fie vizibile 1-2secunde si apoi sa dispara
        TimeSpan timp2;

        public Pairs_game() {
            InitializeComponent();
            PairsGame = this;//ca sa primeasca update de la MainWindow

            lblOptionsScor.Text = "0";// reseteaza scorul
            lblCowntDownTimerText.Visibility = System.Windows.Visibility.Hidden;// la inceput nu este selectat Timp deci nu este vizibil in taskbar
            lblCowntDownTimer.Visibility = System.Windows.Visibility.Hidden;

            //porneste timerul doar ca sa-l initializeze pentru a putea fi oprit cand NewGame, SaveGame, OpenGame sau Exit
            timp = TimeSpan.FromSeconds(0);
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate {
            }, Application.Current.Dispatcher);
            timer.Start();
            //Console.WriteLine("Timer start");
            timer.Stop();
            //Console.WriteLine("Timer stop");
        }

        public void TimerCowntDown(uint timp_presetat) {
            //timp = TimeSpan.FromSeconds(timp_presetat * 60);
            timp = TimeSpan.FromSeconds(timp_presetat);

            //Console.WriteLine("timp_presetat = " + timp_presetat + " minute");
            //Console.WriteLine("timp = " + timp);

            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate {
                lblCowntDownTimer.Text = timp.ToString("c");
                if (timp == TimeSpan.Zero) {
                    timer.Stop();
                    //Console.WriteLine("Timer stop @ end time interval");
                    MessageBox.Show("Timpul a expirat!\rGame Over", "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);

                    MainWindow mainWindow = new MainWindow(); // re-deschide fereastra MainWindow User
                    mainWindow.Show();

                    this.Close(); // aceasta fereastra se va inchide
                }
                timp = timp.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            timer.Start();
            //Console.WriteLine("Timer start");
        }

        public void Timer_VerificaCartile() {
            uint timp_presetat2 = 0;// presetat la secunde
            timp2 = TimeSpan.FromSeconds(timp_presetat2);
            //Console.WriteLine("timp_presetat2 = " + timp_presetat2 + " secunde");
            //Console.WriteLine("timp2 = " + timp2);

            timer2 = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate {
                //Console.WriteLine("timp2 = " + timp2 + " secunde");
                if (timp2 == TimeSpan.Zero) {
                    timer2.Stop();
                    timp2 = TimeSpan.FromSeconds(0);
                    //Console.WriteLine("Timer2 stop");

                    verifica_cartile();
                    carte1 = ""; // sterge simbolurile pentru a reseta verificarea a doua carti intoarse consecutiv
                    carte2 = "";
                    if (numar_total_de_perechi == 0) {
                        MessageBox.Show("Felicitari!\rAti obtinut " + lblOptionsScor.Text + " puncte.\rJocul s-a terminat.", "Game over", MessageBoxButton.OK, MessageBoxImage.Information); // afiseaza o ferestra ca ai castigat

                        MainWindow mainWindow = new MainWindow();// re-deschide fereastra MainWindow User
                        mainWindow.Show();
                        MainWindow.main.Dispatcher.Invoke(new Action(delegate() { MainWindow.main.Update_Scor_utilizator(lblUserName.Text, lblOptionsScor.Text); }));//pune scorul obtinut in fereastra MainWindow

                        this.Close(); // inchide fereastra curenta Pairs_game
                    }
                }
                timp2 = timp2.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            timer2.Start();
            //Console.WriteLine("Timer2 start");
        }

        public void verifica_cartile() {
            //Console.WriteLine("verifica cartile");

            if (carte1 != "" && carte2 != "") { // daca prima carte intoarsa contine simbol, deci si a 2a carte a fost intoarsa si contine simbol, atunci verifica cartile, altfel nu face nimic si merge mai departe
                if (carte1 == carte2) {
                    carte1_obj.Visibility = System.Windows.Visibility.Hidden; // sterge cartea1
                    carte2_obj.Visibility = System.Windows.Visibility.Hidden; // sterge cartea1
                    lblOptionsScor.Text = (Int32.Parse(lblOptionsScor.Text) + 10).ToString();// actualizeazas scorul din TaskBar
                    numar_total_de_perechi--; //daca s-a eliminat o pereche atunci decrementeaza variabila globa
                }
                else {
                    carte1_obj.Background = Background_card;//intoarce cartile curenta cu fata in jos
                    carte2_obj.Background = Background_card;

                    carte1_obj.Content = "";// face simbolurile invizibile
                    carte2_obj.Content = "";

                    lblOptionsScor.Text = (Int32.Parse(lblOptionsScor.Text) - 1).ToString();// actualizeazas scorul din TaskBar
                }
            }
        }

        public void generare_carti() {
            var random = new Random();

            //http://symbols.weebly.com/symbols-2.html
            //char[] chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            char[] chars = "♠♦♣♥☼☄★☆☊☎☮☯☹✉◊℧☀☁☂☃☇☈☉☋☌☍☎☏☚☛☜☝☞☟☢☣☽☾♔♕♖♗♘♙♚♛♜♝♞♟♢♤♧♨♩♪♫♬♭♮♯✂✄✆✇✈✉✌✍✎✏☮✮✯☻☺۞۩๑❀✿♆♪♩♫♬✄✂✦✧∞❦❧♒▲△▼ツ★☆№ஐϟ".ToCharArray();
            gridTabelaJoc.Children.Clear();
            gridTabelaJoc.RowDefinitions.Clear();
            gridTabelaJoc.ColumnDefinitions.Clear();
            Options.IsEnabled = false;// jocul a inceput, dezactiveaza Meniul Options

            //gridTabelaJoc.Background = new SolidColorBrush(Colors.DarkSlateGray);
            gridTabelaJoc.Background = Background_table;

            gridTabelaJoc.ShowGridLines = false;
            gridTabelaJoc.UseLayoutRounding = true;

            int rand_M = Convert.ToInt16(lblOptionsM.Text, 10);// converteste text in intreg
            int col_N = Convert.ToInt16(lblOptionsN.Text, 10);// converteste text in intreg
            numar_total_de_perechi = rand_M * col_N / 2;

            for (int i = 0; i < rand_M; i++) {
                RowDefinition gridRow = new RowDefinition();
                gridTabelaJoc.RowDefinitions.Add(gridRow);
            }
            for (int j = 0; j < col_N; j++) {
                ColumnDefinition gridCol = new ColumnDefinition();
                gridTabelaJoc.ColumnDefinitions.Add(gridCol);
            }

            bool intoarce_matricea = false;
            if (rand_M % 2 != 0) { // daca randurile M sunt impare atunci seteaza variabila pe true intoarce_matricea
                intoarce_matricea = true;
            }

            // MessageBox.Show("bool = " + intoarce_matricea + "\ri= " + rand_M + "\rj= " + col_N);
            Viewbox card_Viewbox;

            for (int i = 0; i < rand_M; i++) {
                for (int j = 0; j < col_N; j++) {
                    card_Viewbox = new Viewbox();

                    card_Viewbox.StretchDirection = StretchDirection.Both;// daca fereastra se redimensioneaza atunci se vor redimensiona si butoanele/carti
                    card_Viewbox.Stretch = Stretch.Uniform;
                    //card_Viewbox.Stretch = Stretch.Fill;

                    card_Viewbox.MinWidth = l_card + L_intre_carduri;
                    card_Viewbox.MinHeight = H_card + L_intre_carduri;
                    //card_Viewbox.MaxWidth = 300;// dimensiunile minime ale ViewBox
                    //card_Viewbox.MaxHeight = 200;

                    Button btnCard = new Button();
                    btnCard.Tag = chars[random.Next(0, chars.Length)];//asigneaza fiecarui buton un caracter aleator din stringul "chars" in Tag
                    btnCard.Content = "";
                    btnCard.FontWeight = FontWeights.UltraLight;

                    //btnCard.Foreground = new SolidColorBrush(Colors.Black); //culorile cartilor cu fata in jos
                    //btnCard.Background = new SolidColorBrush(Colors.DarkRed);
                    btnCard.Foreground = Foreground_card; //culorile cartilor cu fata in jos
                    btnCard.Background = Background_card;

                    btnCard.BorderThickness = new Thickness(0);
                    btnCard.VerticalAlignment = VerticalAlignment.Center;
                    btnCard.HorizontalAlignment = HorizontalAlignment.Center;

                    //btnCard.MinWidth = 60; //20;// dimensiunile minime ale butoanelor/carti
                    //btnCard.MinHeight = 90; // 30;
                    btnCard.MinWidth = l_card; //20;// dimensiunile minime ale butoanelor/carti
                    btnCard.MinHeight = H_card; // 30;

                    btnCard.ClickMode = ClickMode.Release;
                    btnCard.UseLayoutRounding = true;
                    btnCard.FontSize = fontsize_simbol;
                    btnCard.Click += Button_Click;//construieste "private void Button_Click" folosit atunci cand este apasata cartea/butonul

                    card_Viewbox.Child = btnCard;

                    Grid.SetRow(card_Viewbox, i);// pune pe pozitii in card_View fiecare card
                    Grid.SetColumn(card_Viewbox, j);

                    gridTabelaJoc.Children.Add(card_Viewbox);// adauga Viewbox la Grid

                    // ************ completeaza cardul 2 identic
                    card_Viewbox = new Viewbox();
                    card_Viewbox.StretchDirection = StretchDirection.Both;// daca fereastra se redimensioneaza atunci se vor redimensiona si butoanele/carti
                    card_Viewbox.Stretch = Stretch.Uniform;

                    card_Viewbox.MinWidth = l_card + L_intre_carduri;
                    card_Viewbox.MinHeight = H_card + L_intre_carduri;

                    Button btnCard2 = new Button();
                    btnCard2.Content = "";
                    btnCard2.Tag = btnCard.Tag; //copiaza acelasi simbol pentru al 2lea card identic
                    btnCard2.FontWeight = FontWeights.UltraLight;
                    btnCard2.Foreground = Foreground_card; //culorile cartilor cu fata in jos
                    btnCard2.Background = Background_card;
                    btnCard2.BorderThickness = new Thickness(0);
                    btnCard2.VerticalAlignment = VerticalAlignment.Center;
                    btnCard2.HorizontalAlignment = HorizontalAlignment.Center;

                    //btnCard2.MinWidth = 60;// dimensiunile minime ale butoanelor/carti
                    //btnCard2.MinHeight = 90;
                    btnCard2.MinWidth = l_card;// dimensiunile minime ale butoanelor/carti
                    btnCard2.MinHeight = H_card;

                    btnCard2.ClickMode = ClickMode.Release;
                    btnCard2.UseLayoutRounding = true;
                    btnCard2.FontSize = fontsize_simbol;

                    btnCard2.Click += Button_Click;//construieste "private void Button_Click" folosit atunci cand este apasata cartea/butonul
                    card_Viewbox.Child = btnCard2;

                    if (intoarce_matricea) {            // daca randurile M sunt impare, inseamna ca N coloanele sunt pare, pentru a avea M*N=par sa se poata face perechi de carti,
                        // variabila intoarce_matricea este true
                        Grid.SetRow(card_Viewbox, i);   // pune acelasi simbol pe pozitia= acelasi rand(variabila i) dar pe coloana urmatoare(j+1)
                        Grid.SetColumn(card_Viewbox, j + 1);
                    }
                    else {                                  // daca randurile M sunt pare, variabila intoarce_matricea este false
                        Grid.SetRow(card_Viewbox, i + 1);   // pune acelasi simbol pe pozitia= aceeasi coloana (variabila j) dar pe randul urmator (i+1)
                        Grid.SetColumn(card_Viewbox, j);
                    }
                    gridTabelaJoc.Children.Add(card_Viewbox);// adauga Viewbox la Grid

                    if (intoarce_matricea) {// daca randurile M sunt impare
                        j++;
                    }
                }
                if (!intoarce_matricea) {// daca randurile M sunt pare
                    i++;
                }
            }
            face_cartile();
        }

        public void face_cartile() { // "amesteca"cartile aleator
            Random random = new Random();

            for (int k = 0; k < gridTabelaJoc.Children.Count; k++) {
                int index = random.Next(k, gridTabelaJoc.Children.Count);

                UIElement element1 = gridTabelaJoc.Children[k];// selecteaza fiecare element din Grid generat de for(k)
                var copilul_elementului_selectat1 = VisualTreeHelper.GetChild(element1, 0) as ContainerVisual;
                Button buton1 = VisualTreeHelper.GetChild(copilul_elementului_selectat1, 0) as Button;// copilul este butonul din pozitia k din Grid.ViewBox
                
                string char_temp = buton1.Tag.ToString();//copiaza simbolul in variabila temporara

                UIElement element2 = gridTabelaJoc.Children[index];// selecteaza elementul din Grid din pozitia generata aleator de variabila index
                var copilul_elementului_selectat2 = VisualTreeHelper.GetChild(element2, 0) as ContainerVisual;
                Button buton2 = VisualTreeHelper.GetChild(copilul_elementului_selectat2, 0) as Button;// copilul este butonul din pozitia aleatoare din Grid.ViewBox
                
                buton1.Tag = buton2.Tag;
                buton2.Tag = char_temp;
            }  
        }

        private void incarca_carti(StringBuilder carti, StringBuilder vizibilitate) {
            int carte_invizibila = 0;//variabila numara cartie Hidden pentru a actualiza la sfarsit numarul de perechi ramase pana la terminarea jocului

            gridTabelaJoc.Children.Clear();
            gridTabelaJoc.RowDefinitions.Clear();
            gridTabelaJoc.ColumnDefinitions.Clear();
            Options.IsEnabled = false;// jocul a inceput, dezactiveaza Meniul Options

            //gridTabelaJoc.Background = new SolidColorBrush(Colors.DarkSlateGray);
            gridTabelaJoc.Background = Background_table;

            gridTabelaJoc.ShowGridLines = false;
            gridTabelaJoc.UseLayoutRounding = false;

            int rand_M = Convert.ToInt16(lblOptionsM.Text, 10);// converteste text in intreg
            int col_N = Convert.ToInt16(lblOptionsN.Text, 10);// converteste text in intreg

            numar_total_de_perechi = rand_M * col_N / 2;// calculeaza variabila numar_total_de_perechi total

            for (int i = 0; i < rand_M; i++) {
                RowDefinition gridRow = new RowDefinition();
                gridTabelaJoc.RowDefinitions.Add(gridRow);
            }
            for (int j = 0; j < col_N; j++) {
                ColumnDefinition gridCol = new ColumnDefinition();
                gridTabelaJoc.ColumnDefinitions.Add(gridCol);
            }

            bool intoarce_matricea = false;
            if (rand_M % 2 != 0) { // daca randurile M sunt impare atunci seteaza variabila pe true intoarce_matricea
                intoarce_matricea = true;
            }
            // MessageBox.Show("bool = " + intoarce_matricea + "\ri= " + rand_M + "\rj= " + col_N);

            int index = 0;
            Viewbox card_Viewbox;

            for (int i = 0; i < rand_M; i++) {
                for (int j = 0; j < col_N; j++) {

                    card_Viewbox = new Viewbox();

                    card_Viewbox.StretchDirection = StretchDirection.Both;// daca fereastra se redimensioneaza atunci se vor redimensiona si butoanele/carti
                    card_Viewbox.Stretch = Stretch.Uniform;

                    card_Viewbox.MinWidth = l_card + L_intre_carduri;
                    card_Viewbox.MinHeight = H_card + L_intre_carduri;

                    Button btnCard = new Button();
                    //MessageBox.Show(vizibilitate[0].ToString());

                    btnCard.Visibility = System.Windows.Visibility.Visible; // pentru inceput face butonul vizibil
                    if (vizibilitate[index].ToString() == "H") { //verifica daca butonul este vizibil sau nu
                        carte_invizibila++;
                        btnCard.Visibility = System.Windows.Visibility.Hidden;
                    }
                    btnCard.Tag = carti[index];
                    index++;

                    btnCard.Content = "";
                    btnCard.FontWeight = FontWeights.UltraLight;

                    //btnCard.Foreground = new SolidColorBrush(Colors.Black); //culorile cartilor cu fata in jos
                    //btnCard.Background = new SolidColorBrush(Colors.DarkRed);
                    btnCard.Foreground = Foreground_card; //culorile cartilor cu fata in jos
                    btnCard.Background = Background_card;

                    btnCard.BorderThickness = new Thickness(0);
                    btnCard.VerticalAlignment = VerticalAlignment.Center;
                    btnCard.HorizontalAlignment = HorizontalAlignment.Center;

                    //btnCard.MinWidth = 60; //20;// dimensiunile minime ale butoanelor/carti
                    //btnCard.MinHeight = 90; // 30;
                    btnCard.MinWidth = l_card; //20;// dimensiunile minime ale butoanelor/carti
                    btnCard.MinHeight = H_card; // 30;

                    btnCard.ClickMode = ClickMode.Release;
                    btnCard.UseLayoutRounding = true;
                    btnCard.FontSize = fontsize_simbol;
                    btnCard.Click += Button_Click;//construieste "private void Button_Click" folosit atunci cand este apasata cartea/butonul

                    card_Viewbox.Child = btnCard;

                    Grid.SetRow(card_Viewbox, i);// pune pe pozitii in card_View fiecare card
                    Grid.SetColumn(card_Viewbox, j);

                    gridTabelaJoc.Children.Add(card_Viewbox);// adauga Viewbox la Grid


//************************* cardul 2
                    card_Viewbox = new Viewbox();

                    card_Viewbox.StretchDirection = StretchDirection.Both;// daca fereastra se redimensioneaza atunci se vor redimensiona si butoanele/carti
                    card_Viewbox.Stretch = Stretch.Uniform;
                    //card_Viewbox.Stretch = Stretch.Fill;

                    //card_Viewbox.MinHeight = 20;
                    //card_Viewbox.MinWidth = 30;
                    card_Viewbox.MinWidth = l_card + L_intre_carduri;
                    card_Viewbox.MinHeight = H_card + L_intre_carduri;

                    Button btnCard2 = new Button();

                    btnCard2.Visibility = System.Windows.Visibility.Visible; // pentru inceput face butonul vizibil
                    if (vizibilitate[index].ToString() == "H") { //verifica daca butonul este vizibil sau nu
                        carte_invizibila++;
                        btnCard2.Visibility = System.Windows.Visibility.Hidden;
                    }
                    btnCard2.Tag = carti[index];
                    index++;

                    btnCard2.Content = "";
                    btnCard2.FontWeight = FontWeights.UltraLight;

                    //btnCard2.Foreground = new SolidColorBrush(Colors.Black); //culorile cartilor cu fata in jos
                    //btnCard2.Background = new SolidColorBrush(Colors.DarkRed);
                    btnCard2.Foreground = Foreground_card; //culorile cartilor cu fata in jos
                    btnCard2.Background = Background_card;

                    btnCard2.BorderThickness = new Thickness(0);
                    btnCard2.VerticalAlignment = VerticalAlignment.Center;
                    btnCard2.HorizontalAlignment = HorizontalAlignment.Center;

                    //btnCard2.MinWidth = 60; //20;// dimensiunile minime ale butoanelor/carti
                    //btnCard2.MinHeight = 90; // 30;
                    btnCard2.MinWidth = l_card; //20;// dimensiunile minime ale butoanelor/carti
                    btnCard2.MinHeight = H_card; // 30;

                    btnCard2.ClickMode = ClickMode.Release;
                    btnCard2.UseLayoutRounding = true;
                    btnCard2.FontSize = fontsize_simbol;
                    btnCard2.Click += Button_Click;//construieste "private void Button_Click" folosit atunci cand este apasata cartea/butonul

                    card_Viewbox.Child = btnCard2;

                    if (intoarce_matricea) {            // daca randurile M sunt impare, inseamna ca N coloanele sunt pare, pentru a avea M*N=par sa se poata face perechi de carti,
                        // variabila intoarce_matricea este true
                        Grid.SetRow(card_Viewbox, i);   // pune acelasi simbol pe pozitia= acelasi rand(variabila i) dar pe coloana urmatoare(j+1)
                        Grid.SetColumn(card_Viewbox, j + 1);
                    }
                    else {                                  // daca randurile M sunt pare, variabila intoarce_matricea este false
                        Grid.SetRow(card_Viewbox, i + 1);   // pune acelasi simbol pe pozitia= aceeasi coloana (variabila j) dar pe randul urmator (i+1)
                        Grid.SetColumn(card_Viewbox, j);
                    }
                    gridTabelaJoc.Children.Add(card_Viewbox);// adauga Viewbox la Grid

                    if (intoarce_matricea) {// daca randurile M sunt impare
                        j++;
                    }
                }
                if (!intoarce_matricea) {// daca randurile M sunt pare
                    i++;
                }
            }
            numar_total_de_perechi = (rand_M * col_N - carte_invizibila) / 2;// actualizeaza variabila numar_total_de_perechi cate au mai ramas neintoarse
        }





        private void Button_Click(object sender, RoutedEventArgs e) {
            if (sender is System.Windows.Controls.Button) {
                System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;

                if (btn.Content.ToString() == "" && carte2 == "") {//daca cartea selectata este cu fata in jos si daca a 2a carte nu este intoarsa cu fata in sus, astfel blocheza intoarcerea altor carti daca 2 carti sunt intoarse deja
                    //btn.Background = new SolidColorBrush(Colors.Orange);    //intoarce cartea cu fata in sus
                    btn.Background = Bkgrd_view_card;    //intoarce cartea cu fata in sus

                    btn.Content = btn.Tag; // copiaza in Content(informatia vizibila de pe buton) caracterul din Tag

                    if (carte1 == "") { // daca este prima carte intoarsa
                        carte1 = btn.Tag.ToString(); // salveaza simbol in carte1
                        carte1_obj = btn;// salveaza obiectul cartii1 pentru a fi evaluat in Timer_VerificaCartile()
                    }
                    else {
                        carte2 = btn.Tag.ToString(); // salveaza simbol in carte2
                        carte2_obj = btn;// salveaza obiectul cartii2 pentru a fi evaluat in Timer_VerificaCartile()
                        Timer_VerificaCartile();//daca si cartea a 2a a fost intoarsa porneste un timer de 2 secunde dupa care verifica cartile
                    }
                }
            }
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e) {
            About aboutWindow = new About();
            aboutWindow.Show();
        }

        private void MenuItem_Click_NewGame(object sender, RoutedEventArgs e) {
            lblOptionsScor.Text = "0";// reseteaza scorul din TaskBar
            numar_total_de_perechi = 0;//reseteaza contorul de numar perechi

            //sterge tabela de joc
            gridTabelaJoc.Children.Clear();//sterge din tabela de joc cartile continute
            gridTabelaJoc.RowDefinitions.Clear();// sterge Grid
            gridTabelaJoc.ColumnDefinitions.Clear();

            if (timer.IsEnabled) {
                timer.Stop();// opreste timer in cazul in care a fost un joc anterior cu Timp
                //Console.WriteLine("Timer stop");
            }

            NewGame newPairGame = new NewGame();
            newPairGame.ShowDialog();//deschide fereastra NewGame si asteapta pana cand fereastra va fi inchisa

            //if (lblCowntDownTimer.Text == "0") {
            //    //Console.WriteLine("tip joc: Scor");
            //}
            //else {
            //    //Console.WriteLine("tip joc: Timp");
            //}
        }

        private void MenuItem_Click_OpenGame(object sender, RoutedEventArgs e) {
            bool timer_run = false;
            if (timer.IsEnabled) {
                timer.Stop();// opreste timer in cazul in care se doreste salvarea acestuia
                timer_run = true;
                //Console.WriteLine("Timer stop");
            }

            OpenFileDialog openFileOpenGameDialog = new OpenFileDialog();
            openFileOpenGameDialog.Filter = "Saved games (*.txt)|" + lblUserName.Text + "*.txt";
            openFileOpenGameDialog.InitialDirectory = (DirBaza + "\\" + NumeFolderSalvari + "\\");//Open dialog deschide in folderul Salvari

            if (openFileOpenGameDialog.ShowDialog() == true) {
                //MessageBox.Show("Jocul a fost incarcat", "Open game", MessageBoxButton.OK, MessageBoxImage.Information);
                StreamReader reader_openFile = new StreamReader(openFileOpenGameDialog.FileName);

                string linie_curenta = reader_openFile.ReadLine(); //citeste prima linie: User Scor Timp_ramas M N
                string[] string_citit = { "", "", "", "", "" };
                string_citit = linie_curenta.Split('\t'); // caracterul dupa care se face extragerea informatiilor din fisierul salvat este TAB

                lblUserName.Text = string_citit[0];
                lblOptionsScor.Text = string_citit[1];

                if (string_citit[2] != "0") {
                    lblCowntDownTimerText.Visibility = System.Windows.Visibility.Visible;// la inceput nu este selectat Timp deci trebuie facut vizibil in taskbar
                    lblCowntDownTimer.Visibility = System.Windows.Visibility.Visible;

                    uint timp_nou = Convert.ToUInt32(string_citit[2], 10);
                    TimerCowntDown(timp_nou + 1); //seteaza timerul cu noul timp citit din fisier + 1secunda
                    timer.IsEnabled = true; //porneste timerul cu noul timp citit din fisier
                }

                lblOptionsM.Text = string_citit[3];
                lblOptionsN.Text = string_citit[4];

                string[] string_carti_citit = { "", ""};// are 2 elemente, simbolul cartii si daca este vizibil sau nu(a fost intoarsa sau nu)

                System.Text.StringBuilder simbol = new System.Text.StringBuilder();
                System.Text.StringBuilder vizibil = new System.Text.StringBuilder();

                while ((linie_curenta = reader_openFile.ReadLine()) != null)
                {
                    string_carti_citit = linie_curenta.Split('\t'); // caracterul dupa care se face extragerea informatiilor din fisierul salvat este TAB
               
                    //MessageBox.Show(string_carti_citit[0]);
                    simbol.Append(string_carti_citit[0]);//construieste un string cu simbolurile din fisierul incarcat
                    vizibil.Append(string_carti_citit[1].Substring(0, 1));//construieste un string doar cu prima litera V de la Visible sau H de la Hidden
                }

                incarca_carti(simbol, vizibil);
            }

            if (timer_run) {
                //MessageBox.Show("timer start");
                //Console.WriteLine("Timer start");
                timer.Start();//porneste timerul dupa ce a incarcat jocul
            }
        }

        private void MenuItem_Click_SaveGame(object sender, RoutedEventArgs e) {
            bool timer_run = false;
            if (timer.IsEnabled) {
                timer.Stop();// opreste timer in cazul in care a fost un joc anterior cu Timp
                timer_run = true;
                //Console.WriteLine("Timer stop");
                //MessageBox.Show("timer stop");
            }

            SaveFileDialog openFileSaveGameDialog = new SaveFileDialog();
            openFileSaveGameDialog.Filter = "Save game (*.txt)|" + lblUserName.Text + "*.txt";
            openFileSaveGameDialog.InitialDirectory = (DirBaza + "\\" + NumeFolderSalvari + "\\");//Open dialog deschide in folderul Salvari

            String DataTimp = DateTime.Now.ToString("yyMMddHHmmss");// variabila DataTimp este de forma 180421125859 si se va adauga la numele fisierului
            // formatul fisierul de salvat Clau_180421120059.txt
            // prefixul NumeUtilizator "Clau" este obligatoriu pentru a face filtrarea in openDialog si saveDialog.

            openFileSaveGameDialog.FileName = lblUserName.Text + "_" + DataTimp;

            if (openFileSaveGameDialog.ShowDialog() == true) {
                File.WriteAllText(openFileSaveGameDialog.FileName, lblUserName.Text + "\t" + lblOptionsScor.Text + "\t" + timp.TotalSeconds.ToString() + "\t" + lblOptionsM.Text + "\t" + lblOptionsN.Text + "\r");
                
                for (int k = 0; k < gridTabelaJoc.Children.Count; k++) {
                    UIElement element1 = gridTabelaJoc.Children[k];// selecteaza fiecare element din Grid generat de for(k)
                    var copilul_elementului_selectat1 = VisualTreeHelper.GetChild(element1, 0) as ContainerVisual;
                    Button buton = VisualTreeHelper.GetChild(copilul_elementului_selectat1, 0) as Button;// copilul este butonul din pozitia k din Grid.ViewBox

                    File.AppendAllText(openFileSaveGameDialog.FileName, buton.Tag + "\t" + buton.Visibility + "\r");
                }
                //MessageBox.Show("Jocul a fost salvat", "Save game", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (timer_run) {
                //MessageBox.Show("timer start");
                //Console.WriteLine("Timer start");
                timer.Start();//porneste timerul dupa ce a salvat jocul
            }
        }

        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e) {
            if (timer.IsEnabled) {
                timer.Stop();// opreste timer in cazul in care a fost un joc anterior cu Timp
                //Console.WriteLine("Timer stop");
            }

            MainWindow mainWindow = new MainWindow();// re-deschide fereastra MainWindow User
            mainWindow.Show();

            this.Close();
        }

        private void MenuItem_Click_Beginner(object sender, RoutedEventArgs e) {
            Intermediate.IsChecked = false;
            Beginner.IsChecked = true;
            Custom.IsChecked = false;
            lblOptions.Text = "Beginner";
            lblOptionsM.Text = "4";
            lblOptionsN.Text = "4";
        }

        private void MenuItem_Click_Intermediate(object sender, RoutedEventArgs e) {
            Intermediate.IsChecked = true;
            Beginner.IsChecked = false;
            Custom.IsChecked = false;
            lblOptions.Text = "Intermediate";
            lblOptionsM.Text = "6";
            lblOptionsN.Text = "6";
        }

        private void MenuItem_Click_Custom(object sender, RoutedEventArgs e) {
            Intermediate.IsChecked = false;
            Beginner.IsChecked = false;
            Custom.IsChecked = true;
            lblOptions.Text = "Custom";
            lblOptionsM.Text = "0";
            lblOptionsN.Text = "0";

            Custom_format newCustom_format = new Custom_format();
            newCustom_format.Show();
        }

        private void MenuItem_Click_Info(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Pentru fiecare pereche identica de carti punctajul este +10 puncte.\r\rDaca simbolurile de pe cartile intoarse nu sunt identice atunci punctajul este -1 punct.\r\rPentru tipul de joc cu timp se introduce valoarea in minute.\r\rDaca jocul s-a finalizat si user-ul selectat a obtinut mai multe puncte decat a avut pana la acel moment,\ratunci acest nou scor se va actualiza.", "Pairs Info", MessageBoxButton.OK, MessageBoxImage.Information); // afiseaza o ferestra cu informatii despre joc
        }
    }
}
