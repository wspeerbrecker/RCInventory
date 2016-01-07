
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using RCInventory.Model;

namespace RCInventory.ViewModel
{

    public class BatteryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<InventoryItemList> BatteryLV { get; set; }

        public BatteryViewModel()
        {
            BatteryLV = new ObservableCollection<InventoryItemList>();
        }

        public void LoadBatteryList()
        {
            //AddActivityLogRecs();
            BatteryLV.Clear();

            IEnumerable<InventoryItem> BatteryList = App.Database.GetAllItems(App.ItemCategory_BATTERY);

            foreach (var BatteryItem in BatteryList)
            {
                InventoryItemList BList = new InventoryItemList();
                BList.ID = BatteryItem.ID;
                BList.ItemCategory = BatteryItem.ItemCategory;
                BList.ItemType = BatteryItem.ItemType;
                BList.ItemName = BatteryItem.ItemName;
                BList.ItemNumber = BatteryItem.ItemNumber;
                BList.Manufacturer = BatteryItem.Manufacturer;
                BList.ItemFilename = null;
                if (BList.ID != 0)
                {
                    IEnumerable<InventoryMedia> IMList = App.Database.GetMediaRecs(BList.ID);
                    foreach (InventoryMedia IMRec in IMList)
                    {
                        // Load Model Name by looking up the ItemID in the InventoryItem table.
                        BList.ItemFilename = IMRec.Filename;
                        break;
                    }
                }
                IEnumerable<ActivityLog> ActivityLogs = App.Database.GetActivityRecs(BList.ID);
                BList.NoOfFlights = ActivityLogs.Count<ActivityLog>().ToString();
                //
                int iTotalFlightTimeInSeconds = 0;
                foreach (ActivityLog ALog in ActivityLogs)
                {
                    if (ALog.ActivityType == "TIME TRACKING REPORT")
                    {
                        iTotalFlightTimeInSeconds += ALog.LogTimeInSeconds;
                    }
                }
                if (iTotalFlightTimeInSeconds != 0)
                {
                    BList.TotalFlightTimeInSeconds = iTotalFlightTimeInSeconds.ToString();
                    //
                    // MM = 550 / 60 
                    int iTotalHH = 0;
                    int iTotalMM = iTotalFlightTimeInSeconds / 60;
                    int iTotalSS = iTotalFlightTimeInSeconds % 60;
                    if (iTotalMM > 60)
                    {
                        iTotalHH = iTotalMM / 60;
                        iTotalMM = iTotalMM % 60;
                    }
                    BList.TotalFlightTimeHHMMSS = "";
                    if (iTotalHH != 0) BList.TotalFlightTimeHHMMSS += iTotalHH.ToString() + ":";
                    BList.TotalFlightTimeHHMMSS += iTotalMM.ToString() + ":";
                    BList.TotalFlightTimeHHMMSS += iTotalSS.ToString();
                }
                //
                BatteryLV.Add(BList);
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed(this, new PropertyChangedEventArgs(name));
        }
    }
}
