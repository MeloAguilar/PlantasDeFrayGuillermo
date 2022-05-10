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
        /// List<clsPlanta> RecogerPlantasDeCategoria(int id)
        /// 
        /// Método que se encarga de recoger una lista de plantas de la 
        /// Base de datos. El id de la categoria de esa planta debe coincidir 
        /// con el entero aportado como parámetro
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
                    if (reader.HasRows)
                    {
                        clsPlanta p = new clsPlanta();
                        p.IdCategoria = (int)reader["idCategoria"];
                        p.IdPlanta = (int)reader["idPlanta"];
                        p.NombrePlanta = (string)reader["nombrePlanta"];
                        p.Descripcion = (string)reader["descripcion"];

                        if (reader["precio"] != DBNull.Value)
                        {
                            p.Precio = (double)reader["precio"];
                        }

                        plantas.Add(p);
                    }
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
        /// List<clsCategoria> RecogerListadoCompletoCategorias()
        /// 
        /// Método que se encarga de rescatar una lista de Categorias de plantas de la Base de Datos
        /// 
        /// </summary>
        /// <returns> List clsCategoria</returns>
        public List<clsCategoria> RecogerListadoCompletoCategorias()
        {
            List<clsCategoria> categorias = new List<clsCategoria>();

            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                conn = miConexion.getConnection();
                cmd.Connection = conn;
                
                cmd.CommandText = "Select * From categorias";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        clsCategoria c = new clsCategoria();
                        c.IdCategoria = (int)reader["idCategoria"];
                        c.NombreCategoria = (string)reader["nombreCategoria"];


                        categorias.Add(c);
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //Por asegurarme 
                if(conn != null)    
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
        /// a partir de un entero que debe coincidir con el idPlanta de un objeto clsPlanta
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
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {

                        p.IdCategoria = (int)reader["idCategoria"];
                        p.IdPlanta = (int)reader["idPlanta"];
                        p.NombrePlanta = (string)reader["nombrePlanta"];
                        p.Descripcion = (string)reader["descripcion"];

                        if (reader["precio"] != DBNull.Value && (double)reader["precio"] != 0)
                        {
                            p.Precio = (double)reader["precio"];
                        }
                    }
                }
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
                    if (reader.HasRows)
                    {

                        c.IdCategoria = (int)reader["idCategoria"];
                        c.NombreCategoria = (string)reader["nombreCategoria"];
                    }

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
