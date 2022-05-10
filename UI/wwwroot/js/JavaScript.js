window.onload = inicializaEventos;


/**
 * funcion que comprueba el estado del boton
 * btnSaludar y llama a la funcion saludar si este es clicado
 * */
function inicializaEventos() {
    document.getElementById("btnSaludar").addEventListener("click", saludar, false);
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
function saludar()
{
    alert("Hola desde el archivo de JavaScript");
}