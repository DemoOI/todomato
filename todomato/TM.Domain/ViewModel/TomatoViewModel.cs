using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.ViewModel
{
    public class TomatoViewModel
    {
        public string TomatoID { get; set; }
        public string TodoID { get; set; }
        public DateTime SpentTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime FinishTime { get; set; }
        public bool IsCompleted { get; set; }
        public int PauseCount { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Todo Todo { get; set; }
    }
}
