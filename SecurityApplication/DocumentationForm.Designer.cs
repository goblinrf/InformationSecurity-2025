namespace SecurityApplication
{
	partial class DocumentationForm
	{
		private System.ComponentModel.IContainer components = null;
		private TextBox txtDocumentation;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(500, 400);
			this.Text = "Справка о системе";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;

			InitializeDocumentationText();
		}

		private void InitializeDocumentationText()
		{
			txtDocumentation = new TextBox();
			txtDocumentation.Dock = DockStyle.Fill;
			txtDocumentation.Multiline = true;
			txtDocumentation.ReadOnly = true;
			txtDocumentation.ScrollBars = ScrollBars.Vertical;
			txtDocumentation.BorderStyle = BorderStyle.None;
			txtDocumentation.BackColor = Color.White;
			txtDocumentation.Font = new Font("Consolas", 10F, FontStyle.Regular);
			txtDocumentation.Padding = new Padding(10);

			string documentationText = @"
╔═══════════════════════════════════════════════╗
║              СИСТЕМА БЕЗОПАСНОСТИ            ║
╚═══════════════════════════════════════════════╝

▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
ИНФОРМАЦИЯ О РАЗРАБОТЧИКЕ
▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
• Группа: ПИбд-43
• Студент: Кислица Егор
• Вариант: 13

▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
ХАРАКТЕРИСТИКИ СИСТЕМЫ
▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬▬
• Режим шифрования DES: ECB
• Добавление к ключу случайного значения: да
• Алгоритм хэширования: MD4
• Ограничения на выбираемые пароли: Несовпадение с именем пользователя
";

			txtDocumentation.Text = documentationText;
			this.Controls.Add(txtDocumentation);
		}
	}
}