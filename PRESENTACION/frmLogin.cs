using BLL;
using System;
using System.Windows.Forms;

namespace PRESENTACION
{
    public partial class frmLogin : Form
    {
        private ServicioUsuario servicioUsuario;
        public frmLogin()
        {
            InitializeComponent();
            servicioUsuario = new ServicioUsuario();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                bool loginExitoso = servicioUsuario.Login(username, password);

                if (loginExitoso)
                {
                    string rol = servicioUsuario.ObtenerRolUsuario(username);

                    MessageBox.Show($"Login exitoso\nRol: {rol}", "Éxito",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //AbrirFormularioPrincipal(username, rol);
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
