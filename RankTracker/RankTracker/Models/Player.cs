using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankTracker.Models
{
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GameId { get; set; }
        
        public string GUID { get; set; }

        public string Name { get; set; }
        
        public int Rank { get; set; }

        public string Title { get; set; }

        public int RankRated { get; set; }
        [Ignore]
        public virtual List<PlayerHistory> PlayerHistory { get; set; }
    }
}
