using BLL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class frmDoctorMenu : Form
    {
        public frmDoctorMenu()
        {
            InitializeComponent();
        }
        private string DocumentoDoctor;
        private int citaId;
        private HistoriaClinica historia;
        private ServicioCita servicioCita;
        private ServicioDoctor servicioDoctor;
        private ServicioPaciente servicioPaciente;
        private ServicioEspecialidad servicioEspecialidad;
        private ServicioHistoriaClinica servicioHistoriaClinica;
        public frmDoctorMenu(string Documento)
        {

            InitializeComponent();
            this.DocumentoDoctor = Documento;
            servicioCita = new ServicioCita();
            servicioDoctor = new ServicioDoctor();
            servicioPaciente = new ServicioPaciente();
            servicioEspecialidad = new ServicioEspecialidad();
            servicioHistoriaClinica = new ServicioHistoriaClinica();
            CargarCitasConNombres();
            OcultarPaneles();
        }
        private void OcultarPaneles()
        {
            pnlCitasDoctor.Visible = true;
            pnlInfoDoctor.Visible = false;
            pnlHistoriaClinica.Visible = false;
        }

        public void cargarLabel()
        {
            Doctor d = servicioDoctor.ObtenerPorId(DocumentoDoctor);
            Especialidad e = servicioEspecialidad.ObtenerPorId(d.Especialidad_id.ToString());

            txbNombreDoctor.Text = d.Primer_Nombre + " " + d.Segundo_Nombre + " " + d.Primer_Apellido + " " + d.Segundo_Apellido;
            txbCorreoDoctor.Text = d.Correo;
            txbTelefonoDoctor.Text = d.Telefono;
            txbCedulaDoctor.Text = d.DocumentoID;
            txbLicenciaDoctor.Text = d.NumeroLicencia;
            txbEspecialidadDoctor.Text = e.Nombre;
            txbEstadoDoctor.Text = d.Estado;
        }

        private void CargarCitasConNombres()
        {
            try
            {
                if (string.IsNullOrEmpty(DocumentoDoctor))
                {
                    MessageBox.Show("No se pudo obtener el documento del doctor", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<Cita> citas = servicioCita.ObtenerCitasPorDoctor(DocumentoDoctor);

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Fecha", typeof(DateTime));
                dt.Columns.Add("Hora", typeof(string));
                dt.Columns.Add("Paciente", typeof(string));
                dt.Columns.Add("Especialidad", typeof(string));
                dt.Columns.Add("Estado", typeof(string));

                foreach (Cita cita in citas)
                {
                    string nombrePaciente = "N/A";
                    try
                    {
                        Paciente paciente = servicioPaciente.ObtenerPorId(cita.Documento_paciente);
                        if (paciente != null)
                        {
                            nombrePaciente = $"{paciente.Primer_Nombre} {paciente.Primer_Apellido}";
                        }
                    }
                    catch
                    {
                        nombrePaciente = cita.Documento_paciente;
                    }

                    string nombreEspecialidad = "N/A";
                    try
                    {
                        Especialidad especialidad = servicioEspecialidad.ObtenerPorId(cita.Especialidad_id.ToString());
                        if (especialidad != null)
                        {
                            nombreEspecialidad = especialidad.Nombre;
                        }
                    }
                    catch
                    {
                        nombreEspecialidad = cita.Especialidad_id.ToString();
                    }

                    dt.Rows.Add(
                        cita.Id,
                        cita.Fecha,
                        cita.Hora,
                        nombrePaciente,
                        nombreEspecialidad,
                        cita.Estado
                    );
                }

                dgvCitas.DataSource = dt;

                PersonalizarDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar citas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PersonalizarDataGridView()
        {
            if (dgvCitas.Columns.Count > 0)
            {
                dgvCitas.Columns["ID"].Width = 50;
                dgvCitas.Columns["ID"].HeaderText = "N° Cita";

                dgvCitas.Columns["Fecha"].Width = 100;
                dgvCitas.Columns["Fecha"].HeaderText = "Fecha";
                dgvCitas.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";

                dgvCitas.Columns["Hora"].Width = 80;
                dgvCitas.Columns["Hora"].HeaderText = "Hora";

                dgvCitas.Columns["Paciente"].Width = 180;
                dgvCitas.Columns["Paciente"].HeaderText = "Paciente";

                dgvCitas.Columns["Especialidad"].Width = 150;
                dgvCitas.Columns["Especialidad"].HeaderText = "Especialidad";

                dgvCitas.Columns["Estado"].Width = 100;
                dgvCitas.Columns["Estado"].HeaderText = "Estado";

                dgvCitas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvCitas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCitas.MultiSelect = false;
                dgvCitas.ReadOnly = true;
                dgvCitas.AllowUserToAddRows = false;

                ColorearFilasPorEstado();
            }
        }
        private void ColorearFilasPorEstado()
        {
            foreach (DataGridViewRow row in dgvCitas.Rows)
            {
                if (row.Cells["Estado"].Value != null)
                {
                    string estado = row.Cells["Estado"].Value.ToString();

                    switch (estado)
                    {
                        case "Pendiente":
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                            break;
                        case "Completada":
                            row.DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case "Cancelada":
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                            break;
                    }
                }
            }
        }

        private void btnCitasDoctor_Click(object sender, EventArgs e)
        {
            pnlCitasDoctor.Visible = true;
            pnlInfoDoctor.Visible = false;
            pnlHistoriaClinica.Visible = false;
            CargarCitasConNombres();
        }

        private void CargarHistoriaClinica()
        {
            try
            {
                Cita cita = servicioCita.ObtenerPorId(citaId.ToString());
                Paciente paciente = servicioPaciente.ObtenerPorId(cita.Documento_paciente);
                Doctor doctor = servicioDoctor.ObtenerPorId(DocumentoDoctor);

                txbPacienteDoctor.Text = $"{paciente.Primer_Nombre} {paciente.Primer_Apellido}";
                txbCitaIdDoctor.Text = citaId.ToString();
                txbFechaApertura.Text = cita.Fecha.ToString("dd/MM/yyyy");

                CargarHistoriasAnteriores(cita.Documento_paciente, doctor.Especialidad_id);

                HistoriaClinica historiaActual = servicioHistoriaClinica.ObtenerPorCita(citaId);

                if (historiaActual != null)
                {
                    txtDiagnostico.Text = historiaActual.Diagnostico;
                    txtTratamiento.Text = historiaActual.Tratamiento;
                    txtObservaciones.Text = historiaActual.Observaciones;
                }
                else
                {
                    txtDiagnostico.Text = "";
                    txtTratamiento.Text = "";
                    txtObservaciones.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historia: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHistoriasAnteriores(string documentoPaciente, int especialidadId)
        {
            try
            {
                List<HistoriaClinica> historias = servicioHistoriaClinica.ObtenerHistorialPorEspecialidad(
                    documentoPaciente,
                    especialidadId
                );

                DataTable dt = new DataTable();
                dt.Columns.Add("Fecha", typeof(string));
                dt.Columns.Add("Doctor", typeof(string));
                dt.Columns.Add("Diagnóstico", typeof(string));
                dt.Columns.Add("Tratamiento", typeof(string));
                dt.Columns.Add("Observaciones", typeof(string));

                foreach (HistoriaClinica h in historias)
                {
                    Cita citaHistoria = servicioCita.ObtenerPorId(h.Cita_id.ToString());

                    Doctor doctorHistoria = servicioDoctor.ObtenerPorId(h.Doctor_documentoid);
                    string nombreDoctor = $"Dr. {doctorHistoria.Primer_Nombre} {doctorHistoria.Primer_Apellido}";

                    dt.Rows.Add(
                        citaHistoria.Fecha.ToString("dd/MM/yyyy"),
                        nombreDoctor,
                        h.Diagnostico ?? "N/A",
                        h.Tratamiento ?? "N/A",
                        h.Observaciones ?? "N/A"
                    );
                }

                dgvHistorialPrevio.DataSource = dt;

                if (dgvHistorialPrevio.Columns.Count > 0)
                {
                    dgvHistorialPrevio.Columns["Fecha"].Width = 80;
                    dgvHistorialPrevio.Columns["Doctor"].Width = 150;
                    dgvHistorialPrevio.Columns["Diagnóstico"].Width = 200;
                    dgvHistorialPrevio.Columns["Tratamiento"].Width = 200;
                    dgvHistorialPrevio.Columns["Observaciones"].Width = 200;

                    dgvHistorialPrevio.ReadOnly = true;
                    dgvHistorialPrevio.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgvHistorialPrevio.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar historial previo: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void btninfiUserDoctor_Click(object sender, EventArgs e)
        {
            pnlCitasDoctor.Visible = false;
            pnlInfoDoctor.Visible = true;
            pnlHistoriaClinica.Visible = false;
            cargarLabel();
        }

        private void btncerrarsesionDoctor_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin login = new frmLogin();
            login.Show();
        }

        private void btnsaliruserDoctor_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCitasDoctor_MouseEnter(object sender, EventArgs e)
        {
            btnCitasDoctor.BackColor = Color.DarkGreen;
        }
        private void btnCitasDoctor_MouseLeave(object sender, EventArgs e)
        {
            btnCitasDoctor.BackColor = Color.SeaGreen;
        }

        private void btnuserDoctor_MouseEnter(object sender, EventArgs e)
        {
            btnuserDoctor.BackColor = Color.DarkGreen;
        }

        private void btnuserDoctor_MouseLeave(object sender, EventArgs e)
        {
            btnuserDoctor.BackColor = Color.SeaGreen;
        }

        private void btninfiUserDoctor_MouseEnter(object sender, EventArgs e)
        {
            btninfiUserDoctor.BackColor = Color.DarkGreen;
        }

        private void btninfiUserDoctor_MouseLeave(object sender, EventArgs e)
        {
            btninfiUserDoctor.BackColor = Color.SeaGreen;
        }

        private void btncerrarsesionDoctor_MouseEnter(object sender, EventArgs e)
        {
            btncerrarsesionDoctor.BackColor = Color.DarkGreen;
        }

        private void btncerrarsesionDoctor_MouseLeave(object sender, EventArgs e)
        {
            btncerrarsesionDoctor.BackColor = Color.SeaGreen;
        }

        private void btnsaliruserDoctor_MouseEnter(object sender, EventArgs e)
        {
            btnsaliruserDoctor.BackColor = Color.DarkGreen;
        }

        private void btnsaliruserDoctor_MouseLeave(object sender, EventArgs e)
        {
            btnsaliruserDoctor.BackColor = Color.SeaGreen;
        }


        bool menuExpandDoc = true;
        bool menuUserDoc = true;

        private void btnuserDoctor_Click(object sender, EventArgs e)
        {
            timerOpcionUserDoc.Start();

        }

        private void btnmenuDoctor_Click(object sender, EventArgs e)
        {
            timerdocmenu.Start();
        }

        private void timerdocmenu_Tick(object sender, EventArgs e)
        {
            if (menuExpandDoc)
            {
                flowLayoutDocMenu.Width -= 5;
                if (flowLayoutDocMenu.Width <= 56)
                {
                    menuExpandDoc = false;
                    timerdocmenu.Stop();
                }
            }
            else
            {
                flowLayoutDocMenu.Width += 5;
                if (flowLayoutDocMenu.Width >= 192)
                {
                    menuExpandDoc = true;
                    timerdocmenu.Stop();
                }
            }
        }

        private void timerOpcionUserDoc_Tick(object sender, EventArgs e)
        {
            if (menuUserDoc)
            {
                flowLayoutopcionmenuDoc.Height -= 5;
                if (flowLayoutopcionmenuDoc.Height <= 49)
                {
                    menuUserDoc = false;
                    timerOpcionUserDoc.Stop();
                }
            }
            else
            {
                flowLayoutopcionmenuDoc.Height += 5;
                if (flowLayoutopcionmenuDoc.Height >= 200)
                {
                    menuUserDoc = true;
                    timerOpcionUserDoc.Stop();
                }
            }
        }
        private void RedondearBoton(Button boton, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            path.AddArc(new Rectangle(0, 0, radio, radio), 180, 90);
            path.AddArc(new Rectangle(boton.Width - radio, 0, radio, radio), 270, 90);
            path.AddArc(new Rectangle(boton.Width - radio, boton.Height - radio, radio, radio), 0, 90);
            path.AddArc(new Rectangle(0, boton.Height - radio, radio, radio), 90, 90);

            path.CloseFigure();
            boton.Region = new Region(path);
        }

        private void frmDoctorMenu_Load(object sender, EventArgs e)
        {
            RedondearBoton(btncloseDoctor, 20);
            RedondearBoton(btnmax, 20);
            RedondearBoton(btnmin, 20);
        }

        private void btncloseDoctor_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgvCitas_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvCitas.Rows[e.RowIndex];

                citaId = Convert.ToInt32(row.Cells["ID"].Value);
                string estado = row.Cells["Estado"].Value?.ToString();

                if (estado != "Pendiente")
                {
                    MessageBox.Show("Solo se puede editar la historia clínica de citas en estado 'Pendiente'",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                pnlCitasDoctor.Visible = false;
                pnlInfoDoctor.Visible = false;
                pnlHistoriaClinica.Visible = true;

                CargarHistoriaClinica();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir historia clínica: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCompletar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtDiagnostico.Text) &&
                    string.IsNullOrWhiteSpace(txtTratamiento.Text))
                {
                    MessageBox.Show("Debe ingresar al menos un diagnóstico o tratamiento", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool historiaGuardada = servicioHistoriaClinica.GuardarHistoriaDesdeConsulta(
                    citaId,
                    txtDiagnostico.Text.Trim(),
                    txtTratamiento.Text.Trim(),
                    txtObservaciones.Text.Trim()
                );

                if (historiaGuardada)
                {
                    bool citaActualizada = servicioCita.CompletarCita(citaId);

                    if (citaActualizada)
                    {
                        MessageBox.Show("Historia clínica guardada y cita completada exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        pnlCitasDoctor.Visible = true;
                        pnlInfoDoctor.Visible = false;
                        pnlHistoriaClinica.Visible = false;

                        CargarCitasConNombres();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelarProceso_Click(object sender, EventArgs e)
        {
            pnlCitasDoctor.Visible = true;
            pnlInfoDoctor.Visible = false;
            pnlHistoriaClinica.Visible = false;
        }

        private void dgvHistorialPrevio_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvHistorialPrevio.Rows[e.RowIndex];

                string fecha = row.Cells["Fecha"].Value.ToString();
                string doctor = row.Cells["Doctor"].Value.ToString();
                string diagnostico = row.Cells["Diagnóstico"].Value.ToString();
                string tratamiento = row.Cells["Tratamiento"].Value.ToString();
                string observaciones = row.Cells["Observaciones"].Value.ToString();

                string detalle = $"CONSULTA DEL {fecha}\n\n" +
                                $"Doctor: {doctor}\n\n" +
                                $"DIAGNÓSTICO:\n{diagnostico}\n\n" +
                                $"TRATAMIENTO:\n{tratamiento}\n\n" +
                                $"OBSERVACIONES:\n{observaciones}";

                MessageBox.Show(detalle, "Detalle de Consulta",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCitas.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Debe seleccionar una cita", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idCita = Convert.ToInt32(dgvCitas.SelectedRows[0].Cells["ID"].Value);
                string estado = dgvCitas.SelectedRows[0].Cells["Estado"].Value.ToString();

                if (estado == "Cancelada")
                {
                    MessageBox.Show("La cita ya está cancelada", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (estado == "Completada")
                {
                    MessageBox.Show("No se puede cancelar una cita completada", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "¿Está seguro de cancelar esta cita?",
                    "Confirmar cancelación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (servicioCita.CancelarCita(idCita))
                    {
                        MessageBox.Show("Cita cancelada exitosamente", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarCitasConNombres();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cancelar cita: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
