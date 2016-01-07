using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RCInventory.Model
{
    public class ListData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ListType { get; set; }
        public string ListDesc { get; set; }
    }
    public class ListItem
    {
        public string ListDesc { get; set; }
    }
}
