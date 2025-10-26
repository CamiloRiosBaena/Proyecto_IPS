namespace PRESENTACION
{
    partial class frmTipoRol
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTipoRol));
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.chkDoctor = new System.Windows.Forms.CheckBox();
            this.chkPaciente = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.LimeGreen;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.ForeColor = System.Drawing.SystemColors.Window;
            this.btnConfirmar.Location = new System.Drawing.Point(76, 326);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(125, 40);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "CONFIRMAR";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // chkDoctor
            // 
            this.chkDoctor.AutoSize = true;
            this.chkDoctor.BackColor = System.Drawing.SystemColors.Window;
            this.chkDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDoctor.Location = new System.Drawing.Point(13, 7);
            this.chkDoctor.Name = "chkDoctor";
            this.chkDoctor.Size = new System.Drawing.Size(121, 29);
            this.chkDoctor.TabIndex = 1;
            this.chkDoctor.Text = "DOCTOR";
            this.chkDoctor.UseVisualStyleBackColor = false;
            this.chkDoctor.CheckedChanged += new System.EventHandler(this.chkDoctor_CheckedChanged);
            // 
            // chkPaciente
            // 
            this.chkPaciente.AutoSize = true;
            this.chkPaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPaciente.Location = new System.Drawing.Point(12, 8);
            this.chkPaciente.Name = "chkPaciente";
            this.chkPaciente.Size = new System.Drawing.Size(135, 29);
            this.chkPaciente.TabIndex = 2;
            this.chkPaciente.Text = "PACIENTE";
            this.chkPaciente.UseVisualStyleBackColor = true;
            this.chkPaciente.CheckedChanged += new System.EventHandler(this.chkPaciente_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnConfirmar);
            this.panel1.Location = new System.Drawing.Point(42, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 393);
            this.panel1.TabIndex = 3;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chkDoctor);
            this.panel4.Location = new System.Drawing.Point(63, 224);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(163, 43);
            this.panel4.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkPaciente);
            this.panel3.Location = new System.Drawing.Point(63, 267);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(163, 46);
            this.panel3.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Location = new System.Drawing.Point(51, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(192, 183);
            this.panel2.TabIndex = 3;
            // 
            // frmTipoRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(373, 452);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmTipoRol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTipoRol";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTipoRol_FormClosed);
            this.Load += new System.EventHandler(this.frmTipoRol_Load);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.CheckBox chkDoctor;
        private System.Windows.Forms.CheckBox chkPaciente;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
    }
}