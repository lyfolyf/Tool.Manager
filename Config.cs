using Lead.Tool.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lead.Tool.Manager
{
    public class ConfigParam
    {
        public string Name { get; set; }
        public string IToolType { get; set; }
        public string ConfigPath { get; set; }
    }

    public  class Config
    {
        public List<ConfigParam> Param { get; set; }
    }
}
