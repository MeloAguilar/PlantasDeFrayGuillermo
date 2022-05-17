using BL.Listados;
using Entities;

namespace UI.Models
{
    public class CambioDeCategoriaVM
    {
       
        public List<clsPlanta> Plantas { get; set; }

        public List<clsCategoria> Categorias { get; set; }

        public clsCategoria CategoriaSeleccionada { get; set; }


        public CambioDeCategoriaVM()
        {
            clsListadosBL bl = new clsListadosBL();
            Plantas = bl.RecogerListadoCompletoPlantasBL();
            Categorias = bl.RecogerListadoCategoriasBL();
            CategoriaSeleccionada = new clsCategoria();
        }
    }


}
