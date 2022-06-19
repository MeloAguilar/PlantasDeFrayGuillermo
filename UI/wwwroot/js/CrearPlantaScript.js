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
    document.getElementById("formulario").addEventListener("load", GenerarFormulario(), true);

    document.getElementById("btn").addEventListener("click", generarPlantaParaPost, false);
}

/**
 * <header> GenerarFormulario() </header>
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
function GenerarFormulario() {
    var form = document.getElementById("formulario");
   generarInputText(form, "Nombre");
    var label = document.createElement("label");
    label.textContent = "Categoria";
    
    generarInputText(form, "Descripcion");
    generarInputText(form, "Precio");
    GenerarSelectEleccionCategoria(form, "idCategoria");
  
}


/**
 * <header> GenerarSelectEleccionCategoria(form, idSelect) </header>
 * 
 * <summary>
 *  Método que se enarga de generar el ellemento Html select y conforma las opciones, 
 *  con un string como identificador de este y un elemento div que debe encontrarse en la pagina Html a crear
 * </summary>
 * 
 * <pre></pre>
 * 
 * <post></post>
 * @param {HTMLDivElement} form
 * @param {string} idSelect
 */
function GenerarSelectEleccionCategoria(form, idSelect) {
    var llamada = new XMLHttpRequest();
    llamada.open("GET", "http://localhost:5027/api/Categorias", false);

    llamada.onreadystatechange = function () {
        if (llamada.readyState < 4) {

        }
        else if (llamada.readyState == 4 && llamada.status == 200) {
           
            var div = document.createElement("div");
            var label = document.createElement("label");
            label.textContent = "Categoría"
            var br = document.createElement("br");
            var arrayCategorias = JSON.parse(llamada.responseText);
            var select = document.createElement("select");
            select.id = idSelect;
            select.className = "form-select-sm";
            for (var i = 0; i < arrayCategorias.length; i++) {
                var option = document.createElement("option");
                option.setAttribute("id", "op" + arrayCategorias[i].idCategoria);
                option.text = arrayCategorias[i].nombreCategoria;
                option.value = arrayCategorias[i].idCategoria;
                select.appendChild(option);
                
            }
            var boton = document.createElement("input");
            boton.type = "submit";
            boton.id = "btn";
            boton.className = "btn btn-success";
            div.appendChild(label);
            div.appendChild(br);
            div.appendChild(select);
            var br2 = document.createElement("br");
            form.appendChild(div);
            form.appendChild(br2);
            form.appendChild(boton);
        }
    };

    llamada.send();
}



function generarPlantaParaPost() {
    var planta = new clsPlanta();
    var nombre = document.getElementById("Nombre");
    var descripcion = document.getElementById("Descripcion");
    var precioText = document.getElementById("Precio");
    var precio = parseFloat(precioText.value);
    var select = document.getElementById("idCategoria");
    var categoria = select.options[select.selectedIndex];
    planta.nombrePlanta = nombre.value;
    planta.idCategoria = parseInt(categoria.value);
    planta.precio = precio;
    planta.descripcion = descripcion.value;
    IntroducirPlantaMediantePost(planta);


}

/**
 * <header> modificarPaginaDespuesDeInsercion() </header>
 * 
 * <summary>
 * 
 * </summary>
 * 
 * <pre>
 * 
 * </pre>
 * 
 * <post>
 * 
 * </post>
 * 
 * */
function modificarPaginaDespuesDeInsercion() {
    var formulario = document.getElementById("formulario");
    formulario.replaceChildren("");
    GenerarFormulario();
    alert("La planta fue insertada con éxito");


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
 * 
 * @param {clsPlanta} planta
 * */
function IntroducirPlantaMediantePost(planta) {
    var llamada = new XMLHttpRequest();
    llamada.open("POST", "http://localhost:5027/api/Plantas", false);
    var json = JSON.stringify(planta);
    llamada.setRequestHeader('Content-type', 'text/json; charset=utf-8');
    llamada.onreadystatechange = function () {
        if (llamada.readyState < 4) {
        } else if (llamada.readyState == 4 && llamada.status == 200) {

            modificarPaginaDespuesDeInsercion();

        }
    };
    
    llamada.send(json);
}


/**
 * <header> generarInputText(form, idText)
 * 
 * <summary>
 * Método que se encarga de, dado un id y un elemento Html form,
 * generar un elemento div, que será hijo del form,
 * con dis hijos, un elemento label cuyo textContent sea igual al idText pasado como parámetro
 * y un input text.
 * @param {HTMLDivElement} form
 * @param {string} idText
 */
function generarInputText(form, idText) {
    var div = document.createElement("div");
    var br = document.createElement("br");
    var label = document.createElement("label");
    label.nodeValue = idText;
    label.textContent = idText
    var input = document.createElement("input");
    input.type = "text";
    input.id = idText;
    div.appendChild(label);
    div.appendChild(br);
    div.appendChild(input);
    form.appendChild(div);
}