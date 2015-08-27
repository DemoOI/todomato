using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//注意namespace 要跟 EF model一致
namespace TM.Domain
{
    [MetadataType(typeof(TodoMetadata))]
    public partial class Todo
    {
        public class TodoMetadata
        {
            //有關連會導致return json 報錯
            //參考 : http://blog.miniasp.com/post/2012/12/24/ASPNET-Web-API-Self-referencing-loop-detected-for-property-solutions.aspx
            [JsonIgnore]
            public virtual ICollection<Tomato> Tomatoes { get; set; }

        }
    }
}
