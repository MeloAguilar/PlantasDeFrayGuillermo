using DAL.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Gestion
{
    public class clsGestionPlantas
    {

        clsMyConnection miConexion = new clsMyConnection();

        /// <summary>
        /// <header>bool EstablecerPrecioPlanta(int idPlanta, double precio)</header>
        /// 
        /// Función que se encarga de establecer el atributo precio de un objeto clsPlata 
        /// que coincidirá con la misma tabla de la Base de Datos
        /// a partir de dos parámetros de entrada, "idPlanta" que será
        /// el atributo IdPlanta y "precio" que sera el atributo Precio de un 
        /// objeto clsPlanta. Devolverá un entero que indicará 
        /// cuantas columnas han sido afectadas en la BBDD
        /// <precondition>Ninguna.</precondition>
        /// <postcondition>la salida será 0 o 1</postcondition>
        /// </summary>
        /// <param name="idPlanta"></param>
        /// <param name="precio"></param>
        /// <returns>int referente al número de filas afectadas en la base de datos</returns>
        public int EstablecerPrecioPlanta(int idPlanta, double precio)
        {
            int filasAfectadas;
            SqlConnection cnn = null;
            try
            {
                SqlCommand miComando = new SqlCommand();
                cnn = miConexion.getConnection();
                miComando.Connection = cnn;
                miComando.Parameters.AddWithValue("@IdPlanta", idPlanta);
                miComando.Parameters.AddWithValue("@precio", precio);
                miComando.CommandText = "Update plantas set precio = @precio Where idPlanta = @IdPlanta";
                filasAfectadas = miComando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cnn != null)
                    miConexion.closeConnection(ref cnn);
            }
            return filasAfectadas;
        }
        public int ModificarCategoriaDePlanta(int idCategoria, int idPlanta)
        {
            int filasAfectadas;
            SqlConnection cnn = null;
            try
            {
                SqlCommand miComando = new SqlCommand();
                cnn = miConexion.getConnection();
                miComando.Connection = cnn;
                miComando.Parameters.AddWithValue("@IdPlanta", idPlanta);
                miComando.Parameters.AddWithValue("@IdCategoria",idCategoria);
                miComando.CommandText = "Update plantas set idCategoria = @IdCategoria Where idPlanta = @IdPlanta";
                filasAfectadas = miComando.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (cnn != null)
                    miConexion.closeConnection(ref cnn);
            }
            return filasAfectadas;
        }
    }


    
}
