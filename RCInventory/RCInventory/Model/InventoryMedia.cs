using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RCInventory.Model
{
    public class InventoryMedia
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int ItemID { get; set; }
        public string MediaType { get; set; }
        public string MediaDesc { get; set; }
        public string Filename { get; set; }
        public string FileExtension { get; set; }
        public DateTime DateTaken { get; set; }
        public bool DefaultMedia { get; set; }
    }
}
