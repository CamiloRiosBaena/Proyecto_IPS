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
                    MessageBox.Show($"Login exitoso\nRol: {tipoUsuario}", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            frmRegistro formularioRegistro = new frmRegistro();
            formularioRegistro.Show();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
