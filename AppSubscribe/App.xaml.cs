using ClasesGenericas;
using Hangfire;
using System;
using System.Collections.Generic;
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
        public App()
        {
            miApp = this;
            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=tcp:pruebaboilerplate.database.windows.net,1433;Initial Catalog=PruebaHangFire;Persist Security Info=False;User ID=boiler;Password=carmarmu*1234;MultipleActiveResultSets=False;");
            var hangfireContainer = new UnityContainer();
            GlobalConfiguration.Configuration.UseActivator(new UnityJobActivator(hangfireContainer));
     
            var options = new BackgroundJobServerOptions
            {
                ServerName = String.Format(
                    "{0}.{1}",
                    Environment.MachineName,
                    "Prueba"),
                Queues = new[] { "prueba" },
                WorkerCount = 1
            };

            var server = new BackgroundJobServer(options);
            
        }

        public void MostrarMensaje()
        {
            new MessageBox(Strin f"");
        }

        public static void ManejadorEvent(object sender, EventArgs e)
        {
            miApp.Windows.
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
