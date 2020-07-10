using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LosInges.Models.Modelos
{
    public class ListaAS
    {
       public int? IdAuto { get; set; }
        public int IdRestauracion { get; set; }
        public int IdDepartamento { get; set; }
        public string Departamento { get; set; }
        public string Descripcion { get; set; }
    }
}