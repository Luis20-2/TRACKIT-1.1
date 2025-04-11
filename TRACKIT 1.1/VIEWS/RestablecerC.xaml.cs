using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace TRACKIT_1._1.VIEWS
{
    /// <summary>
    /// Lógica de interacción para RestablecerC.xaml
    /// </summary>
    public partial class RestablecerC : Window
    {
        
        private string _correo;


        public RestablecerC(string correo)
        {
            InitializeComponent();
            _correo = correo;
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
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }

        private void Button_ConfirmarPassword(object sender, RoutedEventArgs e)
        {
            string nuevaContraseña = txtNuevaC.Text.Trim();
            string confirmarContraseña = txtConfirC.Text.Trim();

            if (string.IsNullOrEmpty(nuevaContraseña) || string.IsNullOrEmpty(confirmarContraseña))
            {
                MessageBox.Show("Por favor complete todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (nuevaContraseña != confirmarContraseña)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ActualizarContraseñaEnBD(_correo, nuevaContraseña))
            {
                MessageBox.Show("Contraseña actualizada correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Login loginWindow = new Login();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar la contraseña", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
         private bool ActualizarContraseñaEnBD(string correo, string nuevaContraseña)
        {
            string query = "UPDATE USUARIOS SET Contraseña = @contraseña WHERE Correo = @correo"; // Query para actualizar la contraseña
            using (SqlConnection conexion = new SqlConnection(DB.Conexion))
            {
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@contraseña", nuevaContraseña); // ¡Importante hashear!
                comando.Parameters.AddWithValue("@correo", correo);
                try
                {
                    conexion.Open();
                    int rowsAffected = comando.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Error al actualizar contraseña: {ex.Message}");
                    return false;
                }
            }
        }


        

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            RecuperarC recuperar = new RecuperarC();
            recuperar.Show();
            this.Close();
           

        }
    }
}

