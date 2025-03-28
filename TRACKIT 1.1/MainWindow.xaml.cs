using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using TRACKIT_1._1.VIEWS;

namespace TRACKIT_1._1
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
        private void Button_ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            Sidebar.Visibility = (Sidebar.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
        }
        private void ClickInicio(object sender, RoutedEventArgs e)
        {
            // Mostrar la ventana principal
            //MainWindow mainWindow1 = new MainWindow();
            //mainWindow1.Show();

            // Cerrar la ventana actual
            //this.Close();
            DataContext =  new ContentControl();

        }

        private void ReporteInventarioclick(object sender, RoutedEventArgs e)
        {
            DataContext= new ReporteInventario();
        }
        private void ReporteAsistenciaclick(object sender, RoutedEventArgs e)
        {
            DataContext= new Reporteasistencia();
        }
        private void ReporteEmpleadosClick(object sender, RoutedEventArgs e)
        {
            DataContext = new ReporteEmpleados();
        }

        private void Registroempleadosclick(object sender, RoutedEventArgs e)
        {
            DataContext = new RegistroEmpleados();
        }
        private void Listaempleadosclick(Object sender, RoutedEventArgs e)
        {
            DataContext = new ListaEmpleados();
        }
        private void agenda(Object sender, RoutedEventArgs e)
        {
            DataContext = new Agendaview();
        }
        private void ControlAsistencia(Object sender, RoutedEventArgs e)
        {
            DataContext=new ControlAsistencia();
        }
        private void HistorialAsistencia(object sender, RoutedEventArgs e)
        {
            DataContext=new HistorialAsistencia();
        }

        private void RegistroProductos(Object sender, RoutedEventArgs e)
        {
            DataContext=new RegistroProductos();
        }
        private void Invenatariolist(Object sender, RoutedEventArgs e)
        {
            DataContext=new Inventario();
        }

        private void Button_Notificaciones_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationPopup.IsOpen)
            {
                NotificationPopup.IsOpen = false; // Cierra el panel si ya está abierto
            }
            else
            {
                // Actualiza el mensaje de notificaciones
                NotificationMessage.Text = TieneNotificacionesNuevas()
                    ? "Tienes 3 notificaciones nuevas."
                    : "No tienes notificaciones nuevas.";

                NotificationPopup.IsOpen = true; // Abre el panel
            }

        }
        private void CloseNotificationButton_Click(object sender, RoutedEventArgs e)
        {
            NotificationPopup.IsOpen = false;
        }

        // Método de ejemplo para verificar si hay notificaciones nuevas
        private bool TieneNotificacionesNuevas()
        {
            // Lógica para verificar notificaciones (puedes cambiarla según tus necesidades)
            return false; // Cambia a `true` para simular notificaciones nuevas
        }

        private void Button_Buscador_Click(object sender, RoutedEventArgs e)
        {
           
        }
       
    }

}