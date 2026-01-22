using Renci.SshNet;
using System;
using System.IO;
using System.Windows.Forms;

namespace AquateknikkUpdater
{
    public partial class Form1 : Form
    {
        private Token Token;
        private readonly ApiClient api = new ApiClient();
        private string IPadress = "1.2.3.4";
        private string Port = "";
        private string User = "";
        private string Pass = "";
        private Boolean HasUser = true;
        private int StartAut = 1;
        private int StopAut = 1;
        private string Filepath = "";
        private string fileLocation = "";
        public Form1()
        {
            InitializeComponent();

            #region debugging

            btnChooseFlow.Enabled = true;
            btnSendFile.Enabled = true;
            btnUpdateAll.Enabled = true;

            #endregion debugging
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // check if there is connection and if username works

            GetInfo();
            var Login = api.CheckConnection(IPadress, Port);

            if (Login == "1")
            {
                messageBox("Login is required", "Login");
            }
            else if (Login == "2")
            {
                messageBox("No Login required", "No login");
                chckNoUser.Checked = true;
                txtuser.Enabled = false;
                txtpass.Enabled = false;
            }
            else if (Login == "-1")
            {
                messageBox("Check IP", "No connection");
            }
        }

        private static void messageBox(string Message, string Title)
        {
            string message = Message;
            string title = Title;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
        }

        private bool TryReadPort(out int portNumber)
        {
            if (!int.TryParse(txtPort.Text, out portNumber))
            {
                lst_log.Items.Add("Invalid port number");
                return false;
            }
            return true;
        }

        private void GetInfo()
        {
            IPadress = txtIp.Text;
            Port = txtPort.Text;
            User = txtuser.Text;
            Pass = txtpass.Text;
            HasUser = chckNoUser.Checked;

            if (TryReadPort(out var portnumber))
            {
                Token = api.Authenticate(IPadress, portnumber.ToString(), User, Pass);
            }
            else
            {
                Token = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var flow = "";
            GetInfo();

            if (Token == null)
            {
                flow = api.Get_Flows("", IPadress, Port);
            }
            else
            {
                flow = api.Get_Flows(Token.access_token, IPadress, Port);
            }

            var savefilelocation = "";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                savefilelocation = saveFileDialog1.FileName;
            }
            else
            {
                lst_log.Items.Add("Canceled: Current flow not backed up");
                return;
            }

            fileLocation = System.IO.Path.GetDirectoryName(savefilelocation);
            File.WriteAllText(savefilelocation, flow);
            lst_log.Items.Add("Current flow is backed up");
            btnChooseFlow.Enabled = true;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var tmp = (System.Windows.Forms.OpenFileDialog)sender;

            Filepath = tmp.FileName;

            txtFile.Text = Filepath;
            lst_log.Items.Add("File added");

            btnSendFile.Enabled = true;
            btnUpdateAll.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            GetInfo();
            openFileDialog1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)

        {
            GetInfo();
            if (Confirmation())
            {
                SendFlow(IPadress);
                lst_log.Items.Add("File sent");
            }
            else
            {
                lst_log.Items.Add("Canceled");
            }
        }

