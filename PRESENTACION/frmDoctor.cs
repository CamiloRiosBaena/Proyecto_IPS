using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ENTITY;

namespace PRESENTACION
{
    public partial class frmDoctor : Form
    {
        private Form _formTipoRol;
        private ServicioCredenciales servicioCredenciales;
        private ServicioEspecialidad servicioEspecialidad;
        public frmDoctor(Form frmTipoRol)
        {

            InitializeComponent();
            servicioCredenciales = new ServicioCredenciales();
            servicioEspecialidad = new ServicioEspecialidad();
            CargarEspecialidades();
            _formTipoRol = frmTipoRol;
        }

        private void CargarEspecialidades()
        {
            try
            {
                DataTable Especialidades = servicioEspecialidad.ObtenerParaCombo();

                cmbEspecialidad.DataSource = servicioEspecialidad.ObtenerParaCombo();
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

        private void btnRegistroDoctor_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmarPassword.Text;

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

            try
            {
                bool registroExitoso = false;


                        doctor.DocumentoID = txtCedula.Text;
                        doctor.Primer_Nombre = txtNombres.Text;
                        doctor.Primer_Apellido = txtApellidos.Text;
                        doctor.Especialidad_id = Convert.ToInt32(cmbEspecialidad.SelectedValue);
                        doctor.Telefono = txtTelefono.Text;
                        doctor.Correo = txtCorreo.Text;
                        doctor.Estado = "Activado".Trim();
                        doctor.NumeroLicencia = txtNumeroDeLicencia.Text;

                        Console.WriteLine(doctor.Estado);

                        registroExitoso = servicioCredenciales.RegistrarDoctor(doctor, username, password);

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

        private void frmDoctor_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
        }
    }
}
