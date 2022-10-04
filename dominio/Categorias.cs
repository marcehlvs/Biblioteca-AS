using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Categorias
    {
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public override string ToString()
        {
            return Categoria;
        }
    }
}
