using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComandInheritance.Models
{
    public class Comando : IComando
    {
        public string Mensagem { get; set; }
    }
}
