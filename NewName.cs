using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Tool.Manager
{
    public delegate void RetrunNewName(string name);
    public partial class NewName : Form
    {
        public RetrunNewName NewNameEvent;
        public NewName()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewNameEvent(textBox1.Text.Trim());
            this.Close();
        }
    }
}
