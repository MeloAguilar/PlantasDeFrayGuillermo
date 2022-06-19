window.onload = inicializaEventos;


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

/**
 * 
 * <header> inicializaEventos() </header>
 * 
 * <summary>
 * funcion que comprueba el estado del boton
 * btnSaludar y llama a la funcion mostrar si este es clicado
 * </summary>
 * */
function inicializaEventos() {

   
    document.getElementById("tabla").addEventListener("change", mostrar(), true);

    document.getElementById("btnAceptar").addEventListener("click", RecogerDatosChecks, true);

    document.getElementById("selectacion").addEventListener("change", ComprobarCheckBoxes, true);



}


/**
 * <header> mostrar() </header>
 * 
 * <summary> 
 * Funcion principal que se encarga de realizar las dos llamadas a la base de datos para obtener las plantas y las categorias,
 * para que con elas se pueda realizar el método rellenarPlantas()
 * </summary>
 * 
 * <pre> ninguna</pre>
 * 
 * <post> ninguna </post>
 * */
function mostrar() {
    var llamada2 = new XMLHttpRequest();

    llamada2.open("GET", "http://localhost:5027/api/Categorias", false);
    var llamada = new XMLHttpRequest();
    llamada.open("GET", "http://localhost:5027/api/Plantas", true);
    var arrayCat;
    llamada2.onreadystatechange = function () {
        if (llamada2.readyState < 4) {

        }
        else if ((llamada2.readyState == 4 && llamada2.status == 200)) {

            arrayCat = JSON.parse(llamada2.responseText);
            RellenarSelectCategorias(arrayCat);


        }
    };
    llamada2.send();


    llamada.onreadystatechange = function () {
        var listaPlantas;
        if (llamada.readyState < 4) {

        }
        else if ((llamada.readyState == 4 && llamada.status == 200)) {
            listaPlantas = JSON.parse(llamada.responseText);
            RellenarPlantas(listaPlantas, arrayCat);



        }
    };


    llamada.send();
}


/**
 * <header> rellenarFilaConTexto(fila,texto)</header>
 * 
 * <summary>
 * Metodo que, dándo como parametros un elemento 'tr'
 * y un string, genera
 * </summary>
 * 
 * <pre>
 * el elemento 'tr' fila debe existir en el documento html o crearse previamente.</pre>
 * <post>siempre creará un elemento 'td', además del nodo de texto, que se convertirá en hijo de
 * la fila pasada como parámetro
 * </post>
  * @param {HtmlTableRowElement} fila
  * @param {string} texto
  */
function CrearCelda(fila, texto) {
    var columna = document.createElement("td");
    columna.textContent = texto;
    fila.appendChild(columna);
}




/**
 * 
 * <header> rellenarSelectCategorias(arrayCat) </header>
 * 
 * <summary> 
 * Método que se encarga de iyectar los options dentro de los text de
 * los select que se encuentra en el documento html los nombres de las Categorias
 * provenientes de un json que debe contener todas las categorias de plantas que se encuentren en la base de datos Fray Guillermo.
 * </summary>
 * 
 * <pre> </pre>
 * 
 * <post></post>
 * @param {JSON} arrayCat
 */
function RellenarSelectCategorias(arrayCat) {
    var conteo = 1;
    for (var i = 0; i < arrayCat.length; i++) {
        const option = document.createElement("option")
        option.setAttribute("id", "op" + conteo);
        option.text = arrayCat[i].nombreCategoria;
        option.value = arrayCat[i].idCategoria;
        document.getElementById("selectacion").appendChild(option);
        conteo++;
    }
    document.getElementById("op1").setAttribute("selected", "true")

}





/**
 * <header> rellenarPlantas(jsonPlantas, jsonCat) </header>
 * 
 * <summary> 
 * Método que se encargará de rellenar la tabla con la informacion sobre las plantas 
 * </summary>
 * 
 * <pre> jsonPlantas debe ser un json que contenga los datos de todas las plantas de la base de datos Fray Guillermo 
 * y una lista con todas las categorias de la base de datos Fray Guillermo</pre>
 *
 * <post>nada</post>
 * @param {JSON} arrayPlantas
 * @param {JSON} arrayCategorias
 */
