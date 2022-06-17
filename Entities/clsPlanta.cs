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
        [Required(ErrorMessage = "El nombre de la planta es necesario")]
        public string NombrePlanta { get; set; }

  
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "La planta debe estar entre las categorias existentes")]
        public int IdCategoria { get; set; }
     
        public double Precio { get; set; }


       


        public clsPlanta()
        {

        }
    }
}
