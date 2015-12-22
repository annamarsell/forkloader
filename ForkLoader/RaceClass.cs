using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForkLoader
{
    public class RaceClass
    {
        // *** OLA Table RaceClasses ***
        // eventClassId - per class
        // raceClassId - per leg
        // startNumberBase - start number if first leg, 0 otherwise
        // relayLeg - leg

        public int EventClassId { get; set; }
        public int RaceClassId { get; set; }
        public int StartNumberBase { get; set; }
        public int RelayLeg { get; set; }

        public RaceClass(int eventClassId, int raceClassId, int startNumberBase, int relayLeg)
        {
            EventClassId = eventClassId;
            RaceClassId = raceClassId;
            StartNumberBase = startNumberBase;
            RelayLeg = relayLeg;
        }
    }
}
