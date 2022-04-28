using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Conexion
{
    public class clsMyConnection
    {
        //Atributos
        public String server { get; set; }
        public String dataBase { get; set; }
        public String user { get; set; }
        public String pass { get; set; }

        //Constructores

        public clsMyConnection()
        {
            //LAPTOP-L9L59DR6
            this.server = "LAPTOP-L9L59DR6";
            
            this.dataBase = "FrayGuillermo";
            
            this.user = "prueba";
            this.pass = "1234";

        }
        //Con parámetros por si quisiera cambiar las conexiones
        public clsMyConnection(String server, String database, String user, String pass)
        {
            this.server = server;
            this.dataBase = database;
            this.user = user;
            this.pass = pass;
        }


        //METODOS

        /// <summary>
        /// Método que abre una conexión con la base de datos
        /// </summary>
        /// <pre>Nada.</pre>
        /// <returns>Una conexión abierta con la base de datos</returns>
        public SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection();

            try
            {

                connection.ConnectionString = $"server={server};database={dataBase};uid={user};pwd={pass};";
                connection.Open();
            }
            catch (SqlException)
            {
                throw;
            }

            return connection;

        }




        /// <summary>
        /// Este metodo cierra una conexión con la Base de datos
        /// </summary>
        /// <post>La conexion es cerrada</post>
        /// <param name="connection">SqlConnection pr referencia. Conexion a cerrar
        /// </param>
        public void closeConnection(ref SqlConnection connection)
        {
            try
            {
                connection.Close();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
