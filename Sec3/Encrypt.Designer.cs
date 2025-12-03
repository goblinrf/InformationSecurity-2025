namespace Magma
{
	partial class Encrypt
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
			txtOpen = new TextBox();
			txtClose = new TextBox();
			txtKey = new TextBox();
			lblKey = new Label();
			btnEncrypt = new Button();
			lblFirst = new Label();
			lblSecond = new Label();
			SuspendLayout();
			// 
			// txtOpen
			// 
			txtOpen.Location = new Point(13, 42);
			txtOpen.Margin = new Padding(4, 5, 4, 5);
			txtOpen.Name = "txtOpen";
			txtOpen.ReadOnly = true;
			txtOpen.Size = new Size(399, 27);
			txtOpen.TabIndex = 8;
			txtOpen.Click += txtOpen_Click;
			// 
			// txtClose
			// 
			txtClose.Location = new Point(13, 100);
			txtClose.Margin = new Padding(4, 5, 4, 5);
			txtClose.Name = "txtClose";
			txtClose.ReadOnly = true;
			txtClose.Size = new Size(399, 27);
			txtClose.TabIndex = 9;
			txtClose.Click += txtClose_Click;
			// 
			// txtKey
			// 
			txtKey.Location = new Point(13, 160);
			txtKey.Margin = new Padding(4, 5, 4, 5);
			txtKey.Name = "txtKey";
			txtKey.Size = new Size(399, 27);
			txtKey.TabIndex = 11;
			txtKey.Click += txtKey_Click;
			// 
			// lblKey
			// 
			lblKey.AutoSize = true;
			lblKey.Location = new Point(13, 135);
			lblKey.Name = "lblKey";
			lblKey.Size = new Size(46, 20);
			lblKey.TabIndex = 12;
			lblKey.Text = "Ключ";
			// 
			// btnEncrypt
			// 
			btnEncrypt.Location = new Point(111, 228);
			btnEncrypt.Name = "btnEncrypt";
			btnEncrypt.Size = new Size(194, 30);
			btnEncrypt.TabIndex = 13;
			btnEncrypt.Text = "Зашифровать";
			btnEncrypt.UseVisualStyleBackColor = true;
			btnEncrypt.Click += btnEncrypt_Click;
			// 
			// lblFirst
			// 
			lblFirst.AutoSize = true;
			lblFirst.Location = new Point(13, 17);
			lblFirst.Name = "lblFirst";
			lblFirst.Size = new Size(185, 20);
			lblFirst.TabIndex = 15;
			lblFirst.Text = "Незашифрованный файл";
			// 
			// lblSecond
			// 
			lblSecond.AutoSize = true;
			lblSecond.Location = new Point(13, 75);
			lblSecond.Name = "lblSecond";
			lblSecond.Size = new Size(167, 20);
			lblSecond.TabIndex = 16;
			lblSecond.Text = "Зашифрованный файл";
			// 
			// Encrypt
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(426, 267);
			Controls.Add(lblSecond);
			Controls.Add(lblFirst);
			Controls.Add(btnEncrypt);
			Controls.Add(lblKey);
			Controls.Add(txtKey);
			Controls.Add(txtClose);
			Controls.Add(txtOpen);
			Name = "Encrypt";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Encrypt";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private TextBox txtOpen;
		private TextBox txtClose;
		private TextBox txtKey;
		private Label lblKey;
		private Button btnEncrypt;
		private Label lblFirst;
		private Label lblSecond;
	}
}