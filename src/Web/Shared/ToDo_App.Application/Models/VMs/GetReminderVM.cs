using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_App.Domain.Enums;

namespace ToDo_App.Application.Models.VMs
{
    public class GetReminderVM
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Importance Importance { get; set; }
        public bool IsDone { get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}
