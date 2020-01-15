using System;
using System.IO;
using System.Windows.Forms;

namespace AquateknikkUpdater
{
    public partial class Form1 : Form
    {
        private Token Token;
        private readonly RestSharp api = new RestSharp();
        private string IPadress = "";
        private string Port = "";
        private string User = "";
        private string Pass = "";
        private Boolean HasUser = true;
        private int StartAut = 1;
        private int StopAut = 1;

        public Form1()
        {
            InitializeComponent();
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
            else  if (Login == "2")
            {
                messageBox("No Login required", "NO login");
                chckNoUser.Checked = true;
                txtuser.Enabled = false;
                txtpass.Enabled = false;
            }else if (Login == "-1")
            {
                messageBox("Check IP", "No Conenction");
            }
            //if (HasUser != true)
            //{
            //    Token = api.Authenticate(IPadress, Port, User, Pass);
            //    lst_log.Items.Add("Token Received");
            //}
            //else
            //{
            //    lst_log.Items.Add("No User: No Token");
            //}
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var flow = "";
            GetInfo();
            Token = api.Authenticate(IPadress, Port, User, Pass);
            if (Token == null)
            {
                flow = api.Get_Flows("", IPadress, Port);
            }
            else
            {
                flow = api.Get_Flows(Token.access_token, IPadress, Port);
            }


            var fileLocation = "";
            //saveFileDialog1.ShowDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileLocation = saveFileDialog1.FileName;
            }
            else
            {
                lst_log.Items.Add("Canceled: Current flow not backed up");
                return;
            }


            //fileLocation = Got_Flow(fileLocation, 0, flow);
            File.WriteAllText(fileLocation, flow);
            lst_log.Items.Add("Current flow is Backed up");
            btnChooseFlow.Enabled = true;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var tmp = (System.Windows.Forms.OpenFileDialog)sender;

            var filpath = tmp.FileName;

            txtFile.Text = filpath;
            lst_log.Items.Add("File added");

            btnSendFile.Enabled = true;
            btnUpdateAll.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)

        {
            if (Confirmation())
            {
                string file = File.ReadAllText(txtFile.Text);

                //JObject json = JObject.Parse(file);

                if (Token == null)
                {
                    api.Send_Flow(file, "");
                }
                else
                {
                    api.Send_Flow(file, Token.access_token);
                }

                lst_log.Items.Add("File Sendt ");
            }
            else
            {
                lst_log.Items.Add("Canceled");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIp.Text = "192.168.250.5";
            txtPort.Text = "1880";

            btnChooseFlow.Enabled = false;
        }

        private void btnBackupFolder_Click(object sender, EventArgs e)
        {
            var fileLocation = "";
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
            if (Confirmation())
            {
                for (int i = StartAut - 1; i < StopAut; i++)
                {
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
    }
}