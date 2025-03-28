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
using TRACKIT_1._1.VIEWS;
using System.Data.SqlClient;

namespace TRACKIT_1._1.VIEWS
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

           
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
        private void TxtPasswordPlaceholder_GotFocus(object sender, RoutedEventArgs e)
        {
            // Oculta el placeholder y muestra el PasswordBox real
            txtPasswordPlaceholder.Visibility = Visibility.Collapsed;
            pbRealPassword.Visibility = Visibility.Visible;
            pbRealPassword.Focus(); // Enfoca el PasswordBox automáticamente
        }

        private void PbRealPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            // Si no hay contraseña, vuelve a mostrar el placeholder
            if (string.IsNullOrEmpty(pbRealPassword.Password))
            {
                pbRealPassword.Visibility = Visibility.Collapsed;
                txtPasswordPlaceholder.Visibility = Visibility.Visible;
            }
        }
        
        private void PbRealPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            // Muestra el PasswordBox real y oculta el placeholder
            pbRealPassword.Visibility = Visibility.Visible;
            txtPasswordPlaceholder.Visibility = Visibility.Collapsed;
        }   

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)           
            DragMove();

        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
           
        }

        private void txtPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Inicio(object sender, RoutedEventArgs e)
        {   
            
            var email = txtEmail.Text;
            var password = pbRealPassword.Password;
            if (email == "admin" && password == "admin")
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }

        }

        private void Button_Registro(object sender, RoutedEventArgs e)
        {
        
            Registro NuevoRegistro = new Registro();
            this.Close();
            NuevoRegistro.Show();
            

        }

        private void Button_recuperarPassword(object sender, RoutedEventArgs e)
        {
            RecuperarC Recuperar = new RecuperarC();
            this.Close();
            Recuperar.ShowDialog();
            
        }
    }

}
