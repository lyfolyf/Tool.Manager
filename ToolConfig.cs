
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lead.Tool.Interface;
using Lead.Tool.XML;


namespace Lead.Tool.Manager
{
    public partial class ToolConfig : Form
    {
        //工具名，工具实例
        public Dictionary<string, ITool> ToolManager = new Dictionary<string, ITool>();
        private static ToolConfig _Instance = null;

        private static DirectoryInfo di = new DirectoryInfo(string.Format(@"{0}", Application.StartupPath));
        private Config _Config = new Config();
        //类名，类创造者(根据特定的类反射得到)
        private Dictionary<string, ICreat> ToolList = new Dictionary<string, ICreat>();
        private string CreaterClassName = "Lead.Tool.Interface.ICreat";
        private string ToolPath = ProjectPath.PathManager.ConfigPath  + @"\Bin\MyTools\";
        private string ToolConfigPath = "";
        private string ConfigPath = "";

        private string CurrentName = "";
        private string DeleteName = "";
        private string DeleteType = "";

        public StateManger StateMangerUI = null;

        public static ToolConfig GetInstance(string ConfigDic)
        {
            if (_Instance == null)
            {
                _Instance = new ToolConfig(ConfigDic);
            }

            return _Instance;
        }

        private ToolConfig(string ConfigDic)
        {
            InitializeComponent();
            ToolConfigPath = ConfigDic + @"\Config\ToolPathConfig\";
            ConfigPath = ConfigDic + @"\Config\ToolPathConfig\AllToolsConfig.xml";

            if (File.Exists(ConfigPath))
            {
                _Config = (Config)XmlSerializerHelper.ReadXML(ConfigPath, typeof(Config));
            }
            else
            {
                _Config = new Config();
            }

            #region 加载可用工具
            LoadPrimTypeAttributes(ToolPath);

            //加载tools属性的dll
            int x = 0;
            int y = 2;
            foreach(var item in ToolList)
            {
                IconMode b = new IconMode(item.Key,item.Value.Icon);
                b.Width = 60;
                
                b.Location = new System.Drawing.Point(x, y);

                b.IconModelBakEvent += new IconModelBak(AddIcon);

                this.panel1.Controls.Add(b);
                x += b.Width;
            }
            #endregion


            #region 加载已选的可用工具
            if (_Config.Param != null)
            {
                foreach (var item in _Config.Param)
                {
                    if (null == ToolManager)
                    {
                        ToolManager = new Dictionary<string, ITool>();
                    }
                    var instance = GetToolInstance(item.Name, item.IToolType, ToolConfigPath + item.ConfigPath);
                    ToolManager.Add(item.Name, instance);

                    //创建一级列表
                    TreeNode pNode = new TreeNode();
                    pNode.Text = item.Name+ " ("+ item .IToolType+ ")";
                    pNode.Tag = item.Name + " (" + item.IToolType + ")";
                    pNode.ImageIndex = treeView1.ImageIndex;

                    TreeNode pNode1 = new TreeNode();
                    pNode1.Text = item.Name + " (Config)";
                    pNode1.Tag = item.Name +" (Config)";
                    pNode.Nodes.Add(pNode1);

                    TreeNode pNode2 = new TreeNode();
                    pNode2.Text = item.Name +" (Debug)";
                    pNode2.Tag = item.Name +" (Debug)";
                    pNode.Nodes.Add(pNode2);

                    treeView1.Nodes.Add(pNode);
                }
            }
            else
            {
                _Config.Param = new List<ConfigParam>();
            }

            #endregion

            StateMangerUI = new StateManger(ref ToolManager);
        }

        private void RetrunName(string name)
        {
            CurrentName = name;
        }

        private ITool GetToolInstance(string Name,string ToolTypeName,string Path)
        {
            foreach (var item in ToolList)
            {
                if (item.Key == ToolTypeName)
                {
                    return item.Value.GetInstance(Name,Path);
                }
            }
            return null;
        }

        private void AddTool(string Name,string ToolTypeName)
        {
            bool IsFined = false;

            foreach (var item in _Config.Param)
            {
                if(item.Name == Name)
                {
                    IsFined = true;
                    break;
                }
            }

            if (IsFined)
            {
                MessageBox.Show("已存在相同命名的工具！");
                return;
            }
            else
            {
                var temp = new ConfigParam()
                {
                    Name = Name,
                    IToolType = ToolTypeName,
                    ConfigPath = ToolTypeName + "_" + Name + ".xml",
                };
                _Config.Param.Add(temp);

                var tempInstance = GetToolInstance(Name,ToolTypeName, ToolConfigPath + temp.ConfigPath);
                ToolManager.Add(Name, tempInstance);


                //创建一级列表
                TreeNode pNode = new TreeNode();
                pNode.Text = CurrentName + " (" + temp.IToolType + ")"; ;
                pNode.Tag = CurrentName + " (" + temp.IToolType + ")"; ;
                pNode.ImageIndex = 1;
                pNode.SelectedImageIndex = 1;

                TreeNode pNode1 = new TreeNode();
                pNode1.Text = CurrentName + " (Config)";
                pNode1.Tag = CurrentName + " (Config)";
                pNode.Nodes.Add(pNode1);

                TreeNode pNode2 = new TreeNode();
                pNode2.Text = CurrentName + " (Debug)";
                pNode2.Tag = CurrentName + " (Debug)";
                pNode.Nodes.Add(pNode2);

                treeView1.Nodes.Add(pNode);
            }

        }

