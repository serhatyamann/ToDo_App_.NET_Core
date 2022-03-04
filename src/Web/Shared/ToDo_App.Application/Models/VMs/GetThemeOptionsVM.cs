using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App.Application.Models.VMs
{
    public class GetThemeOptionsVM
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public string ThemeColor { get; set; }
    }
}