function RellenarPlantas(arrayPlantas, arrayCategorias) {

    var cuerpoTabla = document.createElement("tbody");

    var tabla = document.getElementById("tabla");
    tabla.appendChild(cuerpoTabla);


    for (var i = 0; i < arrayPlantas.length; i++) {
        var fila = document.createElement("tr");
        fila.id = arrayPlantas[i].idPlanta;
        fila.nodeName = arrayPlantas[i].idPlanta;
        CrearCelda(fila, arrayPlantas[i].nombrePlanta);
        ElegirCategoria(arrayCategorias, arrayPlantas[i].idCategoria, fila);
        if (arrayPlantas[i].precio != null) {
            CrearCelda(fila, arrayPlantas[i].precio);
        }
        else {
            CrearCelda(fila, "0");
        }



        var check = GenerarCheckBox("checkbox" + arrayPlantas[i].idPlanta);
        var columnaCheck = document.createElement("td");
        columnaCheck.appendChild(check);

        fila.appendChild(columnaCheck);
        cuerpoTabla.appendChild(fila);
    }
}





/**
 * <header> elegirCategoria(jsonCat, idPlanta, fila) </header>
 * 
 * 
 * <summary> 
 * Método que se encarga de introducir el nombre de la categoria en la tabla 
 * bajo la cabecera Nombre Categoria comparandose con el id de la categoria de dicha planta
 * </summary>
 * 
 * <pre> </pre>
 * 
 * <post></post>
 * @param {JSON} arrayCategorias
 * @param {int} idCategoriaPlanta
 * @param {document.createElement} fila
 */
function ElegirCategoria(arrayCategorias, idCategoriaPlanta, fila) {
    var categoria;
    var salir = false;
    for (var i = 0; i < arrayCategorias.length && !salir; i++) {
        if (idCategoriaPlanta == arrayCategorias[i].idCategoria) {
            CrearCelda(fila, arrayCategorias[i].nombreCategoria);
            categoria = arrayCategorias[i];
            salir = true;
        }
    }
    return categoria;
}









/**
 * <header> recogerDatosChecks() </header>
 * <summary>
 * Método que se encarga de modificar el value de los 
 * checkboxes cuando cambia la opcion seleccionada en el select
 * </summary>
 * 
 * 
 * <pre>nada</pre>
 * 
 * <post>nada</post>
 * 
 * */
function RecogerDatosChecks() {
    var success = confirm("¿Estas seguro de que deseas cambiar la categoria de las plantas seleccionadas?");

    if (success) {
        var llamada = new XMLHttpRequest();
        llamada.open("GET", "http://localhost:5027/api/Plantas", true);
        llamada.onreadystatechange = function () {
            if (llamada.readyState < 4) {

            } else if (llamada.readyState == 4 && llamada.status == 200) {
                /*comprobarCheckBoxes();*/
                var jsonPlantas = JSON.parse(llamada.responseText);
                CambiarCategoriasVariasPlantas(jsonPlantas);
            }

        };
    }
    else {
        alert("No se modificarán las plantas");
    }
    

    llamada.send();

}


/**
 * 
 * <header>comprobarCheckBoxes()</header>
 * 
 * <summary>
 * Método que modifica el value de todos los checkboxes en base al value seleccionado del select 
 * 
 */
function ComprobarCheckBoxes() {
    var tabla = document.getElementById("tbody");
    var select = document.getElementById("selectacion");
    var valorSeleccionado = select.options[select.selectedIndex].value;
    var llamada = new XMLHttpRequest();
    llamada.open("GET", "http://localhost:5027/api/Plantas", true);
  
    llamada.onreadystatechange = function () {
        if (llamada.readyState < 4) {

        } else if (llamada.status == 200 && llamada.readyState == 4) {
            var arrayPlantas = JSON.parse(llamada.responseText);
            for (var i = 0; i < arrayPlantas.length; i++) {
               
                var check = document.getElementById("checkbox" + arrayPlantas[i].idPlanta);
                check.value = valorSeleccionado;
            }

        }
    };
    llamada.send();
  

}
/**
 * <header> cambiarCategoriaPlanta(jsonPlantas) </header>
 * 
 * <summary>Método que se encarga de comprobar el estado checked de los checkboxes
 * (ya que su número será igual a la cantidad de plantas de la lista)
 * para modificar el atributo idCategoria de uno o varios objetos clsPlanta con idPlanta igual
 * al número que acompaña a la sentencia checkbox dentro del id de cada checkbox, 
 * rellena los datos del objeto basandose en su idPlanta y se lo pasa a la funcion PutPlanta()</summary>
 * 
 * <pre>jsonPlantas debe ser un objeto JSON que contenga una lista de objetos clsPlanta en su interior</pre>
 * <post>nada</post>
 * @param {clsPlanta[]} arrayPlantas
 */
function CambiarCategoriasVariasPlantas(arrayPlantas) {
    var check;
    for (var i = 0; i < arrayPlantas.length; i++) {
        check = document.getElementById("checkbox" + arrayPlantas[i].idPlanta);
        if (check.checked) {
            CambiarCategoriaPlanta(arrayPlantas[i], check);

        }

    }
}

