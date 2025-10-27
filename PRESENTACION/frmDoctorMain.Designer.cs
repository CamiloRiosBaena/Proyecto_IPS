namespace PRESENTACION
{
    partial class frmDoctorMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDoctorMain));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlInfoDoctor = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblLicencia = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCedula = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblEstado = new System.Windows.Forms.Label();
            this.pnlCitasDoctor = new System.Windows.Forms.Panel();
            this.dgvCitas = new System.Windows.Forms.DataGridView();
            this.btnCitas = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.pnlHistoriaClinica = new System.Windows.Forms.Panel();
            this.btnCancelarProceso = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCitaId = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblFechaApertura = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDiagnostico = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTratamiento = new System.Windows.Forms.TextBox();
            this.btnCompletar = new System.Windows.Forms.Button();
            this.dgvHistorialPrevio = new System.Windows.Forms.DataGridView();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlInfoDoctor.SuspendLayout();
            this.pnlCitasDoctor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            this.pnlHistoriaClinica.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialPrevio)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PRESENTACION.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(87, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pnlInfoDoctor
            // 
            this.pnlInfoDoctor.Controls.Add(this.lblEstado);
            this.pnlInfoDoctor.Controls.Add(this.label6);
            this.pnlInfoDoctor.Controls.Add(this.lblEspecialidad);
            this.pnlInfoDoctor.Controls.Add(this.label3);
            this.pnlInfoDoctor.Controls.Add(this.lblLicencia);
            this.pnlInfoDoctor.Controls.Add(this.label5);
            this.pnlInfoDoctor.Controls.Add(this.lblCedula);
            this.pnlInfoDoctor.Controls.Add(this.label7);
            this.pnlInfoDoctor.Controls.Add(this.lblTelefono);
            this.pnlInfoDoctor.Controls.Add(this.label4);
            this.pnlInfoDoctor.Controls.Add(this.lblCorreo);
            this.pnlInfoDoctor.Controls.Add(this.label2);
            this.pnlInfoDoctor.Controls.Add(this.lblNombre);
            this.pnlInfoDoctor.Controls.Add(this.label1);
            this.pnlInfoDoctor.Location = new System.Drawing.Point(105, 12);
            this.pnlInfoDoctor.Name = "pnlInfoDoctor";
            this.pnlInfoDoctor.Size = new System.Drawing.Size(590, 214);
            this.pnlInfoDoctor.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(45, 47);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(35, 13);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "label2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Correo";
            // 
            // lblCorreo
            // 
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Location = new System.Drawing.Point(45, 111);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(35, 13);
            this.lblCorreo.TabIndex = 3;
            this.lblCorreo.Text = "label3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Telefono";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(45, 171);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(35, 13);
            this.lblTelefono.TabIndex = 5;
            this.lblTelefono.Text = "label5";
            // 
            // lblLicencia
            // 
            this.lblLicencia.AutoSize = true;
            this.lblLicencia.Location = new System.Drawing.Point(191, 111);
            this.lblLicencia.Name = "lblLicencia";
            this.lblLicencia.Size = new System.Drawing.Size(35, 13);
            this.lblLicencia.TabIndex = 9;
            this.lblLicencia.Text = "label3";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "N° Licencia";
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.Location = new System.Drawing.Point(191, 47);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(35, 13);
            this.lblCedula.TabIndex = 7;
            this.lblCedula.Text = "label2";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(178, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Cedula";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(292, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Especialidad/es";
            // 
            // lblEspecialidad
            // 
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Location = new System.Drawing.Point(305, 47);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(35, 13);
            this.lblEspecialidad.TabIndex = 11;
            this.lblEspecialidad.Text = "label3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(423, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Estado";
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Location = new System.Drawing.Point(428, 47);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(35, 13);
            this.lblEstado.TabIndex = 13;
            this.lblEstado.Text = "label3";
            // 
            // pnlCitasDoctor
            // 
            this.pnlCitasDoctor.Controls.Add(this.btnCancelar);
            this.pnlCitasDoctor.Controls.Add(this.dgvCitas);
            this.pnlCitasDoctor.Location = new System.Drawing.Point(105, 12);
            this.pnlCitasDoctor.Name = "pnlCitasDoctor";
            this.pnlCitasDoctor.Size = new System.Drawing.Size(665, 341);
            this.pnlCitasDoctor.TabIndex = 2;
            // 
            // dgvCitas
            // 
            this.dgvCitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCitas.Location = new System.Drawing.Point(28, 16);
            this.dgvCitas.Name = "dgvCitas";
            this.dgvCitas.Size = new System.Drawing.Size(609, 224);
            this.dgvCitas.TabIndex = 0;
            this.dgvCitas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCitas_CellDoubleClick);
            // 
            // btnCitas
            // 
            this.btnCitas.Location = new System.Drawing.Point(15, 143);
            this.btnCitas.Name = "btnCitas";
            this.btnCitas.Size = new System.Drawing.Size(75, 23);
            this.btnCitas.TabIndex = 3;
            this.btnCitas.Text = "Citas";
            this.btnCitas.UseVisualStyleBackColor = true;
            this.btnCitas.Click += new System.EventHandler(this.btnCitas_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(562, 248);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // pnlHistoriaClinica
            // 
            this.pnlHistoriaClinica.Controls.Add(this.label14);
            this.pnlHistoriaClinica.Controls.Add(this.dgvHistorialPrevio);
            this.pnlHistoriaClinica.Controls.Add(this.btnCompletar);
            this.pnlHistoriaClinica.Controls.Add(this.txtTratamiento);
            this.pnlHistoriaClinica.Controls.Add(this.label13);
            this.pnlHistoriaClinica.Controls.Add(this.txtObservaciones);
            this.pnlHistoriaClinica.Controls.Add(this.label12);
            this.pnlHistoriaClinica.Controls.Add(this.txtDiagnostico);
            this.pnlHistoriaClinica.Controls.Add(this.label10);
            this.pnlHistoriaClinica.Controls.Add(this.lblFechaApertura);
            this.pnlHistoriaClinica.Controls.Add(this.label11);
            this.pnlHistoriaClinica.Controls.Add(this.lblPaciente);
            this.pnlHistoriaClinica.Controls.Add(this.label9);
            this.pnlHistoriaClinica.Controls.Add(this.lblCitaId);
            this.pnlHistoriaClinica.Controls.Add(this.label8);
            this.pnlHistoriaClinica.Controls.Add(this.btnCancelarProceso);
            this.pnlHistoriaClinica.Location = new System.Drawing.Point(105, 12);
            this.pnlHistoriaClinica.Name = "pnlHistoriaClinica";
            this.pnlHistoriaClinica.Size = new System.Drawing.Size(665, 590);
            this.pnlHistoriaClinica.TabIndex = 5;
            // 
            // btnCancelarProceso
            // 
            this.btnCancelarProceso.Location = new System.Drawing.Point(550, 446);
            this.btnCancelarProceso.Name = "btnCancelarProceso";
            this.btnCancelarProceso.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarProceso.TabIndex = 4;
            this.btnCancelarProceso.Text = "Cancelar";
            this.btnCancelarProceso.UseVisualStyleBackColor = true;
            this.btnCancelarProceso.Click += new System.EventHandler(this.btnCancelarProceso_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Id Cita";
            // 
            // lblCitaId
            // 
            this.lblCitaId.AutoSize = true;
            this.lblCitaId.Location = new System.Drawing.Point(33, 47);
            this.lblCitaId.Name = "lblCitaId";
            this.lblCitaId.Size = new System.Drawing.Size(29, 13);
            this.lblCitaId.TabIndex = 6;
            this.lblCitaId.Text = "label";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 13);
            this.label9.TabIndex = 7;
            this.label9.Text = "Nombre Paciente";
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Location = new System.Drawing.Point(33, 111);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(29, 13);
            this.lblPaciente.TabIndex = 8;
            this.lblPaciente.Text = "label";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 9;
            this.label11.Text = "Fecha Apertura";
            // 
            // lblFechaApertura
            // 
            this.lblFechaApertura.AutoSize = true;
            this.lblFechaApertura.Location = new System.Drawing.Point(33, 171);
            this.lblFechaApertura.Name = "lblFechaApertura";
            this.lblFechaApertura.Size = new System.Drawing.Size(29, 13);
            this.lblFechaApertura.TabIndex = 10;
            this.lblFechaApertura.Text = "label";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 265);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Diagnostico";
            // 
            // txtDiagnostico
            // 
            this.txtDiagnostico.Location = new System.Drawing.Point(16, 289);
            this.txtDiagnostico.Multiline = true;
            this.txtDiagnostico.Name = "txtDiagnostico";
            this.txtDiagnostico.Size = new System.Drawing.Size(271, 67);
            this.txtDiagnostico.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(351, 257);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(78, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Observaciones";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(354, 289);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(271, 67);
            this.txtObservaciones.TabIndex = 14;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 377);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Tratamiento";
            // 
            // txtTratamiento
            // 
            this.txtTratamiento.Location = new System.Drawing.Point(16, 408);
            this.txtTratamiento.Multiline = true;
            this.txtTratamiento.Name = "txtTratamiento";
            this.txtTratamiento.Size = new System.Drawing.Size(271, 67);
            this.txtTratamiento.TabIndex = 16;
            // 
            // btnCompletar
            // 
            this.btnCompletar.Location = new System.Drawing.Point(550, 408);
            this.btnCompletar.Name = "btnCompletar";
            this.btnCompletar.Size = new System.Drawing.Size(75, 23);
            this.btnCompletar.TabIndex = 17;
            this.btnCompletar.Text = "Completar";
            this.btnCompletar.UseVisualStyleBackColor = true;
            this.btnCompletar.Click += new System.EventHandler(this.btnCompletar_Click);
            // 
            // dgvHistorialPrevio
            // 
            this.dgvHistorialPrevio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistorialPrevio.Location = new System.Drawing.Point(154, 42);
            this.dgvHistorialPrevio.Name = "dgvHistorialPrevio";
            this.dgvHistorialPrevio.Size = new System.Drawing.Size(471, 198);
            this.dgvHistorialPrevio.TabIndex = 18;
            this.dgvHistorialPrevio.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistorialPrevio_CellDoubleClick);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(539, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "Historias Clinicas";
            // 
            // frmDoctorMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 614);
            this.Controls.Add(this.pnlHistoriaClinica);
            this.Controls.Add(this.btnCitas);
            this.Controls.Add(this.pnlCitasDoctor);
            this.Controls.Add(this.pnlInfoDoctor);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDoctorMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDoctorMain";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlInfoDoctor.ResumeLayout(false);
            this.pnlInfoDoctor.PerformLayout();
            this.pnlCitasDoctor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            this.pnlHistoriaClinica.ResumeLayout(false);
            this.pnlHistoriaClinica.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorialPrevio)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlInfoDoctor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLicencia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblEspecialidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlCitasDoctor;
        private System.Windows.Forms.DataGridView dgvCitas;
        private System.Windows.Forms.Button btnCitas;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Panel pnlHistoriaClinica;
        private System.Windows.Forms.Button btnCancelarProceso;
        private System.Windows.Forms.Label lblCitaId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblFechaApertura;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtDiagnostico;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTratamiento;
        private System.Windows.Forms.Button btnCompletar;
        private System.Windows.Forms.DataGridView dgvHistorialPrevio;
        private System.Windows.Forms.Label label14;
    }
}