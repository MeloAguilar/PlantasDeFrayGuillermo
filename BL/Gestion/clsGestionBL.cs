using DAL.Gestion;
using DAL.Listados;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Gestion
{
    public class clsGestionBL
    {
       private clsGestionPlantas gestionDal = new clsGestionPlantas();
       private clsListadosPlantas listasDal = new clsListadosPlantas();


        /// <summary>
        /// Método que añade políticas de negocio al método de la Capa DAL,
        /// clase clsGestionPlantas: EstablecerPrecioPlanta(int id, double precio) 
        /// 
        /// </summary>
        /// <param name="id">id de un objeto clsPlanta</param>
        /// <param name="precio">precio de un objeto clsPlanta</param>
        /// <returns> bool exito -> true: se ha podido establecer el precio de la clsPlanta
        ///                         false: no se ha podido establecer el precio de la clsPlanta
        /// </returns>
        public int EstablecerPrecioPlantaBL(int id, double precio)
        {
            return gestionDal.EstablecerPrecioPlanta(id, precio);
        }


        /// <summary>
        /// Método que añade políticas de negocio al método de la Capa DAL,
        /// clase clsGestionPlantas: ModificarCategoriaDePlanta(int id, double precio) 
        /// </summary>
        /// <param name="idCat"></param>
        /// <param name="idPlanta"></param>
        /// <returns></returns>
        public int ModificarCategoriaDePlantaBL(int idCat, int idPlanta)
        {
            return gestionDal.ModificarCategoriaDePlanta(idCat, idPlanta);
        }


        public int CrearPlantaBL(clsPlanta planta)
        {

            if (planta.Precio < 0)
            {
                planta.Precio = 0;
            }
            return gestionDal.CrearPlanta(planta);
        }
    }
}
