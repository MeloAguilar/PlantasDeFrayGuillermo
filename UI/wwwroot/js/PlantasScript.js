window.onload = inicializaEventos;


/**
 * funcion que comprueba el estado del boton
 * btnSaludar y llama a la funcion mostrar si este es clicado
 * */
function inicializaEventos() {
    document.getElementById("btnMostrar").addEventListener("click", mostrar, true);
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

    var llamada = new XMLHttpRequest();
    llamada.withCredentials = true;
    llamada.setRequestHeader = ("Access-Control-Allow-Origin: *");
    llamada.open("GET", "http://localhost:5093/api/Plantas", true);
    llamada.onreadystatechange = function () {
        var listaPlantas;
        if (llamada.onreadyState < 4) {
            const imagen = document.createElement("img");
            imagen.src = "../img/carga1.png";
            document.getElementById("imagen").appendChild(imagen);
        }
        else if ((llamada.readyState == 4 && llamada.status == 200)) {
            var arrayJson = JSON.parse(llamada.responseText);
            var atributo = "<tr>";
            for (var i in arrayJson) {
                for (var j in i) {
                    var atributos = j.split(",");
                    for (var k in atributos) {
                        atributo += "<th>" + k + "<th/>";
                    }
                }
                atributo += "<tr/>"
                document.getElementById("tbody").insertRow(-1).innerHTML = atributo;
            }
        }
    };

    llamada.send();
}

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

