namespace Magma
{
	partial class Decrypt
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
			btnEncrypt = new Button();
			lblKey = new Label();
			txtKey = new TextBox();
			txtOpen = new TextBox();
			txtClose = new TextBox();
			lblFirst = new Label();
			lblSecond = new Label();
			SuspendLayout();
			// 
			// btnEncrypt
			// 
			btnEncrypt.Location = new Point(106, 230);
			btnEncrypt.Name = "btnEncrypt";
			btnEncrypt.Size = new Size(213, 36);
			btnEncrypt.TabIndex = 21;
			btnEncrypt.Text = "Дешифровать";
			btnEncrypt.UseVisualStyleBackColor = true;
			btnEncrypt.Click += btnEncrypt_Click;
			// 
			// lblKey
			// 
			lblKey.AutoSize = true;
			lblKey.Location = new Point(13, 144);
			lblKey.Name = "lblKey";
			lblKey.Size = new Size(46, 20);
			lblKey.TabIndex = 20;
			lblKey.Text = "Ключ";
			// 
			// txtKey
			// 
			txtKey.Location = new Point(15, 169);
			txtKey.Margin = new Padding(4, 5, 4, 5);
			txtKey.Name = "txtKey";
			txtKey.Size = new Size(399, 27);
			txtKey.TabIndex = 19;
			txtKey.Click += txtKey_Click;
			// 
			// txtOpen
			// 
			txtOpen.Location = new Point(15, 112);
			txtOpen.Margin = new Padding(4, 5, 4, 5);
			txtOpen.Name = "txtOpen";
			txtOpen.ReadOnly = true;
			txtOpen.Size = new Size(399, 27);
			txtOpen.TabIndex = 17;
			txtOpen.Click += txtOpen_Click;
			// 
			// txtClose
			// 
			txtClose.Location = new Point(15, 48);
			txtClose.Margin = new Padding(4, 5, 4, 5);
			txtClose.Name = "txtClose";
			txtClose.ReadOnly = true;
			txtClose.Size = new Size(399, 27);
			txtClose.TabIndex = 16;
			txtClose.Click += txtClose_Click;
			// 
			// lblFirst
			// 
			lblFirst.AutoSize = true;
			lblFirst.Location = new Point(15, 23);
			lblFirst.Name = "lblFirst";
			lblFirst.Size = new Size(167, 20);
			lblFirst.TabIndex = 22;
			lblFirst.Text = "Зашифрованный файл";
			// 
			// lblSecond
			// 
			lblSecond.AutoSize = true;
			lblSecond.Location = new Point(13, 87);
			lblSecond.Name = "lblSecond";
			lblSecond.Size = new Size(148, 20);
			lblSecond.TabIndex = 23;
			lblSecond.Text = "Куда расшифровать";
			// 
			// Decrypt
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(427, 278);
			Controls.Add(lblSecond);
			Controls.Add(lblFirst);
			Controls.Add(btnEncrypt);
			Controls.Add(lblKey);
			Controls.Add(txtKey);
			Controls.Add(txtOpen);
			Controls.Add(txtClose);
			Name = "Decrypt";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Decrypt";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Button btnEncrypt;
		private Label lblKey;
		private TextBox txtKey;
		private TextBox txtOpen;
		private TextBox txtClose;
		private Label lblFirst;
		private Label lblSecond;
	}
}