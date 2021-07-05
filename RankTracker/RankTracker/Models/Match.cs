using System;
using System.Collections.Generic;
using System.Text;

namespace RankTracker.Models
{
    public class Match
    {
        public string Id { get; set; }
        public List<Player> Players { get; set; }
    }
}
