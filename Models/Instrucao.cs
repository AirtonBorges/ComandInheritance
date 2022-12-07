using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComandInheritance.Models
{
    public class Instrucao
    {
        public Instrucao(string pTexto)
        {
            Texto = pTexto;
        }

        public Verbo? Verbo { get; private set; }

        public void AdicionarVerbo()
        {

            try
            {
                foreach(var pPalavra in Texto.Split(" "))
                {
                    Enum.TryParse(typeof(Verbo), pPalavra, true, out var xVerbo);
                    Verbo = Verbo ?? (Verbo?)xVerbo;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Texto { get; init; }
    }
}
