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
using System.Xml.Linq;


namespace PRESENTACION
{
    public partial class frmPacienteMenu : Form
    {
        Point lastLocation;
        bool mouseDown;
        private string DocumentoPaciente;
        private bool cargandoDoctores = false;
        private ServicioPaciente servicioPaciente;
        private ServicioEPS servicioEPS;
        private ServicioCiudad servicioCiudad;
        private ServicioResponsable servicioResponsable;
        private ServicioEspecialidad servicioEspecialidad;
        private ServicioDoctor servicioDoctor;
        private ServicioCita servicioCita;
        public frmPacienteMenu(string Documento)
        {
            
            InitializeComponent();
            OcultarPaneles();
            this.DocumentoPaciente = Documento;
            servicioPaciente = new ServicioPaciente();
            servicioEPS = new ServicioEPS();
            servicioCiudad = new ServicioCiudad();
            servicioResponsable = new ServicioResponsable();
            servicioEspecialidad = new ServicioEspecialidad();
            servicioDoctor = new ServicioDoctor();
            servicioCita = new ServicioCita();
            CargarComboBoxes();
        }

        private void OcultarPaneles()
        {
            pnlConsultarCitas.Visible = false;
            pnlInfoUsuario.Visible = false;
            pnlRegistrarCita.Visible = true;
        }


        private void CargarComboBoxes()
        {
            DataTable Especialidad = servicioEspecialidad.ObtenerParaCombo();

            cmbTipoDeCita.DisplayMember = "Texto";
            cmbTipoDeCita.ValueMember = "Id";
            cmbTipoDeCita.SelectedIndex = -1;
            cmbTipoDeCita.DataSource = servicioEspecialidad.ObtenerParaCombo();
        }

        public void cargarLabel()
        {
            Paciente p = servicioPaciente.ObtenerPorId(DocumentoPaciente);
            EPS e = servicioEPS.ObtenerPorId(p.EPS_id.ToString());
            Ciudad c = servicioCiudad.ObtenerPorId(p.Ciudad_id.ToString());
            Responsable r = servicioResponsable.ObtenerPorId(p.Documento_responsable);

            txbnombre.Text = p.Primer_Nombre + " " + p.Segundo_Nombre + " " + p.Primer_Apellido + " " + p.Segundo_Apellido;
            txbcorreo.Text = p.Correo;
            txbtelefono.Text = p.Telefono;
            txbcedula.Text = p.DocumentoID;
            txbedad.Text = p.Edad.ToString() + " años";
            txbsexo.Text = p.Sexo.ToString();
            txbsangre.Text = p.Tipo_sangre + p.RH;
            txbeps.Text = e.Nombre;
            txbcalle.Text = p.Calle;
            txbdireccion.Text = p.Direccion;
            txbbarrio.Text = p.Barrio;
            txbciudad.Text = c.Nombre;
            txbresponsable.Text = r.Primer_Nombre + " " + r.Segundo_Nombre + " " + r.Primer_Apellido + " " + r.Segundo_Apellido;
        }

        private void btnAgendarCitaPaciente_Click(object sender, EventArgs e)
        {
            pnlConsultarCitas.Visible = false;
            pnlInfoUsuario.Visible = false;
            pnlRegistrarCita.Visible = true;
            CargarComboBoxes();
        }

        //metodos para las animaciones del menu y las opciones del user

        bool menuExpand = true;
        bool menuUser = true;

        private void timerMenuPacienet_Tick(object sender, EventArgs e)
        {
            if (menuExpand)
            {
                flowLayoutPacienteMenu.Width -= 5;
                if (flowLayoutPacienteMenu.Width <= 56)
                {
                    menuExpand = false;
                    timerMenuPacienet.Stop();
                }
            }
            else
            {
                flowLayoutPacienteMenu.Width += 5;
                if (flowLayoutPacienteMenu.Width >= 192)
                {
                    menuExpand = true;
                    timerMenuPacienet.Stop();
                }
            }
        }

        private void btnmenu_Click(object sender, EventArgs e)
        {
            timerMenuPacienet.Start();
        }

