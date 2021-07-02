using System;
using System.Collections.Generic;
using System.Text;

namespace RankTracker.Models
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }

        public string Title { get; set; }

        public PlayerHistory PlayerHistory { get; set; }
    }
}
