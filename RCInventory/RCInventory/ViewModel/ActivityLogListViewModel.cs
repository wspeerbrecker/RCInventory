using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Collections.ObjectModel;
using RCInventory.Model;

namespace RCInventory.ViewModel
{

    public class ActivityLogListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ActivityLogList> ActivityLogsLV { get; set; }

        public ActivityLogListViewModel()
        {
            ActivityLogsLV = new ObservableCollection<ActivityLogList>();
        }

        public void LoadActivityLogs()
        {
            //AddActivityLogRecs();
            ActivityLogsLV.Clear();

            IEnumerable<ActivityLog> ALogs = App.Database.GetAllActivities();

            foreach (var ALog in ALogs)
            {
                ActivityLogList ALList = new ActivityLogList();
                ALList.ID = ALog.ID;
                ALList.ItemID = ALog.ItemID;
                ALList.ActivityType = ALog.ActivityType;
                if (ALList.ItemID != 0)
                {
                    InventoryItem ItemRec = App.Database.GetItemRec(ALog.ItemID);
                    // Load Model Name by looking up the ItemID in the InventoryItem table.
                    ALList.ModelName = ItemRec.ItemName;
                }
                else
                {
                    ALList.ModelName = null;
                }
                ALList.LogDateTime = ALog.LogDateTime;
                ALList.LogTimeInSeconds = ALog.LogTimeInSeconds;
                ALList.Location = ALog.Location;
                ALList.LogNotes = ALog.LogNotes;
                ALList.Battery1ItemID = ALog.Battery1ItemID;
                // Load 1st Battery Name by looking up the ItemID in the InventoryItem table.
                if (ALog.Battery1ItemID != 0)
                {
                    InventoryItem Battery1Rec = App.Database.GetItemRec(ALog.Battery1ItemID);
                    if (Battery1Rec != null)
                    { ALList.Battery1Name = Battery1Rec.ItemName; }
                }
                ALList.Battery2ItemID = ALog.Battery2ItemID;
                // Load 2nd Battery Name by looking up the ItemID in the InventoryItem table.
                if (ALog.Battery2ItemID != 0)
                {
                    InventoryItem Battery2Rec = App.Database.GetItemRec(ALog.Battery2ItemID);
                    if (Battery2Rec != null)
                    { ALList.Battery2Name = Battery2Rec.ItemName; }
                }
                //
                ActivityLogsLV.Add(ALList);
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
