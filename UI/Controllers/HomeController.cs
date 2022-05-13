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
        private IndexVM indexVM = new IndexVM();
        private CambioDeCategoriaVM cambioDeCategoriaVM;


        //Al inicializarlo pido directamente el listado de categorias ya que es un listado pequeño
        public HomeController(ILogger<HomeController> logger)
        {
            cambioDeCategoriaVM = new CambioDeCategoriaVM(listasBl.RecogerListadoCompletoPlantasBL());
            cambioDeCategoriaVM.Categorias = listasBl.RecogerListadoCategoriasBL();
            indexVM.ListaCategorias = listasBl.RecogerListadoCategoriasBL();
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
            int exito;
            try
            {
                //No se como hacerlo de forma más profesional
                exito = gestionBL.EstablecerPrecioPlantaBL(planta.IdPlanta, planta.Precio);

                if (exito == 0)
                {
                    ViewBag.Error = "No se encontró ninguna planta con estos datos";
                    return View(planta);
                }
                else if (exito == 1)
                {
                    planta = listasBl.RecogerPlantaBL(planta.IdPlanta);
                    ViewBag.Exito = "Se estableció con éxito el precio de la planta";
                    return View(planta);
                }
            }
            catch (Exception e)
            {

                ViewBag.Error = "Algo no funciona en la Base de Datos. Intentelo más tarde";
                //Preguntar como hacer errores propios para que quede claro que es lo que ha fallado
                return View(planta);
            }

            return View(planta);
        }


        public IActionResult CambioDeCategoria(int idCategoria)
        {
            cambioDeCategoriaVM.Categorias = listasBl.RecogerListadoCategoriasBL();
            return View(cambioDeCategoriaVM);
        }


        //TODO Probar a duplicar la página a ver si queda mejor ya que se podria elegir primero la categoria y despues las plantas de esta
        [HttpPost]
        public IActionResult CambioDeCategoria(CambioDeCategoriaVM vm)
        {
            vm = new CambioDeCategoriaVM(listasBl.RecogerListadoCompletoPlantasBL());
            vm.CategoriaSeleccionada = listasBl.RecogerCategoriaBL(vm.CategoriaSeleccionada.IdCategoria);

            int idPlanta = 0;
            try
            {
                for (int i = 0; i < vm.RepresentacionDeLasPlantasSeleccionadas.Count; i++)
                {
                    if (vm.RepresentacionDeLasPlantasSeleccionadas[i])
                    {
                        idPlanta++;

                        gestionBL.ModificarCategoriaDePlantaBL(vm.CategoriaSeleccionada.IdCategoria, idPlanta);



                    }
                }
                ViewBag.Exito = "La categoria de las plantas seleccionadas se estableció con éxito.";
            }
            catch (Exception e)
            {
                ViewBag.Error = "Algo no funciona en la Base de Datos. Intentelo más tarde";
                return View(vm);
            }

            return View(vm);
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