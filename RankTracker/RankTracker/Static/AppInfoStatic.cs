using RankTracker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RankTracker.Static
{
    public static class AppInfoStatic
    {
        public static Game currentGame;
        public static Player currentPlayer;
        public static List<PlayerView> currentPlayersInMatch;
        public const string DatabaseFilename = "RankTrackerSQLBase.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
