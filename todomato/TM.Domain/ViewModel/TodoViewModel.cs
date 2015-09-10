using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.ViewModel
{
    public class TodoViewModel
    {
        public string TodoID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Nullable<int> NeedTomato { get; set; }
        public Nullable<int> DoneTomato { get; set; }
        public Nullable<bool> IsFinish { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public Nullable<DateTime> UpdateTime { get; set; }

        public string Tag { get; set; }
    }
}
