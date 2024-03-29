﻿using DataLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace BussinesLayer
{
    public class EspecialidadBussines
    {
        public List<Especialidad> listar()
        {
            try
            {
                EspecialidadData espe = new EspecialidadData();
                return espe.ListEspecialidad();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Especialidad especialidadXMatYPrioridad(int mat, int prior)
        {
            try
            {
                return new EspecialidadData().especialidadXMatYPrior(mat, prior);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable listarEspecXEstud(string path, string name)
        {
            try
            {
                return new EspecialidadData().listEspecXEstud(path, name);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable listarEstudXEspec(string path, string name)
        {
            DataTable dt = new DataTable();
            string directorio = "\\Estudiantes_por_especialidad";
            string path0 = path;

            try
            {
                if (!Directory.Exists(string.Format("{0}{1}", path, directorio)))
                {
                    Directory.CreateDirectory(string.Format("{0}{1}", path, directorio));
                }
                path = string.Format("{0}{1}", path, directorio);

                foreach (Especialidad espe in new EspecialidadData().ListEspecialidad())
                {
                    path = string.Format("{0}\\{1}",path,(string.Format("{0}{1}",espe.Nombre,".xlsx")));
                    dt.Merge(new EspecialidadData().listEstudXEspecialidad(espe.idEspecialidad, path, name));
                    path = string.Format("{0}{1}", path0, directorio);
                }
                return dt;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable listarIncosistencias(string path, string name)
        {
            try
            {
                return new EspecialidadData().listErroneas(path, name);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

