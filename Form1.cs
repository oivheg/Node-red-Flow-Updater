using Renci.SshNet;
using System;
using System.IO;
using System.Windows.Forms;

namespace AquateknikkUpdater
{
    public partial class Form1 : Form
    {
        private Token Token;
        private readonly RestSharp api = new RestSharp();
        private string IPadress = "1.2.3.4";
        private string Port = "";
        private string User = "";
        private string Pass = "";
        private Boolean HasUser = true;
        private int StartAut = 1;
        private int StopAut = 1;
        private string Filepath = "";
      private string  fileLocation = "";
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
            // Token = api.Authenticate(IPadress, Port, User, Pass);

            if (Login == "1")
            {
                messageBox("Login is required", "login");
            }
            else if (Login == "2")
            {
                messageBox("No Login required", "NO login");
                chckNoUser.Checked = true;
                txtuser.Enabled = false;
                txtpass.Enabled = false;
            }
            else if (Login == "-1")
            {
                messageBox("Check IP", "No Conenction");
            }
        }

        private static void messageBox(string Message, string Title)
        {
            string message = Message;
            string title = Title;
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result = MessageBox.Show(message, title, buttons);
        }

        private void GetInfo()
        {
            IPadress = txtIp.Text;
            Port = txtPort.Text;
            User = txtuser.Text;
            Pass = txtpass.Text;
            HasUser = chckNoUser.Checked;

            int portnumber = int.Parse(Port);
         
            Token = api.Authenticate(IPadress, portnumber.ToString(), User, Pass);
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
           
            //saveFileDialog1.ShowDialog();
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
            //fileLocation = Got_Flow(fileLocation, 0, flow);
            File.WriteAllText(savefilelocation, flow);
            lst_log.Items.Add("Current flow is Backed up");
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

                lst_log.Items.Add("File Sendt ");
            }
            else
            {
                lst_log.Items.Add("Canceled");
            }
        }

        private void SendFlow(string autIp)
        {
            string file = File.ReadAllText(txtFile.Text);

            //JObject json = JObject.Parse(file);
            int portnumber = int.Parse(Port);
            Token = api.Authenticate(autIp, portnumber.ToString(), User, Pass);
            if (Token == null)
            {
                api.Send_Flow(file, "", autIp, Port);
            }
            else
            {
                api.Send_Flow(file, Token.access_token, autIp, Port);
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
          
            //saveFileDialog1.ShowDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                fileLocation = folderBrowserDialog1.SelectedPath;

                StartAut = int.Parse(txtportstart.Text);
                StopAut = int.Parse(txtportstop.Text);

                GetInfo();
                for (int i = StartAut - 1; i < StopAut; i++)
                {
                    int Autnumber = i + 1;
                    var flow = "";
                    int portnumber = int.Parse(Port);
                    portnumber += i;
                    Token = api.Authenticate(IPadress, portnumber.ToString(), User, Pass);
                    if (Token != null)
                    {
                        flow = api.Get_Flows(Token.access_token, IPadress, Port);
                    }
                    else
                    {
                        flow = api.Get_Flows("", IPadress, Port);
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
                if (txtplant.Text.Equals(""))
                {
                    txtplant.Text = DateTime.Today.ToShortDateString();
                }
                fileLocation += "\\" + txtplant.Text + "";
                DirectoryInfo di = Directory.CreateDirectory(fileLocation);

                File.WriteAllText(fileLocation + "/Backup_Aut" + Autnumber + "_" + DateTime.Today.ToShortDateString() + ".json", flow);
                lst_log.Items.Add("Aut: " + Autnumber + " backed up");
            }

            //return fileLocation;
        }

        private int autnr = 50;

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
            StopAut = int.Parse(txtportstop.Text);
            StartAut = int.Parse(txtportstart.Text);

            GetInfo();
            if (Confirmation())
            {
                string file = File.ReadAllText(Filepath);
                for (int i = StartAut - 1; i < StopAut; i++)
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
            //string host = @"192.168.250.5";
            //string username = "pi";
            //string password = "kalinka17";
            string username = "pi";
            string password = "kalinka17";
            string localFileName = System.IO.Path.GetFileName(@"localfilename");
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
                var files = sftp.ListDirectory(remoteDirectory);
                using (Stream fileStream = File.Create(fileLocation + "\\" + txtplant.Text + "DB.sql"))
                {
                    sftp.DownloadFile("/home/pi/fishfarmbackup.sql", fileStream);
                }
            }

            lst_log.Items.Add("SQL Database backed up");
        }
    }
}