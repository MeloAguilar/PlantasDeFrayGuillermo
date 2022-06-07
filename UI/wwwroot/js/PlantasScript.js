window.onload = inicializaEventos;


/**
 * funcion que comprueba el estado del boton
 * btnSaludar y llama a la funcion saludar si este es clicado
 * */
function inicializaEventos() {
    document.getElementById("tablaPlantas").addEventListener("load", saludar, false);
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
function mostrarPlantas()
{
    var llamada = new XMLHttpRequest();

    llamada.open("GET", "https://localhost/api/Plantas");
}