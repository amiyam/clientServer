namespace PetrolinkAppClient
{
    partial class client
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.dgdCurves = new System.Windows.Forms.DataGridView();
            this.SelCurve = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnXml = new System.Windows.Forms.Button();
            this.dgdData = new System.Windows.Forms.DataGridView();
            this.saveFileXML = new System.Windows.Forms.SaveFileDialog();
            this.grpData = new System.Windows.Forms.GroupBox();
            this.grpServer = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdCurves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdData)).BeginInit();
            this.grpData.SuspendLayout();
            this.grpServer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnConnect.Location = new System.Drawing.Point(381, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(132, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(90, 21);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(100, 23);
            this.txtHost.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(216, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(258, 21);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(59, 23);
            this.txtPort.TabIndex = 3;
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetrieve.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnRetrieve.Location = new System.Drawing.Point(210, 191);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(110, 23);
            this.btnRetrieve.TabIndex = 9;
            this.btnRetrieve.Text = "Retrieve Data";
            this.btnRetrieve.UseVisualStyleBackColor = true;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // grpList
            // 
            this.grpList.BackColor = System.Drawing.Color.PapayaWhip;
            this.grpList.Controls.Add(this.btnStop);
            this.grpList.Controls.Add(this.dgdCurves);
            this.grpList.Controls.Add(this.btnRetrieve);
            this.grpList.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpList.Location = new System.Drawing.Point(20, 77);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(691, 220);
            this.grpList.TabIndex = 10;
            this.grpList.TabStop = false;
            this.grpList.Text = "Curves List";
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnStop.Location = new System.Drawing.Point(80, 191);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(110, 23);
            this.btnStop.TabIndex = 11;
            this.btnStop.Text = "Stop Transfer";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // dgdCurves
            // 
            this.dgdCurves.AllowUserToAddRows = false;
            this.dgdCurves.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(1);
            this.dgdCurves.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgdCurves.BackgroundColor = System.Drawing.Color.PapayaWhip;
            this.dgdCurves.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgdCurves.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkTurquoise;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgdCurves.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgdCurves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdCurves.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelCurve});
            this.dgdCurves.GridColor = System.Drawing.Color.Silver;
            this.dgdCurves.Location = new System.Drawing.Point(80, 19);
            this.dgdCurves.Name = "dgdCurves";
            this.dgdCurves.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgdCurves.Size = new System.Drawing.Size(240, 166);
            this.dgdCurves.TabIndex = 10;
            this.dgdCurves.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgdCurves_CellValueChanged);
            this.dgdCurves.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgdCurves_CurrentCellDirtyStateChanged);
            this.dgdCurves.SelectionChanged += new System.EventHandler(this.dgdCurves_SelectionChanged);
            // 
            // SelCurve
            // 
            this.SelCurve.HeaderText = "Select";
            this.SelCurve.Name = "SelCurve";
            // 
            // btnXml
            // 
            this.btnXml.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXml.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnXml.Location = new System.Drawing.Point(544, 172);
            this.btnXml.Name = "btnXml";
            this.btnXml.Size = new System.Drawing.Size(137, 23);
            this.btnXml.TabIndex = 8;
            this.btnXml.Text = "Download Data";
            this.btnXml.UseVisualStyleBackColor = true;
            this.btnXml.Click += new System.EventHandler(this.btnXml_Click);
            // 
            // dgdData
            // 
            this.dgdData.AllowUserToAddRows = false;
            this.dgdData.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dgdData.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgdData.BackgroundColor = System.Drawing.Color.PapayaWhip;
            this.dgdData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgdData.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgdData.GridColor = System.Drawing.Color.Silver;
            this.dgdData.Location = new System.Drawing.Point(9, 22);
            this.dgdData.MultiSelect = false;
            this.dgdData.Name = "dgdData";
            this.dgdData.ReadOnly = true;
            this.dgdData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgdData.Size = new System.Drawing.Size(672, 150);
            this.dgdData.TabIndex = 9;
            // 
            // saveFileXML
            // 
            this.saveFileXML.InitialDirectory = "C:/";
            this.saveFileXML.Title = "Save Data";
            // 
            // grpData
            // 
            this.grpData.BackColor = System.Drawing.Color.PapayaWhip;
            this.grpData.Controls.Add(this.btnXml);
            this.grpData.Controls.Add(this.dgdData);
            this.grpData.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpData.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpData.Location = new System.Drawing.Point(20, 297);
            this.grpData.Name = "grpData";
            this.grpData.Size = new System.Drawing.Size(691, 201);
            this.grpData.TabIndex = 11;
            this.grpData.TabStop = false;
            this.grpData.Text = "Curves Data Points";
            // 
            // grpServer
            // 
            this.grpServer.BackColor = System.Drawing.Color.PapayaWhip;
            this.grpServer.Controls.Add(this.btnDisconnect);
            this.grpServer.Controls.Add(this.lblMsg);
            this.grpServer.Controls.Add(this.label2);
            this.grpServer.Controls.Add(this.txtPort);
            this.grpServer.Controls.Add(this.label1);
            this.grpServer.Controls.Add(this.txtHost);
            this.grpServer.Controls.Add(this.btnConnect);
            this.grpServer.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpServer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpServer.Location = new System.Drawing.Point(20, 15);
            this.grpServer.Name = "grpServer";
            this.grpServer.Size = new System.Drawing.Size(691, 56);
            this.grpServer.TabIndex = 12;
            this.grpServer.TabStop = false;
            this.grpServer.Text = "Server";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDisconnect.Location = new System.Drawing.Point(528, 21);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(132, 23);
            this.btnDisconnect.TabIndex = 17;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMsg.Location = new System.Drawing.Point(99, -12);
            this.lblMsg.MinimumSize = new System.Drawing.Size(580, 0);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(580, 16);
            this.lblMsg.TabIndex = 16;
            // 
            // client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PapayaWhip;
            this.ClientSize = new System.Drawing.Size(733, 510);
            this.Controls.Add(this.grpServer);
            this.Controls.Add(this.grpList);
            this.Controls.Add(this.grpData);
            this.Name = "client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.client_Load);
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgdCurves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgdData)).EndInit();
            this.grpData.ResumeLayout(false);
            this.grpServer.ResumeLayout(false);
            this.grpServer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.Button btnXml;
        private System.Windows.Forms.DataGridView dgdCurves;
        private System.Windows.Forms.DataGridView dgdData;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelCurve;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.SaveFileDialog saveFileXML;
        private System.Windows.Forms.GroupBox grpData;
        private System.Windows.Forms.GroupBox grpServer;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Button btnDisconnect;
    }
}

