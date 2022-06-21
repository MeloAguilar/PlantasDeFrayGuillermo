window.onload = InicializaEventos;



class clsPlanta {
    constructor(idPlanta, nombrePlanta, descripcion, idCategoria, precio) {
        idPlanta: idPlanta;
        nombrePlanta: nombrePlanta;
        descripcion: descripcion;
        idCategoria: idCategoria;
        precio: precio;
    }
}

class clsCategoria {
    constructor(idCategoria, nombreCategoria) {
        idCategoria: idCategoria;
        nombreCategoria: nombreCategoria;

    }
}

var arrayPlantas;
var arrayCategorias;

function InicializaEventos() {
    document.getElementById("tbody").addEventListener("load", RealizarLlamadaInicial(), true);

    document.getElementById("btnEliminar").addEventListener("click", EliminarRegistrosSeleccionados, true);
}








/**
 * <header> EliminarRegistrosSeleccionados() </header>
 * 
 * <summary>
 *  Método que se encarga de comprobar el estado de todos los checkboxes de la página.
 *  En caso de que su atributo "checked" sea igual a true, se llamará al método 
 *  "DeletePlanta", dandole la planta que se encuentre en la posicion deseada
 *  del array generado por la respuesta de la llamada a la Api como parámetro
 * </summary>
 * 
 * <pre>
 *  La Api debe ser accesible.
 *  Deben existir tantos checkboxes como registros en la tabla plantas de la base de datos Fray Guillermo
 * </pre>
 * <post>
 *  nada
 * </post>
 *
 * */
function EliminarRegistrosSeleccionados() {
    for (var i = 0; i < arrayPlantas.length; i++) {
        var check = document.getElementById("checkbox" + arrayPlantas[i].idPlanta);
        if (check.checked) {
            DeletePlanta(arrayPlantas[i]);
        }
    }
}

/**
 * <header> DeletePlanta(planta) </header>
 * 
 * <summary>
 * Método que realiza una llamada XMLHttpRequest a la api FrayGuillermo
 * para eliminar tantos registros como checks marcados
 * </summary>
 * 
 * <pre>
 * ninguna
 * </pre>
 * <post>
 * ninguna
 * </post>
 * 
 * @param {clsPlanta} planta
 * */
function DeletePlanta(planta) {
    var imagn = document.createElement("img");
    imagn.src = "../img/carga1.png";
    var success = confirm("Está seguro de que desea eliminar" + planta.nombrePlanta);
    if (success) {
        var llamada = new XMLHttpRequest();
        llamada.open("DELETE", "http://localhost:5027/api/Plantas/" + planta.idPlanta, false);

        llamada.onreadystatechange = function () {
            if (llamada.readyState < 4) {
                var div = document.getElementById("imagen");

                div.appendChild(imagn);
            } else if (llamada.readyState == 4 && llamada.status == 200) {
                imagn.remove();
                alert(planta.nombrePlanta + " se eliminó con éxito.");
                RealizarLlamadaInicial();
            }
        };
        llamada.send(null);
    } else {
        alert(planta.nombrePlanta + " no se eliminó");
        document.getElementById("checkbox" + planta.idPlanta).checked = false;
    }


}

/**
 * <header> RealizarLlamadaInicial() </header>
 * 
 * <summary> Método que se llamará siempre que se recargue la página
 * para llamar al método que genera el listado de plantas proporcionandole 
 * un array de objetos clsPlanta
 * 
 * */
function RealizarLlamadaInicial() {
    var imagn = document.createElement("img");
    imagn.src = "../img/carga1.png";
    var llamada = new XMLHttpRequest();
    llamada.open("GET", "http://localhost:5027/api/Plantas", false);

    llamada.onreadystatechange = function () {
        if (llamada.readyState < 4) {
            var div = document.getElementById("imagen");

            div.appendChild(imagn);
        }
        else if (llamada.readyState == 4 && llamada.status == 200) {
            imagn.remove();
            arrayPlantas = JSON.parse(llamada.responseText);
            GenerarListadoPlantas();
        }
    };
    llamada.send();
}


/**
 * 
 * <header> generarListadoPlantas(arrayPlantas)</header>
 *
 * <summary>
 * Método que se encarga de rellenar la página con todas las plantas que se encuentran en la base de datos,
 * junto con el nombre de la categoria a la que pertenecen y un check que recogerá esa planta para eliminarla en caso de estar a true.</summary>
 * 
 * <pre>ninguna</pre>
 * 
 * <post>ninguna</post>
 * 
 */
