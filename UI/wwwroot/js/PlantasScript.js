window.onload = inicializaEventos;


/**
 * funcion que comprueba el estado del boton
 * btnSaludar y llama a la funcion mostrar si este es clicado
 * */
function inicializaEventos() {
    mostrar();
}


/**
 * funcion que abre un alert que nos saluda
 * 
 * 
 * entradas: 
 * 
 * salidas:
 * 
 * 
 * */
function mostrar() {
    var llamada2 = new XMLHttpRequest();

    llamada2.open("GET", "http://localhost:5027/api/Categorias", false);
    var llamada = new XMLHttpRequest();
    llamada.open("GET", "http://localhost:5027/api/Plantas", true);
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



    llamada.onreadystatechange = function () {
        var listaPlantas;
        if (llamada.onreadyState < 4) {
            const imagen = document.createElement("img");
            imagen.src = "../img/carga1.png";
            document.getElementById("imagen").appendChild(imagen);
        }
        else if ((llamada.readyState == 4 && llamada.status == 200)) {
            var arrayJson = JSON.parse(llamada.responseText);

            rellenarPlantas(arrayJson, arrayCat);



        }
    };

    llamada2.send();
    llamada.send();
}


function rellenarSelectCategorias(arrayCat) {
    for (var i = 0; i < arrayCat.length; i++) {
        const option = document.createElement("option")
        option.text = arrayCat[i].nombreCategoria;
        option.value = arrayCat[i].idCategoria;
        document.getElementById("selectacion").appendChild(option);
    }
}

function rellenarPlantas(jsonPlantas, jsonCat) {
    var tabla = document.getElementById("tbody");
    for (var i = 0; i < jsonPlantas.length; i++) {
        var fila = document.createElement("tr");
        var columna = document.createElement("td");
        var textoCelda = document.createTextNode(jsonPlantas[i].nombrePlanta);
        columna.appendChild(textoCelda);
        fila.append(columna);
        for (var j = 0; j < jsonCat.length; j++) {
            if (jsonPlantas[i].idCategoria == jsonCat[j].idCategoria) {
                var celda1 = document.createElement("td");
                textoCelda = document.createTextNode(jsonCat[j].nombreCategoria);
                celda1.appendChild(textoCelda);
                fila.append(celda1);
                j = jsonCat.length;
            }


            var celda3 = document.createElement("td");
            if (jsonPlantas[i].precio != null) {
                textoCelda = document.createTextNode(jsonPlantas[i].precio);
            }
            else {
                textoCelda = document.createTextNode("0");
            }
            celda3.appendChild(textoCelda);
            fila.appendChild(celda3);
        }
        tabla.appendChild(fila);
    }
    
}


//function rellenarPlantas(jsonPlantas, jsonCat) {

//    var tabla = document.getElementById("tbody");
//    for (var i = 0; i < jsonPlantas.length; i++) {
//        var filas = document.createElement("tr");
//        var celdas = document.createElement("td");

//        var valorCelda = document.createTextNode(jsonPlantas[i].nombrePlanta);
//        celdas.appendChild(valorCelda);
//        for (var j = 0; j < jsonCat.length; j++) {
//            var nombreCat = jsonCat[j].nombreCategoria;
//            if (jsonCat[j].idCategoria == jsonPlantas[i].idCategoria) {
//                valorCelda = document.createTextNode(nombreCat);
//                celdas.appendChild(valorCelda);
//                j = jsonCat.length;
//            }
//            if (jsonPlantas[i].Precio != null) {
//                valorCelda = document.createTextNode(jsonPlantas[i].Precio);
//                celdas.appendChild(valorCelda);
//            } else {
//                valorCelda = document.createTextNode("0");
//                celdas.appendChild(valorCelda);
//            }
//            filas.appendChild(celdas);

//        }



//        tabla.appendChild(filas)
//    }
//}


//
 // un a variable que recoja la planta
 // buscar como crear un elemento de tipo td
  //checkBox.SetAttribute() buscar
  //crear checkbox
  //Se pueden hacer dos llamadas ajax a la vez
//No molestar 50 veces al Servidor





















//function crearPlanta(jsonObj) {
//    var atributo = "<tr>";
//    for (var i in jsonObj) {
//        atributo += "<th>" + jsonObj[i] + "<th/>"
//        //if (jsonObj[i] == "NombrePlanta") {
//        //    atributo += "<th>" + nombreCat + "<th/>"
//        //}
//    }
//    atributo += "<tr/>";
//    document.getElementById("tbody").innerHTML += atributo;
//}


//function mostrarPlantas(jsonp, jsonc) {
//    var i;
//    var planta;
//    var categoria;
//    for (i in jsonp) {
//        planta = i;
//        for (var j = 0; j < jsonc.length; j++) {
//            categoria = jsonc[j];
//            if (planta.IdCategoria === categoria.IdCategoria) {
//                crearPlanta(planta, categoria.NombreCategoria);
//            }
//        }

