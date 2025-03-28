using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.IO;

namespace TRACKIT_1._1.VIEWS
{
    /// <summary>
    /// Lógica de interacción para Agendaview.xaml
    /// </summary>
    public partial class Agendaview : UserControl
    {
        private string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "agenda.json");
        private List<Evento> eventos = new List<Evento>();
        private int eventoSeleccionado = -1;
        public Agendaview()
        {
            InitializeComponent();
            CargarEventos();
        }
        private void GuardarEvento_Click(object sender, RoutedEventArgs e)
        {
            if (dpFecha.SelectedDate == null || string.IsNullOrWhiteSpace(txtDetalles.Text))
            {
                MessageBox.Show("Ingrese la fecha y los detalles del evento.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (eventoSeleccionado >= 0)
            {
                eventos[eventoSeleccionado] = new Evento
                {
                    Fecha = dpFecha.SelectedDate.Value.ToShortDateString(),
                    Detalles = txtDetalles.Text
                };
                eventoSeleccionado = -1;
            }
            else
            {
                eventos.Add(new Evento
                {
                    Fecha = dpFecha.SelectedDate.Value.ToShortDateString(),
                    Detalles = txtDetalles.Text
                });
            }

            GuardarEventos();
            CargarEventos();
            txtDetalles.Clear();
        }

        private void CargarEventos()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                eventos = JsonSerializer.Deserialize<List<Evento>>(json) ?? new List<Evento>();
            }
            lvEventos.ItemsSource = null;
            lvEventos.ItemsSource = eventos;
        }

        private void GuardarEventos()
        {
            string json = JsonSerializer.Serialize(eventos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private void EliminarEvento_Click(object sender, RoutedEventArgs e)
        {
            if (lvEventos.SelectedIndex >= 0)
            {
                eventos.RemoveAt(lvEventos.SelectedIndex);
                GuardarEventos();
                CargarEventos();
            }
            else
            {
                MessageBox.Show("Seleccione un evento para eliminar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditarEvento_Click(object sender, RoutedEventArgs e)
        {
            if (lvEventos.SelectedIndex >= 0)
            {
                eventoSeleccionado = lvEventos.SelectedIndex;
                dpFecha.SelectedDate = DateTime.Parse(eventos[eventoSeleccionado].Fecha);
                txtDetalles.Text = eventos[eventoSeleccionado].Detalles;
            }
            else
            {
                MessageBox.Show("Seleccione un evento para editar.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    public class Evento
    {
        public string Fecha { get; set; }
        public string Detalles { get; set; }
    }
}

