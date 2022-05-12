using Entities;

namespace UI.Models
{
    public class CambioDeCategoriaVM
    {
        public List<clsPlanta> Plantas { get; set; }

        public List<clsCategoria> Categorias { get; set; }

        public clsCategoria CategoriaSeleccionada { get; set; }



        public List<bool> RepresentacionDeLasPlantasSeleccionadas { get; set; }
        public CambioDeCategoriaVM()
        {
            Plantas = new List<clsPlanta>();
            Categorias = new List<clsCategoria>();
            CategoriaSeleccionada = new clsCategoria() { IdCategoria = 0, NombreCategoria = "" };
            RepresentacionDeLasPlantasSeleccionadas = new List<bool>();
        }
    }
}
