using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using RCInventory.Model;

namespace RCInventory.View
{
    public partial class RCInventoryDetailsView : ContentPage
    {
        public InventoryItemList Model
        {
            get { return (InventoryItemList)BindingContext; }
            set
            {
                BindingContext = value;
                OnPropertyChanged();
            }
        }

        private static string _sCurrentListType;
        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="model">Instance we want to display</param>
        public RCInventoryDetailsView(InventoryItemList model, string sItemCategory)
        {
            // Bind our BindingContext
            Model = model;

            NavigationPage.SetHasNavigationBar(this, true);

            InitializeComponent();
            // If a specific Item Category is passed into screen then set as category of item being entered.
            if (sItemCategory != "")
            { Model.ItemCategory = sItemCategory; }
            switch (sItemCategory)
            {
                case App.ItemCategory_MODEL:
                    labelItemType.Text = "Model Type";
                    IEnumerable<ActivityLog> ActivityLogs = App.Database.GetActivityRecs(Model.ID);
                    int iTotalFlightTimeInSeconds = 0;
                    foreach (ActivityLog ALog in ActivityLogs)
                    {
                        if (ALog.ActivityType == "TIME TRACKING REPORT")
                        {
                            iTotalFlightTimeInSeconds += ALog.LogTimeInSeconds;
                        }
                    }
                    labelNoOfFlights.Text = iTotalFlightTimeInSeconds.ToString();
                    lblBatteryNo.IsVisible = false;
                    EntryBatteryNo.IsVisible = false;
                    //
                    lblDefaultTime.IsVisible = true;
                    EntryMinutes.IsVisible = true;
                    lblColon.IsVisible = true;
                    EntrySeconds.IsVisible = true;
                    break;
                case App.ItemCategory_BATTERY:
                    labelItemType.IsVisible = false;
                    entryItemType.IsVisible = false;
                    lblBatteryNo.IsVisible = true;
                    EntryBatteryNo.IsVisible = true;
                    lblDefaultTime.IsVisible = false;
                    EntryMinutes.IsVisible = false;
                    lblColon.IsVisible = false;
                    EntrySeconds.IsVisible = false;
                    break;
            }

            //
            if (Model.PurchaseDate == DateTime.MinValue)
            { DTPicker.Date = DateTime.Now; }
            else { DTPicker.Date = Model.PurchaseDate; }
            //
            // Save Button
            btnSaveSC.Clicked += (sender, e) =>
            {
                //var todoItem = (TodoItem)BindingContext;
                //
                Model.PurchaseDate = DTPicker.Date;
                InventoryItem IIRec = TransferToInventoryItemRec(Model);
                Model.ID = App.Database.SaveItem(IIRec);
                Navigation.PopAsync();
            };
            //
            // Cancel Button
            btnCancelSC.Clicked += (sender, e) =>
            {
                Navigation.PopAsync();
            };
            //
            // Delete Button
            btnDeleteSC.Clicked += (sender, e) =>
            {
                App.Database.DeleteItem(Model.ID);
                Navigation.PopAsync();
            };
            //
            // No of Flights Button
            btnNoOfFlights.Clicked += (sender, e) =>
            {
                // create a new details view with the item
                var view = new ActivityLogListView(Model.ID);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            };
            //
            // List Media Button
            btnGallery.Clicked += (sender, e) =>
            {
                Model.PurchaseDate = DTPicker.Date;
                InventoryItem IIRec = TransferToInventoryItemRec(Model);
                Model.ID = App.Database.SaveItem(IIRec);
                // create a new details view with the item
                var view = new MediaListView(Model.ID);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            };
            //
            // Display Item Name list
            entryItemType.Focused += (sender, e) =>
            {
                _sCurrentListType = "ITEMTYPE";
                // create a new details view with the item
                var view = new ListDataListView(_sCurrentListType, true);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            };
            //
            // Display Manufacturer list
            btnListManuf.Clicked += (sender, e) =>
            {
                _sCurrentListType = "MANUFACTURER";
                // create a new details view with the item
                var view = new ListDataListView(_sCurrentListType, true);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            };
            //
            //LoadItemNamePicker();
        }
        //public void LoadItemNamePicker()
        //{
        //    // Get List of Item Types.
        //    IEnumerable<ListData> ListItemTypes = App.Database.GetListByType("ITEMTYPE");

        //    foreach (ListData listItem in ListItemTypes)
        //    {
        //        ItemNamePicker.Items.Add(listItem.ListDesc);
        //    }
            
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            switch (_sCurrentListType)
            {
                case "ITEMTYPE":
                    entryItemType.Text = App.selectedItemType;
                    break;
                case "MANUFACTURER":
                    entryManufacturer.Text = App.selectedManufacturer;
                    break;
            }
            IEnumerable<ActivityLog> ActivityLogs = App.Database.GetActivityRecs(Model.ID);
            int iNoOfFlights = 0;
            foreach (ActivityLog ARec in ActivityLogs)
            {
                if (ARec.ActivityType == "TIME TRACKING REPORT")
                { iNoOfFlights += 1; }
            }
            labelNoOfFlights.Text = iNoOfFlights.ToString();
            //
            IEnumerable<InventoryMedia> IMList = App.Database.GetMediaRecs(Model.ID);
            int iNoOfItems = 0;
            foreach (ActivityLog ARec in ActivityLogs)
            {
                if (ARec.ActivityType == "TIME TRACKING REPORT")
                { iNoOfFlights += 1; }
            }
            lblNoOfItems.Text = "No. of Items: " + IMList.Count<InventoryMedia>().ToString();
        }

        private InventoryItem TransferToInventoryItemRec(InventoryItemList IIList)
        {
            InventoryItem IIRec = new InventoryItem();
            //
            IIRec.ID = IIList.ID;
            IIRec.ItemCategory = IIList.ItemCategory;
            IIRec.ItemName = IIList.ItemName;
            IIRec.ItemNumber = IIList.ItemNumber;
            IIRec.ItemType = IIList.ItemType;
            IIRec.Manufacturer = IIList.Manufacturer;
            IIRec.Notes = IIList.Notes;
            IIRec.PurchaseDate = IIList.PurchaseDate;
            IIRec.PurchasePrice = IIList.PurchasePrice;
            IIRec.RetailPrice = IIList.RetailPrice;
            //
            return IIRec;
        }
    }
}
