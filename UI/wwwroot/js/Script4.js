// JavaScript source code
window.onload = iniciarEventos


function iniciarEventos()
{
    //Evento que escucha al boton mostrar
    document.getElementById("mostrar").addEventListener("click", mostrar, false)
    //Evento que escucha al boton sumarFila
    document.getElementById("sumarFila").addEventListener("click", sumarFila, false)
    //Evento que escucha al boton eliminar
    document.getElementById("eliminar").addEventListener("click", eliminar, false)

}


function mostrar()
{

    //Establezco el texto que irá en el alert
    var text = "";
    //Me traigo la tabla de el formulario
    var table = (document.getElementById("tabla"))
    //establezco que "rows" será igual a la longitud de la tabla
    var rows = table.rows.length;
    //Comenzamos a leer bidimensionalmente
    for (var i = 0; i < rows; i++)
    {
        for (var j = 0; j < 2; j++)
        {
            //Si es el primer dato no escribirá una coma
            if (i == 0 && j == 0)
                text += document.getElementById("tabla").rows[i].cells[j].innerText
            //Cualquiera de los siguientes datos se separará con una coma y un espacio
            else
                text += ", " + document.getElementById("tabla").rows[i].cells[j].innerText
        }
        text += "\n"
    }
    //Envio el alert para que se muestre en pantalla
    alert(text)
}

function sumarFila()
{
    //Recojo la tabla
    var table = document.getElementById("body");
    var atributo = "Celda"
    //Recojo el número de filas de la tabla y le añado 1, ya que va a añadirse 1 a la longitud de la tabla
    var numFila = (table.rows.length+1);
    var numColumna = 1
    //Inserto los datos en la nuev fila
    document.getElementById("body").insertRow(-1).innerHTML = '<th>' + atributo + numFila + numColumna + '</th>' + '<th>' + atributo + numFila + (numColumna+1) + '</th>';
}

function eliminar()
{
    //Recojo la tabla
    var table = document.getElementById("tabla")
    //Cuento la cantidad de filas
    var cantidadFilas = table.rows.length;
    //Compruebo que no sea el encabezado lo unico que existe en la tabla para que la tabla no quede totalmente vacía
    if (cantidadFilas <= 1)
        alert("No se puede eliminar el encabezado")
        //Elimino la última fila
    else (table.deleteRow(cantidadFilas - 1));
}