/**
 * <header> cambiarCategoriaPlanta(planta, check)</header>
 * 
 * <summary> 
 * Método que se encarga de cambiar la categoria de una planta
 * </sumary>
 * 
 * <pre></pre>
 * <post></post>
 * @param {clsPlanta} planta
 * @param {HtmlInputElement} checkbox
 */
function CambiarCategoriaPlanta(planta, checkbox) {
    ComprobarCheckBoxes();
    var numCat = parseInt(checkbox.value);
    planta.idCategoria = numCat;
    checkbox.checked = false;
    PutPlanta(planta);
    ModificarTablaTrasPut(planta);
}


/**
 * <header> PutPlanta(planta) </header>
 * 
 * <summary>
 * Método que se encarga de enviar la request XMLHttpRequest para modificar 
 * una planta dado el modelo de una planta ya esxistente en 
 * la base de datos Fray Guillermo
 * </summary>
 * 
 * <pre>la planta aportada debe estar en la base de datos</pre>
 * <post>realizará una llamada XMLHttpRequest parta modificar un registo de tabla plantas de la base de datos Fray Guillermo</post>
 * @param {clsPlanta} planta
 */
function PutPlanta(planta) {
    var llamada = new XMLHttpRequest();

    llamada.open("PUT", "http://localhost:5027/api/Plantas/" + planta.idPlanta);
    var json = JSON.stringify(planta);
    llamada.setRequestHeader('Content-type', 'text/json; charset=utf-8');
    llamada.onreadystatechange = function () {
        if (llamada.readyState == 4 && llamada.status == 200) {

        } else if(llamada.readyState < 4){

        }
    };

    llamada.send(json);
}




/**
 * <header> modificarTablaTrasPut() </header>
 * 
 * <summary>
 * Método que se encarga de recargar la planta seleccionada para el cambio
 * dejando el check unchecked y su nueva categoría junto a esta
 * </summary>
 * 
 * <pre></pre>
 * 
 * <post></post<
 */
function ModificarTablaTrasPut(planta) {



    var llamada = new XMLHttpRequest();
    llamada.open("GET", "http://localhost:5027/api/Categorias", true);
    llamada.onreadystatechange = function () {
        if (llamada.readyState == 4 && llamada.status == 200) {
            var arrayCategoria = JSON.parse(llamada.responseText);
            
            GenerarFila(planta, arrayCategoria);
        } else if (llamada.readyState < 4) {

        }
    };
    llamada.send();
}


/**
 * <header> generarFila(planta) </header>
 * 
 * <summary>
 * Método que se encarga de generar una fila completa de la tabla 
 * con la categoria de las plantas seleccionadas para cambio modificada
 * </summary>
 * 
 * <pre>
 * Necesita de un objeto clsPlanta ya existente en la base de datos FrayGuillermo
 * </pre>
 * 
 * <post>
 * Generará una fila completa con los datos necesarios para la tabla
 * </post>
 * @param {clsPlanta} planta
 * 
 * @param {JSON} arrayCategorias
 */
function GenerarFila(planta, arrayCategorias) {
    var filaAnterior = document.getElementById(planta.idPlanta);

    var fila = document.createElement("tr");
    fila.id = planta.idPlanta;
    CrearCelda(fila, planta.nombrePlanta);


    ElegirCategoria(arrayCategorias, planta.idCategoria, fila);
    if (planta.precio == 0) {

        CrearCelda(fila, "0");

    }
    else {
        CrearCelda(fila, planta.precio);
    }
    var columna = document.createElement("td");
    var check = GenerarCheckBox("checkbox" + planta.idPlanta);
    columna.appendChild(check);
    fila.appendChild(columna);



    filaAnterior.replaceWith(fila);
}

/**
 * 
 * 
 * 
 * */
/**
 * <header> generarCheckBox() </header>
 * 
 * <summary>Procedimiento que crea un elemento html input checkbox 
 * y le da el valor del value del select con id "selectacion"
 * </summary>
 * 
 * <pre>debe existir un select que muestre las categorias llamado "selectacion"</pre>
 * <post>Siempre devolverá un elemento input checkbox para utilizar</post>
 * 
 * 
 *  @param {string} idCheck
 * */
function GenerarCheckBox(idCheck) {


    var check = document.createElement("input");
    check.type = "checkbox";
    var select = document.getElementById("selectacion");
    var opcionSeleccionada = select.options[select.selectedIndex];


    check.setAttribute("id", idCheck);
    check.setAttribute("value", opcionSeleccionada.value);



    return check;
}