        private void timeropcionesuser_Tick(object sender, EventArgs e)
        {
            if (menuUser)
            {
                flowLayoutopcionmenu.Height -= 5;
                if (flowLayoutopcionmenu.Height <=49 )
                {
                    menuUser = false;
                    timeropcionesuser.Stop();
                }
            }
            else
            {
                flowLayoutopcionmenu.Height += 5;
                if (flowLayoutopcionmenu.Height >= 200)
                {
                    menuUser = true;
                    timeropcionesuser.Stop();
                }
            }
        }
        private void btnuser_Click(object sender, EventArgs e)
        {
            timeropcionesuser.Start();
        }

        //----------------------------------------------------------------------------------------
        //metodo de redondeo de botones
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

        private void ConfigurarTextBoxSoloLectura(TextBox txt)
        {
            txt.ReadOnly = true;
            txt.BackColor = Color.White;
            txt.ForeColor = Color.Black;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Cursor = Cursors.Arrow;
        }
        //----------------------------------------------------------------------------------------
        //estructura principal de muestreo de datos y guardado de datos
        private void frmPacienteMenu_Load(object sender, EventArgs e)
        {
            RedondearBoton(btnclose, 20);
            RedondearBoton(btnmax, 20);
            RedondearBoton(btnmin, 20);

            ConfigurarTextBoxSoloLectura(txbnombre);
            ConfigurarTextBoxSoloLectura(txbedad);
            ConfigurarTextBoxSoloLectura(txbcalle);
            ConfigurarTextBoxSoloLectura(txbresponsable);
            ConfigurarTextBoxSoloLectura(txbcorreo);
            ConfigurarTextBoxSoloLectura(txbsexo);
            ConfigurarTextBoxSoloLectura(txbdireccion);
            ConfigurarTextBoxSoloLectura(txbtelefono);
            ConfigurarTextBoxSoloLectura(txbsangre);
            ConfigurarTextBoxSoloLectura(txbbarrio);
            ConfigurarTextBoxSoloLectura(txbcedula);
            ConfigurarTextBoxSoloLectura(txbeps);
            ConfigurarTextBoxSoloLectura(txbciudad);
        }

        private void cmbTipoDeCita_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoDeCita.SelectedValue == null || cmbTipoDeCita.SelectedIndex == -1)
            {
                return;
            }

            cmbDoctor.DataSource = null;
            cmbDoctor.Items.Clear();
            cmbHora.Items.Clear();

            cargandoDoctores = true;

            int idEspecialidad = Convert.ToInt32(cmbTipoDeCita.SelectedValue);

            List<Doctor> doctores = servicioDoctor.ObtenerPorEspecialidad(idEspecialidad);

            cmbDoctor.DataSource = doctores;
            cmbDoctor.DisplayMember = "NombreCompleto";
            cmbDoctor.ValueMember = "DocumentoId";
            cmbDoctor.SelectedIndex = -1;

