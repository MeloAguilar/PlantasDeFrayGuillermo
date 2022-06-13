using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class clsPlanta
    {
        public int IdPlanta { get; set; }
        public string NombrePlanta { get; set; }
        public string Descripcion { get; set; }
        public int IdCategoria { get; set; }
     
        public double Precio { get; set; }


       


        public clsPlanta()
        {

        }
    }
}
