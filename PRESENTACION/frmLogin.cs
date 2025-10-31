using BLL;
using System;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class frmLogin : Form
    {
        private ServicioCredenciales servicioCredenciales;
        public frmLogin()
        {
            InitializeComponent();
            servicioCredenciales = new ServicioCredenciales();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                string tipoUsuario;
                bool loginExitoso = servicioCredenciales.IniciarSesion(username, password, out tipoUsuario);

                if (loginExitoso)
                {
                    string Documento = servicioCredenciales.ObtenerDocumentoPorUsuario(username, tipoUsuario);

                    if(tipoUsuario == "PACIENTE")
                    {
                        frmPacienteMenu formPacienteMenu = new frmPacienteMenu(Documento);
                        formPacienteMenu.Show(this);
                        this.Hide();
                    }
                    else if (tipoUsuario == "DOCTOR")
                    {
                        frmDoctorMenu formDoctorMenu = new frmDoctorMenu(Documento);
                        formDoctorMenu.Show(this);
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTipoRol formularioRol = new frmTipoRol();
            formularioRol.Show();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
