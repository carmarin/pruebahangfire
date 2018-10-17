using Hangfire;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClasesGenericas
{
    public interface ITareaBase 
    {
        void Ejecutar(DTOPrueba elemento, PerformContext contexts);
    }
}
