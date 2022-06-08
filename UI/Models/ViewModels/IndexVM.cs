using BL.Listados;
using Entities;

namespace UI.Models.ViewModels
{
    public class IndexVM
    {
        public List<clsPlanta> ListaPlantasDeCategoriaSeleccionada { get; set; }

        public List<clsCategoria> ListaCategorias { get; }
        public clsCategoria CategoriaSeleccionada { get; set; }


        public IndexVM()
        {
            clsListadosBL bl = new clsListadosBL();
            ListaPlantasDeCategoriaSeleccionada = new List<clsPlanta>();
            ListaCategorias = bl.RecogerListadoCategoriasBL();
            CategoriaSeleccionada = new clsCategoria { IdCategoria = -1, NombreCategoria="" };
        }
    }
}
