using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RCInventory.Model
{
    public class ActivityLog
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ActivityType { get; set; }
        public int ItemID { get; set; }
        public DateTime LogDateTime { get; set; }
        public int LogTimeInSeconds { get; set; }
        public int Battery1ItemID { get; set; }
        public int Battery2ItemID { get; set; }
        public string Location { get; set; }
        public string LogNotes { get; set; }
    }

    public class ActivityLogList
    {
        public int ID { get; set; }

        public string ActivityType { get; set; }
        public int ItemID { get; set; }
        public string ModelName { get; set; }
        public DateTime LogDateTime { get; set; }
        public int LogTimeInSeconds { get; set; }
        public int Battery1ItemID { get; set; }
        public string Battery1Name { get; set; }
        public int Battery2ItemID { get; set; }
        public string Battery2Name { get; set; }
        public string Location { get; set; }
        public string LogNotes { get; set; }
    }
}