function GenerarListadoPlantas() {
    var tabla = document.getElementById("tbody");
    for (var i = 0; i < arrayPlantas.length; i++) {
        var fila = document.createElement("tr");
        fila.id = "fila" + arrayPlantas[i].idPlanta;
        CrearCelda(fila, arrayPlantas[i].nombrePlanta);
        GenerarNombreCategoria(arrayPlantas[i].idCategoria, fila);
        var idPlantaString = arrayPlantas[i].idPlanta.toString();
        var precioString = arrayPlantas[i].precio.toString();
        CrearCelda(fila, precioString);

        var check = GenerarCheckBox("checkbox" + idPlantaString, idPlantaString);
        var columnaCheck = document.createElement("td");
        columnaCheck.appendChild(check);
        fila.appendChild(columnaCheck);
        tabla.appendChild(fila);
    }

}



/**
 * <header> GenerarNombreCategoria(idCategoria, fila) </header>
 * 
 * <summary> método que se encarga de devolver el nombre de un objeto 
 * clsCategoria si se le ofrece como parámetro un entero que correspona con 
 * la propiedad idCategoria de dicho objeto, de lo contrario devolverá -1
 * <pre>nada</pre>
 * 
 * <post>nada</post>
 * @param {Int32Array} idCategoria
 * @param {HTMLTableRowElement} fila
 */
function GenerarNombreCategoria(idCategoria, fila) {
    var imagn = document.createElement("img");
    imagn.src = "../img/carga1.png";
    var categoria;
    var llamada = new XMLHttpRequest();
    llamada.open("GET", "http://localhost:5027/api/Categorias", false);

    llamada.onreadystatechange = function () {
        if (llamada.readyState < 4) {
            var div = document.getElementById("imagen");

            div.appendChild(imagn);
        } else if (llamada.readyState == 4 && llamada.status == 200) {
            imagn.remove();
            arrayCategorias = JSON.parse(llamada.responseText);
            var nombreCat;
            categoria = ObtenerCategoria(idCategoria, arrayCategorias);
            nombreCat = categoria.nombreCategoria;
            CrearCelda(fila, nombreCat);
            nombre = categoria.nombreCategoria;
        }
    };
    llamada.send();
}


/**
 * <header> ObtenerCategoria(idCategoria, arrayCategorias) </header>
 * 
 * <summary> Método que devuelve una categoria a partir de su id y una lista de 
 * objetos clsCategoria
 * </summary>
 * 
 * <pre>nada</pre>
 * <post>nada</post>
 * @param {Int32Array} idCategoria
 */
function ObtenerCategoria(idCategoria) {
    var categoria;
    var salir = false;
    for (var i = 0; i < arrayCategorias.length && !salir; i++) {

        if (arrayCategorias[i].idCategoria == idCategoria) {
            categoria = arrayCategorias[i];
            salir = true;
        }
    }
    return categoria;
}



/**
 * <header> GenerarCheckBox() </header>
 * 
 * <summary>Procedimiento que crea un elemento html input checkbox 
 * y le da el valor del value introducido como paarametro
 * </summary>
 * 
 * <pre>debe existir un select que muestre las categorias llamado "selectacion"</pre>
 * 
 * <post>Siempre devolverá un elemento input checkbox para utilizar</post>
 * 
 * 
 *  @param {string} idCheck
 * */
function GenerarCheckBox(idCheck, valueCheck) {


    var check = document.createElement("input");
    check.type = "checkbox";

    check.setAttribute("id", idCheck);
    check.setAttribute("value", valueCheck);



    return check;
}




/**
 * <header> CrearCelda(fila,texto)</header>
 * 
 * <summary>
 * Metodo que, dándo como parametros un elemento 'tr'
 * y un string, genera
 * </summary>
 * 
 * <pre>
 * el elemento 'tr' fila debe existir en el documento html o crearse previamente.
 * </pre>
 * 
 * <post>
 * siempre creará un elemento 'td', además del nodo de texto, que se convertirá en hijo de
 * la fila pasada como parámetro
 * </post>
  * @param {HtmlTableRowElement} fila
  * @param {string} texto
  */
function CrearCelda(fila, texto) {
    var celda = document.createElement("td");
    celda.textContent = texto;
    fila.appendChild(celda);
}




