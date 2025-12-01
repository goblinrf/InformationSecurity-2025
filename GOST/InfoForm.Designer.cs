namespace Md4
{
	partial class InfoForm
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
			textBox1.ReadOnly = true;
			textBox1.Size = new Size(363, 446);
			textBox1.TabIndex = 0;
			textBox1.Text = "Вариант:     1\r\nХэш-функция: MD4\r\nГруппа:      ПИбд-43\r\nСтудент:     Кислица Егор\r\nОписание алгоритма: \r\n1. Паддинг\r\n2. Сообщение разбивается на 512-битные блоки.\r\n3. Далее для каждого блока:\r\n\tИмеются 4 регистра: A, B, C, D (по 32 бита).\r\n\tКаждый блок обрабатывается в трёх раундах, и в каждом раунде:\r\n\t16 слов по 32 бита проходят через функции (F, G, H),\r\n\tвыполняются циклические сдвиги (ROL).\r\n\r\n4. После обработки блока значения A, B, C, D прибавляются к текущему хеш-состоянию.\r\nРезультат:\r\n4 регистра (A, B, C, D) объединяются, получаются 128 бит хеша.";
			// 
			// InfoForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(388, 466);
			Controls.Add(textBox1);
			Name = "InfoForm";
			Text = "InfoForm";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private TextBox textBox1;
	}
}