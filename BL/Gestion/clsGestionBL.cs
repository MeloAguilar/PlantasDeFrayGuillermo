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
        clsGestionPlantas gestionDal = new clsGestionPlantas();
        clsListadosPlantas listasDal = new clsListadosPlantas();


        /// <summary>
        /// Método que añade políticas de negocio al método de la Capa DAL,
        /// clase clsGestionPlantas: EstablecerPrecioPlanta(int id, double precio) 
        /// 
        /// Este método se encargará de que el precio de una planta sea 0 antes de establecerlo
        /// ya que se pedía explicitamente que si el precio estaba ya establecido este debía aparecer
        /// y no poderse cambiar
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

        public int ModificarCategoriaDePlantaBL(int idCat, int idPlanta)
        {
            return gestionDal.ModificarCategoriaDePlanta(idCat, idPlanta);
        }
    }
}
