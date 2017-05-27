namespace addon_compatible_assistant
{
    partial class AssistForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssistForm));
            this.listViewDir = new System.Windows.Forms.ListView();
            this.textBoxAdvice = new System.Windows.Forms.TextBox();
            this.labelNotice = new System.Windows.Forms.Label();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.labelListViewName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewDir
            // 
            this.listViewDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewDir.Location = new System.Drawing.Point(12, 66);
            this.listViewDir.Name = "listViewDir";
            this.listViewDir.Size = new System.Drawing.Size(260, 283);
            this.listViewDir.TabIndex = 0;
            this.listViewDir.UseCompatibleStateImageBehavior = false;
            this.listViewDir.View = System.Windows.Forms.View.Details;
            // 
            // textBoxAdvice
            // 
            this.textBoxAdvice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAdvice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAdvice.Location = new System.Drawing.Point(12, 12);
            this.textBoxAdvice.Multiline = true;
            this.textBoxAdvice.Name = "textBoxAdvice";
            this.textBoxAdvice.ReadOnly = true;
            this.textBoxAdvice.Size = new System.Drawing.Size(179, 32);
            this.textBoxAdvice.TabIndex = 2;
            this.textBoxAdvice.Text = "注1: 部分插件仍然需要人工修改\r\n注2: 请自行备份修改前的插件";
            // 
            // labelNotice
            // 
            this.labelNotice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNotice.AutoSize = true;
            this.labelNotice.Location = new System.Drawing.Point(13, 337);
            this.labelNotice.Name = "labelNotice";
            this.labelNotice.Size = new System.Drawing.Size(0, 12);
            this.labelNotice.TabIndex = 3;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(197, 13);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(75, 23);
            this.buttonEdit.TabIndex = 4;
            this.buttonEdit.Text = "修改(&E)";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // labelListViewName
            // 
            this.labelListViewName.AutoSize = true;
            this.labelListViewName.Location = new System.Drawing.Point(13, 51);
            this.labelListViewName.Name = "labelListViewName";
            this.labelListViewName.Size = new System.Drawing.Size(101, 12);
            this.labelListViewName.TabIndex = 5;
            this.labelListViewName.Text = "找到的插件文件夹";
            // 
            // AssistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 361);
            this.Controls.Add(this.labelListViewName);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.labelNotice);
            this.Controls.Add(this.textBoxAdvice);
            this.Controls.Add(this.listViewDir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssistForm";
            this.Text = "插件兼容修改助手";
            this.Shown += new System.EventHandler(this.AssistForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewDir;
        private System.Windows.Forms.TextBox textBoxAdvice;
        private System.Windows.Forms.Label labelNotice;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Label labelListViewName;
    }
}

