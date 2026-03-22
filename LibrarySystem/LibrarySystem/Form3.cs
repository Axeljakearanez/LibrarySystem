using System;
using System.Data;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class Form3 : Form
    {
        DataTable dt = new DataTable();

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("Book ID");
            dt.Columns.Add("Title");
            dt.Columns.Add("Author");
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
            if (string.IsNullOrEmpty(txtBookID.Text) ||
                string.IsNullOrEmpty(txtTitle.Text) ||
                string.IsNullOrEmpty(txtAuthor.Text))
            {
                MessageBox.Show("Please fill all fields.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dt.Rows.Add(txtBookID.Text, txtTitle.Text, txtAuthor.Text);
            ClearFields();
            MessageBox.Show("Book added successfully!", "Success",
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
            txtBookID.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            txtAuthor.Text = row.Cells[2].Value.ToString();
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
            row.Cells[0].Value = txtBookID.Text;
            row.Cells[1].Value = txtTitle.Text;
            row.Cells[2].Value = txtAuthor.Text;
            ClearFields();
            MessageBox.Show("Book updated successfully!", "Success",
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
                MessageBox.Show("Book deleted successfully!", "Success",
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

            // Unbind first
            grid1.DataSource = null;

            // Filter the DataTable
            DataView dv = dt.DefaultView;
            if (string.IsNullOrEmpty(search))
            {
                dv.RowFilter = "";
            }
            else
            {
                dv.RowFilter = string.Format(
                    "CONVERT([Book ID], System.String) LIKE '%{0}%' OR " +
                    "CONVERT([Title], System.String) LIKE '%{0}%' OR " +
                    "CONVERT([Author], System.String) LIKE '%{0}%'", search);
            }

            // Rebind
            grid1.DataSource = dv;
        }

        private void ClearFields()
        {
            txtBookID.Clear();
            txtTitle.Clear();
            txtAuthor.Clear();
        }
    }
}