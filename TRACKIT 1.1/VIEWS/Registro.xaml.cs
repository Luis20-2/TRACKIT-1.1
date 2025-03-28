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

namespace TRACKIT_1._1.VIEWS
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        public Registro()
        {
            InitializeComponent();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }
        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
            
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Foreground == Brushes.LightGray)
            {
                textBox.Text = ""; // Borra el placeholder al enfocar
                textBox.Foreground = Brushes.Black; // Cambia el color
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.LightGray;
                textBox.Text = (string)textBox.Tag; // Restaura el placeholder
            }
        }
        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.LightGray;
                textBox.Text = (string)textBox.Tag;
            }
        }

        private void Button_Registro(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Inicio(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Existente(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Confirmacion(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
