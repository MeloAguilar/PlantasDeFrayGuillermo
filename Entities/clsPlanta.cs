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
        [Required(ErrorMessage = "Se debe establecer un precio para la planta")]

        [Range(0.01, 100.01, ErrorMessage="El valor de {0} debe ser mayor que {1} y menor que {2}")]
        public double Precio { get; set; }


        public clsPlanta()
        {

        }
    }
}
