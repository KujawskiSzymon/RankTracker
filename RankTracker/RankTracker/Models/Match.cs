using System;
using System.Collections.Generic;
using System.Text;

namespace RankTracker.Models
{
    public class Match
    {
        public string Id { get; set; }
        public List<PlayerMatchInfo> Players { get; set; }

        public DateTime Date { get; set; }

        public string WinnnerName { get; set; }

       

    }
}
