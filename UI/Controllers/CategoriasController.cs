
using DAL.Gestion;
using DAL.Listados;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PlantasDeFrayGuillermo.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private clsGestionPlantas gestion = new clsGestionPlantas();
        private clsListadosPlantas listas = new clsListadosPlantas();
        // GET: api/<CategoriasController>
        [HttpGet]
        public IEnumerable<clsCategoria> Get()
        {
            return listas.RecogerListadoCompletoCategorias();
        }

        // GET api/<CategoriasController>/5
        [HttpGet("{id}")]
        public clsCategoria Get(int id)
        {
            return listas.RecogerCategoria(id);
        }

        // POST api/<CategoriasController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
