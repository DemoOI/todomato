using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TM.Domain.ViewModel
{
    public class TomatoListByDayViewModel
    {
        public List<Tomato> List { get; set; } 
        public int CompleteCount { get; set; }
        public DateTime Date { get; set; }
    }
}
