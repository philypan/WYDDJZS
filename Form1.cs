using System.Net.Sockets;
using System.Net;
using utils;
using System.Text;
using System.IO;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Net.Server;
using System.Diagnostics;
using System.Windows.Forms;

namespace WYDDJZS
{
    public partial class Form1 : Form
    {
        private string serverDir = null;
        private string clientDir = null;
        private string clientExe = null;
        private string hostIp = null;

        private HttpServer httpServer = null;

        public Form1()
        {
            InitializeComponent();

            getLocalIp();

            serverDir = ConfigUtil.GetString("serverDir");
            clientExe = ConfigUtil.GetString("clientExe");
            if (clientExe != null)
            {
                clientDir = Path.GetDirectoryName(clientExe);
            }
            setLableDir(lbServerDir, serverDir);
            setLableDir(lbClientExe, clientExe);
        }

        private void getLocalIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    hostIp = ip.ToString();
                    lbLocalIp.Text = hostIp;
                }
            }

        }

        private void btnRefreshIp_Click(object sender, EventArgs e)
        {
            getLocalIp();
        }

        private void lbServerDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string outDir = folderBrowserDialog.SelectedPath;
                if (setLableDir(lbServerDir, outDir))
                {
                    serverDir = outDir;
                    ConfigUtil.SetString("serverDir", outDir);
                }
            }
        }

        private void lbClientExe_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Files (*.exe)|*.exe"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog.FileName;

                if (setLableDir(lbClientExe, path))
                {
                    clientExe = path;
                    ConfigUtil.SetString("clientExe", path);

                    if (clientExe != null)
                    {
                        clientDir = Path.GetDirectoryName(clientExe);
                    }
                }
            }

        }

        private bool setLableDir(Label label, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                label.Text = "点击选择";
                return false;
            }
            else
            {
                label.Text = path;
                return true;
            }
        }

        private void btnModifyIp_Click(object sender, EventArgs e)
        {
            if (false == checkPath())
            {
                return;
            }

            modifyRootDirIp();
            modifyBisrvIp();
            modifyDbsrvIp();
            modifyTmsrvIp();
            modifyClientIp();
        }

        private void btnLaunchServer_Click(object sender, EventArgs e)
        {
            if (false == checkPath())
            {
                return;
            }

            /*public const string BISRV = "bisrv\run";
            public const string DBSRV = "dbsrv\run";
            public const string TMSRV = "tmsrv\run";*/
            Task.Run(() =>
            {
                string path = Path.Combine(serverDir, Consts.BISRV, "bisrv.exe");
                if (File.Exists(path))
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = path;
                    psi.UseShellExecute = false;
                    psi.WorkingDirectory = Path.Combine(serverDir, Consts.BISRV);
                    psi.CreateNoWindow = true;
                    Process.Start(psi);

                    Thread.Sleep(3000);
                }

                path = Path.Combine(serverDir, Consts.DBSRV, "dbsrv.exe");
                if (File.Exists(path))
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = path;
                    psi.UseShellExecute = false;
                    psi.WorkingDirectory = Path.Combine(serverDir, Consts.DBSRV);
                    psi.CreateNoWindow = true;
                    Process.Start(psi);

                    Thread.Sleep(3000);
                }

                path = Path.Combine(serverDir, Consts.TMSRV, "tmsrv.exe");
                if (File.Exists(path))
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = path;
                    psi.UseShellExecute = false;
                    psi.WorkingDirectory = Path.Combine(serverDir, Consts.TMSRV);
                    psi.CreateNoWindow = true;
                    Process.Start(psi);

                    Thread.Sleep(3000);
                }
            });
        }

        private void btnLaunchClient_Click(object sender, EventArgs e)
        {
            if (false == checkPath())
            {
                return;
            }

            Task.Run(() =>
            {
                if (File.Exists(clientExe))
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = clientExe;
                    psi.UseShellExecute = false;
                    psi.WorkingDirectory = clientDir;
                    psi.CreateNoWindow = true;
                    Process.Start(psi);

                    Thread.Sleep(3000);
                }
            });
        }

        private void modifyRootDirIp()
        {
            string path = Path.Combine(serverDir, "serverlist.txt");
            string backFile = Path.Combine(serverDir, "serverlist_zsbk.txt");
            if (File.Exists(path))
            {
                try
                {
                    File.Move(path, backFile, true);

                    insertMessage("备份" + path + "为" + backFile);
                }
                catch
                {

                }

            }

            StringBuilder sb = new StringBuilder();
            /*  0  0  192.168.1.100
              0  1  192.168.1.100
              1  0  192.168.1.100 3000
              1  1  192.168.1.100 3000*/
            sb.Append("0  0  ").Append(hostIp).Append(Environment.NewLine)
                .Append("0  1  ").Append(hostIp).Append(Environment.NewLine)
                .Append("1  0  ").Append(hostIp).Append("3000").Append(Environment.NewLine)
                .Append("1  1  ").Append(hostIp).Append("3000").Append(Environment.NewLine);

            try
            {
                File.WriteAllText(path, sb.ToString());

                insertMessage("写入" + path + "成功");
            }
            catch
            {

            }

        }

        private void modifyBisrvIp()
        {
            //string path = Path.Combine();
        }
        private void modifyDbsrvIp()
        {
            //2.wydserver\dbsrv\run 目录下的 localip.txt redirect.sample.txt
            string localip = Path.Combine(serverDir, Consts.DBSRV, "localip.txt");
            string localipBck = Path.Combine(serverDir, Consts.DBSRV, "localip_zsbk.txt");

            string redirect = Path.Combine(serverDir, Consts.DBSRV, "redirect.sample.txt");
            string redirectBck = Path.Combine(serverDir, Consts.DBSRV, "redirect.sample_zsbk.txt");

            if (File.Exists(localip))
            {
                try
                {
                    File.Move(localip, localipBck, true);
                    insertMessage("备份" + hostIp + "为" + localipBck);

                    File.WriteAllText(localip, hostIp);

                    insertMessage("写入" + localip + "成功");
                }
                catch
                {

                }

            }
            if (File.Exists(redirect))
            {
                try
                {
                    File.Move(redirect, redirectBck, true);
                    insertMessage("备份" + redirect + "为" + redirectBck);

                    File.WriteAllText(redirect, hostIp + " 8895 aaaa5 1111");
                    insertMessage("写入" + redirect + "成功");
                }
                catch
                {

                }

            }
        }

        private void modifyTmsrvIp()
        {
            //3.wydserver\tmsrv\run 目录下的 biserver.txt localip.txt
            string localip = Path.Combine(serverDir, Consts.TMSRV, "localip.txt");
            string localipBck = Path.Combine(serverDir, Consts.TMSRV, "localip_zsbk.txt");


            if (File.Exists(localip))
            {
                try
                {
                    File.Move(localip, localipBck, true);
                    insertMessage("备份" + hostIp + "为" + localipBck);

                    File.WriteAllText(localip, hostIp);

                    insertMessage("写入" + localip + "成功");
                }
                catch
                {

                }

            }

            string biserver = Path.Combine(serverDir, Consts.TMSRV, "biserver.txt");
            string biserverBck = Path.Combine(serverDir, Consts.TMSRV, "biserver_zsbk.txt");
            if (File.Exists(biserver))
            {
                try
                {
                    File.Move(biserver, biserverBck, true);
                    insertMessage("备份" + biserver + "为" + biserverBck);

                    File.WriteAllText(biserver, hostIp);
                    insertMessage("写入" + biserver + "成功");
                }
                catch
                {

                }

            }

            string locakip = Path.Combine(serverDir, Consts.TMSRV, "locakip.txt");
            string locakipBck = Path.Combine(serverDir, Consts.TMSRV, "locakip_zsbk.txt");
            if (File.Exists(locakip))
            {
                try
                {
                    File.Move(locakip, locakipBck, true);
                    insertMessage("备份" + locakip + "为" + locakipBck);

                    File.WriteAllText(locakip, hostIp);
                    insertMessage("写入" + locakip + "成功");
                }
                catch
                {

                }
            }

            string serverlist = Path.Combine(serverDir, Consts.TMSRV, "serverlist.txt");
            string serverlistBck = Path.Combine(serverDir, Consts.TMSRV, "serverlist_zsbk.txt");
            if (File.Exists(locakip))
            {
                try
                {
                    File.Move(serverlist, serverlistBck, true);
                    insertMessage("备份" + serverlist + "为" + serverlistBck);

                    StringBuilder sb = new StringBuilder();
                    /*  0  0  192.168.1.100
                      0  1  192.168.1.100
                      1  0  192.168.1.100 3000
                      1  1  192.168.1.100 3000*/
                    sb.Append("0  0  ").Append(hostIp).Append(Environment.NewLine)
                        .Append("0  1  ").Append(hostIp).Append(Environment.NewLine)
                        .Append("1  0  ").Append(hostIp).Append("3000").Append(Environment.NewLine)
                        .Append("1  1  ").Append(hostIp).Append("3000").Append(Environment.NewLine);
                    File.WriteAllText(serverlist, sb.ToString());
                    insertMessage("写入" + serverlist + "成功");
                }
                catch
                {

                }
            }
        }

        private void modifyClientIp()
        {
            string path = Path.Combine(clientDir, "serverlist.bin");
            string backFile = Path.Combine(clientDir, "serverlist_zsbk.bin");
            if (File.Exists(path))
            {
                Path.GetFullPath(path);
                try
                {
                    File.Move(path, backFile, true );

                    insertMessage("备份" + path + "为" + backFile);
                }
                catch
                {

                }
            }

            byte[] serverAddBuff = new byte[Consts.MAX_SERVERGROUP * Consts.MAX_SERVERNUMBER * Consts.MAX_SERVECHAR_LEN];

            string webUrl = "http://" + hostIp + "/serv01.htm";
            byte[] webArray = Encoding.UTF8.GetBytes(webUrl);
            byte[] ipArray = Encoding.UTF8.GetBytes(hostIp);

            int offset = 0;
            Array.Copy(webArray, 0, serverAddBuff, offset, webArray.Length);
            Array.Copy(ipArray, 0, serverAddBuff, offset + 1 * Consts.MAX_SERVECHAR_LEN, ipArray.Length);
            Array.Copy(ipArray, 0, serverAddBuff, offset + 2 * Consts.MAX_SERVECHAR_LEN, ipArray.Length);

            offset = Consts.MAX_SERVERNUMBER * Consts.MAX_SERVECHAR_LEN;
            Array.Copy(webArray, 0, serverAddBuff, offset, webArray.Length);
            Array.Copy(ipArray, 0, serverAddBuff, offset + 1 * Consts.MAX_SERVECHAR_LEN, ipArray.Length);
            Array.Copy(ipArray, 0, serverAddBuff, offset + 2 * Consts.MAX_SERVECHAR_LEN, ipArray.Length);

            FileStream newBinFile = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(newBinFile);

            for (int k = 0; k < Consts.MAX_SERVERGROUP; k++)
            {
                int groupOffset = k * Consts.MAX_SERVERNUMBER * Consts.MAX_SERVECHAR_LEN;
                for (int j = 0; j < Consts.MAX_SERVERNUMBER; j++)
                {
                    int serverOffset = groupOffset + (j * Consts.MAX_SERVECHAR_LEN);
                    for (int i = 0; i < Consts.MAX_SERVECHAR_LEN; i++)
                    {
                        int indexOffset = serverOffset + i;
                        byte coded = (byte)(serverAddBuff[indexOffset] + Consts.szList[Consts.MAX_SERVECHAR_LEN - 1 - i]);
                        bw.Write(coded);
                    }
                }
            }
            bw.Close();
            newBinFile.Close();
            insertMessage("写入客户端serverlist.bin成功");
        }


        private bool checkPath()
        {
            if (string.IsNullOrEmpty(hostIp))
            {
                MessageBox.Show("获取本机IP失败");
                return false;
            }

            if (string.IsNullOrEmpty(serverDir) || !Directory.Exists(serverDir))
            {
                MessageBox.Show("服务端目录未设置或不存在");
                return false;
            }

            if (string.IsNullOrEmpty(clientDir) || !Directory.Exists(clientDir))
            {
                MessageBox.Show("客户端目录未设置或不存在");
                return false;
            }

            if (string.IsNullOrEmpty(clientExe) || !File.Exists(clientExe))
            {
                MessageBox.Show("游戏执行文件未设置或不存在");
                return false;
            }

            return true;
        }

        private void insertMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            lstbxMessage.Items.Add(message);
            lstbxMessage.TopIndex = lstbxMessage.Items.Count - 1;

            if (lstbxMessage.Items.Count > 1000)
            {
                lstbxMessage.Items.RemoveAt(0);
            }
        }

        private void btnStartWeb_Click(object sender, EventArgs e)
        {
            if (httpServer != null)
            {
                return;
            }

            httpServer = new HttpServer();

            httpServer.AddPrefixes("/serv00.htm", (value) =>
            {
                value.rspDo.content = "100 100 100 100 100 100 100";
            });

            httpServer.AddPrefixes("/serv01.htm", (value) =>
            {
                value.rspDo.content = "100 100 100 100 100 100 100";
            });

            httpServer.Start(80);

            insertMessage("web服务启动");
        }

        private void btnStopWeb_Click(object sender, EventArgs e)
        {
            httpServer?.destory();

            httpServer = null;
            insertMessage("web服务停止");
        }

        private void lstbxMessage_DrawItem(object sender, DrawItemEventArgs e)
        {
            int index = e.Index;//获取当前要进行绘制的行的序号，从0开始。
            if (index < 0)
            {
                return; 
            }

            ListBox listBox = sender as ListBox;
            e.DrawBackground();//画背景颜色
            e.DrawFocusRectangle();//画聚焦项的边框
            Graphics g = e.Graphics;//获取Graphics对象。
            Rectangle itemBounds = e.Bounds;//获取当前要绘制的行的一个矩形范围。
            //文字绘制的区域，留出一定间隔
            Rectangle textBounds = new Rectangle(itemBounds.X, itemBounds.Y + 5, itemBounds.Width, itemBounds.Height);
            string text = Convert.ToString(listBox.Items[index]);
            //因为文本可能会非常长，因此要用自绘实现ListBox项目的自动换行
            TextRenderer.DrawText(g, index + ":" + text, e.Font, textBounds, e.ForeColor, TextFormatFlags.WordBreak);
            g.DrawRectangle(Pens.Blue, itemBounds);//画每一项的边框，这样清楚分出来各项。
        }
      

        private void lstbxMessage_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            int index = e.Index;//获取当前要进行绘制的行的序号，从0开始。

            if (index < 0)
            {
                return;
            }
            string text = Convert.ToString(listBox.Items[index]);
            //超范围后自动换行
            Size size = TextRenderer.MeasureText(index + ":" + text, listBox.Font, listBox.Size, TextFormatFlags.WordBreak);
            e.ItemWidth = size.Width;
            e.ItemHeight = size.Height + 5 * 2;//适当多一点高度，避免太挤
        }
    }
}
