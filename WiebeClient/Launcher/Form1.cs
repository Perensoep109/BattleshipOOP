using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Launcher
{
    public partial class Form1 : Form
    {
        Process m_proc;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += OnClosing;
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (m_proc != null)
            {
                if (!m_proc.HasExited)
                {
                    m_proc.CloseMainWindow();
                    m_proc.Close();
                }
            }
        }

        private void btn_join_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\Battleship.exe");
            startInfo.Arguments = String.Format("--ip {0} --port {1} --gameid {2}", txt_ip.Text, txt_port.Text, txt_gameID.Text);
            startInfo.UseShellExecute = false;
            m_proc = Process.Start(startInfo);
            m_proc.EnableRaisingEvents = true;
            m_proc.Exited += OnProcessExited;
            this.Hide();
        }

        private void OnProcessExited(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                this.Show();
            });
        }
    }
}
