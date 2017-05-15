using CoffeeMachine.Abstraction.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeMachine.Client
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			this.tabControl1.Selected += TabControl1_Selected;
			this.tabControl1.TabIndexChanged += TabControl1_TabIndexChanged;

		}

		private void TabControl1_Selected(object sender, TabControlEventArgs e)
		{
			var tab = (sender as TabControl);
			if (tab != null)
			{
				var testBox = tab.Controls.Find("txtFilePath", false).FirstOrDefault() as TextBox;
				if (testBox != null)
				{

				}
			}
		}

		private void TabControl1_TabIndexChanged(object sender, EventArgs e)
		{
			var tab = (sender as TabControl);
			if (tab != null)
			{

			}
		}

		private async void Form1_Load(object sender, EventArgs e)
		{
			var selectedTab = this.tabControl1.SelectedTab;

			var listView = (selectedTab.Controls.Find("listUsersToApprove", true).FirstOrDefault() as ListView);
			if (listView != null)
			{
				List<Abstraction.Models.PendingUsersResponse> users = null;
				try
				{
					users = await GetUsersForApproval();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

				if (users != null && users.Count > 0)
				{
					foreach (var item in users)
					{
						var viewItem = new ListViewItem();
						viewItem.Text = item.UserName;
						viewItem.ToolTipText = item.Identifier;
						viewItem.Tag = item;
						listView.Items.Add(viewItem);
					}
				}
			}
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog fileDialog = new OpenFileDialog())
			{
				fileDialog.CheckFileExists = true;
				fileDialog.CheckPathExists = true;
				fileDialog.Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
				fileDialog.DefaultExt = "txt";
				fileDialog.ShowReadOnly = true;
				fileDialog.Title = "Choose file for upload";

				var disalogResult = fileDialog.ShowDialog();
				if (disalogResult == DialogResult.OK)
				{
					txtFilePath.Text = fileDialog.FileName;
				}
			}
		}

		private async void btnUpload_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(txtFilePath.Text))
			{
				List<ImportValidationResult> validationResult = null;
				try
				{
					validationResult = await this.AsyncSendFile(txtFilePath.Text);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
				if (validationResult != null && validationResult.Any())
				{
					if (MessageBox.Show(this, "File was uploaded with some error(s). \n\r Do you want to see?",
										"Attention", MessageBoxButtons.YesNo) == DialogResult.Yes)
					{
						var vrf = new ValidationResultForm();
						var container = (vrf as IDataContainer);
						if (container != null)
						{
							container.SetData(validationResult);
						}
						vrf.ShowDialog(this);
					}
				}
				else
				{
					MessageBox.Show(this, "File was uploaded with no errors.");
				}
			}
			else
			{
				MessageBox.Show(this, "You need to select file for upload.");
			}
		}

		private async void btnApproveSelected_Click(object sender, EventArgs e)
		{
			var selected = listUsersToApprove.SelectedItems;
			if (selected.Count > 0)
			{
				var ids = new List<int>(selected.Count);
				foreach (var item in selected)
				{
					var lvi = (item as ListViewItem);
					if (lvi != null && (lvi.Tag as Abstraction.Models.PendingUsersResponse) != null)
					{
						ids.Add((lvi.Tag as Abstraction.Models.PendingUsersResponse).Id);
					}
				}

				if (ids.Any())
				{
					try
					{
						var result = await ApproveUsersAsync(ids);
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, ex.Message);
					}
				}
			}
			else
			{
				MessageBox.Show(this, "Please, select user(s) for approval.");
			}
		}

		private async Task<List<ImportValidationResult>> AsyncSendFile(string filePath)
		{
			const string api = "api/StatisticsApi/uploadsinglefile";
			var url = String.Concat(ConfigurationManager.AppSettings["serverHost"], api);

			HttpContent fileContent = null;
			using (var file = File.OpenRead(filePath))
			{
				byte[] buff = new byte[file.Length];
				await file.ReadAsync(buff, 0, (int)file.Length);

				fileContent = new ByteArrayContent(buff);
			}

			using (var client = new HttpClient())
			{
				using (var formData = new MultipartFormDataContent())
				{
					fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");

					formData.Add(fileContent, "file", "fileName");

					//call service
					var response = await client.PostAsync(url, formData);
					if (!response.IsSuccessStatusCode)
					{
						throw new ApplicationException(response.ReasonPhrase);
					}

					var validationResult = await response.Content.ReadAsAsync<List<ImportValidationResult>>();

					return validationResult;
				}
			}
		}

		private async Task<List<Abstraction.Models.PendingUsersResponse>> GetUsersForApproval()
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Accept", "application/json");

				const string api = "api/pending/getall";
				var response = await client.GetAsync(String.Concat(ConfigurationManager.AppSettings["serverHost"], api));

				if (response.IsSuccessStatusCode)
				{
					var users = await response.Content.ReadAsAsync<List<Abstraction.Models.PendingUsersResponse>>();
					return users;
				}
			}

			return new List<Abstraction.Models.PendingUsersResponse>(0);
		}

		private async Task<bool> ApproveUsersAsync(List<int> ids)
		{
			const string route = "api/pending/byids";
			var url = String.Concat(ConfigurationManager.AppSettings["serverHost"], route);

			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.PostAsJsonAsync<List<int>>(url, ids);

				if (!response.IsSuccessStatusCode)
				{
					return false;
				}

				return true;
			}
		}
	}
}