            cargandoDoctores = false;
        }

        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargandoDoctores || cmbDoctor.SelectedIndex == -1)
            {
                return;
            }

            cmbHora.Items.Clear();

            string documentoDoctor = cmbDoctor.SelectedValue.ToString();
            Doctor doctorSeleccionado = servicioDoctor.ObtenerPorId(documentoDoctor);

            if (doctorSeleccionado != null && !string.IsNullOrEmpty(doctorSeleccionado.HoraAtencion))
            {
                string[] horas = doctorSeleccionado.HoraAtencion.Split(',');
                foreach (string h in horas)
                {
                    cmbHora.Items.Add(h.Trim());
                }
            }
        }

        private void btnAgendaCita_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbTipoDeCita.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un tipo de cita", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbDoctor.SelectedValue == null)
                {
                    MessageBox.Show("Debe seleccionar un doctor", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbHora.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una hora", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Cita cita = new Cita();
                cita.Documento_paciente = DocumentoPaciente;
                cita.Documento_doctor = cmbDoctor.SelectedValue.ToString();
                cita.Especialidad_id = Convert.ToInt32(cmbTipoDeCita.SelectedValue);
                cita.Hora = cmbHora.SelectedItem.ToString();

                DateTime fechaSeleccionada = dtpFecha.Value;
                cita.Fecha = new DateTime(fechaSeleccionada.Year, fechaSeleccionada.Month, fechaSeleccionada.Day);

                cita.Estado = "Pendiente";

                if (servicioCita.Insertar(cita))
                {
                    MessageBox.Show("Cita agendada exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al agendar la cita: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarCitasConNombres()
        {
            try
            {
                if (string.IsNullOrEmpty(DocumentoPaciente))
                {
                    MessageBox.Show("No se pudo obtener el documento del paciente", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                List<Cita> citas = servicioCita.ObtenerCitasPorPaciente(DocumentoPaciente);

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Fecha", typeof(DateTime));
                dt.Columns.Add("Hora", typeof(string));
                dt.Columns.Add("Doctor", typeof(string));
                dt.Columns.Add("Especialidad", typeof(string));
                dt.Columns.Add("Estado", typeof(string));

                foreach (Cita cita in citas)
                {
                    string nombreDoctor = "N/A";
                    try
                    {
                        Doctor doctor = servicioDoctor.ObtenerPorId(cita.Documento_doctor);
                        if (doctor != null)
                        {
                            nombreDoctor = $"Dr. {doctor.Primer_Nombre} {doctor.Primer_Apellido}";
                        }
                    }
                    catch
                    {
                        nombreDoctor = cita.Documento_doctor;
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
                        nombreDoctor,
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

                dgvCitas.Columns["Doctor"].Width = 180;
                dgvCitas.Columns["Doctor"].HeaderText = "Doctor";

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

        private void btnConsultaCitaPaciente_Click(object sender, EventArgs e)
        {
            pnlConsultarCitas.Visible = true;
            pnlInfoUsuario.Visible = false;
            pnlRegistrarCita.Visible = false;
            CargarCitasConNombres();
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

        //----------------------------------------------------------------------------------------
        //accion click de los controles de cierre/min/max
        private void btnclose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //----------------------------------------------------------------------------------------
        //metodo de sombreo de botones

        private void btnAgendarCitaPaciente_MouseEnter(object sender, EventArgs e)
        {
            btnAgendarCitaPaciente.BackColor = Color.DarkGreen;
        }

        private void btnAgendarCitaPaciente_MouseLeave(object sender, EventArgs e)
        {
            btnAgendarCitaPaciente.BackColor = Color.SeaGreen;
        }

        private void btnConsultaCitaPaciente_MouseEnter(object sender, EventArgs e)
        {
            btnConsultaCitaPaciente.BackColor = Color.DarkGreen;
        }

        private void btnConsultaCitaPaciente_MouseLeave(object sender, EventArgs e)
        {
            btnConsultaCitaPaciente.BackColor = Color.SeaGreen;
        }

        private void btnuser_MouseEnter(object sender, EventArgs e)
        {
            btnuser.BackColor = Color.DarkGreen;
        }
        private void btnuser_MouseLeave(object sender, EventArgs e)
        {
            btnuser.BackColor = Color.SeaGreen;
        }
        private void btncerrarsesion_MouseEnter(object sender, EventArgs e)
        {
            btncerrarsesion.BackColor = Color.DarkGreen;
        }
        private void btncerrarsesion_MouseLeave(object sender, EventArgs e)
        {
            btncerrarsesion.BackColor = Color.SeaGreen;
        }
        private void btnsaliruser_MouseEnter(object sender, EventArgs e)
        {
            btnsaliruser.BackColor = Color.DarkGreen;
        }
        private void btnsaliruser_MouseLeave(object sender, EventArgs e)
        {
            btnsaliruser.BackColor = Color.SeaGreen;
        }
        //----------------------------------------------------------------------------------------
        //metodo move
        private void pnlPacieneteControles_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void pnlPacieneteControles_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void pnlPacieneteControles_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        //----------------------------------------------------------------------------------------
        //evento clici de las opciones de user
        private void btncerrarsesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin login = new frmLogin();
            login.Show();
        }

        private void btnsaliruser_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btninfiUserPaciente_Click(object sender, EventArgs e)
        {
            pnlConsultarCitas.Visible = false;
            pnlInfoUsuario.Visible = true;
            pnlRegistrarCita.Visible = false;
            cargarLabel();
        }
    }
}
