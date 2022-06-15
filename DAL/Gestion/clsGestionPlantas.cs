using DAL.Conexion;
using Entities;
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
                miComando.Parameters.AddWithValue("@IdCategoria", idCategoria);
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



        /// <header>int crearPlanta(clsPlanta planta)</header>
        /// 
        /// <summary>
        /// Procedimietno que se encarga de insertar una planta nueva en la base de datos FrayGuillermo
        /// </summary>
        /// 
        /// <pre>
        /// ninguno de los atributos del objeto clsPlanta puede ser null o menor a 0 en el caso del precio
        /// </pre>
        /// 
        /// <post>
        /// Devolverá la cantidad de filas afectadas. 1 -> en caso de que se inserte la planta, 0-> En caso de que exista algún fallo en la base de datos al insertar la planta
        /// </post>
        /// <param name="planta"></param>
        /// <returns></returns>
        public int CrearPlanta(clsPlanta planta)
        {
            int filasAfectadas;
            SqlConnection cnn = null;

            try
            {
                SqlCommand miComando = new SqlCommand();
                cnn = miConexion.getConnection();
                miComando.Connection = cnn;
                miComando.Parameters.AddWithValue("@nombrePlanta", planta.NombrePlanta);
                miComando.Parameters.AddWithValue("@idCategoria", planta.IdCategoria);
                miComando.Parameters.AddWithValue("@precio", planta.Precio);
                miComando.Parameters.AddWithValue("@descripcion", planta.Descripcion);
                miComando.CommandText = "Insert into plantas( nombrePlanta, idCategoria, precio, descripcion) Values @nombrePlanta, @idCategoria, @precio, @descripcion";
                filasAfectadas = miComando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }



            return filasAfectadas;
        }
    }



}
