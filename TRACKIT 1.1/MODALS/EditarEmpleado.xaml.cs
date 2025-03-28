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

namespace TRACKIT_1._1.MODALS
{
    /// <summary>
    /// Lógica de interacción para EditarEmpleado.xaml
    /// </summary>
    public partial class EditarEmpleado : Window
    {
        public EditarEmpleado()
        {
            InitializeComponent();
        }

        public void Guardarclick (object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Guardado correctamente");
        }

       
    }
}
