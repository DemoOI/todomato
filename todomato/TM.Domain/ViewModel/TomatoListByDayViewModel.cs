using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.ViewModel
{
    public class TomatoListByDayViewModel
    {
        public TomatoListByDayViewModel(List<Tomato> list)
        {
            this.TomatoList = list;
            this.CompleteCount = list.Count;
            this.Date = String.Format("{0:MM/dd/yyyy}", list[0].FinishTime);
        }

        public List<Tomato> TomatoList { get; set; } 
        public int CompleteCount { get; set; }
        public string Date { get; set; }
    }
}
