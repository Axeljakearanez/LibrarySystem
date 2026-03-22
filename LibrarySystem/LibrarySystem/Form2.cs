using System;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }

        private void booksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 books = new Form3();
            books.Show();
        }

        private void borrowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 borrower = new Form4();
            borrower.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}