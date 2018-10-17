using Hangfire;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppSubscribe
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            App.miApp.EjecucionTarea += TareaEjecutada;
        }

        private void TareaEjecutada(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {

                lblProcesadas.Content = App.procesadas.Count.ToString();
                lblReencoladas.Content = App.Reencolar.ToString();
            });
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            textBox.IsEnabled = false;
            string usuario = textBox.Text;
            var options = new BackgroundJobServerOptions
            {
                ServerName = String.Format(
                    "{0}.{1}",
                    Environment.MachineName,
                    usuario),
                Queues = new[] { "prueba" },
                WorkerCount = 1
            };

            App.Usuario = usuario;

            var server = new BackgroundJobServer(options);
        }
    }
}
