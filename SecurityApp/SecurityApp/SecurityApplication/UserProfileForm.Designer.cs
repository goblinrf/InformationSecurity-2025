namespace SecurityApplication
{
	partial class UserProfileForm
	{
		private System.ComponentModel.IContainer components = null;

		// Main Controls
		private Panel headerPanel;
		private Panel contentPanel;
		private Panel footerPanel;

		// Header Controls
		private Label lblWelcome;

		// Content Controls
		private Label lblCurrentPassword;
		private Label lblNewPassword;
		private Label lblConfirmPassword;
		private TextBox txtCurrentPassword;
		private TextBox txtNewPassword;
		private TextBox txtConfirmPassword;
		private Label lblPasswordStrength;
		private Label lblConfirmationStatus;
		private TextBox txtRequirements;
		private Label lblValidationMessage;

		// Footer Controls
		private Button btnUpdatePassword;

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
			this.ClientSize = new System.Drawing.Size(450, 500);
			this.Text = "Профиль пользователя";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);

			InitializeHeaderPanel();
			InitializeContentPanel();
			InitializeFooterPanel();
			LayoutForm();
		}

		private void InitializeHeaderPanel()
		{
			headerPanel = new Panel();
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Height = 80;
			headerPanel.BackColor = Color.FromArgb(60, 180, 75);
			headerPanel.Padding = new Padding(20);

			lblWelcome = new Label();
			lblWelcome.Text = "Добро пожаловать, пользователь!";
			lblWelcome.ForeColor = Color.White;
			lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblWelcome.Dock = DockStyle.Fill;
			lblWelcome.TextAlign = ContentAlignment.MiddleCenter;

			headerPanel.Controls.Add(lblWelcome);
		}

		private void InitializeContentPanel()
		{
			contentPanel = new Panel();
			contentPanel.Dock = DockStyle.Fill;
			contentPanel.BackColor = Color.White;
			contentPanel.Padding = new Padding(30);

			// Current Password
			lblCurrentPassword = new Label();
			lblCurrentPassword.Text = "Текущий пароль:";
			lblCurrentPassword.Location = new Point(0, 20);
			lblCurrentPassword.Size = new Size(150, 25);
			lblCurrentPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

			txtCurrentPassword = new TextBox();
			txtCurrentPassword.Location = new Point(160, 20);
			txtCurrentPassword.Size = new Size(270, 30);
			txtCurrentPassword.UseSystemPasswordChar = true;
			txtCurrentPassword.BorderStyle = BorderStyle.FixedSingle;
			txtCurrentPassword.PlaceholderText = "Введите текущий пароль";

			// New Password
			lblNewPassword = new Label();
			lblNewPassword.Text = "Новый пароль:";
			lblNewPassword.Location = new Point(0, 70);
			lblNewPassword.Size = new Size(150, 25);
			lblNewPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

			txtNewPassword = new TextBox();
			txtNewPassword.Location = new Point(160, 70);
			txtNewPassword.Size = new Size(270, 30);
			txtNewPassword.UseSystemPasswordChar = true;
			txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
			txtNewPassword.PlaceholderText = "Введите новый пароль";

			lblPasswordStrength = new Label();
			lblPasswordStrength.Location = new Point(160, 105);
			lblPasswordStrength.Size = new Size(250, 20);
			lblPasswordStrength.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

			// Confirm Password
			lblConfirmPassword = new Label();
			lblConfirmPassword.Text = "Подтверждение:";
			lblConfirmPassword.Location = new Point(0, 140);
			lblConfirmPassword.Size = new Size(150, 25);
			lblConfirmPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

			txtConfirmPassword = new TextBox();
			txtConfirmPassword.Location = new Point(160, 140);
			txtConfirmPassword.Size = new Size(270, 30);
			txtConfirmPassword.UseSystemPasswordChar = true;
			txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
			txtConfirmPassword.PlaceholderText = "Повторите новый пароль";

			lblConfirmationStatus = new Label();
			lblConfirmationStatus.Location = new Point(160, 175);
			lblConfirmationStatus.Size = new Size(200, 20);
			lblConfirmationStatus.Font = new Font("Segoe UI", 8F, FontStyle.Regular);

			// Requirements
			var lblReqTitle = new Label();
			lblReqTitle.Text = "Требования к паролю:";
			lblReqTitle.Location = new Point(0, 210);
			lblReqTitle.Size = new Size(200, 25);
			lblReqTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

			txtRequirements = new TextBox();
			txtRequirements.Location = new Point(20, 240);
			txtRequirements.Size = new Size(410, 80);
			txtRequirements.Multiline = true;
			txtRequirements.ReadOnly = true;
			txtRequirements.BorderStyle = BorderStyle.FixedSingle;
			txtRequirements.BackColor = Color.FromArgb(250, 250, 250);
			txtRequirements.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			txtRequirements.ScrollBars = ScrollBars.Vertical;

			// Validation Message
			lblValidationMessage = new Label();
			lblValidationMessage.Location = new Point(0, 330);
			lblValidationMessage.Size = new Size(360, 25);
			lblValidationMessage.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
			lblValidationMessage.TextAlign = ContentAlignment.MiddleCenter;

			contentPanel.Controls.AddRange(new Control[] {
				lblCurrentPassword, txtCurrentPassword,
				lblNewPassword, txtNewPassword, lblPasswordStrength,
				lblConfirmPassword, txtConfirmPassword, lblConfirmationStatus,
				lblReqTitle, txtRequirements,
				lblValidationMessage
			});
		}

		private void InitializeFooterPanel()
		{
			footerPanel = new Panel();
			footerPanel.Dock = DockStyle.Bottom;
			footerPanel.Height = 70;
			footerPanel.BackColor = Color.FromArgb(240, 240, 240);
			footerPanel.Padding = new Padding(20);

			btnUpdatePassword = new Button();
			btnUpdatePassword.Text = "Обновить пароль";
			btnUpdatePassword.Size = new Size(150, 40);
			btnUpdatePassword.Location = new Point(130, 15);
			btnUpdatePassword.BackColor = Color.FromArgb(60, 180, 75);
			btnUpdatePassword.ForeColor = Color.White;
			btnUpdatePassword.FlatStyle = FlatStyle.Flat;

			footerPanel.Controls.Add(btnUpdatePassword);
		}

		private void LayoutForm()
		{
			this.Controls.AddRange(new Control[] {
				contentPanel,
				footerPanel,
				headerPanel
			});
		}
	}
}