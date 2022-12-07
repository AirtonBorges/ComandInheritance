using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComandInheritance.Models
{
    public abstract class Comando
    {
        public required string Texto { get; set; }

        public abstract Task<bool> Executar(IServiceProvider pServiceProvider);
    }
}
