﻿// Author - Anshu Dutta
// Contact - anshu.dutta@gmail.com
using System;
using Company;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using General;

namespace DAL
{
    public class TranscripcionDAL
    {
        private SqlConnection conn;
        private static string connString;
        private SqlCommand command;       
        private ErrorHandler.ErrorHandler err;

        public TranscripcionDAL(string _connString)
        {
            err = new ErrorHandler.ErrorHandler();
            connString = _connString;            
        }
        /// <summary>
        /// Database INSERT Transcripcion
        /// </summary>
        /// <param name="transcripcion"></param>
        public int Insert(Transcripcion transcripcion)
        {
            try
            {
                string sqlInserString = "INSERT INTO Transcripcion (Login, Estado, NombreFichero, Fichero, FechaRecepcion, FechaTranscripcion, TextoTranscripcion) VALUES (@Login, @Estado, @NombreFichero, @Fichero, @FechaRecepcion, @FechaTranscripcion, @TextoTranscripcion); "
                    + " SELECT SCOPE_IDENTITY();";

                //transcripcion.Fichero = new byte[] { 55, 56, 57 };
                conn = new SqlConnection(connString);
                command = new SqlCommand(sqlInserString, conn);
                command.Parameters.AddWithValue("Login", transcripcion.Login);
                command.Parameters.AddWithValue("Estado", transcripcion.Estado);
                command.Parameters.AddWithValue("NombreFichero", (object)transcripcion.NombreFichero ?? DBNull.Value);
                SqlParameter fichParam = command.Parameters.AddWithValue("Fichero", (object)transcripcion.Fichero ?? DBNull.Value);
                fichParam.DbType = DbType.Binary;

                //SqlParameter fichParam = command.Parameters.AddWithValue("Fichero", (object)transcripcion.Fichero ?? DBNull.Value);
                //fichParam.DbType = DbType.Binary;
                //command.Parameters.Add(new SqlParameter("@Fichero", SqlDbType.VarBinary, -1).Value = (object)transcripcion.Fichero ?? DBNull.Value);
                //if (transcripcion.Fichero == null)
                //{ command.Parameters.Add(new SqlParameter("@Fichero", DBNull.Value)); }
                //else
                //{ command.Parameters.Add(new SqlParameter("@Fichero", transcripcion.Fichero)); }


                //command.Parameters.Add("@Fichero", SqlDbType.VarBinary, -1).Value = (object)transcripcion.Fichero ?? DBNull.Value;

                //SqlParameter fichParam = new SqlParameter("@Fichero", SqlDbType.VarBinary, -1);
                //fichParam.Value = (object)transcripcion.Fichero ?? DBNull.Value;
                ////if (transcripcion.Fichero != null) { fichParam.Value = transcripcion.Fichero; }
                //command.Parameters.Add(fichParam);
                //if (transcripcion.Fichero == null)
                //{ command.Parameters.Add(new SqlParameter("@Fichero", DBNull.Value)); }
                //else
                //{ command.Parameters.Add(new SqlParameter("@Fichero", transcripcion.Fichero)); }
                command.Parameters.AddWithValue("FechaRecepcion", transcripcion.FechaRecepcion);
                command.Parameters.AddWithValue("FechaTranscripcion", (object)transcripcion.FechaTranscripcion ?? DBNull.Value);
                command.Parameters.AddWithValue("TextoTranscripcion", (object)transcripcion.TextoTranscripcion ?? DBNull.Value);

                command.Connection.Open();
                int id = Numeros.ToInt(command.ExecuteScalar());
                command.Connection.Close();
                return id;
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }
        /// <summary>
        /// Database UPDATE Transcripcion
        /// </summary>
        /// <param name="transcripcion"></param>
        public void Update(Transcripcion transcripcion)
        {
            try
            {
                string sqlUpdateString = "UPDATE Transcripcion SET Login = @Login, Estado = @Estado, NombreFichero = @NombreFichero, Fichero = @Fichero, FechaRecepcion = @FechaRecepcion, FechaTranscripcion = @FechaTranscripcion, TextoTranscripcion = @TextoTranscripcion WHERE IdTranscripcion = @IdTranscripcion;";

                conn = new SqlConnection(connString);
                command = new SqlCommand(sqlUpdateString, conn);
                command.Parameters.Add(new SqlParameter("@IdTranscripcion", transcripcion.IdTranscripcion));
                command.Parameters.Add(new SqlParameter("@Login", transcripcion.Login));
                command.Parameters.Add(new SqlParameter("@Estado", transcripcion.Estado));
                command.Parameters.Add(new SqlParameter("@NombreFichero", transcripcion.NombreFichero));
                command.Parameters.Add(new SqlParameter("@Fichero", transcripcion.Fichero));
                command.Parameters.Add(new SqlParameter("@FechaRecepcion", transcripcion.FechaRecepcion));
                command.Parameters.Add(new SqlParameter("@FechaTranscripcion", (object)transcripcion.FechaRecepcion ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TextoTranscripcion", transcripcion.Login));
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }
        /// <summary>
        /// Database DELETE Transcripcion
        /// </summary>
        /// <param name="idTranscripcion"></param>
        public void Delete(int idTranscripcion)
        {
            try
            {
                string sqlDeleteString = "DELETE FROM Transcripcion WHERE IdTranscripcion = @IdTranscripcion;";

                conn = new SqlConnection(connString);
                command = new SqlCommand(sqlDeleteString, conn);
                command.Parameters.Add(new SqlParameter("@IdTranscripcion", idTranscripcion));
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }
        /// <summary>
        /// Database SELECT Transcripcion
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Transcripcion Select(int idTranscripcion)
        {
            try
            {
                Transcripcion transcripcion = null;
                string sqlSelectString = "SELECT IdTranscripcion, Login, Estado, NombreFichero, Fichero, FechaRecepcion, FechaTranscripcion, TextoTranscripcion FROM Transcripcion WHERE IdTranscripcion = @IdTranscripcion;";

                conn = new SqlConnection(connString);
                command = new SqlCommand(sqlSelectString, conn);
                command.Parameters.Add(new SqlParameter("@IdTranscripcion", idTranscripcion));
                command.Connection.Open();

                SqlDataReader dataReader = command.ExecuteReader();
                DataTable dt = new DataTable("Table1");
                if (dataReader != null) { dt.Load(dataReader); }
                command.Connection.Close();
                if (dt.Rows.Count>0)
                {
                    DataRow dr = dt.Rows[0];
                    transcripcion = new Transcripcion();
                    transcripcion.IdTranscripcion = Numeros.ToInt(dr["IdTranscripcion"]);
                    transcripcion.Login = dr["Login"].ToString();
                    transcripcion.Fichero = (byte[])dr["Fichero"];

                }

                return transcripcion;
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }
        /// <summary>
        /// Database SELECT with filters Transcripcion
        /// </summary>
        /// <returns>Transcripcion</returns>
        private List<Transcripcion> SelectFilters(string login, DateTime? desdeFechaRecepcion, DateTime? hastaFechaRecepcion)
        {
            try
            {
                List<Transcripcion> lista = new List<Transcripcion>();

                conn = new SqlConnection(connString);

                string sqlSelectString = "SELECT IdTranscripcion, Login, Estado, NombreFichero, Fichero, FechaRecepcion, FechaTranscripcion, TextoTranscripcion FROM Transcripcion WHERE" +
                    " Login = @Login AND" +
                    " (@DesdeFechaRecepcion IS NULL OR FechaRecepcion >= @DesdeFechaRecepcion)" +
                    " (@HastaFechaRecepcion IS NULL OR FechaRecepcion >= @HastaFechaRecepcion)";
                command = new SqlCommand(sqlSelectString, conn);
                command.Parameters.Add(new SqlParameter("@Login", login));
                command.Parameters.Add(new SqlParameter("@DesdeFechaRecepcion", (object)desdeFechaRecepcion?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@HastaFechaRecepcion", (object)hastaFechaRecepcion ?? DBNull.Value));
                command.Connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                //while (reader.Read())
                //{
                //    Employee emp = new Employee();
                //    emp.FirstName = reader[0].ToString();
                //    emp.LastName = reader[1].ToString();
                //    emp.EmpCode = Convert.ToInt16(reader[2]);
                //    emp.Designation = reader[3].ToString();
                //    empList.Add(emp);
                //}
                //command.Connection.Close();

                return lista;
            }
            catch (Exception ex)
            {
                err.ErrorMessage = ex.Message.ToString();
                throw;
            }
        }
        /// <summary>
        /// Get Exception if any
        /// </summary>
        /// <returns> Error Message</returns>
        public string GetException()
        {
            return err.ErrorMessage.ToString();
        }
    }
}
