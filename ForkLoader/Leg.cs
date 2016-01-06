using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    public class Leg
    {
        public string FromId { get; set; }
        public string ToId { get; set; }
        public bool FromStart { get; set; }
        public bool ToFinish { get; set; }

        public Leg(string from, string to, bool fromStart = false, bool toFinish = false)
        {
            FromId = from;
            ToId = to;
            FromStart = fromStart;
            ToFinish = toFinish;
        }

        public bool IsEqual(Leg otherLeg)
        {
            return (FromId == otherLeg.FromId && ToId == otherLeg.ToId && FromStart == otherLeg.FromStart &&
                    ToFinish == otherLeg.ToFinish);
        }

        public override string ToString()
        {
            return string.Format("From {0} to {1}, from start: {2}, to finish: {3}", FromId, ToId, FromStart, ToFinish);
        }
    }
}
