namespace Md4
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void btnSelectFile_Click(object sender, EventArgs e)
		{
			using (var dlg = new OpenFileDialog())
			{
				dlg.Title = "Выберите файл для хеширования";
				dlg.Filter = "Все файлы (*.*)|*.*";
				dlg.Multiselect = false;

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					txtPath.Text = dlg.FileName;
				}
			}
		}

		private void btnComputeHash_Click(object sender, EventArgs e)
		{
			var path = txtPath.Text;

			if (string.IsNullOrWhiteSpace(path))
			{
				MessageBox.Show("Пожалуйста, выберите файл.", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!File.Exists(path))
			{
				MessageBox.Show("Выбранный файл не существует.", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				Cursor = Cursors.WaitCursor;
				btnComputeHash.Enabled = false;

				byte[] bytes = File.ReadAllBytes(path);
				byte[] hashBytes = Md4.ComputeHash(bytes);

				string hash = BitConverter.ToString(hashBytes)
					.Replace("-", "")
					.ToLowerInvariant();

				txtHash.Text = hash;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при вычислении хеша: {ex.Message}",
					"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				Cursor = Cursors.Default;
				btnComputeHash.Enabled = true;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			txtHash.Clear();
		}

		private void btnInfo_Click(object sender, EventArgs e)
		{
			var info = new InfoForm();
			info.FormClosed += (_, __) => this.Show();
			info.Show();
			this.Hide();
		}

		public void SaveHashToFile(string hashString, string filePath)
		{
			File.WriteAllText(filePath, hashString);
		}

		private void btnSaveToFile_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtHash.Text))
			{
				MessageBox.Show("Нет хеша", "Информация",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			using (var dlg = new SaveFileDialog())
			{
				dlg.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
				dlg.FileName = $"hash_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					try
					{
						File.WriteAllText(dlg.FileName, txtHash.Text);

						MessageBox.Show($"Хеш сохранен в файл:\n{dlg.FileName}",
							"Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}
	}
}

