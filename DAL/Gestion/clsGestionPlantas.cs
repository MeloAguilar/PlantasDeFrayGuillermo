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


        public bool EstablecerPrecioPlanta(int idPlanta, double precio)
        {
            bool exito;
            try {
            SqlCommand miComando = new SqlCommand();
            SqlConnection cnn = miConexion.getConnection();
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
            }
            return exito;
        }
    }
}
