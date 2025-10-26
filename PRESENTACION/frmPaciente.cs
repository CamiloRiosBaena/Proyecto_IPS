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
    public partial class frmPaciente : Form
    {
        private Form _formTipoRol;
        private ServicioCredenciales servicioCredenciales;
        private ServicioResponsable servicioResponsable;
        private ServicioCiudad servicioCiudad;
        private ServicioEPS servicioEPS;

        public frmPaciente(Form frmTipoRol)
        {
            InitializeComponent();
            servicioCredenciales = new ServicioCredenciales();
            servicioResponsable = new ServicioResponsable();
            servicioCiudad = new ServicioCiudad();
            servicioEPS = new ServicioEPS();
            CargarComboBoxes();
            ConfigurarComboBox();
            OcultarPaneles();
            _formTipoRol = frmTipoRol;
        }

        private void ConfigurarComboBox()
        {
            cmbSexo.SelectedIndex = 0;
            cmbTipoDeSangre.SelectedIndex = 0;
            cmbEPS.SelectedIndex = 0;
        }

        private void CargarComboBoxes()
        {
            try
            {
                DataTable ciudadesPacientes = servicioCiudad.ObtenerParaCombo();

                cmbCiudadPaciente.DataSource = servicioCiudad.ObtenerParaCombo();
                cmbCiudadPaciente.DisplayMember = "Texto";
                cmbCiudadPaciente.ValueMember = "Id";
                cmbCiudadPaciente.SelectedIndex = -1;

                DataTable ciudadesResponsables = servicioCiudad.ObtenerParaCombo();

                cmbCiudadResponsable.DataSource = servicioCiudad.ObtenerParaCombo();
                cmbCiudadResponsable.DisplayMember = "Texto";
                cmbCiudadResponsable.ValueMember = "Id";
                cmbCiudadResponsable.SelectedIndex = -1;

                DataTable EPS = servicioEPS.ObtenerParaCombo();

                cmbEPS.DataSource = servicioEPS.ObtenerParaCombo();
                cmbEPS.DisplayMember = "Texto";
                cmbEPS.ValueMember = "Id";
                cmbEPS.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message,
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
//--------------------------------------------------------------------------------------------------------------------
        private void OcultarPaneles()
        {
            pnlPaciente.Visible = true;
            pnlResponsable.Visible = false;
        }
 //--------------------------------------------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            pnl_Paciente.Visible = false;
            pnlResponsable.Visible = true;
        }
//--------------------------------------------------------------------------------------------------------------------
        private void btnRegistroPaciente_Click(object sender, EventArgs e)
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

                        responsable.DocumentoID = txtCedulaResponsable.Text;
                        responsable.Primer_Nombre = txtNombreResponsable.Text;
                        responsable.Primer_Apellido = txtApellidoResponsable.Text;
                        responsable.Parentesco = txtParentesco.Text;
                        responsable.Telefono = txtTelefonoResponsable.Text;
                        responsable.Correo = txtCorreoResponsable.Text;
                        responsable.Direccion = txtDireccionResponsable.Text;
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

                        bool responsableExitoso = servicioResponsable.Insertar(responsable);
                        bool pacienteExitoso = servicioCredenciales.RegistrarPaciente(paciente, username, password);

                        registroExitoso = responsableExitoso && pacienteExitoso;

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
 //--------------------------------------------------------------------------------------------------------------------

        private void btnGuardarResponsable_Click(object sender, EventArgs e)
        {
            pnl_Paciente.Visible = true;
            pnlResponsable.Visible = false;
        }

        private void frmPaciente_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
        }
    }
}
