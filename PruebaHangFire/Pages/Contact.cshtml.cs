using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClasesGenericas;
using Hangfire;
using Hangfire.States;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PruebaHangFire.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet(int id)
        {
            var client = new BackgroundJobClient();
            IState state = new EnqueuedState("prueba");
            DTOPrueba prueba = new DTOPrueba();


            for (int i = 0; i <= id; i++)
            {
                prueba.IdElemento = i.ToString() + "-" + "1";
                client.Create<ITareaBase>(x => x.Ejecutar(prueba, null), state);
            }


            Message = "Your contact page.";

        }
    }
}
