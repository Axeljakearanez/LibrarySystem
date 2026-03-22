using System;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // leave empty
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            lblError.Text = "";

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Please enter username and password.";
                lblError.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (username == "admin" && password == "admin123")
            {
                Form2 menu = new Form2();
                menu.Show();
                this.Hide();
            }
            else
            {
                lblError.Text = "Invalid username or password.";
                lblError.ForeColor = System.Drawing.Color.Red;
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}