namespace chattingRoom
{
    partial class FormChatServer
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private void InitializeComponent()
        {
            this.onlineBox = new System.Windows.Forms.ListBox();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.StartBtn = new System.Windows.Forms.Button();
            this.delBtn = new System.Windows.Forms.Button();
            this.addBtn = new System.Windows.Forms.Button();
            this.sendBox = new System.Windows.Forms.TextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // onlineBox
            // 
            this.onlineBox.FormattingEnabled = true;
            this.onlineBox.ItemHeight = 18;
            this.onlineBox.Location = new System.Drawing.Point(560, 87);
            this.onlineBox.Name = "onlineBox";
            this.onlineBox.Size = new System.Drawing.Size(182, 346);
            this.onlineBox.TabIndex = 1;
            this.onlineBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(26, 87);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(508, 364);
            this.chatBox.TabIndex = 2;
            this.chatBox.Text = "";
            this.chatBox.TextChanged += new System.EventHandler(this.chatBox_TextChanged);
            // 
            // IPBox
            // 
            this.IPBox.Location = new System.Drawing.Point(74, 12);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(119, 28);
            this.IPBox.TabIndex = 4;
            this.IPBox.Text = "127.0.0.1";
            this.IPBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(74, 56);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(119, 28);
            this.PortBox.TabIndex = 5;
            this.PortBox.Text = "2333";
            this.PortBox.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "IP";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(557, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "OnlineList";
            // 
            // StartBtn
            // 
            this.StartBtn.Location = new System.Drawing.Point(242, 11);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(150, 26);
            this.StartBtn.TabIndex = 9;
            this.StartBtn.Text = "Sever start";
            this.StartBtn.UseVisualStyleBackColor = true;
            this.StartBtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(767, 99);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(75, 23);
            this.delBtn.TabIndex = 10;
            this.delBtn.Text = "Delete";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(767, 181);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 23);
            this.addBtn.TabIndex = 11;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            // 
            // sendBox
            // 
            this.sendBox.Location = new System.Drawing.Point(242, 46);
            this.sendBox.Name = "sendBox";
            this.sendBox.Size = new System.Drawing.Size(150, 28);
            this.sendBox.TabIndex = 12;
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(399, 50);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 23);
            this.sendBtn.TabIndex = 14;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // FormChatServer
            // 
            this.ClientSize = new System.Drawing.Size(876, 498);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.sendBox);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.delBtn);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PortBox);
            this.Controls.Add(this.IPBox);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.onlineBox);
            this.Name = "FormChatServer";
            this.Text = "Sever";
            this.Load += new System.EventHandler(this.FormChatServer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

  
        private System.Windows.Forms.ListBox onlineBox;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.Button delBtn;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.TextBox sendBox;
        private System.Windows.Forms.Button sendBtn;
    }
}

