using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Tool.Interface;

namespace Lead.Tool.Manager
{
    public partial class StateManger : UserControl
    {
        private Dictionary<string, ITool> ToolsList = null;
        private Dictionary<string, IToolState> OldToolsList = new Dictionary<string, IToolState>();
        private bool IsFirst = true;

        public StateManger( ref Dictionary<string, ITool> tool)
        {
            InitializeComponent();

            ToolsList = tool;
            foreach (var item in ToolsList)
            {
                this.dataGridView1.Rows.Add(item.Key);
                OldToolsList.Add(item.Key, item.Value.State);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                foreach (var item in ToolsList)
                {
                    if (!IsFirst)
                    {
                        if (!OldToolsList.ContainsKey(item.Key))
                        {
                            OldToolsList.Add(item.Key, item.Value.State);
                        }
                        if (item.Value.State == OldToolsList[item.Key])
                        {
                            continue;
                        }
                    }

                    if (this.dataGridView1.Rows[i].Cells[0].Value.ToString() == item.Key)
                    {
                        OldToolsList[item.Key] = item.Value.State;
                        this.dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.White;
                        this.dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.White;
                        this.dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.White;
                        this.dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.White;

                        this.dataGridView1.Rows[i].Cells[1 + ((int)item.Value.State)].Style.BackColor =
                            item.Value.State == IToolState.ToolMin ? Color.Gray :
                            item.Value.State == IToolState.ToolInit ? Color.GreenYellow :
                            item.Value.State == IToolState.ToolRunning ? Color.Green :Color.Red;
                    }
                }
            }
            IsFirst = false;
        }
    }
}
