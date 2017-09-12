﻿using Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace DataLayer
{
    public class MatriculaData : Matricula
    {
        #region GuardarMatricula
        public void GuardaMatricula()
        {
            string connString = ConfigurationManager.AppSettings["connString"];

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "InsertMatricula";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("estudiante", MySqlDbType.Int16);
                        cmd.Parameters["estudiante"].Value = Estudiante;
                        cmd.Parameters["estudiante"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("curso_lectivo", MySqlDbType.Int16);
                        cmd.Parameters["curso_lectivo"].Value = CursoLectivo;
                        cmd.Parameters["curso_lectivo"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("especialidad1", MySqlDbType.Int16);
                        cmd.Parameters["especialidad1"].Value = Especialidad1;
                        cmd.Parameters["especialidad1"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("especialidad2", MySqlDbType.Int16);
                        cmd.Parameters["especialidad2"].Value = Especialidad2;
                        cmd.Parameters["especialidad2"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("especialidad3", MySqlDbType.Int16);
                        cmd.Parameters["especialidad3"].Value = Especialidad3;
                        cmd.Parameters["especialidad3"].Direction = ParameterDirection.Input;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ActualizarEstudiante
        public void ActualizaEstudiante()
        {
            string connString = ConfigurationManager.AppSettings["connString"];

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UpdateMatricula";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pidmatricula_admision", MySqlDbType.Int16);
                        cmd.Parameters["pidmatricula_admision"].Value = IdMatricula;
                        cmd.Parameters["pidmatricula_admision"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pestudiante", MySqlDbType.Int16);
                        cmd.Parameters["pestudiante"].Value = Estudiante;
                        cmd.Parameters["pestudiante"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pcurso_lectivo", MySqlDbType.Int16);
                        cmd.Parameters["pcurso_lectivo"].Value = CursoLectivo;
                        cmd.Parameters["pcurso_lectivo"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pespecialidad1", MySqlDbType.Int16);
                        cmd.Parameters["pespecialidad1"].Value = Especialidad1;
                        cmd.Parameters["pespecialidad1"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pespecialidad2", MySqlDbType.Int16);
                        cmd.Parameters["pespecialidad2"].Value = Especialidad2;
                        cmd.Parameters["pespecialidad2"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pespecialidad3", MySqlDbType.Int16);
                        cmd.Parameters["pespecialidad3"].Value = Especialidad3;
                        cmd.Parameters["pespecialidad3"].Direction = ParameterDirection.Input;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region BorrarMatricula
        public void BorraMatricula()
        {
            string connString = ConfigurationManager.AppSettings["connString"];

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "DeleteMatricula";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("idmatricula_admision", MySqlDbType.Int16);
                        cmd.Parameters["idmatricula_admision"].Value = IdMatricula;
                        cmd.Parameters["idmatricula_admision"].Direction = ParameterDirection.Input;

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ListMatricula
        public List<Matricula> ListMatricula()
        {
            string connString = ConfigurationManager.AppSettings["connString"];
            List<Matricula> lista = new List<Matricula>(); ;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT matriculas_all;", conn))
                    {
                        MySqlDataReader dr = cmd.ExecuteReader();

                        if (dr.FieldCount > 0)
                        {
                            Matricula mat = new Matricula();

                            while (dr.Read())
                            {
                                for (int h = 0; h < dr.FieldCount; h++)
                                {
                                    mat.IdMatricula = int.Parse(dr[0].ToString());
                                    mat.Estudiante = int.Parse(dr[1].ToString());
                                    mat.CursoLectivo = int.Parse(dr[2].ToString());
                                    mat.Especialidad1 = int.Parse(dr[3].ToString());
                                    mat.Especialidad2 = int.Parse(dr[4].ToString());
                                    mat.Especialidad3 = int.Parse(dr[5].ToString());

                                    lista.Add(mat);
                                }
                            }
                            dr.Close();
                        }
                    }
                }
                return lista;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
