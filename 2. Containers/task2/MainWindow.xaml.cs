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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int password;
        public MainWindow()
        {
            InitializeComponent();
            GeneratePassword();
        }

        private void GeneratePassword() 
        {
            Random rnd = new Random();
            password = rnd.Next(10000, 99999);
            Title = "Password: " + password.ToString();
        }
        private void Check() 
        {
            if (txt1.Text != "")
            {
                int n = int.Parse(txt1.Text);
                if (n == password)
                {
                    MessageBoxResult result = MessageBox.Show("Password is correct!\nNew password was generated", "Confirmation");
                    GeneratePassword();
                    txt1.Clear();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Wrong Password!\nTry again", "Not Confirmed");
                    txt1.Clear();
                }
            }
        }
        private void cmdAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == cmdAnswer7) 
            {
                txt1.Text += "7";
            }
            else if (e.Source == cmdAnswer8)
            {
                txt1.Text += "8";
            }
            if (e.Source == cmdAnswer9)
            {
                txt1.Text += "9";
            }
            if (e.Source == cmdAnswer4)
            {
                txt1.Text += "4";
            }
            if (e.Source == cmdAnswer5)
            {
                txt1.Text += "5";
            }
            if (e.Source == cmdAnswer6)
            {
                txt1.Text += "6";
            }
            if (e.Source == cmdAnswer3)
            {
                txt1.Text += "3";
            }
            if (e.Source == cmdAnswer2)
            {
                txt1.Text += "2";
            }
            if (e.Source == cmdAnswer1)
            {
                txt1.Text += "1";
            }
            if (e.Source == cmdAnswer0)
            {
                txt1.Text += "0";
            }
            if (e.Source == cmdAnswerC)
            {
                txt1.Clear();
            }
            if (e.Source == cmdAnswerOk) 
            {
                Check();
            }
        }
    }
    
}
