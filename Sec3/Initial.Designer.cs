namespace Magma
{
	partial class Initial
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
			btnInfo = new Button();
			btnEncrypt = new Button();
			btnDecrypt = new Button();
			SuspendLayout();
			// 
			// btnInfo
			// 
			btnInfo.Location = new Point(25, 164);
			btnInfo.Name = "btnInfo";
			btnInfo.Size = new Size(202, 72);
			btnInfo.TabIndex = 0;
			btnInfo.Text = "Данные о варианте";
			btnInfo.UseVisualStyleBackColor = true;
			btnInfo.Click += btnInfo_Click;
			// 
			// btnEncrypt
			// 
			btnEncrypt.Location = new Point(25, 14);
			btnEncrypt.Name = "btnEncrypt";
			btnEncrypt.Size = new Size(202, 69);
			btnEncrypt.TabIndex = 1;
			btnEncrypt.Text = "Зашифровать";
			btnEncrypt.UseVisualStyleBackColor = true;
			btnEncrypt.Click += btnEncrypt_Click;
			// 
			// btnDecrypt
			// 
			btnDecrypt.Location = new Point(25, 89);
			btnDecrypt.Name = "btnDecrypt";
			btnDecrypt.Size = new Size(202, 69);
			btnDecrypt.TabIndex = 2;
			btnDecrypt.Text = "Расшифровать";
			btnDecrypt.UseVisualStyleBackColor = true;
			btnDecrypt.Click += btnDecrypt_Click;
			// 
			// Initial
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(249, 248);
			Controls.Add(btnDecrypt);
			Controls.Add(btnEncrypt);
			Controls.Add(btnInfo);
			Name = "Initial";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Initial";
			ResumeLayout(false);
		}

		#endregion

		private Button btnInfo;
		private Button btnEncrypt;
		private Button btnDecrypt;
	}
}