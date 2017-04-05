﻿using System;
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
			var tab = this.tabControl1.SelectedTab;

			var listView = (tab.Controls.Find("listUsersToApprove", true).FirstOrDefault() as ListView);
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
						viewItem.Text = item.Identifier;
						viewItem.ToolTipText = item.UserName;

						listView.Items.Add(viewItem);
					}
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

		private async Task<bool> AsyncSendFile(string filePath)
		{
			const string api = "api/StatisticsApi/upload";
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
					formData.Add(fileContent, "file", "fileName");

					//call service
					var response = await client.PostAsync(url, formData);
					if (!response.IsSuccessStatusCode)
					{
						return false;
					}
				}

				return true;
			}
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.Title = "Choose file for upload";
				ofd.Filter = "Text Files(*.txt) | *.txt | All Files(*.*) | *.*";

				var disalogResult = ofd.ShowDialog();
				if (disalogResult == DialogResult.OK)
				{
					txtFilePath.Text = ofd.FileName;
				}
			}

		}

		private async void btnUpload_Click(object sender, EventArgs e)
		{
			var result = await this.AsyncSendFile(txtFilePath.Text);
			if (result)
			{
				MessageBox.Show("File was uploaded.");
			}
		}
	}
}
