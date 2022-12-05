using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lead.Tool.Manager
{
    public delegate void IconModelBak(string ToolTypeName);
    public partial class IconMode : UserControl
    {
        string _name = "";
        public event IconModelBak IconModelBakEvent;
        Button b;
        public IconMode(string name,Image Icon)
        {
            InitializeComponent();
            b = new Button();
            b.Name = name;
            _name = name;
            b.Image = Icon;
            b.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(b);
            b.Click += new EventHandler(AddIcon);
            b.MouseEnter += new EventHandler(btnMainThreadException_MouseEnter);
        }


        private void btnMainThreadException_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show(b.Name, this.b);
        }

        private void AddIcon(object sender, EventArgs e)
        {
            IconModelBakEvent(_name);
        }

        private void panel3_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("_name",this);
        }
    }
}
