using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCI_Project
{
    public partial class frmMain : Form
    {
        string power = "off";
        int volume = 0;
        int channel = 3;
        string captions = "off";
        string A = "not configured";
        string B = "not configured";
        string C = "not configured";
        string D = "not configured";
        string muted = "off";
        string[] Inputs = { "Cable", "Satellite", "HDMI" };
        int currInput = 0;
        string menu = "closed";
        string list = "closed";
        int oldChannel = 3;

        string oldv = "";
        string newv = "";
        bool setupMode = false;
        string currBinding = "";
        string finalBind = "";
        string bindFor = "";
        System.Timers.Timer t = new System.Timers.Timer(5000);

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            t.Elapsed += PollUpdates;
            tbxTvStateOld.Text = tbxTvState.Text;
            tbxTvState.Text = "Power: " + power;
            tbxTvState.Text += "\r\nVolume: " + volume.ToString();
            tbxTvState.Text += "\r\nChannel: " + channel.ToString();
            tbxTvState.Text += "\r\nCaptions: " + captions;
            tbxTvState.Text += "\r\nA: " + A;
            tbxTvState.Text += "\r\nB: " + B;
            tbxTvState.Text += "\r\nC: " + C;
            tbxTvState.Text += "\r\nD: " + D;
            tbxTvState.Text += "\r\nMuted: " + muted;
            tbxTvState.Text += "\r\nInput: " + Inputs[currInput];
            tbxTvState.Text += "\r\nMenu: " + menu;
            tbxTvState.Text += "\r\nList: " + list;
            treeView1.ExpandAll();
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            tbxTvStateOld.Text = tbxTvState.Text;
            tbxTvState.Text = "Power: " + power;
            tbxTvState.Text += "\r\nVolume: " + volume.ToString();
            tbxTvState.Text += "\r\nChannel: " + channel.ToString();
            tbxTvState.Text += "\r\nCaptions: " + captions;
            tbxTvState.Text += "\r\nA: " + A;
            tbxTvState.Text += "\r\nB: " + B;
            tbxTvState.Text += "\r\nC: " + C;
            tbxTvState.Text += "\r\nD: " + D;
            tbxTvState.Text += "\r\nMuted: " + muted;
            tbxTvState.Text += "\r\nInput: " + Inputs[currInput];
            tbxTvState.Text += "\r\nMenu: " + menu;
            tbxTvState.Text += "\r\nList: " + list;
        }
        #region State Buttons
        private void btnPower_Click(object sender, EventArgs e)
        {
            oldv = power;
            if (power == "off")
                power = "on";
            else
                power = "off";
            newv = power;
            PrintToLog("Power", oldv, newv);
        }

        private void btnVolUp_Click(object sender, EventArgs e)
        {
            oldv = volume.ToString();
            if (volume < 100)
                volume++;
            newv = volume.ToString();
            PrintToLog("Volume", oldv, newv);
        }

        private void btnVolDown_Click(object sender, EventArgs e)
        {
            oldv = volume.ToString();
            if (volume > 0)
                volume--;
            newv = volume.ToString();
            PrintToLog("Volume", oldv, newv);
        }

        private void btnChUp_Click(object sender, EventArgs e)
        {
            oldv = channel.ToString();
            oldChannel = channel;
            if (channel < 100)
                channel++;
            else
                channel = 1;
            newv = channel.ToString();
            PrintToLog("Channel", oldv, newv);
        }

        private void btnChDown_Click(object sender, EventArgs e)
        {
            oldv = channel.ToString();
            oldChannel = channel;
            if (channel > 1)
                channel--;
            else
                channel = 100;
            newv = channel.ToString();
            PrintToLog("Channel", oldv, newv);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            oldv = channel.ToString();
            int temp = channel;
            channel = oldChannel;
            oldChannel = temp;
            if (channel > 1)
                channel--;
            else
                channel = 100;
            newv = channel.ToString();
            PrintToLog("Channel", oldv, newv);
        }

        private void btnCaptions_Click(object sender, EventArgs e)
        {
            oldv = captions;
            if (captions == "off")
                captions = "on";
            else
                captions = "off";
            newv = captions;
            PrintToLog("Captions", oldv, newv);
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            oldv = channel.ToString();
            oldChannel = channel;
            if (A == "not configured")
            { /*do nothing*/}
            else
                channel = int.Parse(A);
            newv = channel.ToString();
            BindFor("A");
            PrintToLog("Channel", oldv, newv);
        }

        private void btnB_Click(object sender, EventArgs e)
        {
            oldv = channel.ToString();
            oldChannel = channel;
            if (B == "not configured")
            { /*do nothing*/}
            else
                channel = int.Parse(B);
            newv = channel.ToString();
            BindFor("B");
            PrintToLog("Channel", oldv, newv);
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            oldv = channel.ToString();
            oldChannel = channel;
            if (C == "not configured")
            { /*do nothing*/}
            else
                channel = int.Parse(C);
            newv = channel.ToString();
            BindFor("C");
            PrintToLog("Channel", oldv, newv);
        }

        private void btnD_Click(object sender, EventArgs e)
        {
            oldv = channel.ToString();
            oldChannel = channel;
            if (D == "not configured")
            { /*do nothing*/}
            else
                channel = int.Parse(D);
            newv = channel.ToString();
            BindFor("D");
            PrintToLog("Channel", oldv, newv);
        }

        private void btnMute_Click(object sender, EventArgs e)
        {
            oldv = muted;
            if (muted == "off")
                muted = "on";
            else
                muted = "off";
            newv = muted;
            PrintToLog("Muted", oldv, newv);
        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            oldv = Inputs[currInput];
            currInput = (currInput++) % 3;
            newv = Inputs[currInput];
            PrintToLog("Input", oldv, newv);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            oldv = menu;
            if (menu == "closed")
                menu = "open";
            else
                menu = "closed";
            newv = menu;
            PrintToLog("Menu", oldv, newv);
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            oldv = list;
            if (list == "closed")
                list = "open";
            else
                list = "closed";
            newv = list;
            PrintToLog("List", oldv, newv);
        }
        #endregion

        private void btnCover_Click(object sender, EventArgs e)
        {
            pbxCover.Visible = !pbxCover.Visible;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            gbxInfo.Visible = !gbxInfo.Visible;
        }

        public void PrintToLog(string changed, string oldVal, string newVal)
        {
            if (setupMode)
            { }
            else if (oldVal == newVal)
                tbxPresses.Text += changed + " -- unchanged\r\n";
            else
                tbxPresses.Text += changed + " -- from: " + oldVal + " to: " + newVal + "\r\n";

        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            tbxPresses.Text = "";
        }

        private void btnSetup_Click(object sender, EventArgs e)
        {
            currBinding = "";
            setupMode = true;
            finalBind = "";

            t.Start();
        }
        private void PollUpdates(object sender, EventArgs e)
        {
            setupMode = false;
            finalBind = currBinding.Length > 3 ? currBinding.Substring(0, 3) : currBinding;
            switch (bindFor)
            {
                case "A":
                    oldv =A;
                    A = finalBind;
                    btnA.Text = finalBind;
                    break;
                case "B":
                    oldv =B;
                    B = finalBind;
                    btnB.Text = finalBind;
                    break;
                case "C":
                    oldv =C;
                    C = finalBind;
                    btnC.Text = finalBind;
                    break;
                case "D":
                    oldv =D;
                    D = finalBind;
                    btnD.Text = finalBind;
                    break;
                default:
                    break;
            }
            t.Stop();
            //THIS doesn't work Crossthread error; should look into solution
            string text = "";
            if (oldv==finalBind)
                text = "Binding " + bindFor + " -- unchanged\r\n";
            else
             text= "Binding " + bindFor + " -- from: " + oldv + " to: " + finalBind + "\r\n";
            tbxPresses.Invoke(new Action(() => tbxPresses.Text += text));
            //PrintToLog("Binding " + bindFor, oldv, finalBind);
        }
        public void BindFor(string src)
        {
            if (setupMode)
                bindFor = src;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            currBinding += ((Button)sender).Text;
        }
    }

}
