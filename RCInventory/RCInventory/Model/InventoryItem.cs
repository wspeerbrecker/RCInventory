using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RCInventory.Model
{
    public class InventoryItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ItemCategory { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public string ItemNumber { get; set; } // Unique Number assigned by the user to the Item e.g., Battery Number
        public string Manufacturer { get; set; }
        public int DefaultTimeInSeconds { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PurchasePrice { get; set; }
        public double RetailPrice { get; set; }
        public string Notes { get; set; }
    }

    public class InventoryItemList
    {
        public int ID { get; set; }

        public string ItemFilename { get; set; }
        public string ItemCategory { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public string ItemNumber { get; set; }
        public string Manufacturer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PurchasePrice { get; set; }
        public double RetailPrice { get; set; }
        public string Notes { get; set; }
        public string NoOfFlights { get; set; }
        public string TotalFlightTimeInSeconds { get; set; }
        public string TotalFlightTimeHHMMSS { get; set; }
    }
}
