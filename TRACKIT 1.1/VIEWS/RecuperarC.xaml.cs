using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Net.Mail;
using System.Net;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace TRACKIT_1._1.VIEWS
{
    /// <summary>
    /// Lógica de interacción para RecuperarContraseña.xaml
    /// </summary>
    public partial class RecuperarC : Window
    {

        public RecuperarC()
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
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }
        public bool ValidarCorreo(string correo)
        {
            string query = "SELECT * FROM USUARIOS WHERE Correo = @correo"; // Query para buscar el correo
            using (SqlConnection conexión = new SqlConnection(DB.Conexion)) // Conexión a la base de datos
            {
                SqlCommand comando = new SqlCommand(query, conexión); // Comando para ejecutar la query
                comando.Parameters.AddWithValue("@correo", correo); // Parámetro para el correo
                try
                {
                    conexión.Open();
                    object resultado = comando.ExecuteScalar(); // Ejecuta la query y guarda el resultado
                    if (resultado != null) // Si el resultado no es nulo, el correo existe
                    {
                        return true;
                    }
                    else // Si el resultado es nulo, el correo no existe
                    {
                        return false;
                    }
                }
                catch (SqlException ex) // Si hay un error en la conexión, muestra un mensaje
                {
                    MessageBox.Show($"Error en la conexion en la base de datos: {ex.Message}");
                    return false;
                }
            }


        }

        private void Button_Buscar(object sender, RoutedEventArgs e)
        {
            // Verifica si el TextBox está vacío o contiene el placeholder
            string correo = txtRecuperacion.Text.Trim(); // Obtiene el correo del TextBox

            // Verifica si el correo es nulo o contiene solo espacios en blanco
            if (ValidarCorreo(correo))
            {
                // Pasar el correo al constructor de CodigoVerficacion
                CodigoVerficacion codigoVerficacion = new CodigoVerficacion(correo);
                this.Close();
                codigoVerficacion.Show(); ;
            }
            // Verifica si el correo es nulo o contiene solo espacios en blanco
            if (string.IsNullOrWhiteSpace(correo) || correo == (string)txtRecuperacion.Tag) 
            {
                MessageBox.Show("Por favor ingrese un correo electrónico válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Verifica si el correo es válido
            if (!EsCorreoValido(correo))
            {
                MessageBox.Show("Por favor ingrese un correo electrónico válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Verifica si el correo existe en la base de datos
            if (ValidarCorreo(correo))
            {
                CodigoVerficacion codigoVerficacion = new CodigoVerficacion(correo); // Crear una nueva instancia de la ventana de verificación
                this.Close();
                

            }
            else
            {
                MessageBox.Show("El correo no se encuentra registrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);  // Muestra un mensaje de error si el correo no existe
            }


        }
       
        private bool EsCorreoValido(string correo)
        {
            // Verifica si el correo tiene un formato válido
            try
            {
                var addr = new MailAddress(correo);
                return addr.Address == correo;
            }
            catch
            {
                return false;
            }
        }

       
        

        private void Button_Cancelar(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();

        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
            
        }

       
    }
}

