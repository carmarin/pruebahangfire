using ClasesGenericas;
using Hangfire;
using Hangfire.Annotations;
using Hangfire.Common;
using Hangfire.Server;
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
        

        public void Ejecutar(DTOPrueba elemento)
        {
            manejador?.Invoke(this, new PruebaEventoArgs() { Id = elemento.IdElemento });
        }
    }

    public class PruebaEventoArgs : EventArgs
    {
        public int Id { get; set; }
    }

}
