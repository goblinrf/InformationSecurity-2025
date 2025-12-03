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
	public partial class Encrypt : Form
	{
		public Encrypt()
		{
			InitializeComponent();
		}
		private void btnEncrypt_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtOpen.Text))
			{
				MessageBox.Show("Пожалуйста, выберите файл, который нужно зашифровать.",
					"Не указан входной файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (string.IsNullOrWhiteSpace(txtClose.Text))
			{
				MessageBox.Show("Укажите путь, куда сохранить зашифрованный файл.",
					"Путь не выбран", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (string.IsNullOrWhiteSpace(txtKey.Text))
			{
				MessageBox.Show("Введите пароль для шифрования.",
					"Пароль отсутствует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			try
			{
				MagmaGostOS.Encrypt(txtOpen.Text, txtClose.Text, txtKey.Text);

				MessageBox.Show("Файл успешно зашифрован!",
					"Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Произошла ошибка при шифровании:\n{ex.Message}",
					"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void txtOpen_Click(object sender, EventArgs e)
		{
			using var dialog = new OpenFileDialog
			{
				Title = "Выберите файл для шифрования",
				Filter = "Все файлы (*.*)|*.*",
				Multiselect = false
			};

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				txtOpen.Text = dialog.FileName;
			}
		}

		private void txtClose_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtOpen.Text) || !File.Exists(txtOpen.Text))
			{
				MessageBox.Show("Перед сохранением выберите файл, который нужно шифровать.",
					"Сначала выберите входной файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			using var dialog = new SaveFileDialog
			{
				Title = "Куда сохранить зашифрованный файл",
				Filter = "Зашифрованные файлы (*.enc)|*.enc|Все файлы (*.*)|*.*",
				DefaultExt = ".enc",
				AddExtension = true,
				InitialDirectory = Path.GetDirectoryName(txtOpen.Text),
				FileName = $"{Path.GetFileNameWithoutExtension(txtOpen.Text)}_enc.enc"
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