        private void AddIcon(string ToolTypeName)
        {
            Name NameUI = new Name();
            NameUI.StartPosition = FormStartPosition.CenterScreen;
            NameUI.RetrunNameRvent += new RetrunName(RetrunName);
            NameUI.ShowDialog();
            if (CurrentName !="")
            {
                AddTool(CurrentName, ToolTypeName);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var t = e.Node.Text;
            foreach (var item in ToolManager)
            {
                if (t == item.Key + " (Config)")
                {
                    this.Text = "ToolsManager         当前工具名：" + t;
                    this.panel3.Controls.Clear();
                    item.Value.ConfigUI.Dock = DockStyle.Fill;
                    this.panel3.Controls.Add(item.Value.ConfigUI);
                }
                if (t == item.Key + " (Debug)")
                {
                    this.Text = "ToolsManager         当前工具名：" + t;
                    this.panel3.Controls.Clear();
                    item.Value.DebugUI.Dock = DockStyle.Fill;
                    this.panel3.Controls.Add(item.Value.DebugUI);
                }
            }
        }

        public T GetFactoryClass<T>(string dllPath, string className)
        {
            T factory = default( T);
            if (!string.IsNullOrEmpty(dllPath) && !string.IsNullOrEmpty(className))
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dllPath);
                    Type[] types = assembly.GetExportedTypes();
                    foreach (Type type in types)
                    {
                        var temp = type.GetInterfaces();
                        if (type.IsClass)
                        {
                            if (null != type.GetInterface(className))
                            {
                                factory = (T)Activator.CreateInstance(type);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }

            return factory;
        }

        private int LoadPrimTypeAttributes(string primFolderPath)
        {
            int ret = 0;
            int result;
            if (string.IsNullOrEmpty(primFolderPath))
            {
                result = -1;
            }
            else
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(primFolderPath);
                    if (dir == null)
                    {
                        result = -1;
                        return result;
                    }
                    FileSystemInfo[] files = dir.GetFileSystemInfos("*.dll");
                    for (int i = 0; i < files.Length; i++)
                    {
                        FileInfo file = files[i] as FileInfo;
                        if (file != null)
                        {
                            string primFileName = file.FullName;
                            //不包含
                            if (!this.ToolList.Keys.Contains(primFileName))
                            {
                                try
                                {
                                    ICreat CreaterInstance = GetFactoryClass<ICreat>(primFileName, this.CreaterClassName);

                                    if (CreaterInstance != null)
                                    {
                                        this.ToolList.Add(CreaterInstance.Name, CreaterInstance);
                                    }
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show("加载程序"+ primFileName + "失败:"+ex.Message);
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("加载程序集失败:"+ex.Message);
                    throw new Exception("加载程序集失败");
                }
                result = ret;
            }
            return result;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string Path = ConfigPath;
            XmlSerializerHelper.WriteXML(_Config, Path, typeof(Config));
            MessageBox.Show("保存配置成功");
        }

        private void ToolConfig_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            var x = sender.ToString();
            bool IsFined = false;

            if (e.Button == MouseButtons.Right)
            {
                foreach (var item in _Config.Param)
                {
                    if (this.treeView1.SelectedNode.Text.Contains(item.Name + " (" + item.IToolType + ")"))
                    {
                        IsFined = true;
                        DeleteName = item.Name;
                        DeleteType = item.IToolType;
                        break;
                    }
                }
                if (!IsFined)
                {
                    return;
                }
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var re = MessageBox.Show("是否删除工具：" + DeleteName + " (" + DeleteType + ") ？", "提示", MessageBoxButtons.OKCancel);

            if (re == DialogResult.Cancel)
            {
                return;
            }

            if (DeleteName != "")
            {
                foreach (var item in this.treeView1.Nodes)
                {
                    if (item.ToString() .Contains(DeleteName+" ("+DeleteType+")") )
                    {
                        var tem = this.treeView1.SelectedNode;
                        tem.Remove();
                        break;
                    }
                }

                foreach (var item in _Config.Param)
                {
                    if (item.Name == DeleteName && item.IToolType == DeleteType)
                    {
                        if (File.Exists(ToolConfigPath + item.ConfigPath))
                        {
                            File.Delete(ToolConfigPath + item.ConfigPath);
                        }
                        _Config.Param.Remove(item);
                        break;
                    }

                }
                foreach (var item in ToolManager)
                {
                    if (item.Key == DeleteName)
                    {
                        ToolManager.Remove(DeleteName);
                        break;
                    }

                }

                DeleteType = "";
                DeleteName = "";
            }

        }

        private string NewName = "";
        public void NewNameBack(string Name)
        {
            NewName = Name;
        }

        private void 重命名当前工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var re = MessageBox.Show("是否重命名工具：" + DeleteName + " (" + DeleteType + ") ？", "提示", MessageBoxButtons.OKCancel);

            if (re == DialogResult.Cancel)
            {
                return;
            }

            NewName NameUI = new NewName();
            NameUI.StartPosition = FormStartPosition.CenterScreen;
            NameUI.NewNameEvent += new  RetrunNewName(NewNameBack);
            NameUI.ShowDialog();


            if (NewName != "")
            {
                foreach (var item in _Config.Param)
                {
                    if (item.Name == DeleteName && item.IToolType == DeleteType)
                    {
                        item.Name = NewName;
                        if (File.Exists(ToolConfigPath + item.ConfigPath))
                        {
                            File.Copy(ToolConfigPath + item.ConfigPath, ToolConfigPath + item.IToolType +"_"+ NewName + ".xml");
                            File.Delete(ToolConfigPath + item.ConfigPath);
                        }
                        item.ConfigPath = item.IToolType + "_" + NewName + ".xml";
                        break;
                    }
                }
            }

        }
    }
}
