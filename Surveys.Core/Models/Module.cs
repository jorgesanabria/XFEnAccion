using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Surveys.Core.Models
{
    public class Module
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public ICommand LoadModuleCommand { get; set; }
    }
}
