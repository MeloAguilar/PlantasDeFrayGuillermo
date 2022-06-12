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

/**
 * funcion que comprueba el estado del boton
 * btnSaludar y llama a la funcion mostrar si este es clicado
 * */
function inicializaEventos() {
    mostrar();
    document.getElementById("btnAceptar").addEventListener("click", recogerDatosChecks, true);
    document.getElementById("crear").addEventListener("click", crear)
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
    llamada.open("GET", "http://localhost:5027/api/Plantas", false);
    var arrayCat;
    llamada2.onreadystatechange = function () {
        if (llamada2.readyState < 4) {
            const imagen = document.createElement("img");
            imagen.src = "../img/carga1.png";
            document.getElementById("imagen").appendChild(imagen);
        }
        else if ((llamada2.readyState == 4 && llamada2.status == 200)) {
            arrayCat = JSON.parse(llamada2.responseText);
            rellenarSelectCategorias(arrayCat);


        }
    };
    llamada2.send();


    llamada.onreadystatechange = function () {
        var listaPlantas;
        if (llamada.readyState < 4) {
            const imagen = document.createElement("img");
            imagen.src = "../img/carga1.png";
            document.getElementById("imagen").appendChild(imagen);
        }
        else if ((llamada.readyState == 4 && llamada.status == 200)) {
            var jsonPlantas = JSON.parse(llamada.responseText);
            rellenarPlantas(jsonPlantas, arrayCat);



        }
    };


    llamada.send();
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
function rellenarSelectCategorias(arrayCat) {
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
 * @param {JSON} jsonPlantas
 * @param {JSON} jsonCat
 */
function rellenarPlantas(jsonPlantas, jsonCat) {
    var numCheck = 1;
    var tabla = document.getElementById("tbody");
    for (var i = 0; i < jsonPlantas.length; i++) {
        var fila = document.createElement("tr");
        var columna = document.createElement("td");
        var textoColumna = document.createTextNode(jsonPlantas[i].nombrePlanta);
        columna.appendChild(textoColumna);
        fila.append(columna);
        elegirCategoria(jsonCat, jsonPlantas[i].idCategoria, fila);
        var columna3 = document.createElement("td");
        if (jsonPlantas[i].precio != null) {
            textoColumna = document.createTextNode(jsonPlantas[i].precio);
        }
        else {
            textoColumna = document.createTextNode("0");
        }



        columna3.appendChild(textoColumna);
        fila.appendChild(columna3);


        var columna4 = document.createElement("td");
        //La forma de crear inputs, ya sea checkbox, radio, button..
        var check = document.createElement("input");
        check.type = "checkbox";
        //Como seleccionar la option de un select y ponerselo como value a un check
        var select = document.getElementById("selectacion");
        var opcionSeleccionada = select.options[select.selectedIndex];

        check.setAttribute("id", "checkbox" + numCheck);
        check.setAttribute("value", opcionSeleccionada.value)


        columna4.appendChild(check);
        fila.appendChild(columna4);

        tabla.appendChild(fila);
        numCheck++;
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
 * @param {JSON} jsonCat
 * @param {int} idCategoriaPlanta
 * @param {document.createElement} fila
 */
function elegirCategoria(jsonCat, idCategoriaPlanta, fila) {
    var textocolumna;
    var salir = false;
    for (var j = 0; j < jsonCat.length && !salir; j++) {
        if (idCategoriaPlanta == jsonCat[j].idCategoria) {
            var columna1 = document.createElement("td");
            textocolumna = document.createTextNode(jsonCat[j].nombreCategoria);
            columna1.appendChild(textocolumna);
            fila.append(columna1);
            salir = true;
        }
    }
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
function recogerDatosChecks() {
    var llamada = new XMLHttpRequest();
    llamada.open("GET", "http://localhost:5027/api/Plantas", true);

    llamada.onreadystatechange = function () {
        if (llamada.readyState < 4) {

        } else if (llamada.readyState == 4 && llamada.status == 200) {
            var jsonPlantas = JSON.parse(llamada.responseText);
            cambiarCategoriaPlanta(jsonPlantas);
        }

    }

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
 * @param {any} jsonPlantas
 */
function cambiarCategoriaPlanta(jsonPlantas) {
    for (var i = 0; i < jsonPlantas.length; i++) {

        var check = document.getElementById("checkbox" + jsonPlantas[i].idPlanta);
        var select = document.getElementById("selectacion");
        var opcionSeleccionada = select.options[select.selectedIndex];
        check.value = opcionSeleccionada.value;
        if (check.checked) {
            var numCat = parseInt(check.value);
            var planta = new clsPlanta();
            planta.idPlanta = jsonPlantas[i].idPlanta;
            planta.nombrePlanta = jsonPlantas[i].nombrePlanta;
            planta.descripcion = jsonPlantas[i].descripcion;
            planta.idCategoria = numCat;
            planta.precio = jsonPlantas[i].precio;
            PutPlanta(planta);
        }
    }
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
            alert(json);
        } else {
            alert(json);
        }
    };

    llamada.send(json);

}


















