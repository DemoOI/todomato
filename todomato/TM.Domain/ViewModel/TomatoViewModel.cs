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
        public string SpentTime { get; set; }
        public string CreateTime { get; set; }
        public string FinishTime { get; set; }
        public bool IsCompleted { get; set; }
        public Nullable<int> PauseCount { get; set; }

        public virtual Todo Todo { get; set; }
    }
}
