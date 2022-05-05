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
        /// bool EstablecerPrecioPlanta(int idPlanta, double precio)
        /// 
        /// Método que se encarga de establecer el atributo precio de un objeto clsPlata 
        /// que coincidirá con la misma tabla de la Base de Datos
        /// a partir de dos parámetros de entrada, "idPlanta" que será
        /// el atributo IdPlanta y "precio" que sera el atributo Precio de un 
        /// objeto clsPlanta. Devolverá un booleano que indicará si se realizío o no
        /// la sentencia update que se envió hacia la BBDD
        /// </summary>
        /// <param name="idPlanta"></param>
        /// <param name="precio"></param>
        /// <returns>bool exito</returns>
        public bool EstablecerPrecioPlanta(int idPlanta, double precio)
        {
            bool exito;
            SqlConnection cnn = null;
            try {
            SqlCommand miComando = new SqlCommand();
            cnn = miConexion.getConnection();
            miComando.Connection = cnn;
            miComando.Parameters.AddWithValue("@IdPlanta", idPlanta);
            miComando.Parameters.AddWithValue("@precio", precio);
            miComando.CommandText = "Update plantas set precio = @precio Where idPlanta = @IdPlanta";
            miComando.ExecuteNonQuery();
            exito = true;
            }
            catch (Exception e)
            {
                exito = false;
            }finally
            {
                if(cnn != null)
                    miConexion.closeConnection(ref cnn);
            }
            return exito;
        }
    }
}
