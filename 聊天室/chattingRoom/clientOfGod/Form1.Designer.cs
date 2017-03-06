namespace clientOfGod
{
    partial class chatClient
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
            this.IPBox = new System.Windows.Forms.TextBox();
            this.portBox = new System.Windows.Forms.TextBox();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.onlineList = new System.Windows.Forms.ListBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.sendBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.msgBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // IPBox
            // 
            this.IPBox.Location = new System.Drawing.Point(92, 21);
            this.IPBox.Name = "IPBox";
            this.IPBox.Size = new System.Drawing.Size(155, 28);
            this.IPBox.TabIndex = 0;
            this.IPBox.Text = "127.0.0.1";
            this.IPBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(92, 72);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(155, 28);
            this.portBox.TabIndex = 1;
            this.portBox.Text = "2333";
            // 
            // chatBox
            // 
            this.chatBox.Location = new System.Drawing.Point(277, 49);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(538, 257);
            this.chatBox.TabIndex = 3;
            this.chatBox.Text = "";
            this.chatBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // onlineList
            // 
            this.onlineList.FormattingEnabled = true;
            this.onlineList.ItemHeight = 18;
            this.onlineList.Location = new System.Drawing.Point(36, 220);
            this.onlineList.Name = "onlineList";
            this.onlineList.Size = new System.Drawing.Size(211, 220);
            this.onlineList.TabIndex = 4;
            this.onlineList.SelectedIndexChanged += new System.EventHandler(this.onlineList_SelectedIndexChanged);
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(157, 159);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(90, 28);
            this.connectBtn.TabIndex = 5;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(720, 406);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(95, 34);
            this.sendBtn.TabIndex = 6;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 18);
            this.label1.TabIndex = 7;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "who\'s Online:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(92, 115);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(155, 28);
            this.nameBox.TabIndex = 11;
            this.nameBox.Text = "Ghost";
            this.nameBox.TextChanged += new System.EventHandler(this.nameBox_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 18);
            this.label4.TabIndex = 12;
            this.label4.Text = "Name:";
            // 
            // msgBox
            // 
            this.msgBox.Location = new System.Drawing.Point(277, 327);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(437, 113);
            this.msgBox.TabIndex = 15;
            this.msgBox.Text = "";
            // 
            // chatClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 472);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.onlineList);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.IPBox);
            this.Name = "chatClient";
            this.Text = "GiveMe5";
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.ListBox onlineList;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox msgBox;
    }
}

