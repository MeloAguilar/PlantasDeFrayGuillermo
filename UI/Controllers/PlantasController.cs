﻿
using BL.Gestion;
using BL.Listados;
using DAL.Gestion;
using DAL.Listados;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace apiDeFrayGuillermo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantasController : ControllerBase
    {
        private clsGestionBL gestion = new clsGestionBL();
        private clsListadosBL listas = new clsListadosBL();
        // GET: api/<PlantasController>
        [HttpGet]
        public IActionResult Get()
        {
            IActionResult result = null;

            IEnumerable<clsPlanta> plantas = new List<clsPlanta>();
            try
            {
                plantas = listas.RecogerListadoCompletoPlantasBL();
            }
            catch (System.Web.Http.HttpResponseException e)
            {
                result = BadRequest();
            }
            if (plantas == null || plantas.Count() == 0)
            {
                result = NoContent();
            }
            else
            {
                result = Ok(plantas);
            }

            return result;
        }

        // GET api/<PlantasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            IActionResult result = null;

            clsPlanta planta = new clsPlanta();
            try
            {
                planta = listas.RecogerPlantaBL(id);
            }
            catch (System.Web.Http.HttpResponseException e)
            {
                BadRequest();
            }
            if (planta == null)
            {
                result = NoContent();
            }
            else
            {
                result = Ok(planta);
            }

            return result;
        }

        // POST api/<PlantasController>
        [HttpPost]
        public IActionResult Post([FromBody] clsPlanta planta)
        {
            IActionResult result = null;
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = gestion.CrearPlantaBL(planta);
            }
            catch (System.Web.Http.HttpResponseException e)
            {
                result = BadRequest();
            }
            if (filasAfectadas == 0)
            {
                result=NoContent();
            }
            else
            {
                result = Ok();
            }

            return result;
        }

        // PUT api/<PlantasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] clsPlanta planta)
        {
            IActionResult result = null;
            int filasAfectadas = 0;
            try
            {
                filasAfectadas = gestion.ModificarCategoriaDePlantaBL(planta.IdCategoria, id);
            }
            catch (System.Web.Http.HttpResponseException e)
            {
                result = BadRequest();
            }
            if (filasAfectadas == 0)
            {
                result=NoContent();
            }
            else
            {
                result = Ok();
            }

            return result;
        }

        // DELETE api/<PlantasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
