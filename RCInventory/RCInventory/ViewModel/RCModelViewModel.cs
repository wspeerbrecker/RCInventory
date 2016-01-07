using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using RCInventory.Model;

namespace RCInventory.ViewModel
{

    public class RCModelViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<InventoryItemList> RCModelLV { get; set; }

        public RCModelViewModel()
        {
            RCModelLV = new ObservableCollection<InventoryItemList>();
        }

        public void LoadRCModels()
        {
            //AddActivityLogRecs();
            RCModelLV.Clear();

            IEnumerable<InventoryItem> RCModels = App.Database.GetAllItems(App.ItemCategory_MODEL);
            
            foreach (var RCModel in RCModels)
            {
                InventoryItemList MList = new InventoryItemList();
                MList.ID = RCModel.ID;
                MList.ItemCategory = RCModel.ItemCategory;
                MList.ItemType = RCModel.ItemType;
                MList.ItemName = RCModel.ItemName;
                MList.ItemNumber = RCModel.ItemNumber;
                MList.Manufacturer = RCModel.Manufacturer;
                MList.ItemFilename = null;
                if (MList.ID != 0)
                {
                    IEnumerable<InventoryMedia> IMList = App.Database.GetMediaRecs(MList.ID);
                    foreach (InventoryMedia IMRec in IMList)
                    {
                        // Load Model Name by looking up the ItemID in the InventoryItem table.
                        MList.ItemFilename = IMRec.Filename;
                        break;
                    }
                }
                IEnumerable<ActivityLog> ActivityLogs = App.Database.GetActivityRecs(MList.ID);
                MList.NoOfFlights = ActivityLogs.Count<ActivityLog>().ToString();
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
                    MList.TotalFlightTimeInSeconds = iTotalFlightTimeInSeconds.ToString();
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
                    MList.TotalFlightTimeHHMMSS = "";
                    if (iTotalHH != 0) MList.TotalFlightTimeHHMMSS += iTotalHH.ToString() + ":";
                    MList.TotalFlightTimeHHMMSS += iTotalMM.ToString() + ":";
                    MList.TotalFlightTimeHHMMSS += iTotalSS.ToString();
                }
                //
                RCModelLV.Add(MList);
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
