using System;
using System.Collections.Generic;
using System.Text;

namespace RankTracker.Models
{
    public class Game
    {
        
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Player> Players { get; set; }
    }
}
