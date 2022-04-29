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

        public bool EstablecerPrecioPlantaBL(int id, double precio)
        {
            bool exito = false;
            clsPlanta p = listasDal.RecogerPlanta(id);
            if (p.Precio == 0)
            {
                exito = gestionDal.EstablecerPrecioPlanta(id, precio);
            }
            return exito;
        }
    }
}
