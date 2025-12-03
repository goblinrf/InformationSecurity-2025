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
	public partial class Initial : Form
	{
		public Initial()
		{
			InitializeComponent();
		}

		private void btnEncrypt_Click(object sender, EventArgs e)
		{
			var encryptForm = new Encrypt();
			encryptForm.FormClosed += (s, ea) => this.Show();
			encryptForm.Show();
			this.Hide();
		}
		private void btnDecrypt_Click(object sender, EventArgs e)
		{
			var decryptForm = new Decrypt();
			decryptForm.FormClosed += (s, ea) => this.Show();
			decryptForm.Show();
			this.Hide();
		}

		private void btnInfo_Click(object sender, EventArgs e)
		{
			var readmeForm = new Info();
			readmeForm.FormClosed += (s, ea) => this.Show();
			readmeForm.Show();
			this.Hide();
		}
	}
}
