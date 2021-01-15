using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray.AutoTask.LiWo.Domain
{
    public class LiWoRequest
    {
        public string Appid { get; set; }

        public string FunctionId { get; set; }

        public int LoginType { get; set; }

        public double T => (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
    }
}
