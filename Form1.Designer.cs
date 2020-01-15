namespace AquateknikkUpdater
{
    partial class Form1
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
            this.btnTestConn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnChooseFlow = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnSendFile = new System.Windows.Forms.Button();
            this.lst_log = new System.Windows.Forms.CheckedListBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lbl_log = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.lblIp = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblport = new System.Windows.Forms.Label();
            this.lblToAuth = new System.Windows.Forms.Label();
            this.txtportstop = new System.Windows.Forms.TextBox();
            this.lblFromAut = new System.Windows.Forms.Label();
            this.txtportstart = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBackupFolder = new System.Windows.Forms.Button();
            this.lblSingle = new System.Windows.Forms.Label();
            this.lblMultiple = new System.Windows.Forms.Label();
            this.txtplant = new System.Windows.Forms.TextBox();
            this.lbl_anlegg = new System.Windows.Forms.Label();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.txtpass = new System.Windows.Forms.TextBox();
            this.lbluser = new System.Windows.Forms.Label();
            this.lblpass = new System.Windows.Forms.Label();
            this.chckNoUser = new System.Windows.Forms.CheckBox();
            this.btnUpdateAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestConn
            // 
            this.btnTestConn.Location = new System.Drawing.Point(216, 59);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(115, 38);
            this.btnTestConn.TabIndex = 0;
            this.btnTestConn.Text = "Test Conenction";
            this.btnTestConn.UseVisualStyleBackColor = true;
            this.btnTestConn.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(50, 274);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 20);
            this.button2.TabIndex = 1;
            this.button2.Text = "Backup Current flow";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Node-Red (*.json)|*.json|All files (*.*)|*.*";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnChooseFlow
            // 
            this.btnChooseFlow.Location = new System.Drawing.Point(50, 333);
            this.btnChooseFlow.Name = "btnChooseFlow";
            this.btnChooseFlow.Size = new System.Drawing.Size(212, 20);
            this.btnChooseFlow.TabIndex = 2;
            this.btnChooseFlow.Text = "Velg ønsket FLow fil";
            this.btnChooseFlow.UseVisualStyleBackColor = true;
            this.btnChooseFlow.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(268, 333);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(245, 20);
            this.txtFile.TabIndex = 3;
            // 
            // btnSendFile
            // 
            this.btnSendFile.Enabled = false;
            this.btnSendFile.Location = new System.Drawing.Point(53, 383);
            this.btnSendFile.Name = "btnSendFile";
            this.btnSendFile.Size = new System.Drawing.Size(134, 23);
            this.btnSendFile.TabIndex = 4;
            this.btnSendFile.Text = "Oppdater enkel";
            this.btnSendFile.UseVisualStyleBackColor = true;
            this.btnSendFile.Click += new System.EventHandler(this.button4_Click);
            // 
            // lst_log
            // 
            this.lst_log.Enabled = false;
            this.lst_log.FormattingEnabled = true;
            this.lst_log.Location = new System.Drawing.Point(557, 27);
            this.lst_log.Name = "lst_log";
            this.lst_log.Size = new System.Drawing.Size(220, 379);
            this.lst_log.TabIndex = 5;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "json";
            this.saveFileDialog1.FileName = "Flows";
            this.saveFileDialog1.Filter = "Node-Red (*.json)|*.json|All files (*.*)|*.*";
            // 
            // lbl_log
            // 
            this.lbl_log.AutoSize = true;
            this.lbl_log.Location = new System.Drawing.Point(634, 8);
            this.lbl_log.Name = "lbl_log";
            this.lbl_log.Size = new System.Drawing.Size(29, 13);
            this.lbl_log.TabIndex = 6;
            this.lbl_log.Text = "LOG";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(15, 77);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(100, 20);
            this.txtIp.TabIndex = 7;
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(15, 58);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(50, 13);
            this.lblIp.TabIndex = 9;
            this.lblIp.Text = "Ip adress";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(121, 77);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(66, 20);
            this.txtPort.TabIndex = 10;
            // 
            // lblport
            // 
            this.lblport.AutoSize = true;
            this.lblport.Location = new System.Drawing.Point(118, 58);
            this.lblport.Name = "lblport";
            this.lblport.Size = new System.Drawing.Size(26, 13);
            this.lblport.TabIndex = 11;
            this.lblport.Text = "Port";
            // 
            // lblToAuth
            // 
            this.lblToAuth.AutoSize = true;
            this.lblToAuth.Location = new System.Drawing.Point(433, 137);
            this.lblToAuth.Name = "lblToAuth";
            this.lblToAuth.Size = new System.Drawing.Size(50, 13);
            this.lblToAuth.TabIndex = 15;
            this.lblToAuth.Text = "To aut nr";
            // 
            // txtportstop
            // 
            this.txtportstop.Location = new System.Drawing.Point(419, 156);
            this.txtportstop.Name = "txtportstop";
            this.txtportstop.Size = new System.Drawing.Size(66, 20);
            this.txtportstop.TabIndex = 14;
            // 
            // lblFromAut
            // 
            this.lblFromAut.AutoSize = true;
            this.lblFromAut.Location = new System.Drawing.Point(353, 137);
            this.lblFromAut.Name = "lblFromAut";
            this.lblFromAut.Size = new System.Drawing.Size(60, 13);
            this.lblFromAut.TabIndex = 13;
            this.lblFromAut.Text = "From aut nr";
            // 
            // txtportstart
            // 
            this.txtportstart.Location = new System.Drawing.Point(352, 156);
            this.txtportstart.Name = "txtportstart";
            this.txtportstart.Size = new System.Drawing.Size(61, 20);
            this.txtportstart.TabIndex = 12;
            // 
            // btnBackupFolder
            // 
            this.btnBackupFolder.Location = new System.Drawing.Point(343, 185);
            this.btnBackupFolder.Name = "btnBackupFolder";
            this.btnBackupFolder.Size = new System.Drawing.Size(152, 20);
            this.btnBackupFolder.TabIndex = 16;
            this.btnBackupFolder.Text = "Backup folder ";
            this.btnBackupFolder.UseVisualStyleBackColor = true;
            this.btnBackupFolder.Click += new System.EventHandler(this.btnBackupFolder_Click);
            // 
            // lblSingle
            // 
            this.lblSingle.AutoSize = true;
            this.lblSingle.Location = new System.Drawing.Point(50, 243);
            this.lblSingle.Name = "lblSingle";
            this.lblSingle.Size = new System.Drawing.Size(51, 13);
            this.lblSingle.TabIndex = 17;
            this.lblSingle.Text = "One auth";
            // 
            // lblMultiple
            // 
            this.lblMultiple.AutoSize = true;
            this.lblMultiple.Location = new System.Drawing.Point(362, 118);
            this.lblMultiple.Name = "lblMultiple";
            this.lblMultiple.Size = new System.Drawing.Size(105, 13);
            this.lblMultiple.TabIndex = 18;
            this.lblMultiple.Text = "Backup multiple auts";
            // 
            // txtplant
            // 
            this.txtplant.Location = new System.Drawing.Point(15, 27);
            this.txtplant.Name = "txtplant";
            this.txtplant.Size = new System.Drawing.Size(144, 20);
            this.txtplant.TabIndex = 19;
            // 
            // lbl_anlegg
            // 
            this.lbl_anlegg.AutoSize = true;
            this.lbl_anlegg.Location = new System.Drawing.Point(15, 11);
            this.lbl_anlegg.Name = "lbl_anlegg";
            this.lbl_anlegg.Size = new System.Drawing.Size(69, 13);
            this.lbl_anlegg.TabIndex = 20;
            this.lbl_anlegg.Text = "Anleggsnavn";
            // 
            // txtuser
            // 
            this.txtuser.Location = new System.Drawing.Point(102, 156);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(100, 20);
            this.txtuser.TabIndex = 21;
            // 
            // txtpass
            // 
            this.txtpass.Location = new System.Drawing.Point(102, 182);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(100, 20);
            this.txtpass.TabIndex = 22;
            // 
            // lbluser
            // 
            this.lbluser.AutoSize = true;
            this.lbluser.Location = new System.Drawing.Point(30, 156);
            this.lbluser.Name = "lbluser";
            this.lbluser.Size = new System.Drawing.Size(55, 13);
            this.lbluser.TabIndex = 23;
            this.lbluser.Text = "Username";
            // 
            // lblpass
            // 
            this.lblpass.AutoSize = true;
            this.lblpass.Location = new System.Drawing.Point(30, 185);
            this.lblpass.Name = "lblpass";
            this.lblpass.Size = new System.Drawing.Size(52, 13);
            this.lblpass.TabIndex = 24;
            this.lblpass.Text = "password";
            // 
            // chckNoUser
            // 
            this.chckNoUser.AutoSize = true;
            this.chckNoUser.Location = new System.Drawing.Point(34, 133);
            this.chckNoUser.Name = "chckNoUser";
            this.chckNoUser.Size = new System.Drawing.Size(140, 17);
            this.chckNoUser.TabIndex = 25;
            this.chckNoUser.Text = "No User Login Required";
            this.chckNoUser.UseVisualStyleBackColor = true;
            this.chckNoUser.CheckedChanged += new System.EventHandler(this.chckNoUser_CheckedChanged);
            // 
            // btnUpdateAll
            // 
            this.btnUpdateAll.Enabled = false;
            this.btnUpdateAll.Location = new System.Drawing.Point(356, 383);
            this.btnUpdateAll.Name = "btnUpdateAll";
            this.btnUpdateAll.Size = new System.Drawing.Size(134, 23);
            this.btnUpdateAll.TabIndex = 26;
            this.btnUpdateAll.Text = "Oppdater ALLE";
            this.btnUpdateAll.UseVisualStyleBackColor = true;
            this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnUpdateAll);
            this.Controls.Add(this.chckNoUser);
            this.Controls.Add(this.lblpass);
            this.Controls.Add(this.lbluser);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.lbl_anlegg);
            this.Controls.Add(this.txtplant);
            this.Controls.Add(this.lblMultiple);
            this.Controls.Add(this.lblSingle);
            this.Controls.Add(this.btnBackupFolder);
            this.Controls.Add(this.lblToAuth);
            this.Controls.Add(this.txtportstop);
            this.Controls.Add(this.lblFromAut);
            this.Controls.Add(this.txtportstart);
            this.Controls.Add(this.lblport);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblIp);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.lbl_log);
            this.Controls.Add(this.lst_log);
            this.Controls.Add(this.btnSendFile);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.btnChooseFlow);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnTestConn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTestConn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnChooseFlow;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnSendFile;
        private System.Windows.Forms.CheckedListBox lst_log;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lbl_log;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblport;
        private System.Windows.Forms.Label lblToAuth;
        private System.Windows.Forms.TextBox txtportstop;
        private System.Windows.Forms.Label lblFromAut;
        private System.Windows.Forms.TextBox txtportstart;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnBackupFolder;
        private System.Windows.Forms.Label lblSingle;
        private System.Windows.Forms.Label lblMultiple;
        private System.Windows.Forms.TextBox txtplant;
        private System.Windows.Forms.Label lbl_anlegg;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.TextBox txtpass;
        private System.Windows.Forms.Label lbluser;
        private System.Windows.Forms.Label lblpass;
        private System.Windows.Forms.CheckBox chckNoUser;
        private System.Windows.Forms.Button btnUpdateAll;
    }
}

