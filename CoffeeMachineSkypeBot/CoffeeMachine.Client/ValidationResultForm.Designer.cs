namespace CoffeeMachine.Client
{
	partial class ValidationResultForm
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
			this.components = new System.ComponentModel.Container();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.importValidationResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.rowIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.importValidationResultBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rowIdDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.importValidationResultBindingSource;
			this.dataGridView1.Location = new System.Drawing.Point(13, 13);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.Size = new System.Drawing.Size(836, 433);
			this.dataGridView1.TabIndex = 0;
			// 
			// importValidationResultBindingSource
			// 
			this.importValidationResultBindingSource.DataSource = typeof(CoffeeMachine.Abstraction.Dto.ImportValidationResult);
			// 
			// rowIdDataGridViewTextBoxColumn
			// 
			this.rowIdDataGridViewTextBoxColumn.DataPropertyName = "RowId";
			this.rowIdDataGridViewTextBoxColumn.HeaderText = "RowId";
			this.rowIdDataGridViewTextBoxColumn.Name = "rowIdDataGridViewTextBoxColumn";
			this.rowIdDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// messageDataGridViewTextBoxColumn
			// 
			this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
			this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
			this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
			this.messageDataGridViewTextBoxColumn.ReadOnly = true;
			this.messageDataGridViewTextBoxColumn.Width = 450;
			// 
			// ValidationResultForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(861, 458);
			this.Controls.Add(this.dataGridView1);
			this.MaximizeBox = false;
			this.Name = "ValidationResultForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Validation results";
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.importValidationResultBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn rowIdDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource importValidationResultBindingSource;
	}
}