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
        public void GuardaMatricula(Matricula mat)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //TODO: Selecciona Curso Lectivo activo
                    using (MySqlCommand cmd = new MySqlCommand("call InsertMatricula("+ mat.Estudiante +", "+ mat.CursoLectivo +
                        ", " + mat.Grupo + ")", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    using (MySqlCommand cmd = new MySqlCommand("call InsertEleccionEspecialidad(" + 
                        matriculaXEstudiante(mat.Estudiante).IdMatricula + ", " + mat.Especialidad1 + ", 1)", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    using (MySqlCommand cmd = new MySqlCommand("call InsertEleccionEspecialidad(" + 
                        matriculaXEstudiante(mat.Estudiante).IdMatricula + ", " + mat.Especialidad2 + ", 2)", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    using (MySqlCommand cmd = new MySqlCommand("call InsertEleccionEspecialidad(" + 
                        matriculaXEstudiante(mat.Estudiante).IdMatricula + ", " + mat.Especialidad3 + ", 3)", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            #region Stored Procedure
            /*
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "InsertMatricula";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("estudiante", MySqlDbType.Int32);
                        cmd.Parameters["estudiante"].Value = mat.Estudiante;
                        cmd.Parameters["estudiante"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("curso_lectivo", MySqlDbType.Int32);
                        cmd.Parameters["curso_lectivo"].Value = mat.CursoLectivo;
                        cmd.Parameters["curso_lectivo"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("grupo", MySqlDbType.Int32);
                        cmd.Parameters["grupo"].Value = mat.Grupo;
                        cmd.Parameters["grupo"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("especialidad1", MySqlDbType.Int32);
                        cmd.Parameters["especialidad1"].Value = mat.Especialidad1;
                        cmd.Parameters["especialidad1"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("especialidad2", MySqlDbType.Int32);
                        cmd.Parameters["especialidad2"].Value = mat.Especialidad2;
                        cmd.Parameters["especialidad2"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("especialidad3", MySqlDbType.Int32);
                        cmd.Parameters["especialidad3"].Value = mat.Especialidad3;
                        cmd.Parameters["especialidad3"].Direction = ParameterDirection.Input;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            */
            #endregion
        }
        #endregion

        #region ActualizarMatricula
        public void ActualizaMatricula(Matricula mat)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //TODO: Selecciona Curso Lectivo activo
                    using (MySqlCommand cmd = new MySqlCommand("call UpdateMatricula("+ mat.IdMatricula +"," + mat.Estudiante + ", " + mat.CursoLectivo +
                        ", " + mat.Grupo + ")", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    using (MySqlCommand cmd = new MySqlCommand("call UpdateEleccionEspecialidad(" + mat.IdMatricula + "," + mat.Especialidad1 + ", 1)", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    using (MySqlCommand cmd = new MySqlCommand("call UpdateEleccionEspecialidad(" + mat.IdMatricula + "," + mat.Especialidad2 + ", 2)", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    using (MySqlCommand cmd = new MySqlCommand("call UpdateEleccionEspecialidad(" + mat.IdMatricula + "," + mat.Especialidad3 + ", 3)", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }

            #region Stored Procedure
            /*try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UpdateMatricula";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pidmatricula_admision", MySqlDbType.Int32);
                        cmd.Parameters["pidmatricula_admision"].Value = mat.IdMatricula;
                        cmd.Parameters["pidmatricula_admision"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pestudiante", MySqlDbType.Int32);
                        cmd.Parameters["pestudiante"].Value = mat.Estudiante;
                        cmd.Parameters["pestudiante"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pcurso_lectivo", MySqlDbType.Int32);
                        cmd.Parameters["pcurso_lectivo"].Value = mat.CursoLectivo;
                        cmd.Parameters["pcurso_lectivo"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pgrupo", MySqlDbType.Int16);
                        cmd.Parameters["pgrupo"].Value = mat.Grupo;
                        cmd.Parameters["pgrupo"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pespecialidad1", MySqlDbType.Int32);
                        cmd.Parameters["pespecialidad1"].Value = mat.Especialidad1;
                        cmd.Parameters["pespecialidad1"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pespecialidad2", MySqlDbType.Int32);
                        cmd.Parameters["pespecialidad2"].Value = mat.Especialidad2;
                        cmd.Parameters["pespecialidad2"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pespecialidad3", MySqlDbType.Int32);
                        cmd.Parameters["pespecialidad3"].Value = mat.Especialidad3;
                        cmd.Parameters["pespecialidad3"].Direction = ParameterDirection.Input;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }*/
            #endregion
        }
        #endregion

        #region BorrarMatricula
        public void BorraMatricula(int id)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "DeleteEleccionEspecialidadXParam";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pmatricula", MySqlDbType.Int32);
                        cmd.Parameters["pmatricula"].Value = id;
                        cmd.Parameters["pmatricula"].Direction = ParameterDirection.Input;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "DeleteMatricula";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pidmatricula", MySqlDbType.Int16);
                        cmd.Parameters["pidmatricula"].Value = id;
                        cmd.Parameters["pidmatricula"].Direction = ParameterDirection.Input;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
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
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

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

        #region IdMatriculaXEstudiante
        public int IdMatriculaXEstudiante(int est)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            try
            {

                #region Procedure
                //using (MySqlConnection conn = new MySqlConnection())
                //{
                //    using (MySqlCommand cmd = new MySqlCommand())
                //    {
                //        conn.ConnectionString = connString;
                //        cmd.Connection = conn;
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.CommandText = "select_student_id";

                //        MySqlParameter pidestudiante = new MySqlParameter();
                //        pidestudiante.ParameterName = "@pidestudiante";
                //        pidestudiante.Direction = ParameterDirection.Input;
                //        pidestudiante.MySqlDbType = MySqlDbType.Int32;
                //        pidestudiante.Value = id;
                //        cmd.Parameters.Add(pidestudiante);

                //        MySqlParameter pcedula = new MySqlParameter();
                //        pcedula.ParameterName = "@pcedula";
                //        pcedula.Direction = ParameterDirection.ReturnValue;
                //        pcedula.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pcedula);

                //        MySqlParameter pnombre = new MySqlParameter();
                //        pnombre.ParameterName = "@pnombre";
                //        pnombre.Direction = ParameterDirection.ReturnValue;
                //        pnombre.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pnombre);

                //        MySqlParameter papellido1 = new MySqlParameter();
                //        papellido1.ParameterName = "@papellido1";
                //        papellido1.Direction = ParameterDirection.ReturnValue;
                //        papellido1.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(papellido1);

                //        MySqlParameter papellido2 = new MySqlParameter();
                //        papellido2.ParameterName = "@papellido2";
                //        papellido2.Direction = ParameterDirection.ReturnValue;
                //        papellido2.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(papellido2);

                //        MySqlParameter pdireccion = new MySqlParameter();
                //        pdireccion.ParameterName = "@pdireccion";
                //        pdireccion.Direction = ParameterDirection.ReturnValue;
                //        pdireccion.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pdireccion);

                //        MySqlParameter ptelefono = new MySqlParameter();
                //        ptelefono.ParameterName = "@ptelefono";
                //        ptelefono.Direction = ParameterDirection.ReturnValue;
                //        ptelefono.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(ptelefono);

                //        MySqlParameter pcelular = new MySqlParameter();
                //        pcelular.ParameterName = "@pcelular";
                //        pcelular.Direction = ParameterDirection.ReturnValue;
                //        pcelular.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pcelular);

                //        MySqlParameter pemail = new MySqlParameter();
                //        pemail.ParameterName = "@pemail";
                //        pemail.Direction = ParameterDirection.ReturnValue;
                //        pemail.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pemail);

                //        MySqlParameter pctpp = new MySqlParameter();
                //        pctpp.ParameterName = "@pctpp";
                //        pctpp.Direction = ParameterDirection.ReturnValue;
                //        pctpp.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pctpp);

                //        conn.Open();
                //        cmd.ExecuteNonQuery();

                //        est.Cedula = pcedula.Value.ToString();
                //        est.Nombre = pnombre.Value.ToString();
                //        est.Apellido1 = papellido1.Value.ToString();
                //        est.Apellido2 = papellido2.Value.ToString();
                //        est.Direccion = pdireccion.Value.ToString();
                //        est.Telefono = ptelefono.Value.ToString();
                //        est.Celular = pcelular.Value.ToString();
                //        est.Email = pemail.Value.ToString();
                //        est.Ctpp = int.Parse(pctpp.Value.ToString());
                //    }
                //}
                //return est;
                #endregion

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM matriculas_admision WHERE estudiante=" + est + ";", conn))
                    {
                        conn.Open();
                        MySqlDataReader dr = cmd.ExecuteReader();

                        if (dr.FieldCount > 0)
                        {
                            while (dr.Read())
                            {
                                return dr.GetInt32(0);
                            }
                            dr.Close();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return 0;
        }
        #endregion

        #region SeleccionaMatricula por estudiante
        public Matricula matriculaXEstudiante(int id)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            Matricula mat = new Matricula();

            try
            {

                #region Procedure
                //using (MySqlConnection conn = new MySqlConnection())
                //{
                //    using (MySqlCommand cmd = new MySqlCommand())
                //    {
                //        conn.ConnectionString = connString;
                //        cmd.Connection = conn;
                //        cmd.CommandType = CommandType.StoredProcedure;
                //        cmd.CommandText = "select_student_id";

                //        MySqlParameter pidestudiante = new MySqlParameter();
                //        pidestudiante.ParameterName = "@pidestudiante";
                //        pidestudiante.Direction = ParameterDirection.Input;
                //        pidestudiante.MySqlDbType = MySqlDbType.Int32;
                //        pidestudiante.Value = id;
                //        cmd.Parameters.Add(pidestudiante);

                //        MySqlParameter pcedula = new MySqlParameter();
                //        pcedula.ParameterName = "@pcedula";
                //        pcedula.Direction = ParameterDirection.ReturnValue;
                //        pcedula.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pcedula);

                //        MySqlParameter pnombre = new MySqlParameter();
                //        pnombre.ParameterName = "@pnombre";
                //        pnombre.Direction = ParameterDirection.ReturnValue;
                //        pnombre.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pnombre);

                //        MySqlParameter papellido1 = new MySqlParameter();
                //        papellido1.ParameterName = "@papellido1";
                //        papellido1.Direction = ParameterDirection.ReturnValue;
                //        papellido1.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(papellido1);

                //        MySqlParameter papellido2 = new MySqlParameter();
                //        papellido2.ParameterName = "@papellido2";
                //        papellido2.Direction = ParameterDirection.ReturnValue;
                //        papellido2.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(papellido2);

                //        MySqlParameter pdireccion = new MySqlParameter();
                //        pdireccion.ParameterName = "@pdireccion";
                //        pdireccion.Direction = ParameterDirection.ReturnValue;
                //        pdireccion.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pdireccion);

                //        MySqlParameter ptelefono = new MySqlParameter();
                //        ptelefono.ParameterName = "@ptelefono";
                //        ptelefono.Direction = ParameterDirection.ReturnValue;
                //        ptelefono.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(ptelefono);

                //        MySqlParameter pcelular = new MySqlParameter();
                //        pcelular.ParameterName = "@pcelular";
                //        pcelular.Direction = ParameterDirection.ReturnValue;
                //        pcelular.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pcelular);

                //        MySqlParameter pemail = new MySqlParameter();
                //        pemail.ParameterName = "@pemail";
                //        pemail.Direction = ParameterDirection.ReturnValue;
                //        pemail.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pemail);

                //        MySqlParameter pctpp = new MySqlParameter();
                //        pctpp.ParameterName = "@pctpp";
                //        pctpp.Direction = ParameterDirection.ReturnValue;
                //        pctpp.MySqlDbType = MySqlDbType.VarChar;
                //        cmd.Parameters.Add(pctpp);

                //        conn.Open();
                //        cmd.ExecuteNonQuery();

                //        est.Cedula = pcedula.Value.ToString();
                //        est.Nombre = pnombre.Value.ToString();
                //        est.Apellido1 = papellido1.Value.ToString();
                //        est.Apellido2 = papellido2.Value.ToString();
                //        est.Direccion = pdireccion.Value.ToString();
                //        est.Telefono = ptelefono.Value.ToString();
                //        est.Celular = pcelular.Value.ToString();
                //        est.Email = pemail.Value.ToString();
                //        est.Ctpp = int.Parse(pctpp.Value.ToString());
                //    }
                //}
                //return est;
                #endregion

                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM matriculas_admision WHERE estudiante=" + id + ";", conn))
                    {
                        conn.Open();
                        MySqlDataReader dr = cmd.ExecuteReader();

                        if (dr.FieldCount > 0)
                        {
                            while (dr.Read())
                            {
                                mat.IdMatricula = dr.GetInt32(0);
                                mat.Estudiante = dr.GetInt32(1);
                                mat.CursoLectivo = dr.GetInt32(2);
                                mat.Grupo = dr.GetInt32(3);
                            }
                            dr.Close();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mat;
        }
        #endregion

        #region SeleccionaMatriculasXCursoLectivo
        public List<Matricula> MatriculasXCursoLectivo(int curso)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            List<Matricula> matriculas = new List<Matricula>();

            //TODO: Recuperar las matriculas por el curso lectivo
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SeleccionarMatriculasXCursoLect";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pcurso_lectivo", MySqlDbType.Int32);
                        cmd.Parameters["pcurso_lectivo"].Value = curso;
                        cmd.Parameters["pcurso_lectivo"].Direction = ParameterDirection.Input;

                        conn.Open();
                        MySqlDataReader dr = cmd.ExecuteReader();
                        if (dr.FieldCount > 0)
                        {
                            while (dr.Read())
                            {
                                Matricula mat = new Matricula();
                                mat.IdMatricula = dr.GetInt32(0);
                                mat.Estudiante = dr.GetInt32(1);
                                mat.Cedula = dr.GetString(2);

                                matriculas.Add(mat);
                            }
                            dr.Close();
                        }
                    }
                }
                return matriculas;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Cantidad matriculas de Curso Activo
        public int qtyMatsXCursoActivo()
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            int matriculas = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //TODO: Selecciona Curso Lectivo activo
                    using (MySqlCommand cmd = new MySqlCommand("SELECT qty FROM qty_mats_curso_act;", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        matriculas = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return matriculas;
        }
        #endregion

        #region Cedula por matricula
        public string CedulaXMatricula(int mat)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand("CALL cedula_x_matricula(" + mat + ");", conn))
                    {
                        conn.Open();
                        MySqlDataReader dr = cmd.ExecuteReader();

                        if (dr.FieldCount > 0)
                        {
                            while (dr.Read())
                            {
                                return dr.GetString(0);
                            }
                            dr.Close();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }
        #endregion

        #region ActualizarEspecialidadX
        public void ActualizaEspecialidadX(Matricula mat)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UpdateEspecialidadX";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("pidmatricula_admision", MySqlDbType.Int32);
                        cmd.Parameters["pidmatricula_admision"].Value = mat.IdMatricula;
                        cmd.Parameters["pidmatricula_admision"].Direction = ParameterDirection.Input;
                        cmd.Parameters.Add("pespe", MySqlDbType.Int32);
                        cmd.Parameters["pespe"].Value = mat.Especialidadx;
                        cmd.Parameters["pespe"].Direction = ParameterDirection.Input;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Cantidad matriculas por Especialidad X
        public int qtyMatsXEspecialidadX(int espe)
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            int matriculas = 0;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //TODO: Selecciona Curso Lectivo activo
                    using (MySqlCommand cmd = new MySqlCommand("CALL cantidad_x_especialidad("+ espe +");", conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        matriculas = Convert.ToInt32(cmd.ExecuteScalar());
                        conn.Close();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return matriculas;
        }
        #endregion
    }
}
