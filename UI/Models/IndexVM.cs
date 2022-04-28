using Entities;

namespace UI.Models
{
    public class IndexVM
    {
        public List<clsPlanta> ListaPlantas { get; set; }

        public List<clsCategoria> ListaCategorias { get; set; }
        public clsCategoria CategoriaSeleccionada { get; set; }

        public IndexVM()
        {
            ListaPlantas = new List<clsPlanta>();
            ListaCategorias = new List<clsCategoria>();
            CategoriaSeleccionada = new clsCategoria { IdCategoria = -1, NombreCategoria="" };
        }
    }
}
