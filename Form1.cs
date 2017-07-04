using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Ini;

namespace Server_Status
{
    public partial class Form1 : Form
    {
        InitializeINI INIFile = new InitializeINI("Config.ini");
      
        string[] Array = new string[3];
        string HTML = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void updater_Tick(object sender, EventArgs e)
        {
            MyTurn.Check(arma_label, Array[0]);
            MyTurn.Check(redist_label, Array[1]);
            MyTurn.Check(bec_label, Array[2]);

            if(MyTurn.ProcessRunning("Bec") == false)
            {
                Process Proc = Process.Start(@"C:\Server\ArmA3\A3Files\Bec\Bec.exe", @"-f Config.cfg");
            }

            TextWriter Writer = new StreamWriter(@"C:\xampp\htdocs\index.html");
            HTML = String.Format(textBox1.Text, arma_label.Text, redist_label.Text, bec_label.Text);
            
            Writer.WriteLine(HTML);
            Writer.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(!File.Exists(INIFile.ExternPath))
            {
                INIFile.Write("Names", "Server", "arma3server");
                INIFile.Write("Names", "Redis", "redis-server");
                INIFile.Write("Names", "Bec", "Bec");
            }

            Array[0] = INIFile.Read("Names", "Server");
            Array[1] = INIFile.Read("Names", "Redis");
            Array[2] = INIFile.Read("Names", "Bec"); 
            updater.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }

    public static class MyTurn
    {
        public static bool ProcessRunning(string processname)
        {
            foreach (Process Procs in Process.GetProcesses())
            {
                if (Procs.ProcessName.Contains(processname))
                {
                    return true;
                }
            }
            return false;
        }

        public static void Check(this Label Source, string proc)
        {
            if (ProcessRunning(proc) == true)
            {
                Source.Text = "Online!";
                Source.ForeColor = Color.DarkGreen;
            }
            else
            {
                Source.Text = "Offline!";
                Source.ForeColor = Color.DarkRed;
            }
        }
    }
}
