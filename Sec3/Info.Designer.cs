namespace Magma
{
	partial class Info
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
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// textBox1
			// 
			textBox1.Location = new Point(12, 12);
			textBox1.Multiline = true;
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(419, 391);
			textBox1.TabIndex = 0;
			textBox1.Text = "Вариант: 6\r\nАлгоритм: ГОСТ Р 34.12-2018 Магма (ГОСТ 28147). Режим гаммирования с ОС.\r\nГруппа: ПИбд-43\r\nСтудент: Кислица Егор\r\nОписание алгоритма:\r\n1. Из начального значения (IV) с помощью шифра Магма получают первый блок гаммы.\r\n2. Каждый следующий блок гаммы вычисляется путём шифрования предыдущего блока шифртекста.\r\n3. Гамма побайтно складывается по XOR с открытым текстом — получается шифртекст.\r\n4. Для расшифровки выполняется тот же XOR гаммы с шифртекстом — получается исходный текст.\r\n5. То есть Магма создаёт последовательность гаммы, а шифрование/дешифрование идёт через XOR.";
			// 
			// Info
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(447, 415);
			Controls.Add(textBox1);
			Name = "Info";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "InformationForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBox1;
	}
}