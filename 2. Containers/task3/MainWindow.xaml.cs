using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace task3
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
        private void cmd_result_Click(object sender, RoutedEventArgs e)
        {
            double[,] matr = new double[3,3];

            matr[0, 0] = double.Parse(txt1.Text);
            matr[0, 1] = double.Parse(txt2.Text);
            matr[0, 2] = double.Parse(txt3.Text);

            matr[1, 0] = double.Parse(txt4.Text);
            matr[1, 1] = double.Parse(txt5.Text);
            matr[1, 2] = double.Parse(txt6.Text);

            matr[2, 0] = double.Parse(txt7.Text);
            matr[2, 1] = double.Parse(txt8.Text);
            matr[2, 2] = double.Parse(txt9.Text);

            res.Text = Determinant(matr).ToString();
        }
        double Determinant(double[,] matr)  //нахождение определителя по минорам квадратичной матрицы
        {
            return matr[0,0] * matr[1,1] * matr[2,2] - matr[0,0] * matr[1,2] * matr[2,1]
                 - matr[0,1] * matr[1,0] * matr[2,2] + matr[0,1] * matr[1,2] * matr[2,0]
                 + matr[0,2] * matr[1,0] * matr[2,1] - matr[0,2] * matr[1,1] * matr[2,0];
        }
        //PreviewTextInput="pnl_PreviewTextInput" PreviewKeyDown="pnl_PreviewKeyDown"
        private void pnl_PreviewTextInput(object sender, TextCompositionEventArgs e) 
        {
            short val;
            if (!Int16.TryParse(e.Text, out val)) 
            {
                e.Handled = true;
            }
        }
        private void pnl_PreviewKeyDown(object sender, KeyEventArgs e) 
        {
            if (e.Key == Key.Space) 
            {
                e.Handled = true;
            }
        }
    }
}
