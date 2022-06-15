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














function InicializaEventos() {
    document.getElementById("formulario").addEventListener("load", generarFormulario(), true);

    document.getElementById("btnAceptar").addEventListener("click", introducirPlantaMediantePost(), true);
}

/**
 * <header> generarFormulario() </header>
 * 
 * <summary>
 * Procedimiento que se encarga de generar el 
 * formulario sobre el que se introducirán los datos 
 * para realizar el POST de la nueva planta
 * </summary>
 * 
 * <pre></pre>
 * 
 * <post></post>
 */
function generarFormulario() {
    var form = document.getElementById("formulario")
   generarInputText(form, "Nombre");
    var label = document.createElement("label");
    label.textContent = "Categoria";

    generarSelectEleccionCategoria(form, "idCategoria");
    generarInputText(form, "Descripcion");
    generarInputText(form, "Precio");
  
}



function generarSelectEleccionCategoria(form, idSelect) {
    var llamada = new XMLHttpRequest();
    llamada.open("GET", "http://localhost:5027/api/Categorias", true);

    llamada.onreadystatechange = function () {
        if (llamada.readyState < 4) {

        }
        else if (llamada.readyState == 4 && llamada.status == 200) {
            var div = document.createElement("div");
            var label = document.createElement("label");
            label.textContent = "Categoria"
            div.className = "form-control";
            var categorias = JSON.parse(llamada.responseText);
            var select = document.createElement("select");
            select.id = idSelect;
            for (var i = 0; i < categorias.length; i++) {
                var option = document.createElement("option");
                option.value = categorias[i].idCategoria;
                option.text = categorias[i].nombreCategoria;
                select.appendChild(option);
                div.appendChild(select);
                form.appendChild(div);
            }
           
        }
    };

    llamada.send();
}



/**
 * <header> introducirPlantaMediantePost(planta)
 * <summary> Procedimiento que se encarga de generar un elemento XMLHttpRequest 
 * de tipo POST y envía una planta a la api para que sea establecida dentro de la base de datos
 * </summary>
 * 
 * <pre>
 * Todas las propiedaders de planta deben ser válidos
 * </pre>
 * 
 * <post>
 * Siempre que la planta no se encuentre repetida, 
 * se introducirá la planta en la base de datos
 * </post>
 * */
function introducirPlantaMediantePost() {
    var planta = new clsPlanta();
    var llamada = new XMLHttpRequest();
    llamada.open("POST", "http://localhost:5027/api/Plantas", true);

    llamada.onreadystatechange = function () {
        if (llamada.readyState < 4) {

        } else if (llamada.readyState == 4 && llamada.status == 200) {
            var nombre = document.getElementById("Nombre");
            var descripcion = document.getElementById("Descripcion");
            var categoria = document.getElementById("idCategoria");
            var precio = document.getElementById("Precio");

           
            planta.nombrePlanta = nombre.value;
            planta.categoria = categoria.options[categoria.selectedIndex].value;
            planta.descripcion = descripcion.value;
            planta.precio = parseInt(precio.value);

            alert("La planta fue insertada con éxito");


        }
    };
    
    llamada.send(JSON.stringify(planta));
}



function generarInputText(form, idText) {
    var div = document.createElement("div");
    var label = document.createElement("label");
    div.className = "form-control";
    label.nodeValue = idText;
    label.textContent = idText
    var input = document.createElement("input");
    input.type = "text";
    input.id = idText;
    div.appendChild(label);
    div.appendChild(input);
    form.appendChild(div);
}