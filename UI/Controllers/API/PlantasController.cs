using BL.Gestion;
using BL.Listados;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantasController : ControllerBase
    {
       private clsListadosBL listas = new clsListadosBL();
        private clsGestionBL gestion = new clsGestionBL();
        // GET: api/<ApiController>
        [HttpGet]
        public IEnumerable<clsPlanta> Get()
        {
            return listas.RecogerListadoCompletoPlantasBL();
        }

        // GET api/<ApiController>/5
        [HttpGet("{id}")]
        public clsPlanta Get(int id)
        {
            return listas.RecogerPlantaBL(id);
        }

        // POST api/<ApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
