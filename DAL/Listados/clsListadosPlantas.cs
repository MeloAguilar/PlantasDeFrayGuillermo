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
        /// List<clsPlanta> RecogerListadoCompletoPlantas()
        /// 
        /// Método que se encarga de traer una lista de las plantas 
        /// que se encuentran en la Base de Datos FrayGuillermo
        /// </summary>
        /// <returns>List clsPlanta</returns>
        public List<clsPlanta> RecogerListadoCompletoPlantas()
        {
            List<clsPlanta> plantas = new List<clsPlanta>();
             
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

                SqlConnection conn = miConexion.getConnection();
                cmd.Connection = conn;
                cmd.CommandText = "Select * From plantas";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        clsPlanta planta = new clsPlanta();
                        planta.IdCategoria = (int)reader["idCategoria"];
                        planta.IdPlanta = (int)reader["idPlanta"];
                        planta.NombrePlanta = (string)reader["nombrePlanta"];
                        planta.Descripcion = (string)reader["descripcion"];
                    if (reader["precio"] != DBNull.Value)
                    {
                        planta.Precio = (double)reader["precio"];
                    }
                       

                        plantas.Add(planta);
                    }
                }
           
            return plantas;
        }


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
            SqlConnection conn = miConexion.getConnection();
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
            SqlConnection conn = miConexion.getConnection();
            cmd.Connection = conn;
            SqlDataReader reader;
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
            SqlDataReader reader;
            SqlConnection conn = miConexion.getConnection();
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
            SqlConnection conn = miConexion.getConnection();
            cmd.Connection = conn;
            SqlDataReader reader;
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
            return c;
        }
    }
}
