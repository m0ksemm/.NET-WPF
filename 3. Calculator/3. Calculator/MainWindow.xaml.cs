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

namespace _3._Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
        }
        private void cmdEnter(object sender, EventArgs e)
        {

            IfMessage();
            if (Done) 
            {
                Done = false;
                textbox1.Clear();
                textbox2.Clear();
            } 
            if (textbox2.Text == "0")
                textbox2.Clear();
            if (sender.Equals(button7))
            {
                textbox2.Text += 7.ToString();
            }
            else if (sender.Equals(button8))
            {
                textbox2.Text += 8.ToString();
            }
            if (sender.Equals(button9))
            {
                textbox2.Text += 9.ToString();
            }
            if (sender.Equals(button4))
            {
                textbox2.Text += 4.ToString();
            }
            if (sender.Equals(button5))
            {
                textbox2.Text += 5.ToString();
            }
            if (sender.Equals(button6))
            {
                textbox2.Text += 6.ToString();
            }
            if (sender.Equals(button1))
            {
                textbox2.Text += 1.ToString();
            }
            if (sender.Equals(button2))
            {
                textbox2.Text += 2.ToString();
            }
            if (sender.Equals(button3))
            {
                textbox2.Text += 3.ToString();
            }
        }
        private void cmdС(object sender, EventArgs e)
        {
            textbox1.Clear();
            textbox2.Text = "0";
        }
        private void cmdBack(object sender, EventArgs e)
        {
            if (textbox2.Text.Length != 0)
                textbox2.Text = textbox2.Text.Remove(textbox2.Text.Length - 1, 1);
            if (textbox2.Text.Length == 0)
                textbox2.Text = "0";
        }
        private void cmdEnterZero(object sender, EventArgs e)
        {
            if (textbox2.Text != "0")
                textbox2.Text += 0.ToString();
        }
        private void cmdEnterPoint(object sender, EventArgs e)
        {
            IfMessage();
            if (textbox2.Text.Length != 0 && textbox2.Text.Contains(".") == false)
                textbox2.Text += ".";
        }
        private void cmdOperation(object sender, EventArgs e)
        {
            try
            {
                IfMessage();
                if (Done)
                {
                    Done = false;
                    //textbox1.Text = textbox2.Text;
                    textbox1.Clear();
                }

                if (Done == false)
                {
                    if (textbox1.Text != "" && textbox2.Text != "")
                    {
                        Result();
                        Done = false;
                    }
                    else if (sender.Equals(buttonDevide) && textbox2.Text == "0")
                    {
                        textbox2.Text = textbox2.Text.Remove(textbox2.Text.Length - 1, 1);
                    }
                }

                //else Result();
                if (sender.Equals(buttonMinus) && textbox2.Text != "0.")
                {
                    if (textbox2.Text.Length == 0 || textbox2.Text == "0")
                        textbox2.Text = "-";
                    else
                    {
                        textbox1.Text = textbox2.Text + " - ";
                        textbox2.Text = "0";
                    }
                }
                if (sender.Equals(buttonPlus) && textbox2.Text != "0.")
                {
                    textbox1.Text = textbox2.Text + " + ";
                    textbox2.Text = "0";
                }
                if (sender.Equals(buttonDevide) && textbox2.Text != "0.")
                {
                    textbox1.Text = textbox2.Text + " / ";
                    textbox2.Text = "0";
                }
                if (sender.Equals(buttonMultiply) && textbox2.Text != "0.")
                {
                    textbox1.Text = textbox2.Text + " * ";
                    textbox2.Text = "0";
                }
            }
            catch(Exception ex) 
            {
                textbox1.Text = "";
                textbox2.Text = "0";
            }
        }

        private void cmdRedo(object sender, EventArgs e) 
        {
            textbox2.Text = "0";
        }

        private bool Done = false;

        private void Result() 
        {
            string str = textbox1.Text;
            string val1 = "";
            string val2 = "";
            string sign = "";
            foreach (char s in str)
            {
                if (s == '.')
                    val1 += ',';
                else if (s != ' ')
                    val1 += s;
            }
            sign = val1.Substring(val1.Length - 1);
            val1 = val1.Remove(val1.Length - 1, 1);

            foreach (char s in textbox2.Text)
            {
                if (s == '.')
                    val2 += ',';
                else if (s != ' ' || s == '-')
                    val2 += s;
            }

            double x = Convert.ToDouble(val1);
            double y = Convert.ToDouble(val2);

            if (sign == "+")
            {
                double result = x + y;
                textbox1.Text += textbox2.Text;
                textbox2.Text = result.ToString();
            }
            else if (sign == "-")
            {
                double result = x - y;
                textbox1.Text += textbox2.Text;
                textbox2.Text = result.ToString();
            }
            else if (sign == "*")
            {
                double result = x * y;
                textbox1.Text += textbox2.Text;
                textbox2.Text = result.ToString();
            }
            if (sign == "/")
            {
                if (y != 0) 
                {
                    double result = x / y;
                    textbox1.Text += textbox2.Text;
                    textbox2.Text = result.ToString();
                }
                else 
                {
                    textbox1.Text += textbox2.Text;
                    textbox2.Text = "You can't divide by 0!";
                    message = true;
                }
            }
            Done = true;

        }
        bool message = false;
        void IfMessage() 
        {
            if (message) 
            {
                textbox1.Clear();
                textbox2.Clear();
                message = false;
            }
        }
        private void cmdResult(object sender, EventArgs e)
        {
            try
            {
                if (textbox1.Text != "" && textbox2.Text != "" && Done == false)
                    Result();
            }
            catch(Exception ex) 
            {
                textbox1.Text = "";
                textbox2.Text = "0";
            }
        }
        
    }
}
