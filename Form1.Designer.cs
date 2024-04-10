namespace WYDDJZS
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            lbLocalIp = new Label();
            lbServerDir = new Label();
            lbClientDir = new Label();
            lbClientExe = new Label();
            lstbxMessage = new ListBox();
            btnModifyIp = new Button();
            btnLaunchServer = new Button();
            btnLaunchClient = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            btnStopWeb = new Button();
            btnStartWeb = new Button();
            btnRefreshIp = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(55, 17);
            label1.TabIndex = 0;
            label1.Text = "本机IP：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 117);
            label2.Name = "label2";
            label2.Size = new Size(78, 17);
            label2.TabIndex = 1;
            label2.Text = "客户端EXE：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 77);
            label3.Name = "label3";
            label3.Size = new Size(80, 17);
            label3.TabIndex = 2;
            label3.Text = "客户端目录：";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 39);
            label4.Name = "label4";
            label4.Size = new Size(80, 17);
            label4.TabIndex = 3;
            label4.Text = "服务端目录：";
            // 
            // lbLocalIp
            // 
            lbLocalIp.AutoSize = true;
            lbLocalIp.Font = new Font("Microsoft YaHei UI", 14F);
            lbLocalIp.Location = new Point(93, 5);
            lbLocalIp.Name = "lbLocalIp";
            lbLocalIp.Size = new Size(71, 25);
            lbLocalIp.TabIndex = 4;
            lbLocalIp.Text = "0.0.0.0";
            // 
            // lbServerDir
            // 
            lbServerDir.BackColor = SystemColors.ActiveBorder;
            lbServerDir.Font = new Font("Microsoft YaHei UI", 10F);
            lbServerDir.Location = new Point(93, 35);
            lbServerDir.Name = "lbServerDir";
            lbServerDir.Size = new Size(409, 24);
            lbServerDir.TabIndex = 5;
            lbServerDir.Text = "点击选择";
            lbServerDir.Click += lbServerDir_Click;
            // 
            // lbClientDir
            // 
            lbClientDir.BackColor = SystemColors.ActiveBorder;
            lbClientDir.Font = new Font("Microsoft YaHei UI", 10F);
            lbClientDir.Location = new Point(93, 73);
            lbClientDir.Name = "lbClientDir";
            lbClientDir.Size = new Size(409, 24);
            lbClientDir.TabIndex = 6;
            lbClientDir.Text = "点击选择目录";
            lbClientDir.Click += lbClientDir_Click;
            // 
            // lbClientExe
            // 
            lbClientExe.BackColor = SystemColors.ActiveBorder;
            lbClientExe.Font = new Font("Microsoft YaHei UI", 8F);
            lbClientExe.Location = new Point(93, 113);
            lbClientExe.Name = "lbClientExe";
            lbClientExe.Size = new Size(409, 24);
            lbClientExe.TabIndex = 7;
            lbClientExe.Text = "点击选择命运启动文件";
            lbClientExe.Click += lbClientExe_Click;
            // 
            // lstbxMessage
            // 
            lstbxMessage.DrawMode = DrawMode.OwnerDrawVariable;
            lstbxMessage.FormattingEnabled = true;
            lstbxMessage.ItemHeight = 17;
            lstbxMessage.Location = new Point(521, 9);
            lstbxMessage.Name = "lstbxMessage";
            lstbxMessage.Size = new Size(340, 412);
            lstbxMessage.TabIndex = 8;
            lstbxMessage.DrawItem += lstbxMessage_DrawItem;
            lstbxMessage.MeasureItem += lstbxMessage_MeasureItem;
            // 
            // btnModifyIp
            // 
            btnModifyIp.Location = new Point(18, 22);
            btnModifyIp.Name = "btnModifyIp";
            btnModifyIp.Size = new Size(75, 23);
            btnModifyIp.TabIndex = 9;
            btnModifyIp.Text = "修改IP";
            btnModifyIp.UseVisualStyleBackColor = true;
            btnModifyIp.Click += btnModifyIp_Click;
            // 
            // btnLaunchServer
            // 
            btnLaunchServer.Location = new Point(378, 22);
            btnLaunchServer.Name = "btnLaunchServer";
            btnLaunchServer.Size = new Size(87, 23);
            btnLaunchServer.TabIndex = 10;
            btnLaunchServer.Text = "启动服务器";
            btnLaunchServer.UseVisualStyleBackColor = true;
            btnLaunchServer.Click += btnLaunchServer_Click;
            // 
            // btnLaunchClient
            // 
            btnLaunchClient.Location = new Point(378, 76);
            btnLaunchClient.Name = "btnLaunchClient";
            btnLaunchClient.Size = new Size(87, 23);
            btnLaunchClient.TabIndex = 11;
            btnLaunchClient.Text = "启动客户端";
            btnLaunchClient.UseVisualStyleBackColor = true;
            btnLaunchClient.Click += btnLaunchClient_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnModifyIp);
            groupBox1.Location = new Point(12, 155);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(490, 131);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "IP";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnStopWeb);
            groupBox2.Controls.Add(btnStartWeb);
            groupBox2.Controls.Add(btnLaunchServer);
            groupBox2.Controls.Add(btnLaunchClient);
            groupBox2.Location = new Point(12, 308);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(490, 105);
            groupBox2.TabIndex = 13;
            groupBox2.TabStop = false;
            groupBox2.Text = "Launch";
            // 
            // btnStopWeb
            // 
            btnStopWeb.Location = new Point(6, 63);
            btnStopWeb.Name = "btnStopWeb";
            btnStopWeb.Size = new Size(117, 36);
            btnStopWeb.TabIndex = 13;
            btnStopWeb.Text = "停止WEB服务";
            btnStopWeb.UseVisualStyleBackColor = true;
            btnStopWeb.Click += btnStopWeb_Click;
            // 
            // btnStartWeb
            // 
            btnStartWeb.Location = new Point(6, 22);
            btnStartWeb.Name = "btnStartWeb";
            btnStartWeb.Size = new Size(117, 36);
            btnStartWeb.TabIndex = 12;
            btnStartWeb.Text = "启动WEB服务";
            btnStartWeb.UseVisualStyleBackColor = true;
            btnStartWeb.Click += btnStartWeb_Click;
            // 
            // btnRefreshIp
            // 
            btnRefreshIp.Location = new Point(274, 7);
            btnRefreshIp.Name = "btnRefreshIp";
            btnRefreshIp.Size = new Size(75, 23);
            btnRefreshIp.TabIndex = 10;
            btnRefreshIp.Text = "刷新";
            btnRefreshIp.UseVisualStyleBackColor = true;
            btnRefreshIp.Click += btnRefreshIp_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(865, 425);
            Controls.Add(btnRefreshIp);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(lstbxMessage);
            Controls.Add(lbClientExe);
            Controls.Add(lbClientDir);
            Controls.Add(lbServerDir);
            Controls.Add(lbLocalIp);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "命运单机助手";
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lbLocalIp;
        private Label lbServerDir;
        private Label lbClientDir;
        private Label lbClientExe;
        private ListBox lstbxMessage;
        private Button btnModifyIp;
        private Button btnLaunchServer;
        private Button btnLaunchClient;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnStartWeb;
        private Button btnRefreshIp;
        private Button btnStopWeb;
    }
}
