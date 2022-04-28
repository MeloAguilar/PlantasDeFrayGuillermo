﻿using DAL.Listados;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Listados
{
    public class clsListadosBL
    {
        clsListadosPlantas dal = new clsListadosPlantas();

        public List<clsPlanta> RecogerListadoPlantasBL()
        {
            return dal.RecogerListadoCompletoPlantas();
        }


        public List<clsCategoria> RecogerListadoCategoriasBL()
        {
            return dal.RecogerListadoCompletoCategorias();
        }

        public List<clsPlanta> RecogerPlantasDeCategoriaBL(int id)
        {
            return dal.RecogerPlantasDeCategoria(id);
        }

        public clsPlanta RecogerPlantaBL(int id)
        {
            return dal.RecogerPlanta(id);
        }


        public clsCategoria RecogerCategoriaBL(int id)
        {
            return dal.RecogerCategoria(id);
        }
    }
}
