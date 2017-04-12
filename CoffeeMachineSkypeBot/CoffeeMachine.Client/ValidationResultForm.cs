using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CoffeeMachine.Abstraction.Dto;

namespace CoffeeMachine.Client
{
	public partial class ValidationResultForm : Form, IDataContainer
	{
		private List<ImportValidationResult> validationData;

		public ValidationResultForm()
		{
			InitializeComponent();
			this.Load += ValidationResultForm_Load;
		}

		public void SetData(object data)
		{
			if ((validationData = (data as List<ImportValidationResult>)) == null)
			{
				validationData = new List<ImportValidationResult>();
			}
		}

		private void ValidationResultForm_Load(object sender, EventArgs e)
		{
			this.dataGridView1.DataSource = validationData;
		}
	}
}
