using DAL.Conexion;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Listados
{


    public class clsListadosPlantas
    {
        clsMyConnection miConexion = new clsMyConnection();


        /// <summary>
        ///  <header>clsCategoria GenerarCategoria(SqlDataReader reader)</header>
        ///  Método que se encarga de recoger los datos contenidos en
        ///  un SqlDataReader, transformarlos en variables que concuerden con
        ///  los atributos de la clase clsCategoria.
        ///  <preconditions>
        ///  reader debe contener todos los atributos de un objeto
        ///  clsCategoria que se encuentre en la base de datos FrayGuillermo
        ///  </preconditions>
        ///  <postconditions>
        ///  el método siempre devolverá un objeto clsCategoria que concuerde
        ///  con un registro de la tabla plantas de la base de datos FrayGuillermo
        ///  </postconditions>
        /// </summary>
        /// <param name="reader"></param>
        /// <return>clsCategoria: </return>
        private static clsCategoria GenerarCategoria(SqlDataReader reader)
        {

            clsCategoria c = new clsCategoria();
            try
            {
                if (reader.HasRows)
                {
                    c.IdCategoria = (int)reader["idCategoria"];
                    if (reader["nombreCategoria"] != DBNull.Value)
                    c.NombreCategoria = (string)reader["nombreCategoria"];

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return c;
        }



        /// <summary>
        ///  <header>clsPlanta GenerarPlanta(SqlDataReader reader)</header>
        ///  Método que se encarga de recoger los datos contenidos en
        ///  un SqlDataReader, transformarlos en variables que concuerden con
        ///  los atributos de la clase clsPlanta.
        ///  <preconditions>
        ///  reader debe contener todos los atributos de un objeto
        ///  clsPlanta que se encuentre en la base de datos FrayGuillermo,
        ///  por lo tanto tampoco puede ser null.
        ///  </preconditions>
        ///  <postconditions>
        ///  el método siempre devolverá un objeto clsPlanta que concuerde
        ///  con un registro de la tabla plantas de la base de datos FrayGuillermo
        ///  </postconditions>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns name="p">clsPlanta con atributos provinientes de la BBDD FRayGuillermo</returns>
        private static clsPlanta GenerarPlanta(SqlDataReader reader)
        {
            clsPlanta p = new clsPlanta();

            try
            {
                if (reader.HasRows)
                {

                    p.IdCategoria = (int)reader["idCategoria"];
                    p.IdPlanta = (int)reader["idPlanta"];
                    p.NombrePlanta = (string)reader["nombrePlanta"];
                    p.Descripcion = (string)reader["descripcion"];

                    if (reader["precio"] != DBNull.Value)
                    {
                        p.Precio = (double)reader["precio"];
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return p;
        }


        /// <summary>
        /// List<clsPlanta> RecogerPlantasDeCategoria(int id)
        /// 
        /// Método que se encarga de recoger una lista de plantas de la 
        /// Base de datos FrayGuillermo.
        /// <preconditions>
        /// id debe concordar con el idCategoria de una de las filas 
        /// de la tabla categorias de la BBDD FrayGuillermo
        /// </preconditions>
        /// <postconditions>
        /// Siempre devolverá un listado con las plantas cuyo idCategoria sea 
        /// igual al id introducido
        /// </postconditions>
        /// </summary>
        /// <param name="id"> id de un objeto clsCategoria</param>
        /// <returns>List clsPlanta</returns>
        public List<clsPlanta> RecogerPlantasDeCategoria(int id)
        {
            List<clsPlanta> plantas = new List<clsPlanta>();

            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;


            SqlConnection conn = null;
            try
            {
                conn = miConexion.getConnection();
                cmd.Connection= conn;
                cmd.Parameters.AddWithValue("@idCategoria", id);
                cmd.CommandText = "Select * From plantas Where idCategoria = @idCategoria ";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    plantas.Add(GenerarPlanta(reader));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //VS me ha obligao
                if (conn != null)
                    miConexion.closeConnection(ref conn);
            }

            return plantas;
        }


        /// <summary>
        /// <header>
        /// List-clsPlanta RecogerListadoCompletoPlantas()
        /// </header>
        /// Metodo encargado de generar una lista de objetos clsPlanta
        /// a partir de una llamada a la base de datos FrayGuillermo
        /// <preconditions>
        /// Ninguna
        /// </preconditions>
        /// <postconditions>
        /// Devuelve un listado de plantas que concuerdan con la tabla plantas de la 
        /// base de datos FrayGuillermo
        /// </postconditions>
        /// </summary>
        /// <returns></returns>
        public List<clsPlanta> RecogerListadoCompletoPlantas()
        {
            List<clsPlanta> plantas = new List<clsPlanta>();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            SqlDataReader reader = null;

            try
            {
                conn = miConexion.getConnection();

                cmd.Connection = conn;

                cmd.CommandText = "Select * From plantas";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    plantas.Add(GenerarPlanta(reader));

                }
                
            }
            catch(Exception e)
            {
                throw e;
            }
            return plantas;
    }

    /// <summary>
    /// List-clsCategoria RecogerListadoCompletoCategorias()
    /// 
    /// Método que se encarga de rescatar una lista de Categorias de plantas de la Base de Datos
    /// 
    /// <preconditions>
    /// Ninguna
    /// </preconditions>
    /// <postconditions>
    /// En caso de que la base de datos no falle,
    /// devolvera el listado completo de categorias 
    /// que se encuentran en la base de datos FrayGuillermo
    /// </postconditions>
    /// </summary>
    /// <returns name="categorias"> List clsCategoria</returns>
    public List<clsCategoria> RecogerListadoCompletoCategorias()
    {
        List<clsCategoria> categorias = new List<clsCategoria>();

        SqlCommand cmd = new SqlCommand();
        SqlConnection conn = new SqlConnection();
        SqlDataReader reader = null;
        try
        {
            conn = miConexion.getConnection();
            cmd.Connection = conn;

            cmd.CommandText = "Select * From categorias";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categorias.Add(GenerarCategoria(reader));

            }
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            //Por asegurarme 
            if (conn != null)
                miConexion.closeConnection(ref conn);
            if (reader != null)
                reader.Close();
        }


        return categorias;
    }


    /// <summary>
    /// clsPlanta RecogerPlanta(int id)
    /// 
    /// Método encargado de extraer un objeto clsPlanta de una base de datos
    /// a partir de un enter
    /// <preconditions>
    /// id debe coincidi
    /// </preconditions>
    /// <postconditions>
    /// 
    /// </postconditions>
    /// </summary>
    /// <param name="id">id de un objeto clsPlanta</param>
    /// <returns>clsPlanta p</returns>
    public clsPlanta RecogerPlanta(int id)
    {
        clsPlanta p = new clsPlanta();
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader = null;
        SqlConnection conn = null;
        try
        {
            conn = miConexion.getConnection();
            cmd.Connection= conn;
            cmd.Parameters.AddWithValue("@idPlanta", id);
            cmd.CommandText = "Select * From plantas Where idPlanta = @idPlanta ";
            reader = cmd.ExecuteReader();
            while(reader.Read())
            p = GenerarPlanta(reader);
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (reader != null)
                reader.Close();

            if (conn != null)
                miConexion.closeConnection(ref conn);
        }



        return p;
    }

    /// <summary>
    /// clsCategoria RecogerCategoria(int id)
    /// 
    /// Método que se encarga de rescatar un objeto de tipo 
    /// clsCategoria de la Base de Datos
    /// </summary>
    /// <param name="id"> id de un objeto clsCategoria</param>
    /// <returns>clsCategoria c</returns>
    public clsCategoria RecogerCategoria(int id)
    {
        SqlCommand cmd = new SqlCommand();
        clsCategoria c = new clsCategoria();
        SqlConnection conn = null;
        SqlDataReader reader = null;
        try
        {
            conn = miConexion.getConnection();
            cmd.Connection = conn;

            cmd.Parameters.AddWithValue("@idCategoria", id);
            cmd.CommandText = "Select * From categorias Where idCategoria = @idCategoria";
            reader = cmd.ExecuteReader();
                while (reader.Read()) 
                {
                    c = GenerarCategoria(reader); 
                }
            
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (conn != null)
                conn.Close();
            if (reader != null)
                reader.Close();
        }

        return c;
    }
}
}
