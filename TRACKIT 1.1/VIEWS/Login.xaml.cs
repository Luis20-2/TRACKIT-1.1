using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;




namespace TRACKIT_1._1.VIEWS
{
   public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            CargarContraseña();

           
        }

       public bool validarUsuario(string email, string password)
        {
            // Query para buscar el usuario
            string query = "SELECT * FROM USUARIOS WHERE Correo = @email AND Contraseña = @password"; // Query para buscar el usuario
            using (SqlConnection conexion = new SqlConnection(DB.Conexion)) // Conexión a la base de datos
            {
                SqlCommand comando = new SqlCommand(query, conexion);  // Comando para ejecutar la query
                comando.Parameters.AddWithValue("@email", email);   // Parámetro para el email
                comando.Parameters.AddWithValue("@password", password);  // Parámetro para la contraseña
                try  // Intenta ejecutar la query
                {
                    conexion.Open();
                     object resultado = comando.ExecuteScalar();  // Ejecuta la query y guarda el resultado
                    if (resultado != null)  // Si el resultado no es nulo, el usuario existe
                    {
                        return true;
                    }
                    else  // Si el resultado es nulo, el usuario no existe
                    {
                        return false;
                    }
                }
                catch (SqlException ex)  // Si hay un error en la conexión, muestra un mensaje
                {
                    MessageBox.Show($"Error en la conexion en la base de datos: {ex.Message}");
                    return false;
                }
            }
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

      

        private void Button_Inicio(object sender, RoutedEventArgs e)
        {

            string email = txtEmail.Text;
            string password = pbRealPassword.Password;
            bool recordar = RecordarC.IsChecked == true;
            Login login = new Login();
            if (login.validarUsuario(email, password))
            {
                GuardarContraseña(password, recordar);
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
                
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

        
        private void GuardarContraseña( String contraseña, bool recordar)
        {
            // Guardar la contraseña en el archivo de configuración
            Properties.Settings.Default.ContraseñaRecordada = recordar ? contraseña:"";
            Properties.Settings.Default.RecordarContraseña = recordar ;
            Properties.Settings.Default.Save();
        }

        private void CargarContraseña()
        {
            if (Properties.Settings.Default.RecordarContraseña)
            {        
                pbRealPassword.Password = Properties.Settings.Default.ContraseñaRecordada;
                RecordarC.IsChecked = true;
            }
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }

}

