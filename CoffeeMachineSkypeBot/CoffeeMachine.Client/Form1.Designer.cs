namespace CoffeeMachine.Client
{
	partial class Form1
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
			this.tabStatistics = new System.Windows.Forms.TabPage();
			this.tabControl1.SuspendLayout();
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
			this.tabUserApproval.Location = new System.Drawing.Point(4, 22);
			this.tabUserApproval.Name = "tabUserApproval";
			this.tabUserApproval.Padding = new System.Windows.Forms.Padding(3);
			this.tabUserApproval.Size = new System.Drawing.Size(942, 320);
			this.tabUserApproval.TabIndex = 0;
			this.tabUserApproval.Text = "Users to approve ";
			this.tabUserApproval.UseVisualStyleBackColor = true;
			// 
			// tabStatistics
			// 
			this.tabStatistics.Location = new System.Drawing.Point(4, 22);
			this.tabStatistics.Name = "tabStatistics";
			this.tabStatistics.Padding = new System.Windows.Forms.Padding(3);
			this.tabStatistics.Size = new System.Drawing.Size(942, 320);
			this.tabStatistics.TabIndex = 1;
			this.tabStatistics.Text = "Statisctics";
			this.tabStatistics.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(974, 370);
			this.Controls.Add(this.tabControl1);
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "CoffeeMachine Client";
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabUserApproval;
		private System.Windows.Forms.TabPage tabStatistics;
	}
}

