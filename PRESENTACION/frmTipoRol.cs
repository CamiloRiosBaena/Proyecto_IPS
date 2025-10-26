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
    public partial class frmTipoRol : Form
    {
        public frmTipoRol()
        {
            InitializeComponent();
        }

        private void frmTipoRol_Load(object sender, EventArgs e)
        {
            chkDoctor.Checked = false;
            chkPaciente.Checked = false;
        }

        private void chkDoctor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDoctor.Checked)
                chkPaciente.Checked = false;
        }


        private void chkPaciente_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPaciente.Checked)
                chkDoctor.Checked = false;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (!chkDoctor.Checked && !chkPaciente.Checked)
            {
                MessageBox.Show("Por favor selecciona un rol antes de continuar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (chkDoctor.Checked)
            {
                frmDoctor formDoctor = new frmDoctor(this);
                formDoctor.Show(this);
                this.Hide();
            }

            else if (chkPaciente.Checked)
            {
                frmPaciente formPaciente = new frmPaciente(this);
                formPaciente.Show(this);
                this.Hide();
            }
        }
        private void frmTipoRol_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
        }
    }
}
