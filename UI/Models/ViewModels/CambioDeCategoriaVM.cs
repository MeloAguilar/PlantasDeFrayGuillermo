using BL.Listados;
using Entities;

namespace UI.Models.ViewModels
{
    public class CambioDeCategoriaVM
    {

        public List<clsPlantaConSeleccion> Plantas { get; set; }

        public List<clsCategoria> Categorias { get; set; }

        public clsCategoria CategoriaSeleccionada { get; set; }


        public CambioDeCategoriaVM()
        {
            Plantas = new List<clsPlantaConSeleccion>();
            clsListadosBL bl = new clsListadosBL();
            foreach(var planta in bl.RecogerListadoCompletoPlantasBL())
            {
                var p = new clsPlantaConSeleccion();
                p.IdPlanta = planta.IdPlanta;
                p.IdCategoria = planta.IdCategoria;
                p.Descripcion = planta.Descripcion;
                p.NombrePlanta = planta.NombrePlanta;
                p.Precio = planta.Precio;
                Plantas.Add(p);
            }
            Categorias = bl.RecogerListadoCategoriasBL();
            CategoriaSeleccionada = new clsCategoria();
        }
    }


}
