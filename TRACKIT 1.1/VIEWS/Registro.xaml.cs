using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace TRACKIT_1._1.VIEWS
{
    public partial class Registro : Window
    {
        // Variables para almacenar el código de verificación y el correo electrónico
        private string _codigoVerificacion;
        private string _correoRegistro;

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
            login.Show();
            this.Close();
        }

        // Método para manejar el evento de clic en el botón de registro
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;  // Obtiene el TextBox que disparó el evento
            if (textBox.Foreground == Brushes.LightGray)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        // Método para manejar el evento de pérdida de foco en el TextBox
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.LightGray;
                textBox.Text = (string)textBox.Tag;
            }
        }

        // Método para manejar el evento de carga del TextBox
        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.LightGray;
                textBox.Text = (string)textBox.Tag;
            }
        }
        // Método para manejar el evento de clic en el botón de registro
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (passwordBox.Tag != null && passwordBox.Password == (string)passwordBox.Tag)
            {
                passwordBox.Password = "";
                passwordBox.Foreground = Brushes.Black;
            }
        }
        // Método para manejar el evento de pérdida de foco en el PasswordBox
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Foreground = Brushes.LightGray;
                passwordBox.Password = (string)passwordBox.Tag;
            }
        }
        // Método para manejar el evento de carga del PasswordBox
        private void PasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            if (string.IsNullOrWhiteSpace(passwordBox.Password))
            {
                passwordBox.Foreground = Brushes.LightGray;
                passwordBox.Password = (string)passwordBox.Tag;
            }
        }
        

        /// Método para validar el correo electrónico
        private void EnviarCorreoVerificacion_Click(object sender, RoutedEventArgs e)
        {
            _correoRegistro = txtCorreo.Text.Trim();

            // Validar correo electrónico
            if (!EsCorreoValido(_correoRegistro))
            {
                MessageBox.Show("Por favor ingrese un correo electrónico válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Generar código de verificación
            _codigoVerificacion = GenerarCodigoVerificacion();

            try
            {
                EnviarCorreo(_correoRegistro, _codigoVerificacion);
                MessageBox.Show($"Se ha enviado un código de verificación a {_correoRegistro}", "Código enviado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar el código: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Método para validar el formato del correo electrónico
        private bool EsCorreoValido(string correo)
        {
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
        // Método para generar un código de verificación aleatorio
        private string GenerarCodigoVerificacion()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Genera un número aleatorio de 6 dígitos
        }
        // Método para enviar el correo de verificación
        private void EnviarCorreo(string correoDestino, string codigoVerificacion)
        {
            // Configuración del cliente SMTP
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587, // Puerto SMTP de Gmail
                Credentials = new NetworkCredential("trackitverificacion.1@gmail.com", "smls kqvp klnu yfjb"),
                EnableSsl = true,
            };
            // Crear el mensaje de correo
            var mensaje = new MailMessage
            {
                From = new MailAddress("trackitverificacion.1@gmail.com"),
                Subject = "Código de Verificación - TRACKIT",
                Body = $"Tu código de verificación es: {codigoVerificacion}\n\nEste código expirará en 10 minutos.",
                IsBodyHtml = false,
            };

            mensaje.To.Add(correoDestino); // Agregar el destinatario
            smtpClient.Send(mensaje);// Enviar el correo
        }
        // Método para manejar el evento de clic en el botón de confirmación
        private void Button_Confirmacion_Click(object sender, RoutedEventArgs e)
        {
            // Validar campos
            if (string.IsNullOrWhiteSpace(txtCorreo.Text) || txtCorreo.Text == "Correo electrónico")
            {
                MessageBox.Show("Por favor ingrese un correo electrónico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword1.Password) || txtPassword1.Password == "Contraseña de 6 a 16 dígitos")
            {
                MessageBox.Show("Por favor ingrese una contraseña", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtPassword1.Password != txtPassword2.Password)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCodigoConfirmacion.Text) || txtCodigoConfirmacion.Text == "Introduzca el código de confirmación")
            {
                MessageBox.Show("Por favor ingrese el código de confirmación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Verificar código
            if (txtCodigoConfirmacion.Text != _codigoVerificacion)
            {
                MessageBox.Show("El código de verificación es incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Registrar usuario
            try
            {
                RegistrarUsuario(_correoRegistro, txtPassword1.Password);
                MessageBox.Show("Registro exitoso! Ahora puede iniciar sesión", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Login login = new Login();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar usuario: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegistrarUsuario(string correo, string password)
        {
            // TODO: Implementar hash de la contraseña antes de almacenarla
            string query = "INSERT INTO USUARIOS (Correo, Contraseña) VALUES (@correo, @password)";

            using (SqlConnection conexion = new SqlConnection(DB.Conexion))
            {
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@correo", correo);
                comando.Parameters.AddWithValue("@password", password); // Deberías hashear esto

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private void Button_Existente_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void txtCodigoConfirmacion_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}