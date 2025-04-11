using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Net.Mail;
using System.Net;
using TRACKIT_1._1.VIEWS;
using Microsoft.Data.SqlClient;
using System;





namespace TRACKIT_1._1.VIEWS
{
   
    public partial class CodigoVerficacion : Window
    {
        //Variables para guardar el correo de registro y el codigo generado
        private string _correoRegistro;
        private string _codigoGenerado;
        public CodigoVerficacion( string correo)
        {
            InitializeComponent();
            _correoRegistro = correo;   // Guardar el correo de registro
            _codigoGenerado = GenerarCodigo();  // Generar el código de verificación


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

      

        private void Button_Obtener(object sender, RoutedEventArgs e)
        {
            if(!EsCorreoValido(_correoRegistro))
            {
                MessageBox.Show("El correo almacenado no es válido", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _codigoGenerado = GenerarCodigo();

            try
            {
                EnviarCorreo(_correoRegistro, _codigoGenerado);
                MessageBox.Show($"Se ha enviado un código a {_correoRegistro}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al enviar el código: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private string GenerarCodigo()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString(); // Genera un número aleatorio de 6 dígitos

        }
        

        private bool EsCorreoValido(string correo)
        {
            try
            {
                var addr = new MailAddress(correo);  // Verifica si el formato del correo es válido
                return addr.Address == correo;   
            }
            catch
            {
                return false;
            }
        }

        private void Button_RestablecerPassword(object sender, RoutedEventArgs e)
        {
            // Validar el código ingresado
            string codigoIngresado = txtCodigoVerificacion.Text.Trim();
            if (string.IsNullOrEmpty(codigoIngresado))
            {
                MessageBox.Show("Por favor ingrese el código de verificación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (codigoIngresado == _codigoGenerado)
            {
                MessageBox.Show("Código de verificación correcto", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                // Abrir ventana de nueva contraseña
                RestablecerC nuevaContraseñaWindow = new RestablecerC(_correoRegistro);
                nuevaContraseñaWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Código de verificación incorrecto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            RecuperarC Recuperar = new RecuperarC();
            Recuperar.Show();
            this.Close();
        }
    }
}
