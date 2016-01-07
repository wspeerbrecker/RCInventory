using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using RCInventory.ViewModel;
using RCInventory.Model;

namespace RCInventory.View
{
	public partial class ActivityLogDetailsView : ContentPage
	{
        private int _iBatteryListDisplayedID;
        private string _sCurrentListType;

        public Model.ActivityLogList Model
        {
            get { return (Model.ActivityLogList)BindingContext; }
            set
            {
                BindingContext = value;
                OnPropertyChanged();
            }
        }

        public ActivityLogDetailsView(Model.ActivityLogList model)
        {
            _iBatteryListDisplayedID = 0;
            // Bind our BindingContext
            Model = model;

            NavigationPage.SetHasNavigationBar(this, true);

            InitializeComponent();
            //
            LoadFields();
            //
            // Save Button
            btnSaveSC.Clicked += (sender, e) => {
                // Save the Date/Time value.
                Model.LogDateTime = PickerLogDate.Date;
                Model.LogDateTime = Model.LogDateTime.Add(PickerLogTime.Time);
                if ((EntryMinutes.Text != "") && (EntrySeconds.Text != ""))
                {
                    int iMinutesInSecs = Convert.ToInt32(EntryMinutes.Text) * 60;
                    int iTimeInSecs = iMinutesInSecs + Convert.ToInt32(EntrySeconds.Text);
                    Model.LogTimeInSeconds = iTimeInSecs;
                }
                ActivityLog ALRec = TransferToActivityLogRec(Model);
                Model.ID = App.Database.SaveActivityLog(ALRec);
                Navigation.PopAsync();
            };
            //
            // Cancel Button
            btnCancelSC.Clicked += (sender, e) => {
                Navigation.PopAsync();
            };
            //
            // Delete Button
            btnDeleteSC.Clicked += (sender, e) => {
                App.Database.DeleteActivityLog(Model.ID);
                Navigation.PopAsync();
            };
            //
            // Display Item Name list
            entryActivityType.Focused += (sender, e) =>
            {
                _sCurrentListType = "ACTIVITYTYPE";
                // create a new details view with the item
                var view = new ListDataListView(_sCurrentListType, true);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            };
            //
            // Display Item Name list
            entryModelName.Focused += (sender, e) =>
            {
                // create a new details view with the item
                var view = new RCModelListView(false);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            };
            //
            // Display Battery 1 Name list
            entryBattery1Name.Focused += (sender, e) =>
            {
                _iBatteryListDisplayedID = 1;
                // create a new details view with the item
                var view = new BatteryListView(false);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            };
            //
            // Display Battery 2 Name list
            entryBattery2Name.Focused += (sender, e) =>
            {
                _iBatteryListDisplayedID = 2;
                // create a new details view with the item
                var view = new BatteryListView(false);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            };
        }

        private void LoadFields()
        {
            if (Model.ID != 0)
            {
                PickerLogDate.Date = Model.LogDateTime;
                PickerLogTime.Time = Model.LogDateTime.TimeOfDay;
                //
                if (Model.LogTimeInSeconds != 0)
                {
                    int iMinutes = Model.LogTimeInSeconds / 60;
                    EntryMinutes.Text = iMinutes.ToString();
                    int iSeconds = Model.LogTimeInSeconds % 60;
                    EntrySeconds.Text = iSeconds.ToString();
                }
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            if (App.selectedItemName != null)
            {
                entryModelName.Text = App.selectedItemName;
                Model.ItemID = App.selectedItemID;
                App.selectedItemName = null;
            }
            //
            if (App.selectedBatteryName != null)
            {
                if (_iBatteryListDisplayedID == 1)
                {
                    entryBattery1Name.Text = App.selectedBatteryName;
                    App.selectedBatteryName = null;
                    Model.Battery1ItemID = App.selectedBatteryID;
                    App.selectedBatteryID = 0;
                }
                //
                if (_iBatteryListDisplayedID == 2)
                {
                    entryBattery2Name.Text = App.selectedBatteryName;
                    App.selectedBatteryName = null;
                    Model.Battery2ItemID = App.selectedBatteryID;
                    App.selectedBatteryID = 0;
                }
                _iBatteryListDisplayedID = 0; //
            }
            //
            switch (_sCurrentListType)
            {
                case "ACTIVITYTYPE":
                    entryActivityType.Text = App.selectedActivityType;
                    break;
            }
            switch (entryActivityType.Text)
            {
                case "TIME TRACKING REPORT":
                    lblDuration.IsVisible = true;
                    EntryMinutes.IsVisible = true;
                    lblTimeColon.IsVisible = true;
                    EntrySeconds.IsVisible = true;
                    lblBattery1.IsVisible = true;
                    entryBattery1Name.IsVisible = true;
                    lblBattery2.IsVisible = true;
                    entryBattery2Name.IsVisible = true;
                    break;
                default:
                    lblDuration.IsVisible = false;
                    EntryMinutes.IsVisible = false;
                    lblTimeColon.IsVisible = false;
                    EntrySeconds.IsVisible = false;
                    lblBattery1.IsVisible = false;
                    entryBattery1Name.IsVisible = false;
                    lblBattery2.IsVisible = false;
                    entryBattery2Name.IsVisible = false;
                    break;
            }
        }
        private ActivityLog TransferToActivityLogRec(ActivityLogList ALList)
        {
            ActivityLog ALogRec = new ActivityLog();
            //
            ALogRec.ID = ALList.ID;
            ALogRec.ActivityType = ALList.ActivityType;
            ALogRec.ItemID = ALList.ItemID;
            ALogRec.LogDateTime = ALList.LogDateTime;
            ALogRec.LogTimeInSeconds = ALList.LogTimeInSeconds;
            ALogRec.Location = ALList.Location;
            ALogRec.LogNotes = ALList.LogNotes;
            ALogRec.Battery1ItemID = ALList.Battery1ItemID;
            ALogRec.Battery2ItemID = ALList.Battery2ItemID;
            //
            return ALogRec;
        }
    }
}
