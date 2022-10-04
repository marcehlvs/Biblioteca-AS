using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace dominio
{
    public class Libro
    {
        public int IdLibros { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public Categorias Categoria { get; set; }
        [DisplayName("Título")]
        public string Titulo { get; set; }
        public string Autor { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        public string UrlTapaLibro { get; set; }
        [DisplayName("Fecha de Publicación")]
        public DateTime FechaPublicacion { get; set; }
        public int Stock { get; set; } 


    }
}
