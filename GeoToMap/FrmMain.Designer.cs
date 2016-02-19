namespace GeoToMap
{
    partial class FrmMain
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.covariablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuraciónToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aCercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboGeocodificador = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCarpetaSalida = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colDireccion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colW = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colN = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colX = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colY = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMunicipio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colProvincia = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colRegion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPais = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colGeo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRegeo = new System.Windows.Forms.Button();
            this.checkCovariables = new System.Windows.Forms.CheckBox();
            this.btnSelDirectorio = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGoogleMaps = new System.Windows.Forms.Button();
            this.btnAnade = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.configuraciónToolStripMenuItem,
            this.ayudaToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(847, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.covariablesToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // covariablesToolStripMenuItem
            // 
            this.covariablesToolStripMenuItem.Name = "covariablesToolStripMenuItem";
            this.covariablesToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.covariablesToolStripMenuItem.Text = "Covariables";
            this.covariablesToolStripMenuItem.Click += new System.EventHandler(this.covariablesToolStripMenuItem_Click);
            // 
            // configuraciónToolStripMenuItem
            // 
            this.configuraciónToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraciónToolStripMenuItem1});
            this.configuraciónToolStripMenuItem.Name = "configuraciónToolStripMenuItem";
            this.configuraciónToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.configuraciónToolStripMenuItem.Text = "Configuración";
            // 
            // configuraciónToolStripMenuItem1
            // 
            this.configuraciónToolStripMenuItem1.Name = "configuraciónToolStripMenuItem1";
            this.configuraciónToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.configuraciónToolStripMenuItem1.Text = "Configuración";
            this.configuraciónToolStripMenuItem1.Click += new System.EventHandler(this.configuracionToolStripMenuItem1_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ayudaToolStripMenuItem1,
            this.aCercaDeToolStripMenuItem});
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            // 
            // ayudaToolStripMenuItem1
            // 
            this.ayudaToolStripMenuItem1.Name = "ayudaToolStripMenuItem1";
            this.ayudaToolStripMenuItem1.Size = new System.Drawing.Size(138, 22);
            this.ayudaToolStripMenuItem1.Text = "Ayuda";
            this.ayudaToolStripMenuItem1.Click += new System.EventHandler(this.ayudaToolStripMenuItem1_Click);
            // 
            // aCercaDeToolStripMenuItem
            // 
            this.aCercaDeToolStripMenuItem.Name = "aCercaDeToolStripMenuItem";
            this.aCercaDeToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.aCercaDeToolStripMenuItem.Text = "Acerca de ...";
            this.aCercaDeToolStripMenuItem.Click += new System.EventHandler(this.aCercaDeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(12, 20);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "*.*";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(619, 39);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(206, 23);
            this.btnGrabar.TabIndex = 19;
            this.btnGrabar.Text = "Generar Archivos Salida y SHP";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(834, 114);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(26, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "Geocodificador: ";
            // 
            // cboGeocodificador
            // 
            this.cboGeocodificador.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGeocodificador.FormattingEnabled = true;
            this.cboGeocodificador.Items.AddRange(new object[] {
            "GOOGLE MAPS",
            "BING MAPS",
            "DESAFIO"});
            this.cboGeocodificador.Location = new System.Drawing.Point(139, 3);
            this.cboGeocodificador.Name = "cboGeocodificador";
            this.cboGeocodificador.Size = new System.Drawing.Size(235, 28);
            this.cboGeocodificador.TabIndex = 23;
            this.cboGeocodificador.SelectedIndexChanged += new System.EventHandler(this.cboGeocodificador_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 482);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(834, 16);
            this.progressBar1.TabIndex = 24;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "carpeta de salida:";
            // 
            // txtCarpetaSalida
            // 
            this.txtCarpetaSalida.Location = new System.Drawing.Point(139, 37);
            this.txtCarpetaSalida.Name = "txtCarpetaSalida";
            this.txtCarpetaSalida.Size = new System.Drawing.Size(559, 20);
            this.txtCarpetaSalida.TabIndex = 26;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 73);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView1);
            this.splitContainer1.Size = new System.Drawing.Size(834, 330);
            this.splitContainer1.SplitterDistance = 114;
            this.splitContainer1.TabIndex = 27;
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDireccion,
            this.colW,
            this.colN,
            this.colX,
            this.colY,
            this.colMunicipio,
            this.colProvincia,
            this.colRegion,
            this.colPais,
            this.colGeo});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(834, 212);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            // 
            // colDireccion
            // 
            this.colDireccion.Text = "Dirección";
            this.colDireccion.Width = 226;
            // 
            // colW
            // 
            this.colW.Text = "Longitud";
            this.colW.Width = 92;
            // 
            // colN
            // 
            this.colN.Text = "Latitud";
            this.colN.Width = 90;
            // 
            // colX
            // 
            this.colX.Text = "X";
            this.colX.Width = 107;
            // 
            // colY
            // 
            this.colY.Text = "Y";
            this.colY.Width = 106;
            // 
            // colMunicipio
            // 
            this.colMunicipio.Text = "Municipio";
            this.colMunicipio.Width = 86;
            // 
            // colProvincia
            // 
            this.colProvincia.Text = "Provincia";
            this.colProvincia.Width = 85;
            // 
            // colRegion
            // 
            this.colRegion.Text = "Región";
            // 
            // colPais
            // 
            this.colPais.Text = "País";
            // 
            // colGeo
            // 
            this.colGeo.Text = "GeoReferenciador";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.progressBar1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 73F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(840, 501);
            this.tableLayoutPanel1.TabIndex = 28;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.btnRegeo);
            this.panel1.Controls.Add(this.checkCovariables);
            this.panel1.Controls.Add(this.btnSelDirectorio);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboGeocodificador);
            this.panel1.Controls.Add(this.txtCarpetaSalida);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(834, 64);
            this.panel1.TabIndex = 29;
            // 
            // btnRegeo
            // 
            this.btnRegeo.Location = new System.Drawing.Point(513, 6);
            this.btnRegeo.Name = "btnRegeo";
            this.btnRegeo.Size = new System.Drawing.Size(145, 23);
            this.btnRegeo.TabIndex = 29;
            this.btnRegeo.Text = "Volver a Georreferenciar";
            this.btnRegeo.UseVisualStyleBackColor = true;
            this.btnRegeo.Click += new System.EventHandler(this.btnRegeo_Click);
            // 
            // checkCovariables
            // 
            this.checkCovariables.AutoSize = true;
            this.checkCovariables.Location = new System.Drawing.Point(392, 10);
            this.checkCovariables.Name = "checkCovariables";
            this.checkCovariables.Size = new System.Drawing.Size(115, 17);
            this.checkCovariables.TabIndex = 28;
            this.checkCovariables.Text = "Añade Covariables";
            this.checkCovariables.UseVisualStyleBackColor = true;
            this.checkCovariables.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnSelDirectorio
            // 
            this.btnSelDirectorio.Location = new System.Drawing.Point(704, 35);
            this.btnSelDirectorio.Name = "btnSelDirectorio";
            this.btnSelDirectorio.Size = new System.Drawing.Size(44, 23);
            this.btnSelDirectorio.TabIndex = 27;
            this.btnSelDirectorio.Text = "...";
            this.btnSelDirectorio.UseVisualStyleBackColor = true;
            this.btnSelDirectorio.Click += new System.EventHandler(this.btnSelDirectorio_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.groupBox1.Controls.Add(this.btnGoogleMaps);
            this.groupBox1.Controls.Add(this.btnAnade);
            this.groupBox1.Controls.Add(this.btnGrabar);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 409);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(834, 67);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Acciones:";
            // 
            // btnGoogleMaps
            // 
            this.btnGoogleMaps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoogleMaps.Location = new System.Drawing.Point(676, 10);
            this.btnGoogleMaps.Name = "btnGoogleMaps";
            this.btnGoogleMaps.Size = new System.Drawing.Size(149, 23);
            this.btnGoogleMaps.TabIndex = 29;
            this.btnGoogleMaps.Text = "Ver en Google Maps";
            this.btnGoogleMaps.UseVisualStyleBackColor = true;
            this.btnGoogleMaps.Click += new System.EventHandler(this.btnGoogleMaps_Click);
            // 
            // btnAnade
            // 
            this.btnAnade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnade.Location = new System.Drawing.Point(9, 19);
            this.btnAnade.Name = "btnAnade";
            this.btnAnade.Size = new System.Drawing.Size(109, 23);
            this.btnAnade.TabIndex = 28;
            this.btnAnade.Text = "Añade Registro";
            this.btnAnade.UseVisualStyleBackColor = true;
            this.btnAnade.Click += new System.EventHandler(this.btnAnade_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 532);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Herramienta de Georreferenciación GeoToMap ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.FrmBuscaDireccion_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aCercaDeToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboGeocodificador;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCarpetaSalida;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader colDireccion;
        private System.Windows.Forms.ColumnHeader colW;
        private System.Windows.Forms.ColumnHeader colN;
        private System.Windows.Forms.ColumnHeader colX;
        private System.Windows.Forms.ColumnHeader colY;
        private System.Windows.Forms.ColumnHeader colMunicipio;
        private System.Windows.Forms.ColumnHeader colProvincia;
        private System.Windows.Forms.ColumnHeader colRegion;
        private System.Windows.Forms.ColumnHeader colPais;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAnade;
        private System.Windows.Forms.ColumnHeader colGeo;
        private System.Windows.Forms.Button btnGoogleMaps;
        private System.Windows.Forms.Button btnSelDirectorio;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuraciónToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem covariablesToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkCovariables;
        private System.Windows.Forms.Button btnRegeo;
    }
}

