﻿using BussinesLayer;
using System;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class MainForm : Form
    {
        //TODO: Jalar notas de base de datos de piad
        //TODO: Filtrar estudiantes y notas por sección en el MainForm
        //TODO: Crear vista "notas_trendimiento"
        //TODO: Cambio de "idasignatura" de Taller1 y Taller2 a 312 y 313 respectivamente
        //TODO: Cambio de id a talleres en vistas "notas_curso_activo8" y "notas_curso_activo9"
        public MainForm()
        {
            InitializeComponent();
        }

        #region TabEstudiantes
        private void tbPgEstudiantes_Enter(object sender, EventArgs e)
        {
            llenarEstudianteDatosDtGrdVw();
        }

        private void llenarEstudianteDatosDtGrdVw()
        {
            EstudiantesBussines est = new EstudiantesBussines();
            dtGrdVwEstudiantes.DataSource = est.listar();
            formateaDTEstudiantes();
        }

        private void formateaDTEstudiantes()
        {
            dtGrdVwEstudiantes.Columns["IdEstudiante"].HeaderText = "Id";
            dtGrdVwEstudiantes.Columns["Cedula"].HeaderText = "Cédula";
            dtGrdVwEstudiantes.Columns["Nombre"].HeaderText = "Nombre";
            dtGrdVwEstudiantes.Columns["Apellido1"].HeaderText = "Primer apellido";
            dtGrdVwEstudiantes.Columns["Apellido2"].HeaderText = "Segundo apellido";
            dtGrdVwEstudiantes.Columns["IdGrupo"].Visible = false;
            dtGrdVwEstudiantes.Columns["Direccion"].HeaderText = "Dirección";
            dtGrdVwEstudiantes.Columns["Telefono"].HeaderText = "Teléfono";
            dtGrdVwEstudiantes.Columns["Celular"].HeaderText = "Celular";
            dtGrdVwEstudiantes.Columns["Email"].HeaderText = "Correo electrónico";
            dtGrdVwEstudiantes.Columns["Ctpp"].Visible = false;

            if(!dtGrdVwEstudiantes.Columns.Contains("Local"))
            {
                DataGridViewCheckBoxColumn DtGVCl = new DataGridViewCheckBoxColumn();
                DtGVCl.DataPropertyName = "Ctpp";
                DtGVCl.TrueValue = "1";
                DtGVCl.FalseValue = "0";
                DtGVCl.HeaderText = "Local";
                DtGVCl.Name = "Local";
                DtGVCl.ReadOnly = true;
                dtGrdVwEstudiantes.Columns.AddRange(new DataGridViewColumn[] { DtGVCl });
            }

            foreach (DataGridViewColumn column in dtGrdVwEstudiantes.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dtGrdVwEstudiantes.Refresh();
            dtGrdVwEstudiantes.Update();
        }

        private void vaciarEstudianteDatosDtGrdVw()
        {
            dtGrdVwEstudiantes.DataSource = null;
            dtGrdVwEstudiantes.Refresh();
            dtGrdVwEstudiantes.Update();
        }

        private void dtGrdVwEstudiantes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditStudent editStudent = new EditStudent(2, int.Parse(dtGrdVwEstudiantes.Rows[e.RowIndex].Cells["IdEstudiante"].Value.ToString()));
            editStudent.Show();
            editStudent.rfDT += EditStudent_rfDT;
        }

        private void btnAddEstud_Click(object sender, EventArgs e)
        {
            EditStudent nuevoEst = new EditStudent(1);
            nuevoEst.Show();
            nuevoEst.rfDT += EditStudent_rfDT;
        }

        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            EditStudent editStudent = new EditStudent(2, int.Parse(dtGrdVwEstudiantes.Rows[dtGrdVwEstudiantes.CurrentRow.Index].Cells[0].Value.ToString()));
            editStudent.Show();
            editStudent.rfDT += EditStudent_rfDT;
        }

        private void EditStudent_rfDT()
        {
            refrescaDTEstud();
        }

        private void btnDelStud_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("¿Desea eliminar al estudiantes",
                "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (rs == DialogResult.Yes)
            {
                EstudiantesBussines bs = new EstudiantesBussines();
                MatriculaBussines mbs = new MatriculaBussines();
                try
                {
                    mbs.borrarMatricula(mbs.idMatriculaXEstudiante(int.Parse(dtGrdVwEstudiantes.Rows[dtGrdVwEstudiantes.CurrentRow.Index].Cells[0].Value.ToString())));
                    bs.borrarEstudiante(int.Parse(dtGrdVwEstudiantes.Rows[dtGrdVwEstudiantes.CurrentRow.Index].Cells[0].Value.ToString()));
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                MessageBox.Show("Estudiante eliminado de manera exitosa");
                refrescaDTEstud();
            }
        }

        private void refrescaDTEstud()
        {
            vaciarEstudianteDatosDtGrdVw();
            llenarEstudianteDatosDtGrdVw();
        }
        #endregion

        #region TabOrienta
        private void tbPgOrienta_Enter(object sender, EventArgs e)
        {
            llenarOrientaDatosDtGrdVw();
        }

        private void llenarOrientaDatosDtGrdVw()
        {
            NotaBussines ori = new NotaBussines();
            dtGrdVwOrienta.DataSource = ori.listarNotasOrienta();
            formateaDTNotasOrienta();
        }

        private void vaciarOrientaDatosDtGrdVw()
        {
            dtGrdVwOrienta.DataSource = null;
            formateaDTNotasOrienta();
        }

        private void dtGrdVwOrienta_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            editOrienta editOrienta1 = new editOrienta(
                //Indica si es nota nueva o existente
                dtGrdVwOrienta.Rows[e.RowIndex].Cells["IdNota"].Value != null ? 2 : 1,

                int.Parse(dtGrdVwOrienta.Rows[e.RowIndex].Cells["Matricula"].Value.ToString()),

                dtGrdVwOrienta.Rows[e.RowIndex].Cells["IdNota"].Value != null ?
                int.Parse(dtGrdVwOrienta.Rows[e.RowIndex].Cells["IdNota"].Value.ToString()) : 0,

                string.Format("{0} {1} {2}", dtGrdVwOrienta.Rows[e.RowIndex].Cells["Nombre1"].Value.ToString(),
                dtGrdVwOrienta.Rows[e.RowIndex].Cells["ApellidoOne"].Value.ToString(),
                dtGrdVwOrienta.Rows[e.RowIndex].Cells["ApellidoTwo"].Value.ToString()),

                dtGrdVwOrienta.Rows[e.RowIndex].Cells["Entrevista"].Value != null ?
                decimal.Parse(dtGrdVwOrienta.Rows[e.RowIndex].Cells["Entrevista"].Value.ToString()) : 0,

                dtGrdVwOrienta.Rows[e.RowIndex].Cells["Vocacional"].Value != null ?
                decimal.Parse(dtGrdVwOrienta.Rows[e.RowIndex].Cells["Vocacional"].Value.ToString()) : 0);

            editOrienta1.Show();
            editOrienta1.rfDTOri += EditNotasOrienta_rfDT;
        }

        private void btnEditOrienta_Click(object sender, EventArgs e)
        {
            editOrienta editOrienta1 = new editOrienta(
                //Indica si es nota nueva o existente
                dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["IdNota"].Value != null ? 2 : 1,

                int.Parse(dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["Matricula"].Value.ToString()),

                dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["IdNota"].Value != null ?
                int.Parse(dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["IdNota"].Value.ToString()) : 0,

                string.Format("{0} {1} {2}", dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["Nombre1"].Value.ToString(),
                dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["ApellidoOne"].Value.ToString(),
                dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["ApellidoTwo"].Value.ToString()),

                dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["Entrevista"].Value != null ?
                decimal.Parse(dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["Entrevista"].Value.ToString()) : 0,

                dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["Vocacional"].Value != null ?
                decimal.Parse(dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["Vocacional"].Value.ToString()) : 0);

            editOrienta1.Show();
            editOrienta1.rfDTOri += EditNotasOrienta_rfDT;
        }

        private void btnEliminarOrienta_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("¿Desea eliminar las notas",
                "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (rs == DialogResult.Yes)
            {
                NotaBussines bs = new NotaBussines();
                try
                {
                    if (dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["IdNota"].Value == null)
                    {
                        MessageBox.Show("El estudiante no tiene notas asignadas");
                    }
                    else
                    {
                        bs.editNotaOrienta(int.Parse(dtGrdVwOrienta.Rows[dtGrdVwOrienta.CurrentRow.Index].Cells["IdNota"].Value.ToString()), 0, 0);
                        MessageBox.Show("Notas eliminadas de manera exitosa");
                        refrescaDTNotasOrienta();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void EditNotasOrienta_rfDT()
        {
            refrescaDTNotasOrienta();
        }

        private void refrescaDTNotasOrienta()
        {
            vaciarOrientaDatosDtGrdVw();
            llenarOrientaDatosDtGrdVw();
            formateaDTNotasOrienta();
        }

        private void formateaDTNotasOrienta()
        {
            #region Oculta columnas nulas
            dtGrdVwOrienta.Columns["IdNota"].Visible = false;
            dtGrdVwOrienta.Columns["Matricula"].Visible = false;
            dtGrdVwOrienta.Columns["Asignatura"].Visible = false;
            dtGrdVwOrienta.Columns["Curso_lectivo"].Visible = false;
            dtGrdVwOrienta.Columns["Nivel"].Visible = false;
            dtGrdVwOrienta.Columns["Periodo"].Visible = false;
            dtGrdVwOrienta.Columns["PeriodoNombre"].Visible = false;
            dtGrdVwOrienta.Columns["Calificacion"].Visible = false;
            #endregion

            #region Nombre de columnas
            dtGrdVwOrienta.Columns["Apellido1"].HeaderText = "Primer apellido";
            dtGrdVwOrienta.Columns["Apellido2"].HeaderText = "Segundo apellido";
            dtGrdVwOrienta.Columns["Nombre"].HeaderText = "Nombre";
            dtGrdVwOrienta.Columns["Entrevista"].HeaderText = "Entrevista";
            dtGrdVwOrienta.Columns["Vocacional"].HeaderText = "Vocacional";
            #endregion

            dtGrdVwOrienta.Refresh();
            dtGrdVwOrienta.Update();
        }
        #endregion

        #region TabNotas8
        private void tbPgNotas8_Enter(object sender, EventArgs e)
        {
            llenarNotas8DtGrdVw();
        }

        private void llenarNotas8DtGrdVw()
        {
            NotaBussines nt = new NotaBussines();
            dtGrdVwNotas.DataSource = nt.listarNotasBasicas8();
            formateaDTNotas8();
        }

        private void vaciarDtGrdVwNotas8()
        {
            dtGrdVwNotas.DataSource = null;
            dtGrdVwNotas.Refresh();
            dtGrdVwNotas.Update();
        }

        private void btnEditNota_Click(object sender, EventArgs e)
        {
            EditNotas8 notas8 = new EditNotas8(
                int.Parse(dtGrdVwNotas.Rows[dtGrdVwNotas.CurrentRow.Index].Cells[0].Value.ToString()));
            notas8.Show();
            notas8.RfDTNotas8 += Notas8_RfDTNotas8;
        }

        private void dtGrdVwNotas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditNotas8 notas8 = new EditNotas8(
                int.Parse(dtGrdVwNotas.Rows[e.RowIndex].Cells["IdMatricula"].Value.ToString()));

            notas8.Show();
            notas8.RfDTNotas8 += Notas8_RfDTNotas8;
        }

        private void Notas8_RfDTNotas8()
        {
            refrescaDTNotas8();
        }

        private void btnDelNota_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("¿Desea eliminar las notas",
                "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (rs == DialogResult.Yes)
            {
                NotaBussines bs = new NotaBussines();
                try
                {
                    bs.delNotasXMatYNivel(int.Parse(dtGrdVwNotas.Rows[dtGrdVwNotas.CurrentRow.Index].Cells["IdMatricula"].Value.ToString()), 8);
                    MessageBox.Show("Notas eliminadas de manera exitosa");
                    refrescaDTNotas8();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void refrescaDTNotas8()
        {
            vaciarDtGrdVwNotas8();
            llenarNotas8DtGrdVw();
            formateaDTNotas8();
        }

        private void formateaDTNotas8()
        {
            dtGrdVwNotas.Columns["id_esp1"].Visible = false;
            dtGrdVwNotas.Columns["id_esp2"].Visible = false;
            dtGrdVwNotas.Columns["id_esp3"].Visible = false;
            dtGrdVwNotas.Columns["id_cie1"].Visible = false;
            dtGrdVwNotas.Columns["id_cie2"].Visible = false;
            dtGrdVwNotas.Columns["id_cie3"].Visible = false;
            dtGrdVwNotas.Columns["id_estsoc1"].Visible = false;
            dtGrdVwNotas.Columns["id_estsoc2"].Visible = false;
            dtGrdVwNotas.Columns["id_estsoc3"].Visible = false;
            dtGrdVwNotas.Columns["id_mat1"].Visible = false;
            dtGrdVwNotas.Columns["id_mat2"].Visible = false;
            dtGrdVwNotas.Columns["id_mat3"].Visible = false;
            dtGrdVwNotas.Columns["id_ing1"].Visible = false;
            dtGrdVwNotas.Columns["id_ing2"].Visible = false;
            dtGrdVwNotas.Columns["id_ing3"].Visible = false;
            dtGrdVwNotas.Columns["id_civ1"].Visible = false;
            dtGrdVwNotas.Columns["id_civ2"].Visible = false;
            dtGrdVwNotas.Columns["id_civ3"].Visible = false;
            dtGrdVwNotas.Columns["id_talI1"].Visible = false;
            dtGrdVwNotas.Columns["id_talI2"].Visible = false;
            dtGrdVwNotas.Columns["id_talI3"].Visible = false;
            dtGrdVwNotas.Columns["id_talII1"].Visible = false;
            dtGrdVwNotas.Columns["id_talII2"].Visible = false;
            dtGrdVwNotas.Columns["id_talII3"].Visible = false;
            foreach (DataGridViewColumn column in dtGrdVwNotas.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dtGrdVwNotas.Refresh();
            dtGrdVwNotas.Update();
        }
        #endregion

        #region TabNotas9
        private void tbPgNotas9_Enter(object sender, EventArgs e)
        {
            llenarNotas9DtGrdVw();
        }

        private void llenarNotas9DtGrdVw()
        {
            NotaBussines nt = new NotaBussines();
            dtGrdVwNotas9.DataSource = nt.listarNotasBasicas9();
            formateaDTNotas9();
        }

        private void vaciarDtGrdVwNotas9()
        {
            dtGrdVwNotas9.DataSource = null;
            dtGrdVwNotas9.Refresh();
            dtGrdVwNotas9.Update();
        }

        private void btnEditNota9_Click(object sender, EventArgs e)
        {
            EditNotas9 notas9 = new EditNotas9(
                int.Parse(dtGrdVwNotas9.Rows[dtGrdVwNotas9.CurrentRow.Index].Cells[0].Value.ToString()));
            notas9.Show();
            notas9.RfDTNotas9 += Notas9_RfDTNotas9;
        }

        private void dtGrdVwNotas9_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            EditNotas9 notas9 = new EditNotas9(
                int.Parse(dtGrdVwNotas9.Rows[e.RowIndex].Cells["IdMatricula"].Value.ToString()));

            notas9.Show();
            notas9.RfDTNotas9 += Notas9_RfDTNotas9;
        }

        private void Notas9_RfDTNotas9()
        {
            refrescaDTNotas9();
        }

        private void btnDelNota9_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("¿Desea eliminar las notas",
                "Borrar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);

            if (rs == DialogResult.Yes)
            {
                NotaBussines bs = new NotaBussines();
                try
                {
                    bs.delNotasXMatYNivel(int.Parse(dtGrdVwNotas9.Rows[dtGrdVwNotas9.CurrentRow.Index].Cells["IdMatricula"].Value.ToString()), 9);
                    MessageBox.Show("Notas eliminadas de manera exitosa");
                    refrescaDTNotas9();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void refrescaDTNotas9()
        {
            vaciarDtGrdVwNotas9();
            llenarNotas9DtGrdVw();
            formateaDTNotas9();
        }

        private void formateaDTNotas9()
        {
            dtGrdVwNotas9.Columns["id_esp1"].Visible = false;
            dtGrdVwNotas9.Columns["id_esp2"].Visible = false;
            dtGrdVwNotas9.Columns["id_esp3"].Visible = false;
            dtGrdVwNotas9.Columns["esp3"].Visible = false;
            dtGrdVwNotas9.Columns["id_cie1"].Visible = false;
            dtGrdVwNotas9.Columns["id_cie2"].Visible = false;
            dtGrdVwNotas9.Columns["id_cie3"].Visible = false;
            dtGrdVwNotas9.Columns["cie3"].Visible = false;
            dtGrdVwNotas9.Columns["id_estsoc1"].Visible = false;
            dtGrdVwNotas9.Columns["id_estsoc2"].Visible = false;
            dtGrdVwNotas9.Columns["id_estsoc3"].Visible = false;
            dtGrdVwNotas9.Columns["estsoc3"].Visible = false;
            dtGrdVwNotas9.Columns["id_mat1"].Visible = false;
            dtGrdVwNotas9.Columns["id_mat2"].Visible = false;
            dtGrdVwNotas9.Columns["id_mat3"].Visible = false;
            dtGrdVwNotas9.Columns["mat3"].Visible = false;
            dtGrdVwNotas9.Columns["id_ing1"].Visible = false;
            dtGrdVwNotas9.Columns["id_ing2"].Visible = false;
            dtGrdVwNotas9.Columns["id_ing3"].Visible = false;
            dtGrdVwNotas9.Columns["ing3"].Visible = false;
            dtGrdVwNotas9.Columns["id_civ1"].Visible = false;
            dtGrdVwNotas9.Columns["id_civ2"].Visible = false;
            dtGrdVwNotas9.Columns["id_civ3"].Visible = false;
            dtGrdVwNotas9.Columns["civ3"].Visible = false;
            dtGrdVwNotas9.Columns["id_talI1"].Visible = false;
            dtGrdVwNotas9.Columns["id_talI2"].Visible = false;
            dtGrdVwNotas9.Columns["id_talI3"].Visible = false;
            dtGrdVwNotas9.Columns["talI3"].Visible = false;
            dtGrdVwNotas9.Columns["id_talII1"].Visible = false;
            dtGrdVwNotas9.Columns["id_talII2"].Visible = false;
            dtGrdVwNotas9.Columns["id_talII3"].Visible = false;
            dtGrdVwNotas9.Columns["talII3"].Visible = false;
            foreach (DataGridViewColumn column in dtGrdVwNotas9.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
            dtGrdVwNotas9.Refresh();
            dtGrdVwNotas9.Update();
        }
        #endregion

        #region Configuracion
        private void btnConfig_Click(object sender, EventArgs e)
        {
            string pass;

            pass = Microsoft.VisualBasic.Interaction.InputBox("Ingrese contraseña ", "Ingreso de contraseña", "selvanegra$2015", 100, 0);

            if (pass == "selvanegra$2015")
            {
                Config config = new Config();
                config.Show();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEscogenciaEspec_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Muestra la escogencia de especialidad por los estudiantes");
        }
    }
    #endregion
}
