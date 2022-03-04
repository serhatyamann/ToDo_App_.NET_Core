using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_App.Application.Models.VMs
{
    public class GetCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
