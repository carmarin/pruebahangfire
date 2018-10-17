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
using System.Windows.Threading;

namespace AppSubscribe
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public event EventHandler FinalizarTarea;

        public string IdTarea { get; set; }
        public Window1(string idTarea)
        {
            this.IdTarea = idTarea;
            InitializeComponent();
            label1.Dispatcher.Invoke(DispatcherPriority.Normal,
                new Action(() => { label1.Content = idTarea.ToString(); label3.Content = App.Usuario; }));
            IniciarTimer();
        }

        private void IniciarTimer()
        {
            DispatcherTimer dispathcer = new DispatcherTimer();

            //EL INTERVALO DEL TIMER ES DE 0 HORAS,0 MINUTOS Y 10 SEGUNDOS 
            dispathcer.Interval = new TimeSpan(0, 0, 3);

            //EL EVENTO TICK SE SUBSCRIBE A UN CONTROLADOR DE EVENTOS UTILIZANDO LAMBDA 
            dispathcer.Tick += (s, a) =>
            {
                FinalizarTarea?.Invoke(this, new PruebaEventoArgs() { Id = IdTarea });
                dispathcer.Stop();
                this.Close();
            };
            dispathcer.Start();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
