﻿namespace GeoToMap
{
    partial class FrmModificaElemento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModificaElemento));
            this.labelDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.btnGeoCodifica = new System.Windows.Forms.Button();
            this.txtLat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.btnModifica = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labPais = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labRegion = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labProvincia = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labGeo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelDireccion
            // 
            this.labelDireccion.AutoSize = true;
            this.labelDireccion.Location = new System.Drawing.Point(12, 9);
            this.labelDireccion.Name = "labelDireccion";
            this.labelDireccion.Size = new System.Drawing.Size(55, 13);
            this.labelDireccion.TabIndex = 0;
            this.labelDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(13, 26);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(369, 20);
            this.txtDireccion.TabIndex = 1;
            this.txtDireccion.TextChanged += new System.EventHandler(this.txtDireccion_TextChanged);
            // 
            // btnGeoCodifica
            // 
            this.btnGeoCodifica.Location = new System.Drawing.Point(161, 113);
            this.btnGeoCodifica.Name = "btnGeoCodifica";
            this.btnGeoCodifica.Size = new System.Drawing.Size(79, 23);
            this.btnGeoCodifica.TabIndex = 2;
            this.btnGeoCodifica.Text = "Geocodifica";
            this.btnGeoCodifica.UseVisualStyleBackColor = true;
            this.btnGeoCodifica.Click += new System.EventHandler(this.btnGeoCodifica_Click);
            // 
            // txtLat
            // 
            this.txtLat.Location = new System.Drawing.Point(101, 165);
            this.txtLat.Name = "txtLat";
            this.txtLat.Size = new System.Drawing.Size(100, 20);
            this.txtLat.TabIndex = 3;
            this.txtLat.TextChanged += new System.EventHandler(this.txtDireccion_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Latitutd:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Longitud:";
            // 
            // txtLon
            // 
            this.txtLon.Location = new System.Drawing.Point(101, 190);
            this.txtLon.Name = "txtLon";
            this.txtLon.Size = new System.Drawing.Size(100, 20);
            this.txtLon.TabIndex = 6;
            this.txtLon.TextChanged += new System.EventHandler(this.txtDireccion_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "X:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Y:";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(251, 165);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(100, 20);
            this.txtX.TabIndex = 9;
            this.txtX.TextChanged += new System.EventHandler(this.txtDireccion_TextChanged);
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(251, 190);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(100, 20);
            this.txtY.TabIndex = 10;
            this.txtY.TextChanged += new System.EventHandler(this.txtDireccion_TextChanged);
            // 
            // btnModifica
            // 
            this.btnModifica.Location = new System.Drawing.Point(160, 219);
            this.btnModifica.Name = "btnModifica";
            this.btnModifica.Size = new System.Drawing.Size(79, 23);
            this.btnModifica.TabIndex = 11;
            this.btnModifica.Text = "Modifica";
            this.btnModifica.UseVisualStyleBackColor = true;
            this.btnModifica.Click += new System.EventHandler(this.btnModifica_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labPais);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.labRegion);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.labProvincia);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(15, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(367, 64);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // labPais
            // 
            this.labPais.AutoSize = true;
            this.labPais.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labPais.Location = new System.Drawing.Point(162, 44);
            this.labPais.Name = "labPais";
            this.labPais.Size = new System.Drawing.Size(0, 13);
            this.labPais.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(124, 44);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "PAÍS :";
            // 
            // labRegion
            // 
            this.labRegion.AutoSize = true;
            this.labRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labRegion.Location = new System.Drawing.Point(162, 28);
            this.labRegion.Name = "labRegion";
            this.labRegion.Size = new System.Drawing.Size(0, 13);
            this.labRegion.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Región (Nivel1 Administrativo) :";
            // 
            // labProvincia
            // 
            this.labProvincia.AutoSize = true;
            this.labProvincia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labProvincia.Location = new System.Drawing.Point(162, 11);
            this.labProvincia.Name = "labProvincia";
            this.labProvincia.Size = new System.Drawing.Size(0, 13);
            this.labProvincia.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Provincia (nivel2 admisitrativo) :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(89, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Geocodificador:";
            // 
            // labGeo
            // 
            this.labGeo.AutoSize = true;
            this.labGeo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labGeo.Location = new System.Drawing.Point(178, 144);
            this.labGeo.Name = "labGeo";
            this.labGeo.Size = new System.Drawing.Size(0, 13);
            this.labGeo.TabIndex = 14;
            // 
            // FrmModificaElemento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 247);
            this.Controls.Add(this.labGeo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnModifica);
            this.Controls.Add(this.txtY);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLat);
            this.Controls.Add(this.btnGeoCodifica);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.labelDireccion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmModificaElemento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modifica Elemento de Dirección";
            this.Load += new System.EventHandler(this.FrmModificaElemento_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelDireccion;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Button btnGeoCodifica;
        private System.Windows.Forms.TextBox txtLat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.Button btnModifica;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labPais;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labRegion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labProvincia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labGeo;
    }
}