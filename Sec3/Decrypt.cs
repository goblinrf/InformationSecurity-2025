using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magma
{
	public partial class Decrypt : Form
	{
		public Decrypt()
		{
			InitializeComponent();
		}

		private void btnEncrypt_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtClose.Text))
			{
				MessageBox.Show("Выберите файл, который необходимо расшифровать.",
					"Файл не выбран", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (string.IsNullOrWhiteSpace(txtOpen.Text))
			{
				MessageBox.Show("Укажите место для сохранения расшифрованного файла.",
					"Путь не указан", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (string.IsNullOrWhiteSpace(txtKey.Text))
			{
				MessageBox.Show("Введите пароль, необходимый для расшифровки.",
					"Пароль отсутствует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				MagmaGostOS.Decrypt(txtClose.Text, txtOpen.Text, txtKey.Text);

				MessageBox.Show("Файл успешно расшифрован!",
					"Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Во время расшифровки возникла ошибка:\n{ex.Message}",
					"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtOpen_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtClose.Text) || !File.Exists(txtClose.Text))
			{
				MessageBox.Show("Сначала выберите зашифрованный файл.",
					"Файл не выбран", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using var dialog = new SaveFileDialog
			{
				Title = "Куда сохранить расшифрованный файл",
				Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
				DefaultExt = ".txt",
				AddExtension = true,
				InitialDirectory = Path.GetDirectoryName(txtClose.Text)
			};

			string encryptedName = Path.GetFileNameWithoutExtension(txtClose.Text);
			encryptedName = encryptedName.Replace("_enc", "");
			dialog.FileName = $"{encryptedName}_dec.txt";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtOpen.Text = dialog.FileName;
			}
		}

		private void txtClose_Click(object sender, EventArgs e)
		{

			using var dialog = new OpenFileDialog
			{
				Title = "Выберите файл для расшифровки",
				Filter = "Зашифрованные файлы (*.enc)|*.enc|Все файлы (*.*)|*.*",
				Multiselect = false
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtClose.Text = dialog.FileName;
			}
			
		}

		private void txtKey_Click(object sender, EventArgs e)
		{
			using var dialog = new OpenFileDialog
			{
				Title = "Выберите файл с паролем",
				Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*",
				FilterIndex = 1,
				Multiselect = false
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					txtKey.Text = File.ReadAllText(dialog.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Не удалось прочитать файл пароля:\n{ex.Message}",
						"Ошибка чтения", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
