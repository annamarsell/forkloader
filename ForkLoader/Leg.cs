using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    public class Leg
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public bool FromStart { get; set; }
        public bool ToFinish { get; set; }

        public Leg(int from, int to, bool fromStart = false, bool toFinish = false)
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