        private void SendFlow(string autIp)
        {
            if (string.IsNullOrWhiteSpace(txtFile.Text) || !File.Exists(txtFile.Text))
            {
                lst_log.Items.Add("No file selected");
                return;
            }

            string file = File.ReadAllText(txtFile.Text);

            if (!TryReadPort(out var portnumber))
            {
                return;
            }

            Token = api.Authenticate(autIp, portnumber.ToString(), User, Pass);
            if (Token == null)
            {
                api.Send_Flow(file, "", autIp, portnumber.ToString());
            }
            else
            {
                api.Send_Flow(file, Token.access_token, autIp, portnumber.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIp.Text = "192.168.100.220";
            txtPort.Text = "1880";

            //btnChooseFlow.Enabled = false;
        }
 
        private void btnBackupFolder_Click(object sender, EventArgs e)
        {
 
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                fileLocation = folderBrowserDialog1.SelectedPath;

                if (!int.TryParse(txtportstart.Text, out StartAut) || !int.TryParse(txtportstop.Text, out StopAut))
                {
                    lst_log.Items.Add("Invalid start/stop port numbers");
                    return;
                }

                GetInfo();
                for (int i = StartAut -1; i < StopAut; i++)
                {
                    int Autnumber = i +1;
                    var flow = "";
                    if (!TryReadPort(out var basePort))
                    {
                        return;
                    }
                    int portnumber = basePort + i;
                    Token = api.Authenticate(IPadress, portnumber.ToString(), User, Pass);
                    if (Token != null)
                    {
                        flow = api.Get_Flows(Token.access_token, IPadress, portnumber.ToString());
                    }
                    else
                    {
                        flow = api.Get_Flows("", IPadress, portnumber.ToString());
                    }

                    Save_Flows(fileLocation, Autnumber, flow);
                }
                btnChooseFlow.Enabled = true;
            }
        }

        private void Save_Flows(string fileLocation, int Autnumber, string flow)
        {
            if (flow == null)
            {
                lst_log.Items.Add("--- Aut: " + Autnumber + " NOT backed up");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtplant.Text))
                {
                    txtplant.Text = DateTime.Today.ToString("yyyy-MM-dd");
                }
                var folder = System.IO.Path.Combine(fileLocation, txtplant.Text);
                DirectoryInfo di = Directory.CreateDirectory(folder);

                var fileName = $"Backup_Aut{Autnumber}_{DateTime.Today:yyyy-MM-dd}.json";
                File.WriteAllText(System.IO.Path.Combine(folder, fileName), flow);
                lst_log.Items.Add("Aut: " + Autnumber + " backed up");
            }

            //return fileLocation;
        }

        private int autnr =50;

        private void chckNoUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chckNoUser.Checked == true)
            {
                txtuser.Enabled = false;
                txtpass.Enabled = false;
            }
            else
            {
                txtuser.Enabled = true;
                txtpass.Enabled = true;
            }
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtportstop.Text, out StopAut) || !int.TryParse(txtportstart.Text, out StartAut))
            {
                lst_log.Items.Add("Invalid start/stop port numbers");
                return;
            }

            GetInfo();
            if (Confirmation())
            {
                if (string.IsNullOrWhiteSpace(Filepath) || !File.Exists(Filepath))
                {
                    lst_log.Items.Add("No file selected");
                    return;
                }

                string file = File.ReadAllText(Filepath);
                for (int i = StartAut -1; i < StopAut; i++)
                {
                    var tmpIPadress = IPadress + (autnr + i);
 
                    SendFlow(tmpIPadress);

                    lst_log.Items.Add("---Aut Number " + i + " updated");
                }
                lst_log.Items.Add("All updated");
            }
            else
            {
                lst_log.Items.Add("Canceled");
            }
        }

        private bool Confirmation()
        {
            string message = "Sure you want to update ALL?";
            string title = "Confirm";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                //this.Close();

                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnsql_Click(object sender, EventArgs e)
        {
            BackupSql();
        }

        private void BackupSql()
        {
            string host = @"" + IPadress;
            string username = "pi";
            string password = "kalinka17";
            string remoteDirectory = "/home/pi/";

            using (var sshclient = new SshClient(host, username, password))
            {
                sshclient.Connect();
                SshCommand sc1 = sshclient.CreateCommand("ssh pi@192.168.250.220");
                sc1.Execute();
                SshCommand sc2 = sshclient.CreateCommand(password);
                sc2.Execute();
                string sshanswere = sc1.Result;
                string loginanswere = sc2.Result;

                SshCommand sc = sshclient.CreateCommand("mysqldump -u root -pkalinka17 fishfarm > fishfarmbackup.sql");
                sc.Execute();
                string answer = sc.Result;
            }

            using (var sftp = new SftpClient(host, username, password))
            {
                sftp.Connect();
                using (Stream fileStream = File.Create(System.IO.Path.Combine(fileLocation, txtplant.Text + "DB.sql")))
                {
                    sftp.DownloadFile("/home/pi/fishfarmbackup.sql", fileStream);
                }
            }

            lst_log.Items.Add("SQL Database backed up");
        }
    }
}