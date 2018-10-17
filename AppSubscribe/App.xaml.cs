using ClasesGenericas;
using Hangfire;
using Hangfire.States;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;

namespace AppSubscribe
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App miApp;

        public static string Usuario;

        public static List<string> procesadas = new List<string>();

        public event EventHandler EjecucionTarea;

        public static int Reencolar { get; set; }

        public App()
        {
            miApp = this;

            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=tcp:pruebaboilerplate.database.windows.net,1433;Initial Catalog=PruebaHangFire;Persist Security Info=False;User ID=boiler;Password=carmarmu*1234;MultipleActiveResultSets=False;");
            var hangfireContainer = new UnityContainer();
            GlobalConfiguration.Configuration.UseActivator(new UnityJobActivator(hangfireContainer));
            
        }



        public void MostrarMensaje(string id)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                Window1 ventana = new Window1(id);
                ventana.FinalizarTarea += EjecutarFinalizarTarea;
                bool? finalizado = ventana.ShowDialog();
            });
            
        }

        private void EjecutarFinalizarTarea(object sender, EventArgs e)
        {
            PruebaEventoArgs evento = (PruebaEventoArgs)e;
            string[] elementos = evento.Id.Split('-');
            if (elementos.Count() > 1)
            {
                if (String.Equals(elementos[1], "1"))
                {
                    EncolarElemento(elementos[0] + "-" + "2");
                }
            }
        }

        public static void ManejadorEvent(object sender, EventArgs e)
        {
            PruebaEventoArgs evento = (PruebaEventoArgs)e;
            string[] elementos = evento.Id.Split('-');
            if (elementos.Count() > 0)
            {
                if (procesadas.Contains(elementos[0]))
                {
                    EncolarElemento(evento.Id);
                    Reencolar++;
                }
                else
                {
                    procesadas.Add(elementos[0]);
                    miApp.MostrarMensaje(evento.Id);
                }
            }

            miApp.EjecucionTarea?.Invoke(miApp, new EventArgs());
            cambiarEstadoTerminada(evento.IdTarea);
        }

        private static void cambiarEstadoTerminada(string idTarea)
        {
            var client = new BackgroundJobClient();
        }

        private static void EncolarElemento(string id)
        {
            var client = new BackgroundJobClient();
            IState state = new EnqueuedState("prueba");
            DTOPrueba prueba = new DTOPrueba() { IdElemento = id };
            client.Create<ITareaBase>(x => x.Ejecutar(prueba, null), state);
        }

        public class UnityJobActivator : JobActivator
        {
            private readonly IUnityContainer hangfireContainer;

            public UnityJobActivator(IUnityContainer hangfireContainer)
            {
                this.hangfireContainer = hangfireContainer;

                GestorComponente Gestor;
                Gestor = new GestorComponente();
                Gestor.manejador += ManejadorEvent;
                this.hangfireContainer
                          .RegisterInstance<ITareaBase>(Gestor);
                //don't forget to register child dependencies as well
            }

            public override object ActivateJob(Type type)
            {
                return hangfireContainer.Resolve(type);
            }
        }
    }

   
}
