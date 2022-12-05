namespace Lead.Tool.Manager
{
    partial class StateManger
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.未初始化 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.初始化 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.运行 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.终止 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.名称,
            this.未初始化,
            this.初始化,
            this.运行,
            this.终止});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(858, 392);
            this.dataGridView1.TabIndex = 3;
            // 
            // 名称
            // 
            this.名称.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.名称.HeaderText = "名称";
            this.名称.Name = "名称";
            this.名称.Width = 66;
            // 
            // 未初始化
            // 
            this.未初始化.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.未初始化.HeaderText = "未初始化";
            this.未初始化.Name = "未初始化";
            this.未初始化.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // 初始化
            // 
            this.初始化.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.初始化.HeaderText = "初始化";
            this.初始化.Name = "初始化";
            // 
            // 运行
            // 
            this.运行.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.运行.HeaderText = "运行";
            this.运行.Name = "运行";
            // 
            // 终止
            // 
            this.终止.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.终止.HeaderText = "终止";
            this.终止.Name = "终止";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StateManger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Name = "StateManger";
            this.Size = new System.Drawing.Size(858, 392);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 未初始化;
        private System.Windows.Forms.DataGridViewTextBoxColumn 初始化;
        private System.Windows.Forms.DataGridViewTextBoxColumn 运行;
        private System.Windows.Forms.DataGridViewTextBoxColumn 终止;
        private System.Windows.Forms.Timer timer1;
    }
}
