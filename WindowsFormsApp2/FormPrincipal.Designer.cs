namespace WindowsFormsApp2
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.txtVersion = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.lbStatus = new MaterialSkin.Controls.MaterialLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialRaisedButton2 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtPmtu = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.ckConsola = new MaterialSkin.Controls.MaterialCheckBox();
            this.materialRaisedButton3 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.dgvRecientes = new System.Windows.Forms.DataGridView();
            this.colNumServer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServidor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUbicacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConectados = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelServers = new MaterialSkin.Controls.MaterialLabel();
            this.lblLanPlay = new MaterialSkin.Controls.MaterialLabel();
            this.pnDatos = new System.Windows.Forms.Panel();
            this.labelDatos = new System.Windows.Forms.Label();
            this.cargaGrids = new System.ComponentModel.BackgroundWorker();
            this.ckInternet = new MaterialSkin.Controls.MaterialCheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecientes)).BeginInit();
            this.pnDatos.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(12, 502);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(121, 19);
            this.materialLabel1.TabIndex = 7;
            this.materialLabel1.Text = "Lan-Play Version";
            // 
            // txtVersion
            // 
            this.txtVersion.Depth = 0;
            this.txtVersion.Hint = "0.0.X";
            this.txtVersion.Location = new System.Drawing.Point(12, 524);
            this.txtVersion.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.PasswordChar = '\0';
            this.txtVersion.SelectedText = "";
            this.txtVersion.SelectionLength = 0;
            this.txtVersion.SelectionStart = 0;
            this.txtVersion.Size = new System.Drawing.Size(165, 23);
            this.txtVersion.TabIndex = 8;
            this.txtVersion.Text = "0.0.6";
            this.txtVersion.UseSystemPasswordChar = false;
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Depth = 0;
            this.lbStatus.Font = new System.Drawing.Font("Roboto", 11F);
            this.lbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbStatus.Location = new System.Drawing.Point(12, 547);
            this.lbStatus.MouseState = MaterialSkin.MouseState.HOVER;
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(0, 19);
            this.lbStatus.TabIndex = 9;
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.BackColor = System.Drawing.SystemColors.Control;
            this.materialRaisedButton1.Cursor = System.Windows.Forms.Cursors.Help;
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Location = new System.Drawing.Point(415, 502);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(122, 35);
            this.materialRaisedButton1.TabIndex = 23;
            this.materialRaisedButton1.Text = "Conect";
            this.toolTip1.SetToolTip(this.materialRaisedButton1, "Connect to the selected server");
            this.materialRaisedButton1.UseVisualStyleBackColor = false;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // materialRaisedButton2
            // 
            this.materialRaisedButton2.BackColor = System.Drawing.SystemColors.Control;
            this.materialRaisedButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.materialRaisedButton2.Depth = 0;
            this.materialRaisedButton2.Location = new System.Drawing.Point(444, 75);
            this.materialRaisedButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton2.Name = "materialRaisedButton2";
            this.materialRaisedButton2.Primary = true;
            this.materialRaisedButton2.Size = new System.Drawing.Size(93, 35);
            this.materialRaisedButton2.TabIndex = 27;
            this.materialRaisedButton2.Text = "Reload Servers";
            this.toolTip1.SetToolTip(this.materialRaisedButton2, "Reload the server list");
            this.materialRaisedButton2.UseVisualStyleBackColor = false;
            this.materialRaisedButton2.Click += new System.EventHandler(this.materialRaisedButton2_Click);
            // 
            // txtPmtu
            // 
            this.txtPmtu.Depth = 0;
            this.txtPmtu.Hint = "0.0.X";
            this.txtPmtu.Location = new System.Drawing.Point(186, 524);
            this.txtPmtu.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtPmtu.Name = "txtPmtu";
            this.txtPmtu.PasswordChar = '\0';
            this.txtPmtu.SelectedText = "";
            this.txtPmtu.SelectionLength = 0;
            this.txtPmtu.SelectionStart = 0;
            this.txtPmtu.Size = new System.Drawing.Size(41, 23);
            this.txtPmtu.TabIndex = 29;
            this.txtPmtu.Text = "1470";
            this.toolTip1.SetToolTip(this.txtPmtu, "mtu that will be used for the connection,");
            this.txtPmtu.UseSystemPasswordChar = false;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.linkLabel2.Location = new System.Drawing.Point(186, 503);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(41, 18);
            this.linkLabel2.TabIndex = 30;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "MTU";
            this.toolTip1.SetToolTip(this.linkLabel2, "Redirect to a tool to validate your ideal mtu");
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // ckConsola
            // 
            this.ckConsola.AutoSize = true;
            this.ckConsola.Depth = 0;
            this.ckConsola.Font = new System.Drawing.Font("Roboto", 10F);
            this.ckConsola.Location = new System.Drawing.Point(230, 524);
            this.ckConsola.Margin = new System.Windows.Forms.Padding(0);
            this.ckConsola.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ckConsola.MouseState = MaterialSkin.MouseState.HOVER;
            this.ckConsola.Name = "ckConsola";
            this.ckConsola.Ripple = true;
            this.ckConsola.Size = new System.Drawing.Size(118, 30);
            this.ckConsola.TabIndex = 31;
            this.ckConsola.Text = "Show Console";
            this.toolTip1.SetToolTip(this.ckConsola, "Show the information of the lan-play console");
            this.ckConsola.UseVisualStyleBackColor = true;
            this.ckConsola.CheckedChanged += new System.EventHandler(this.ckConsola_CheckedChanged);
            // 
            // materialRaisedButton3
            // 
            this.materialRaisedButton3.BackColor = System.Drawing.SystemColors.Control;
            this.materialRaisedButton3.Cursor = System.Windows.Forms.Cursors.Help;
            this.materialRaisedButton3.Depth = 0;
            this.materialRaisedButton3.Location = new System.Drawing.Point(459, 543);
            this.materialRaisedButton3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton3.Name = "materialRaisedButton3";
            this.materialRaisedButton3.Primary = true;
            this.materialRaisedButton3.Size = new System.Drawing.Size(78, 23);
            this.materialRaisedButton3.TabIndex = 12;
            this.materialRaisedButton3.Text = "Credits";
            this.materialRaisedButton3.UseVisualStyleBackColor = false;
            this.materialRaisedButton3.Click += new System.EventHandler(this.materialRaisedButton3_Click);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(6, 16);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(64, 19);
            this.materialLabel2.TabIndex = 15;
            this.materialLabel2.Text = "Servidor";
            // 
            // dgvRecientes
            // 
            this.dgvRecientes.AllowUserToAddRows = false;
            this.dgvRecientes.AllowUserToDeleteRows = false;
            this.dgvRecientes.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvRecientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumServer,
            this.colServidor,
            this.colUbicacion,
            this.colEstatus,
            this.colConectados,
            this.colPing});
            this.dgvRecientes.Location = new System.Drawing.Point(31, 113);
            this.dgvRecientes.MultiSelect = false;
            this.dgvRecientes.Name = "dgvRecientes";
            this.dgvRecientes.ReadOnly = true;
            this.dgvRecientes.RowHeadersVisible = false;
            this.dgvRecientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecientes.Size = new System.Drawing.Size(506, 383);
            this.dgvRecientes.TabIndex = 24;
            // 
            // colNumServer
            // 
            this.colNumServer.HeaderText = "No.";
            this.colNumServer.Name = "colNumServer";
            this.colNumServer.ReadOnly = true;
            this.colNumServer.Width = 75;
            // 
            // colServidor
            // 
            this.colServidor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colServidor.HeaderText = "Servidor";
            this.colServidor.Name = "colServidor";
            this.colServidor.ReadOnly = true;
            // 
            // colUbicacion
            // 
            this.colUbicacion.HeaderText = "Ubicación";
            this.colUbicacion.Name = "colUbicacion";
            this.colUbicacion.ReadOnly = true;
            // 
            // colEstatus
            // 
            this.colEstatus.HeaderText = "Estatus";
            this.colEstatus.Name = "colEstatus";
            this.colEstatus.ReadOnly = true;
            this.colEstatus.Width = 65;
            // 
            // colConectados
            // 
            this.colConectados.HeaderText = "Conetados";
            this.colConectados.Name = "colConectados";
            this.colConectados.ReadOnly = true;
            this.colConectados.Width = 60;
            // 
            // colPing
            // 
            this.colPing.HeaderText = "Latencia";
            this.colPing.Name = "colPing";
            this.colPing.ReadOnly = true;
            this.colPing.Width = 55;
            // 
            // labelServers
            // 
            this.labelServers.AutoSize = true;
            this.labelServers.Depth = 0;
            this.labelServers.Font = new System.Drawing.Font("Roboto", 11F);
            this.labelServers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelServers.Location = new System.Drawing.Point(31, 91);
            this.labelServers.MouseState = MaterialSkin.MouseState.HOVER;
            this.labelServers.Name = "labelServers";
            this.labelServers.Size = new System.Drawing.Size(138, 19);
            this.labelServers.TabIndex = 20;
            this.labelServers.Text = "Servers (Loading...)";
            // 
            // lblLanPlay
            // 
            this.lblLanPlay.AutoSize = true;
            this.lblLanPlay.Depth = 0;
            this.lblLanPlay.Font = new System.Drawing.Font("Roboto", 11F);
            this.lblLanPlay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLanPlay.Location = new System.Drawing.Point(583, 91);
            this.lblLanPlay.MouseState = MaterialSkin.MouseState.HOVER;
            this.lblLanPlay.Name = "lblLanPlay";
            this.lblLanPlay.Size = new System.Drawing.Size(95, 19);
            this.lblLanPlay.TabIndex = 25;
            this.lblLanPlay.Text = "Lan Play Info";
            // 
            // pnDatos
            // 
            this.pnDatos.AutoScroll = true;
            this.pnDatos.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnDatos.Controls.Add(this.labelDatos);
            this.pnDatos.Location = new System.Drawing.Point(587, 113);
            this.pnDatos.Name = "pnDatos";
            this.pnDatos.Size = new System.Drawing.Size(532, 383);
            this.pnDatos.TabIndex = 26;
            // 
            // labelDatos
            // 
            this.labelDatos.AutoSize = true;
            this.labelDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.labelDatos.ForeColor = System.Drawing.Color.White;
            this.labelDatos.Location = new System.Drawing.Point(4, 4);
            this.labelDatos.Name = "labelDatos";
            this.labelDatos.Size = new System.Drawing.Size(287, 18);
            this.labelDatos.TabIndex = 28;
            this.labelDatos.Text = "Here is what launches the lan play console";
            // 
            // cargaGrids
            // 
            this.cargaGrids.WorkerReportsProgress = true;
            this.cargaGrids.DoWork += new System.ComponentModel.DoWorkEventHandler(this.cargaGrids_DoWork);
            this.cargaGrids.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.cargaGrids_ProgressChanged);
            this.cargaGrids.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.cargaGrids_RunWorkerCompleted);
            // 
            // ckInternet
            // 
            this.ckInternet.AutoSize = true;
            this.ckInternet.Checked = true;
            this.ckInternet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckInternet.Depth = 0;
            this.ckInternet.Font = new System.Drawing.Font("Roboto", 10F);
            this.ckInternet.Location = new System.Drawing.Point(230, 499);
            this.ckInternet.Margin = new System.Windows.Forms.Padding(0);
            this.ckInternet.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ckInternet.MouseState = MaterialSkin.MouseState.HOVER;
            this.ckInternet.Name = "ckInternet";
            this.ckInternet.Ripple = true;
            this.ckInternet.Size = new System.Drawing.Size(111, 30);
            this.ckInternet.TabIndex = 32;
            this.ckInternet.Text = "Fake-internet";
            this.ckInternet.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(416, 34);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 33;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 575);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ckInternet);
            this.Controls.Add(this.ckConsola);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.txtPmtu);
            this.Controls.Add(this.materialRaisedButton2);
            this.Controls.Add(this.pnDatos);
            this.Controls.Add(this.lblLanPlay);
            this.Controls.Add(this.dgvRecientes);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.labelServers);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.materialRaisedButton3);
            this.Controls.Add(this.lbStatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(7, 227);
            this.MaximizeBox = false;
            this.Name = "FormPrincipal";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lan Play Server Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.FormPrincipal_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecientes)).EndInit();
            this.pnDatos.ResumeLayout(false);
            this.pnDatos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtVersion;
        private MaterialSkin.Controls.MaterialLabel lbStatus;
        private System.Windows.Forms.ToolTip toolTip1;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton3;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private System.Windows.Forms.DataGridView dgvRecientes;
        private MaterialSkin.Controls.MaterialLabel labelServers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumServer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServidor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUbicacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConectados;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPing;
        private MaterialSkin.Controls.MaterialLabel lblLanPlay;
        private System.Windows.Forms.Panel pnDatos;
        private System.Windows.Forms.Label labelDatos;
        private System.ComponentModel.BackgroundWorker cargaGrids;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton2;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtPmtu;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private MaterialSkin.Controls.MaterialCheckBox ckConsola;
        private MaterialSkin.Controls.MaterialCheckBox ckInternet;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

