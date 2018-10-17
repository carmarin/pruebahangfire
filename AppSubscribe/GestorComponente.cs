using ClasesGenericas;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSubscribe
{
    public class GestorComponente : ITareaBase
    {

        public event EventHandler manejador;

        public GestorComponente()
        {

        }
        

        public void Ejecutar(DTOPrueba elemento,  PerformContext context)
        {
            manejador?.Invoke(this, new PruebaEventoArgs() { Id = elemento.IdElemento, IdTarea = context.BackgroundJob.Id });
        }
    }

    public class PruebaEventoArgs : EventArgs
    {
        public string Id { get; set; }

        public string IdTarea { get; set; }
    }

}
