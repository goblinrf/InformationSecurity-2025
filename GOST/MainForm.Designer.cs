namespace Md4
{
	partial class MainForm
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

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			lblFile = new Label();
			txtPath = new TextBox();
			btnSelectFile = new Button();
			btnComputeHash = new Button();
			lblHash = new Label();
			txtHash = new TextBox();
			btnClear = new Button();
			button1 = new Button();
			btnSaveToFile = new Button();
			SuspendLayout();
			// 
			// lblFile
			// 
			lblFile.AutoSize = true;
			lblFile.Location = new Point(26, 94);
			lblFile.Margin = new Padding(4, 0, 4, 0);
			lblFile.Name = "lblFile";
			lblFile.Size = new Size(48, 20);
			lblFile.TabIndex = 8;
			lblFile.Text = "Файл:";
			// 
			// txtPath
			// 
			txtPath.Location = new Point(26, 119);
			txtPath.Margin = new Padding(4, 5, 4, 5);
			txtPath.Name = "txtPath";
			txtPath.ReadOnly = true;
			txtPath.Size = new Size(399, 27);
			txtPath.TabIndex = 7;
			// 
			// btnSelectFile
			// 
			btnSelectFile.Location = new Point(26, 156);
			btnSelectFile.Margin = new Padding(4, 5, 4, 5);
			btnSelectFile.Name = "btnSelectFile";
			btnSelectFile.Size = new Size(200, 46);
			btnSelectFile.TabIndex = 6;
			btnSelectFile.Text = "Выбрать файл";
			btnSelectFile.UseVisualStyleBackColor = true;
			btnSelectFile.Click += btnSelectFile_Click;
			// 
			// btnComputeHash
			// 
			btnComputeHash.Location = new Point(234, 156);
			btnComputeHash.Margin = new Padding(4, 5, 4, 5);
			btnComputeHash.Name = "btnComputeHash";
			btnComputeHash.Size = new Size(191, 46);
			btnComputeHash.TabIndex = 5;
			btnComputeHash.Text = "Получить хеш";
			btnComputeHash.UseVisualStyleBackColor = true;
			btnComputeHash.Click += btnComputeHash_Click;
			// 
			// lblHash
			// 
			lblHash.AutoSize = true;
			lblHash.Location = new Point(26, 242);
			lblHash.Margin = new Padding(4, 0, 4, 0);
			lblHash.Name = "lblHash";
			lblHash.Size = new Size(92, 20);
			lblHash.TabIndex = 4;
			lblHash.Text = "Hex от хэша";
			// 
			// txtHash
			// 
			txtHash.Location = new Point(26, 267);
			txtHash.Margin = new Padding(4, 5, 4, 5);
			txtHash.Multiline = true;
			txtHash.Name = "txtHash";
			txtHash.ReadOnly = true;
			txtHash.ScrollBars = ScrollBars.Vertical;
			txtHash.Size = new Size(399, 32);
			txtHash.TabIndex = 3;
			// 
			// btnClear
			// 
			btnClear.Location = new Point(26, 309);
			btnClear.Margin = new Padding(4, 5, 4, 5);
			btnClear.Name = "btnClear";
			btnClear.Size = new Size(200, 46);
			btnClear.TabIndex = 1;
			btnClear.Text = "Очистить";
			btnClear.UseVisualStyleBackColor = true;
			btnClear.Click += btnClear_Click;
			// 
			// button1
			// 
			button1.Location = new Point(26, 14);
			button1.Margin = new Padding(4, 5, 4, 5);
			button1.Name = "button1";
			button1.Size = new Size(399, 46);
			button1.TabIndex = 11;
			button1.Text = "Данные о варианте";
			button1.UseVisualStyleBackColor = true;
			button1.Click += btnInfo_Click;
			// 
			// btnSaveToFile
			// 
			btnSaveToFile.Location = new Point(234, 309);
			btnSaveToFile.Margin = new Padding(4, 5, 4, 5);
			btnSaveToFile.Name = "btnSaveToFile";
			btnSaveToFile.Size = new Size(191, 46);
			btnSaveToFile.TabIndex = 12;
			btnSaveToFile.Text = "Сохранить в файл";
			btnSaveToFile.UseVisualStyleBackColor = true;
			btnSaveToFile.Click += btnSaveToFile_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(452, 370);
			Controls.Add(btnSaveToFile);
			Controls.Add(button1);
			Controls.Add(btnClear);
			Controls.Add(txtHash);
			Controls.Add(lblHash);
			Controls.Add(btnComputeHash);
			Controls.Add(btnSelectFile);
			Controls.Add(txtPath);
			Controls.Add(lblFile);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			Margin = new Padding(4, 5, 4, 5);
			MaximizeBox = false;
			Name = "MainForm";
			Text = "Md4";
			ResumeLayout(false);
			PerformLayout();
		}
		private Label lblFile;
		private TextBox txtPath;
		private Button btnSelectFile;
		private Button btnComputeHash;
		private Label lblHash;
		private TextBox txtHash;
		private Button btnClear;
		private Button button1;
		private Button btnSaveToFile;
	}
}