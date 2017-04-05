namespace CoffeeMachine.Client
{
	partial class MainForm
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabUserApproval = new System.Windows.Forms.TabPage();
			this.listUsersToApprove = new System.Windows.Forms.ListView();
			this.tabStatistics = new System.Windows.Forms.TabPage();
			this.btnSelect = new System.Windows.Forms.Button();
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnUpload = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabUserApproval.SuspendLayout();
			this.tabStatistics.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabUserApproval);
			this.tabControl1.Controls.Add(this.tabStatistics);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(950, 346);
			this.tabControl1.TabIndex = 0;
			// 
			// tabUserApproval
			// 
			this.tabUserApproval.Controls.Add(this.listUsersToApprove);
			this.tabUserApproval.Location = new System.Drawing.Point(4, 22);
			this.tabUserApproval.Name = "tabUserApproval";
			this.tabUserApproval.Padding = new System.Windows.Forms.Padding(3);
			this.tabUserApproval.Size = new System.Drawing.Size(942, 320);
			this.tabUserApproval.TabIndex = 0;
			this.tabUserApproval.Text = "Users to approve ";
			this.tabUserApproval.UseVisualStyleBackColor = true;
			// 
			// listUsersToApprove
			// 
			this.listUsersToApprove.Location = new System.Drawing.Point(6, 6);
			this.listUsersToApprove.Name = "listUsersToApprove";
			this.listUsersToApprove.Size = new System.Drawing.Size(445, 308);
			this.listUsersToApprove.TabIndex = 0;
			this.listUsersToApprove.UseCompatibleStateImageBehavior = false;
			// 
			// tabStatistics
			// 
			this.tabStatistics.Controls.Add(this.btnUpload);
			this.tabStatistics.Controls.Add(this.btnSelect);
			this.tabStatistics.Controls.Add(this.txtFilePath);
			this.tabStatistics.Controls.Add(this.label1);
			this.tabStatistics.Location = new System.Drawing.Point(4, 22);
			this.tabStatistics.Name = "tabStatistics";
			this.tabStatistics.Padding = new System.Windows.Forms.Padding(3);
			this.tabStatistics.Size = new System.Drawing.Size(942, 320);
			this.tabStatistics.TabIndex = 1;
			this.tabStatistics.Text = "Statisctics";
			this.tabStatistics.UseVisualStyleBackColor = true;
			// 
			// btnSelect
			// 
			this.btnSelect.Location = new System.Drawing.Point(305, 32);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(75, 23);
			this.btnSelect.TabIndex = 2;
			this.btnSelect.Text = "Select..";
			this.btnSelect.UseVisualStyleBackColor = true;
			this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
			// 
			// txtFilePath
			// 
			this.txtFilePath.Location = new System.Drawing.Point(22, 34);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.Size = new System.Drawing.Size(266, 20);
			this.txtFilePath.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(103, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select file to upload:";
			// 
			// btnUpload
			// 
			this.btnUpload.Location = new System.Drawing.Point(22, 60);
			this.btnUpload.Name = "btnUpload";
			this.btnUpload.Size = new System.Drawing.Size(75, 23);
			this.btnUpload.TabIndex = 3;
			this.btnUpload.Text = "Upload";
			this.btnUpload.UseVisualStyleBackColor = true;
			this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(974, 370);
			this.Controls.Add(this.tabControl1);
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "CoffeeMachine Client";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabUserApproval.ResumeLayout(false);
			this.tabStatistics.ResumeLayout(false);
			this.tabStatistics.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabUserApproval;
		private System.Windows.Forms.TabPage tabStatistics;
		private System.Windows.Forms.ListView listUsersToApprove;
		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSelect;
		private System.Windows.Forms.Button btnUpload;
	}
}

