using BLL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class frmRegistro : Form
    {
        private ServicioUsuario servicioUsuario;
        private ServicioCiudad servicioCiudad;
        private ServicioEPS servicioEPS;
        private ServicioEspecialidad servicioEspecialidad;
        public frmRegistro()
        {
            InitializeComponent();

            servicioUsuario = new ServicioUsuario();
            servicioCiudad = new ServicioCiudad();
            servicioEPS = new ServicioEPS();
            servicioEspecialidad = new ServicioEspecialidad();
            ConfigurarComboBox();
            CargarComboBoxes();
            OcultarPaneles();
        }

        private void ConfigurarComboBox()
        {
            cmbRol.SelectedIndex = 0;
            cmbSexo.SelectedIndex = 0;
            cmbTipoDeSangre.SelectedIndex = 0;
            cmbEPS.SelectedIndex = 0;
        }
        private void CargarComboBoxes()
        {
            try
            {
                DataTable ciudadesPacientes = servicioCiudad.ObtenerParaCombo();

                cmbCiudadPaciente.DataSource = ciudadesPacientes;
                cmbCiudadPaciente.DisplayMember = "Texto";
                cmbCiudadPaciente.ValueMember = "Id";
                cmbCiudadPaciente.SelectedIndex = -1;

                DataTable ciudadesResponsables = servicioCiudad.ObtenerParaCombo();

                cmbCiudadResponsable.DataSource = ciudadesResponsables;
                cmbCiudadResponsable.DisplayMember = "Texto";
                cmbCiudadResponsable.ValueMember = "Id";
                cmbCiudadResponsable.SelectedIndex = -1;

                DataTable EPS = servicioEPS.ObtenerParaCombo();

                cmbEPS.DataSource = EPS;
                cmbEPS.DisplayMember = "Texto";
                cmbEPS.ValueMember = "Id";
                cmbEPS.SelectedIndex = -1;

                DataTable Especialidades = servicioEspecialidad.ObtenerParaCombo();

                cmbEspecialidad.DataSource = Especialidades;
                cmbEspecialidad.DisplayMember = "Texto";
                cmbEspecialidad.ValueMember = "Id";
                cmbEspecialidad.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message,
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OcultarPaneles()
        {
            pnlPaciente.Visible = true;
            pnlDoctor.Visible = false;
            pnlResponsable.Visible = false;
        }
        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            string seleccion = cmbRol.SelectedItem.ToString();

            switch (seleccion)
            {
                case "Paciente":
                    pnlPaciente.Visible = true;
                    pnlDoctor.Visible = false;
                    break;

                case "Doctor":
                    pnlPaciente.Visible = false;
                    pnlResponsable.Visible = false; 
                    pnlDoctor.Visible = true;
                    break;
            }
        }
        private void btnRegistro_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmarPassword.Text;
            string rol = cmbRol.SelectedItem.ToString();

            Paciente paciente = new Paciente();

            Doctor doctor = new Doctor();

            Responsable responsable = new Responsable();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, complete usuario y contraseña", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(rol))
            {
                MessageBox.Show("Por favor, seleccione un rol", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool registroExitoso = false;

                switch (rol)
                {
                    case "Paciente":

                        responsable.DocumentoID = txtCedulaResponsable.Text;
                        responsable.Primer_Nombre = txtNombreResponsable.Text;
                        responsable.Primer_Apellido = txtApellidoResponsable.Text;
                        responsable.Parentesco = txtParentesco.Text;
                        responsable.Telefono = txtTelefonoResponsable.Text;
                        responsable.Correo = txtCorreoResponsable.Text;
                        responsable.Direccion = txtCorreoResponsable.Text;
                        responsable.Barrio = txtBarrioResponsable.Text;
                        responsable.Calle = txtCalleResponsable.Text;
                        responsable.Ciudad_id = Convert.ToInt32(cmbCiudadResponsable.SelectedValue);
                        responsable.Ocupacion = txtOcupacionResponsable.Text;

                        paciente.DocumentoID = txtCedulaPaciente.Text;
                        paciente.Primer_Nombre = txtNombrePaciente.Text;
                        paciente.Primer_Apellido = txtApellidoPaciente.Text;
                        paciente.Sexo = char.Parse(cmbSexo.SelectedItem.ToString());
                        paciente.Edad = int.Parse(txtEdadPaciente.Text);
                        paciente.Correo = txtCorreoPaciente.Text;
                        paciente.Telefono = txtTelefonoPaciente.Text;
                        paciente.Direccion = txtDireccionPaciente.Text;
                        paciente.Barrio = txtBarrioPaciente.Text;
                        paciente.Calle = txtCallePaciente.Text;
                        paciente.Ciudad_id = Convert.ToInt32(cmbCiudadPaciente.SelectedValue);
                        paciente.EPS_id = Convert.ToInt32(cmbEPS.SelectedValue);
                        paciente.Tipo_sangre = cmbTipoDeSangre.SelectedItem.ToString();
                        paciente.RH = char.Parse(cmbRH.SelectedItem.ToString());
                        paciente.Documento_responsable = responsable.DocumentoID;

                        bool responsableExitoso = servicioUsuario.RegistrarResponsable(responsable);
                        bool pacienteExitoso = servicioUsuario.RegistrarPaciente(paciente, username, password);

                        registroExitoso = responsableExitoso && pacienteExitoso;
                        break;

                    case "Doctor":

                        doctor.DocumentoID = txtCedula.Text;
                        doctor.Primer_Nombre = txtNombres.Text;
                        doctor.Primer_Apellido = txtApellidos.Text;
                        doctor.Especialidad_id = Convert.ToInt32(cmbEspecialidad.SelectedValue);
                        doctor.Telefono = txtTelefono.Text;
                        doctor.Correo = txtCorreo.Text;
                        doctor.Estado = "Activado".Trim();
                        doctor.NumeroLicencia = txtNumeroDeLicencia.Text;

                        Console.WriteLine(doctor.Estado);

                        registroExitoso = servicioUsuario.RegistrarDoctor(doctor, username, password);
                        break;
                    default:
                        MessageBox.Show("Rol no válido", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                if (registroExitoso)
                {
                    MessageBox.Show("Registro exitoso", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error en el registro", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el registro: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Registro_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlPaciente.Visible = false;
            pnlResponsable.Visible = true;
        }

        private void btnGuardarResponsable_Click(object sender, EventArgs e)
        {
            pnlPaciente.Visible = true;
            pnlResponsable.Visible = false;
        }
    }
}
