using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankTracker.Models
{
    public class Match
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int GameId { get; set; }
        [Ignore]
        public List<PlayerMatchInfo> Players { get; set; }

        public DateTime Date { get; set; }

        public string WinnnerName { get; set; }

       

    }
}
