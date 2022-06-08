using Entities;

namespace UI.Models
{
    public class clsPlantaConSeleccion : clsPlanta
    {
        public bool? SeleccionadaParaCambioDeCategoria { get; set; }

        public clsPlantaConSeleccion() : base()
        {
            SeleccionadaParaCambioDeCategoria = null;
        }
    }
}
