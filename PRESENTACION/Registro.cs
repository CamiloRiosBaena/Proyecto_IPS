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
    public partial class Registro : Form
    {
        private ServicioUsuario servicioUsuario;
        public Registro()
        {
            InitializeComponent();
            servicioUsuario = new ServicioUsuario();
            ConfigurarComboBox();
            OcultarPaneles();
        }

        private void ConfigurarComboBox()
        {
            cmbRol.SelectedIndex = 0;
            cmbSexo.SelectedIndex = 0;
            cmbTipoDeSangre.SelectedIndex = 0;
            cmbEPS.SelectedIndex = 0;
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
                        responsable.Nombre = txtNombreResponsable.Text.Trim() + " " + txtApellidoResponsable.Text.Trim();
                        responsable.Parentesco = txtParentesco.Text;
                        responsable.Telefono = txtTelefonoResponsable.Text;
                        responsable.Correo = txtCorreoResponsable.Text;
                        responsable.Direccion = txtCorreoResponsable.Text;
                        responsable.Ocupacion = txtOcupacionResponsable.Text;

                        paciente.DocumentoID = txtCedulaPaciente.Text;
                        paciente.Nombre = txtNombrePaciente.Text.Trim() + " " + txtApellidoPaciente.Text.Trim();
                        paciente.Sexo = char.Parse(cmbSexo.SelectedItem.ToString());
                        paciente.Edad = int.Parse(txtEdadPaciente.Text);
                        paciente.Correo = txtCorreoPaciente.Text;
                        paciente.Telefono = txtTelefonoPaciente.Text;
                        paciente.Direccion = txtDireccionPaciente.Text;
                        paciente.EPS = cmbEPS.SelectedItem.ToString();
                        paciente.Tipo_sangre = cmbTipoDeSangre.SelectedItem.ToString();
                        paciente.Id_responsable = responsable.DocumentoID;

                        bool responsableExitoso = servicioUsuario.RegistrarResponsable(responsable);
                        bool pacienteExitoso = servicioUsuario.RegistrarPaciente(paciente, username, password);

                        registroExitoso = responsableExitoso && pacienteExitoso;
                        break;

                    case "Doctor":

                        doctor.DocumentoID = txtCedula.Text;
                        doctor.Nombre = txtNombres.Text.Trim() + " " + txtApellidos.Text.Trim();
                        doctor.Especialidad = txtEspecialidad.Text;
                        doctor.Telefono = txtTelefono.Text;
                        doctor.Correo = txtCorreo.Text;
                        doctor.Estado = "Activado".Trim();

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
            Login login = new Login();
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
