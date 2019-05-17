﻿using System;
using System.Collections.Generic;

namespace Module6.EventSourcing.QueryStack.Model
{
    public class CourtSchedule
    {
        public CourtSchedule()
        {
            Slots = new List<Slot>();
            CourtName = String.Empty;
        }

        public int CourtId { get; set; }
        public string CourtName { get; set; }
        public IList<Slot> Slots { get; set; }
    }
}