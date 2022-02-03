using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankTracker.Models
{
    public class PlayerHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
         public int PlayerId { get; set; }
        public int RankHistory { get; set; }
        public DateTime Date { get; set; }
        public string fontColor { get; set; }
    }
}
