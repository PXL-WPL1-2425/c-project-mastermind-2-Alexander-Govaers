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
        string kleur1, kleur2, kleur3, kleur4;

        int attempts;
        //start van het spel 100 punten
       // int points = 100;


        public MainWindow()
        {
            InitializeComponent();

            


        }
        /// <summary>
        /// 
        /// <para> Deze methode beschikt over GenerateNumbers() method die ervoor zorgt dat..... </para>
        /// </summary>
        /// <param name="sender"> Bechrijving sender....</param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            GenerateNumbers();

            //pointslabel.Content = $"Jouw huidige score: {points}/100";
            solutionTextBox.Visibility = Visibility.Hidden;


        }

        private void GenerateNumbers()
        {
            kleur1 = ChooseColor(rnd.Next(0, 6));
            kleur2 = ChooseColor(rnd.Next(0, 6));
            kleur3 = ChooseColor(rnd.Next(0, 6));
            kleur4 = ChooseColor(rnd.Next(0, 6));

            solutionTextBox.Text = $"MasterMind: {kleur1}, {kleur2}, {kleur3}, {kleur4}";
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


            // messagebox einde spel commit 06 - pogingen + commit 09 -  spel einde
            if (attempts >= 10)
            {
                this.Close();
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
                    oplossing = kleur1;
                    break;

                case 1:
                    oplossing = kleur2;
                    break;

                case 2:
                    oplossing = kleur3;
                    break;
                case 3:
                    oplossing = kleur4;
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
                //points -= 1;
                kleurLabel.BorderThickness = new Thickness(4);
            }
            else
            {
                kleurLabel.BorderThickness = new Thickness(0);
               // points -= 2;
            }


        }
    }
}
