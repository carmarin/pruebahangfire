using Hangfire;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServicioPrueba
{
    public partial class Service1 : ServiceBase
    {
        private BackgroundJobServer _server;
        public Service1()
        {
            InitializeComponent();
            GlobalConfiguration.Configuration.UseSqlServerStorage("Server=tcp:pruebaboilerplate.database.windows.net,1433;Initial Catalog=PruebaHangFire;Persist Security Info=False;User ID=boiler;Password=carmarmu*1234;MultipleActiveResultSets=False;");

        }

        protected override void OnStart(string[] args)
        {
            _server = new BackgroundJobServer();
        }

        protected override void OnStop()
        {
            _server.Dispose();
        }
    }
}
