using System;
using System.Data;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class Form4 : Form
    {
        DataTable dt = new DataTable();

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("Borrower ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Address");
            dt.Columns.Add("Contact No");
            grid1.DataSource = dt;

            // Make grid read only
            grid1.ReadOnly = true;

            // Clean grid appearance
            grid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            grid1.BackgroundColor = System.Drawing.Color.White;
            grid1.GridColor = System.Drawing.Color.LightGray;
            grid1.RowHeadersVisible = false;
            grid1.AllowUserToResizeRows = false;
            grid1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // Header style
            grid1.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.SteelBlue;
            grid1.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            grid1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            grid1.ColumnHeadersDefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grid1.ColumnHeadersHeight = 35;

            // Row style
            grid1.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            grid1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SteelBlue;
            grid1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            grid1.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grid1.RowTemplate.Height = 30;

            // Alternating row colors
            grid1.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.AliceBlue;

            // Fix search textbox visibility
            txtSearch.ForeColor = System.Drawing.Color.Black;
            txtSearch.BackColor = System.Drawing.Color.White;
            txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBorrowerID.Text) ||
                string.IsNullOrEmpty(txtName.Text) ||
                string.IsNullOrEmpty(txtAddress.Text) ||
                string.IsNullOrEmpty(txtContact.Text))
            {
                MessageBox.Show("Please fill all fields.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dt.Rows.Add(txtBorrowerID.Text, txtName.Text,
                txtAddress.Text, txtContact.Text);
            ClearFields();
            MessageBox.Show("Borrower added successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (grid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = grid1.SelectedRows[0];
            txtBorrowerID.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            txtAddress.Text = row.Cells[2].Value.ToString();
            txtContact.Text = row.Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (grid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataGridViewRow row = grid1.SelectedRows[0];
            row.Cells[0].Value = txtBorrowerID.Text;
            row.Cells[1].Value = txtName.Text;
            row.Cells[2].Value = txtAddress.Text;
            row.Cells[3].Value = txtContact.Text;
            ClearFields();
            MessageBox.Show("Borrower updated successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grid1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Are you sure you want to delete?",
                "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                grid1.Rows.RemoveAt(grid1.SelectedRows[0].Index);
                ClearFields();
                MessageBox.Show("Borrower deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Records saved!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.ToLower();

            DataView dv = dt.DefaultView;
            if (string.IsNullOrEmpty(search))
            {
                dv.RowFilter = "";
            }
            else
            {
                dv.RowFilter = string.Format(
                    "CONVERT([Borrower ID], System.String) LIKE '%{0}%' OR " +
                    "CONVERT([Name], System.String) LIKE '%{0}%' OR " +
                    "CONVERT([Address], System.String) LIKE '%{0}%' OR " +
                    "CONVERT([Contact No], System.String) LIKE '%{0}%'", search);
            }

            grid1.DataSource = dv;
        }

        private void txtContact_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void ClearFields()
        {
            txtBorrowerID.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtContact.Clear();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}