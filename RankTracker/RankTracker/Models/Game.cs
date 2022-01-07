using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankTracker.Models
{
    public class Game
    {
        
        public string GUID { get; set; }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Ignore]
        public virtual List<Player> Players { get; set; }
        [Ignore]
        public virtual List<Match> Matches { get; set; }
    }
}
