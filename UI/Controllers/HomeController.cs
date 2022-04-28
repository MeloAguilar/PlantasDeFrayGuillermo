using BL.Gestion;
using BL.Listados;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private clsListadosBL listasBl = new clsListadosBL();
        private clsGestionBL gestionBL = new clsGestionBL();
        private readonly ILogger<HomeController> _logger;
        private IndexVM vm = new IndexVM();

        public HomeController(ILogger<HomeController> logger)
        {
            vm.ListaCategorias = listasBl.RecogerListadoCategoriasBL();
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(vm);
        }


        [HttpPost]
        public IActionResult Index(IndexVM virtualVM)
        {
            vm.ListaPlantas = listasBl.RecogerPlantasDeCategoriaBL(virtualVM.CategoriaSeleccionada.IdCategoria);
            vm.CategoriaSeleccionada = listasBl.RecogerCategoriaBL(virtualVM.CategoriaSeleccionada.IdCategoria);
            return View(vm);
        }


        public IActionResult PonerPrecio(int id)
        {
            clsPlanta planta = listasBl.RecogerPlantaBL(id);
            return View(planta);
        }


        [HttpPost]
        public IActionResult PonerPrecio(clsPlanta p)
        {
                        
            gestionBL.EstablecerPrecioPlanta(p.IdPlanta, p.Precio);
            return View(p);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}