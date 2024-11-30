using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1_final_MasterMind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //random nummer genereren
        Random rnd = new Random();
        //kleuren doorheen de code gebruiken


        string[] kleuren = new string[4];

        int attempts;
        //start van het spel 100 punten
        int points = 100;

        int row = 0;

        public MainWindow()
        {
            InitializeComponent();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            GenerateNumbers();

            pointslabel.Content = $"Jouw huidige score: {points}/100";
            solutionTextBox.Visibility = Visibility.Hidden;


        }

        private void GenerateNumbers()
        {

            kleuren[0] = ChooseColor(rnd.Next(0, 6));
            kleuren[1] = ChooseColor(rnd.Next(0, 6));
            kleuren[2] = ChooseColor(rnd.Next(0, 6));
            kleuren[3] = ChooseColor(rnd.Next(0, 6));

            solutionTextBox.Text = $"MasterMind: {kleuren[0]}, {kleuren[1]}, {kleuren[2]}, {kleuren[3]}";
        }
        /// <summary>
        /// Via deze method wordt er een kleur gegeven aan het random nummer
        /// </summary>
        /// <param name="willekeurigNummer">Geeft een willekeurig nummer mee</param>
        /// <returns>Een kleurnaam adhv willekeurig nummer</returns>
        private string ChooseColor(int willekeurigNummer)
        {
            if (willekeurigNummer == 0)
            {
                return "Rood";
            }
            else if (willekeurigNummer == 1)
            {
                return "Geel";
            }
            else if (willekeurigNummer == 2)
            {
                return "Oranje";
            }
            else if (willekeurigNummer == 3)
            {
                return "Wit";
            }
            else if (willekeurigNummer == 4)
            {
                return "Groen";
            }
            else if (willekeurigNummer == 5)
            {
                return "Blauw";
            }
            else
            {
                return "ERROR";
            }
        }

        private void colorChange(object sender, SelectionChangedEventArgs e)
        {

            ComboBox changedcombobox = sender as ComboBox;

            if (changedcombobox == comboBox1)
            {
                label1.Background = GetColorFromIndex(changedcombobox.SelectedIndex);
            }
            else if (changedcombobox == comboBox2)
            {
                label2.Background = GetColorFromIndex(changedcombobox.SelectedIndex);
            }
            else if (changedcombobox == comboBox3)
            {
                label3.Background = GetColorFromIndex(changedcombobox.SelectedIndex);
            }
            else if (changedcombobox == comboBox4)
            {
                label4.Background = GetColorFromIndex(changedcombobox.SelectedIndex);
            }

        }
        private Brush GetColorFromIndex(int selectedIndex)
        {

            switch (selectedIndex)
            {
                case 0:

                    return Brushes.Red;

                case 1:

                    return Brushes.White;

                case 2:
                    return Brushes.Yellow;

                case 3:
                    return Brushes.Orange;

                case 4:
                    return Brushes.Green;

                case 5:
                    return Brushes.Blue;

                default:
                    return Brushes.White;


            }
        }


        // solution-show f9
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F9 && solutionTextBox.Visibility == Visibility.Visible)
            {
                solutionTextBox.Visibility = Visibility.Hidden;
            }
            else if (e.Key == Key.F9)
            {
                solutionTextBox.Visibility = Visibility.Visible;
            }
        }

        private void checkButton_Click(object sender, RoutedEventArgs e)
        {

            LabelChanged(label1, 0, comboBox1);
            LabelChanged(label2, 1, comboBox2);
            LabelChanged(label3, 2, comboBox3);
            LabelChanged(label4, 3, comboBox4);


            attempts++;
            UpdateTitle();
            Historiek();
            pointslabel.Content = $"Jouw huidige score: {points}/100";
            HasWon();





            if (attempts >= 10)
            {
                MessageBoxResult result = MessageBox.Show($"You Failed!" +
                 $" De juiste kleurencombinatie: {kleuren[0]}, {kleuren[1]}, {kleuren[2]}, {kleuren[3]}  \r\n " +
                 $"Wil je opnieuw proberen?",
                 "Play Again?",
                 MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    points = 100;
                    attempts = 0;
                    UpdateTitle();
                    GenerateNumbers();
                    comboBox1.Text = "";
                    label1.BorderThickness = new Thickness(0);
                    comboBox2.Text = "";
                    label2.BorderThickness = new Thickness(0);
                    comboBox3.Text = "";
                    label3.BorderThickness = new Thickness(0);
                    comboBox4.Text = "";
                    label4.BorderThickness = new Thickness(0);
                    historiekgrid.Children.Clear();
                    pointslabel.Content = $"Jouw huidige score: {points}/100";


                }
                else
                {
                    this.Close();
                }
            }

        }
        private void UpdateTitle()
        {

            this.Title = $"Mastermind: {attempts} pogingen ondernomen";

        }
        private void LabelChanged(Label kleurLabel, int positie, ComboBox input)

        {
            string oplossing;


            switch (positie)
            {
                case 0:
                    oplossing = kleuren[0];
                    break;

                case 1:
                    oplossing = kleuren[1];
                    break;

                case 2:
                    oplossing = kleuren[2];
                    break;
                case 3:
                    oplossing = kleuren[3];
                    break;
                default:
                    oplossing = "error";
                    break;

            }

            if (input.Text == oplossing)
            {
                kleurLabel.BorderBrush = Brushes.DarkRed;
                kleurLabel.BorderThickness = new Thickness(4);

            }
            else if (solutionTextBox.Text.Contains(input.Text) && input.Text != "")
            {
                kleurLabel.BorderBrush = Brushes.Wheat;
                points -= 1;
                kleurLabel.BorderThickness = new Thickness(4);
            }
            else
            {
                kleurLabel.BorderThickness = new Thickness(0);
                points -= 2;
            }

        }
        private void Historiek()
        {
            RowDefinition newRow = new RowDefinition();
            newRow.Height = GridLength.Auto;
            historiekgrid.RowDefinitions.Add(newRow);

            Label historiekLabel1 = new Label();
            historiekLabel1.Height = 50;
            historiekLabel1.Width = 50;
            historiekLabel1.Margin = new Thickness(2);
            historiekLabel1.Background = label1.Background;
            historiekLabel1.BorderBrush = label1.BorderBrush;
            historiekLabel1.BorderThickness = label1.BorderThickness;

            Grid.SetRow(historiekLabel1, row);
            Grid.SetColumn(historiekLabel1, 0);

            Label historiekLabel2 = new Label();
            historiekLabel2.Height = 50;
            historiekLabel2.Width = 50;
            historiekLabel2.Margin = new Thickness(2);
            historiekLabel2.Background = label2.Background;
            historiekLabel2.BorderBrush = label2.BorderBrush;
            historiekLabel2.BorderThickness = label2.BorderThickness;
            Grid.SetRow(historiekLabel2, row);
            Grid.SetColumn(historiekLabel2, 1);

            Label historiekLabel3 = new Label();
            historiekLabel3.Height = 50;
            historiekLabel3.Width = 50;
            historiekLabel2.Margin = new Thickness(2);
            historiekLabel3.Background = label3.Background;
            historiekLabel3.BorderBrush = label3.BorderBrush;
            historiekLabel3.BorderThickness = label3.BorderThickness;
            Grid.SetRow(historiekLabel3, row);
            Grid.SetColumn(historiekLabel3, 2);

            Label historiekLabel4 = new Label();
            historiekLabel4.Height = 50;
            historiekLabel4.Width = 50;
            historiekLabel2.Margin = new Thickness(2);
            historiekLabel4.Background = label4.Background;
            historiekLabel4.BorderBrush = label4.BorderBrush;
            historiekLabel4.BorderThickness = label4.BorderThickness;
            Grid.SetRow(historiekLabel4, row);
            Grid.SetColumn(historiekLabel4, 3);

            historiekgrid.Children.Add(historiekLabel1);
            historiekgrid.Children.Add(historiekLabel2);
            historiekgrid.Children.Add(historiekLabel3);
            historiekgrid.Children.Add(historiekLabel4);

            row++;

        }

        private void HasWon()
        {
            if (label1.BorderBrush == Brushes.DarkRed && label2.BorderBrush == Brushes.DarkRed && label3.BorderBrush == Brushes.DarkRed && label4.BorderBrush == Brushes.DarkRed)
            {


                MessageBoxResult result = MessageBox.Show($"Je hebt gewonnen in {attempts}!" +
                    " \r\n Wil je opnieuw proberen?", "WINNER!", MessageBoxButton.YesNo, MessageBoxImage.Information);


                if (result == MessageBoxResult.Yes)
                {
                    points = 100;
                    attempts = 0;
                    UpdateTitle();
                    GenerateNumbers();
                    comboBox1.Text = "";
                    label1.BorderThickness = new Thickness(0);
                    comboBox2.Text = "";
                    label2.BorderThickness = new Thickness(0);
                    comboBox3.Text = "";
                    label3.BorderThickness = new Thickness(0);
                    comboBox4.Text = "";
                    label4.BorderThickness = new Thickness(0);
                    historiekgrid.Children.Clear();
                    pointslabel.Content = $"Jouw huidige score: {points}/100";


                }
                else
                {
                    this.Close();
                }
            }

        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult reply = MessageBox.Show("Ben je zeker dat je wilt afsluiten?", $"Mastermind: {attempts}/10 pogingen ondernomen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (reply == MessageBoxResult.Yes)
            {
                e.Cancel = false;

            }
            else
            {
                e.Cancel = true;
            }
        }



    }
}

