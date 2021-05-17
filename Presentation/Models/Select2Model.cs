using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Select2Model
    {
        public string id { get; set; }
        public string text { get; set; }
    }
    public class Select2Result
    {
        public List<Select2Model> Results { get; set; }
        public int Total { get; set; }
    }
}
