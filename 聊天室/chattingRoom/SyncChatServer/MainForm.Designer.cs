namespace SyncChatServer
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.msgBox = new System.Windows.Forms.ListBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.onlineBox = new System.Windows.Forms.ListBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.delBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.msgBox);
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(784, 490);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态信息";
            // 
            // msgBox
            // 
            this.msgBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msgBox.FormattingEnabled = true;
            this.msgBox.ItemHeight = 18;
            this.msgBox.Location = new System.Drawing.Point(4, 25);
            this.msgBox.Margin = new System.Windows.Forms.Padding(4);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(776, 461);
            this.msgBox.TabIndex = 0;
            this.msgBox.SelectedIndexChanged += new System.EventHandler(this.onlineList_SelectedIndexChanged);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(189, 519);
            this.btn_Start.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(112, 34);
            this.btn_Start.TabIndex = 1;
            this.btn_Start.Text = "开始监听";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(483, 519);
            this.btn_Stop.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(112, 34);
            this.btn_Stop.TabIndex = 2;
            this.btn_Stop.Text = "停止监听";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // onlineBox
            // 
            this.onlineBox.FormattingEnabled = true;
            this.onlineBox.ItemHeight = 18;
            this.onlineBox.Location = new System.Drawing.Point(834, 43);
            this.onlineBox.Name = "onlineBox";
            this.onlineBox.Size = new System.Drawing.Size(178, 346);
            this.onlineBox.TabIndex = 3;
            this.onlineBox.SelectedIndexChanged += new System.EventHandler(this.onlineBox_SelectedIndexChanged);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(834, 415);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(111, 25);
            this.addBtn.TabIndex = 4;
            this.addBtn.Text = "添加用户";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // delBtn
            // 
            this.delBtn.Location = new System.Drawing.Point(834, 455);
            this.delBtn.Name = "delBtn";
            this.delBtn.Size = new System.Drawing.Size(111, 25);
            this.delBtn.TabIndex = 5;
            this.delBtn.Text = "删除用户";
            this.delBtn.UseVisualStyleBackColor = true;
            this.delBtn.Click += new System.EventHandler(this.delBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 562);
            this.Controls.Add(this.delBtn);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.onlineBox);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "SyncChatServer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox msgBox;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.ListBox onlineBox;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Button delBtn;
    }
}

