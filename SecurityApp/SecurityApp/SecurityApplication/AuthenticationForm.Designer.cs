namespace SecurityApplication
{
	partial class AuthenticationForm
	{
		private System.ComponentModel.IContainer components = null;

		// Main Controls
		private Panel mainPanel;
		private Panel headerPanel;
		private Panel formPanel;
		private Panel footerPanel;

		// Header Controls
		private Label lblTitle;
		private Label lblSubtitle;

		// Form Controls
		private Label lblUsername;
		private Label lblPassword;
		private TextBox txtUsername;
		private TextBox txtPassword;

		// Footer Controls
		private Button btnAuthenticate;
		private Button btnHelp;

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
			this.ClientSize = new System.Drawing.Size(400, 300);
			this.Text = "Аутентификация в системе";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);

			InitializeHeaderPanel();
			InitializeFormPanel();
			InitializeFooterPanel();
			LayoutMainForm();
		}

		private void InitializeHeaderPanel()
		{
			headerPanel = new Panel();
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 80;
			headerPanel.BackColor = Color.FromArgb(70, 130, 180);
			headerPanel.Padding = new Padding(20);

			lblTitle = new Label();
			lblTitle.Text = "Авторизация пользователя";
			lblTitle.ForeColor = Color.White;
			lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblTitle.Dock = DockStyle.Top;
			lblTitle.Height = 35;
			lblTitle.TextAlign = ContentAlignment.MiddleCenter;

			headerPanel.Controls.Add(lblSubtitle);
			headerPanel.Controls.Add(lblTitle);
		}

		private void InitializeFormPanel()
		{
			formPanel = new Panel();
			formPanel.Dock = DockStyle.Fill;
			formPanel.BackColor = Color.White;
			formPanel.Padding = new Padding(40);

			// Username Section
			lblUsername = new Label();
			lblUsername.Text = "Имя пользователя:";
			lblUsername.Location = new Point(0, 20);
			lblUsername.Size = new Size(150, 25);
			lblUsername.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

			txtUsername = new TextBox();
			txtUsername.Location = new Point(160, 20);
			txtUsername.Size = new Size(180, 30);
			txtUsername.BorderStyle = BorderStyle.FixedSingle;
			txtUsername.PlaceholderText = "Введите логин";

			// Password Section
			lblPassword = new Label();
			lblPassword.Text = "Пароль:";
			lblPassword.Location = new Point(0, 70);
			lblPassword.Size = new Size(150, 25);
			lblPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

			txtPassword = new TextBox();
			txtPassword.Location = new Point(160, 70);
			txtPassword.Size = new Size(180, 30);
			txtPassword.BorderStyle = BorderStyle.FixedSingle;
			txtPassword.UseSystemPasswordChar = true;
			txtPassword.PlaceholderText = "Введите пароль";

			formPanel.Controls.AddRange(new Control[] {
				lblUsername, txtUsername,
				lblPassword, txtPassword
			});
		}

		private void InitializeFooterPanel()
		{
			footerPanel = new Panel();
			footerPanel.Dock = DockStyle.Bottom;
			footerPanel.Height = 70;
			footerPanel.BackColor = Color.FromArgb(240, 240, 240);
			footerPanel.Padding = new Padding(20);

			btnHelp = new Button();
			btnHelp.Text = "Справка";
			btnHelp.Size = new Size(100, 35);
			btnHelp.Location = new Point(20, 15);
			btnHelp.BackColor = Color.FromArgb(200, 200, 200);
			btnHelp.FlatStyle = FlatStyle.Flat;

			btnAuthenticate = new Button();
			btnAuthenticate.Text = "Войти";
			btnAuthenticate.Size = new Size(140, 35);
			btnAuthenticate.Location = new Point(220, 15);
			btnAuthenticate.BackColor = Color.FromArgb(70, 130, 180);
			btnAuthenticate.ForeColor = Color.White;
			btnAuthenticate.FlatStyle = FlatStyle.Flat;

			footerPanel.Controls.Add(btnHelp);
			footerPanel.Controls.Add(btnAuthenticate);
		}

		private void LayoutMainForm()
		{
			this.Controls.AddRange(new Control[] {
				formPanel,
				footerPanel,
				headerPanel
			});
		}
	}
}