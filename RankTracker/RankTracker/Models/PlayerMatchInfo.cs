using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankTracker.Models
{
  public  class PlayerMatchInfo
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int Points { get; set; }
       public string PlayerName { get; set; }
       public string RankChange { get; set; }
      


    }
}
