using BL.Gestion;
using BL.Listados;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UI.Models.ViewModels;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private clsListadosBL listasBl = new clsListadosBL();
        private clsGestionBL gestionBL = new clsGestionBL();
        private readonly ILogger<HomeController> _logger;
        private IndexVM indexVM = new IndexVM();
        private CambioDeCategoriaVM cambioDeCategoriaVM;


        //Al inicializarlo pido directamente el listado de categorias ya que es un listado pequeño
        public HomeController(ILogger<HomeController> logger)
        {
            cambioDeCategoriaVM = new CambioDeCategoriaVM();
            _logger = logger;
        }

        public IActionResult Index()
        {

            try
            {
                return View(indexVM);
            }
            catch (Exception e)
            {
                ViewBag.Error = "No se pudo conectar con la base de datos";
                return View(indexVM);
            }


        }


        [HttpPost]
        public IActionResult Index(IndexVM virtualVM)
        {
            try
            {
                //Establezco la categoria seleccionada
                indexVM.CategoriaSeleccionada = listasBl.RecogerCategoriaBL(virtualVM.CategoriaSeleccionada.IdCategoria);
                //Recojo solo las plantas que necesito, sin llenar la memoria con el resto que no vamos a utilizar
                indexVM.ListaPlantasDeCategoriaSeleccionada = listasBl.RecogerPlantasDeCategoriaBL(indexVM.CategoriaSeleccionada.IdCategoria);



            }
            catch (Exception e)
            {
                ViewBag.Error = "Error al recoger las categorias de la base de datos";
                return View("Error");
            }
            return View(indexVM);
        }


        public IActionResult PonerPrecio(int id)
        {
            //Gracias al helper que nos dará directamente el id de la planta
            //recogeremos esa planta para mostrarla en la vista PonerPrecio
            clsPlanta planta = null;
            try
            {
                planta = listasBl.RecogerPlantaBL(id);
            }
            catch (Exception e)
            {
                ViewBag.Error = "Error al establecer la planta";
                return View("Error");
            }

            return View(planta);
        }


        [HttpPost]
        public IActionResult PonerPrecio(clsPlanta planta)
        {
            //Esto
            int filasAfectadas;
            try
            {
                //No se como hacerlo de forma más profesional
                filasAfectadas = gestionBL.EstablecerPrecioPlantaBL(planta.IdPlanta, planta.Precio);

                if (filasAfectadas == 0)
                {
                    ViewBag.Error = "No se encontró ninguna planta con estos datos";
                    
                }
                else if (filasAfectadas == 1)
                {
                    ViewBag.Exito = "Se estableció con éxito el precio de la planta";
                    
                }
            }
            catch (Exception e)
            {

                ViewBag.Error = "Algo no funciona en la Base de Datos. Intentelo más tarde";
                //Preguntar como hacer errores propios para que quede claro que es lo que ha fallado
                
            }

            return View(planta);
        }


        public IActionResult CambioDeCategoria()
        {
            cambioDeCategoriaVM = new CambioDeCategoriaVM();      
            return View(cambioDeCategoriaVM);
        }




        [HttpPost]
        public IActionResult CambioDeCategoria(CambioDeCategoriaVM vm)
        {
            int filas = 0;
            int idPlanta = 0;
            try
            {

                for (int i = 0; i < vm.Plantas.Count; i++)
                {
                    
                    if ((bool)vm.Plantas.ElementAt(i).SeleccionadaParaCambioDeCategoria)
                    {
                        idPlanta = vm.Plantas.ElementAt(i).IdPlanta;
                        filas += gestionBL.ModificarCategoriaDePlantaBL(vm.CategoriaSeleccionada.IdCategoria, idPlanta); 
                    }
                }
                if(filas > 0)
                ViewBag.Exito = "La categoria de las plantas seleccionadas se estableció con éxito.";
                else
                {
                    ViewBag.Error = "No se realizaon modificaciones en la Base de Datos";
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Algo no funciona en la Base de Datos. Intentelo más tarde";
                return View(new CambioDeCategoriaVM());
            }
   
            foreach(var item in vm.Plantas)
            {
                if(item.IdCategoria == vm.CategoriaSeleccionada.IdCategoria)
                {
                    item.SeleccionadaParaCambioDeCategoria = true;
                }
            }
            CambioDeCategoriaVM cDMVM = new CambioDeCategoriaVM();
            cDMVM.CategoriaSeleccionada = (vm.CategoriaSeleccionada);
            return View(cDMVM);
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