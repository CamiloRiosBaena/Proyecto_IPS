namespace PRESENTACION
{
    partial class frmPacienteMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPacienteMain));
            this.pnlRegistrarCita = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbHora = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.cmbDoctor = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbTipoDeCita = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlConsultarCitas = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dgvCitas = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCorreo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSexo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblCedula = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTipoSangre = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblEps = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblBarrio = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblCiudad = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblResponsable = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCalle = new System.Windows.Forms.Label();
            this.pnlInfoUsuario = new System.Windows.Forms.Panel();
            this.pnlRegistrarCita.SuspendLayout();
            this.pnlConsultarCitas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlInfoUsuario.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRegistrarCita
            // 
            this.pnlRegistrarCita.Controls.Add(this.button2);
            this.pnlRegistrarCita.Controls.Add(this.label17);
            this.pnlRegistrarCita.Controls.Add(this.cmbHora);
            this.pnlRegistrarCita.Controls.Add(this.label15);
            this.pnlRegistrarCita.Controls.Add(this.dtpFecha);
            this.pnlRegistrarCita.Controls.Add(this.cmbDoctor);
            this.pnlRegistrarCita.Controls.Add(this.label16);
            this.pnlRegistrarCita.Controls.Add(this.cmbTipoDeCita);
            this.pnlRegistrarCita.Controls.Add(this.label14);
            this.pnlRegistrarCita.Location = new System.Drawing.Point(105, 12);
            this.pnlRegistrarCita.Name = "pnlRegistrarCita";
            this.pnlRegistrarCita.Size = new System.Drawing.Size(665, 271);
            this.pnlRegistrarCita.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(488, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Agendar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(223, 109);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(30, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Hora";
            // 
            // cmbHora
            // 
            this.cmbHora.FormattingEnabled = true;
            this.cmbHora.Location = new System.Drawing.Point(226, 133);
            this.cmbHora.Name = "cmbHora";
            this.cmbHora.Size = new System.Drawing.Size(121, 21);
            this.cmbHora.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(223, 34);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(37, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "Fecha";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Location = new System.Drawing.Point(226, 55);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(200, 20);
            this.dtpFecha.TabIndex = 6;
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.FormattingEnabled = true;
            this.cmbDoctor.Location = new System.Drawing.Point(50, 133);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new System.Drawing.Size(121, 21);
            this.cmbDoctor.TabIndex = 5;
            this.cmbDoctor.SelectedIndexChanged += new System.EventHandler(this.cmbDoctor_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(49, 109);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "Especialista";
            // 
            // cmbTipoDeCita
            // 
            this.cmbTipoDeCita.FormattingEnabled = true;
            this.cmbTipoDeCita.Location = new System.Drawing.Point(50, 58);
            this.cmbTipoDeCita.Name = "cmbTipoDeCita";
            this.cmbTipoDeCita.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoDeCita.TabIndex = 1;
            this.cmbTipoDeCita.SelectedIndexChanged += new System.EventHandler(this.cmbTipoDeCita_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(47, 34);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Tipo de Cita";
            // 
            // pnlConsultarCitas
            // 
            this.pnlConsultarCitas.Controls.Add(this.btnCancelar);
            this.pnlConsultarCitas.Controls.Add(this.dgvCitas);
            this.pnlConsultarCitas.Location = new System.Drawing.Point(105, 12);
            this.pnlConsultarCitas.Name = "pnlConsultarCitas";
            this.pnlConsultarCitas.Size = new System.Drawing.Size(665, 341);
            this.pnlConsultarCitas.TabIndex = 1;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(562, 248);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // dgvCitas
            // 
            this.dgvCitas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCitas.Location = new System.Drawing.Point(28, 16);
            this.dgvCitas.Name = "dgvCitas";
            this.dgvCitas.Size = new System.Drawing.Size(609, 224);
            this.dgvCitas.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Apartar Cita";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(15, 183);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 5;
            this.btnConsultar.Text = "Consultar ";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PRESENTACION.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(87, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(37, 61);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(35, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Correo ";
            // 
            // lblCorreo
            // 
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Location = new System.Drawing.Point(37, 117);
            this.lblCorreo.Name = "lblCorreo";
            this.lblCorreo.Size = new System.Drawing.Size(35, 13);
            this.lblCorreo.TabIndex = 3;
            this.lblCorreo.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Edad";
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Location = new System.Drawing.Point(223, 61);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(35, 13);
            this.lblEdad.TabIndex = 5;
            this.lblEdad.Text = "label1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(212, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Sexo";
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.Location = new System.Drawing.Point(223, 115);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(35, 13);
            this.lblSexo.TabIndex = 7;
            this.lblSexo.Text = "label1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Telefono";
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(39, 171);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(35, 13);
            this.lblTelefono.TabIndex = 9;
            this.lblTelefono.Text = "label1";
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.Location = new System.Drawing.Point(39, 227);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(35, 13);
            this.lblCedula.TabIndex = 10;
            this.lblCedula.Text = "label1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Cedula";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(208, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Tipo de sangre";
            // 
            // lblTipoSangre
            // 
            this.lblTipoSangre.AutoSize = true;
            this.lblTipoSangre.Location = new System.Drawing.Point(223, 171);
            this.lblTipoSangre.Name = "lblTipoSangre";
            this.lblTipoSangre.Size = new System.Drawing.Size(35, 13);
            this.lblTipoSangre.TabIndex = 13;
            this.lblTipoSangre.Text = "label1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(211, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "EPS";
            // 
            // lblEps
            // 
            this.lblEps.AutoSize = true;
            this.lblEps.Location = new System.Drawing.Point(223, 227);
            this.lblEps.Name = "lblEps";
            this.lblEps.Size = new System.Drawing.Size(35, 13);
            this.lblEps.TabIndex = 15;
            this.lblEps.Text = "label1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(341, 95);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Direccion";
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Location = new System.Drawing.Point(358, 117);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(35, 13);
            this.lblDireccion.TabIndex = 17;
            this.lblDireccion.Text = "label1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(341, 151);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Barrio";
            // 
            // lblBarrio
            // 
            this.lblBarrio.AutoSize = true;
            this.lblBarrio.Location = new System.Drawing.Point(358, 171);
            this.lblBarrio.Name = "lblBarrio";
            this.lblBarrio.Size = new System.Drawing.Size(35, 13);
            this.lblBarrio.TabIndex = 19;
            this.lblBarrio.Text = "label1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(341, 205);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Ciudad";
            // 
            // lblCiudad
            // 
            this.lblCiudad.AutoSize = true;
            this.lblCiudad.Location = new System.Drawing.Point(358, 227);
            this.lblCiudad.Name = "lblCiudad";
            this.lblCiudad.Size = new System.Drawing.Size(35, 13);
            this.lblCiudad.TabIndex = 21;
            this.lblCiudad.Text = "label1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(474, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "Responsable";
            // 
            // lblResponsable
            // 
            this.lblResponsable.AutoSize = true;
            this.lblResponsable.Location = new System.Drawing.Point(485, 61);
            this.lblResponsable.Name = "lblResponsable";
            this.lblResponsable.Size = new System.Drawing.Size(35, 13);
            this.lblResponsable.TabIndex = 23;
            this.lblResponsable.Text = "label1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(341, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Calle";
            // 
            // lblCalle
            // 
            this.lblCalle.AutoSize = true;
            this.lblCalle.Location = new System.Drawing.Point(358, 61);
            this.lblCalle.Name = "lblCalle";
            this.lblCalle.Size = new System.Drawing.Size(35, 13);
            this.lblCalle.TabIndex = 25;
            this.lblCalle.Text = "label1";
            // 
            // pnlInfoUsuario
            // 
            this.pnlInfoUsuario.Controls.Add(this.lblCalle);
            this.pnlInfoUsuario.Controls.Add(this.label4);
            this.pnlInfoUsuario.Controls.Add(this.lblResponsable);
            this.pnlInfoUsuario.Controls.Add(this.label13);
            this.pnlInfoUsuario.Controls.Add(this.lblCiudad);
            this.pnlInfoUsuario.Controls.Add(this.label12);
            this.pnlInfoUsuario.Controls.Add(this.lblBarrio);
            this.pnlInfoUsuario.Controls.Add(this.label11);
            this.pnlInfoUsuario.Controls.Add(this.lblDireccion);
            this.pnlInfoUsuario.Controls.Add(this.label10);
            this.pnlInfoUsuario.Controls.Add(this.lblEps);
            this.pnlInfoUsuario.Controls.Add(this.label9);
            this.pnlInfoUsuario.Controls.Add(this.lblTipoSangre);
            this.pnlInfoUsuario.Controls.Add(this.label8);
            this.pnlInfoUsuario.Controls.Add(this.label7);
            this.pnlInfoUsuario.Controls.Add(this.lblCedula);
            this.pnlInfoUsuario.Controls.Add(this.lblTelefono);
            this.pnlInfoUsuario.Controls.Add(this.label6);
            this.pnlInfoUsuario.Controls.Add(this.lblSexo);
            this.pnlInfoUsuario.Controls.Add(this.label5);
            this.pnlInfoUsuario.Controls.Add(this.lblEdad);
            this.pnlInfoUsuario.Controls.Add(this.label3);
            this.pnlInfoUsuario.Controls.Add(this.lblCorreo);
            this.pnlInfoUsuario.Controls.Add(this.label2);
            this.pnlInfoUsuario.Controls.Add(this.label1);
            this.pnlInfoUsuario.Controls.Add(this.lblNombre);
            this.pnlInfoUsuario.Location = new System.Drawing.Point(105, 12);
            this.pnlInfoUsuario.Name = "pnlInfoUsuario";
            this.pnlInfoUsuario.Size = new System.Drawing.Size(649, 271);
            this.pnlInfoUsuario.TabIndex = 2;
            // 
            // frmPacienteMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 425);
            this.Controls.Add(this.pnlConsultarCitas);
            this.Controls.Add(this.pnlRegistrarCita);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlInfoUsuario);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPacienteMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPacienteMain";
            this.pnlRegistrarCita.ResumeLayout(false);
            this.pnlRegistrarCita.PerformLayout();
            this.pnlConsultarCitas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCitas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlInfoUsuario.ResumeLayout(false);
            this.pnlInfoUsuario.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlRegistrarCita;
        private System.Windows.Forms.Panel pnlConsultarCitas;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbDoctor;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cmbTipoDeCita;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbHora;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.DataGridView dgvCitas;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCorreo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSexo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTipoSangre;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblEps;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblBarrio;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblCiudad;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblResponsable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCalle;
        private System.Windows.Forms.Panel pnlInfoUsuario;
    }
}