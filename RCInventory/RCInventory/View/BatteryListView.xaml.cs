using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using RCInventory.Model;
using RCInventory.ViewModel;

namespace RCInventory.View
{
	public partial class BatteryListView : ContentPage
	{
        private bool _isDataEditable;
        BatteryViewModel vm;

        public BatteryListView (bool isDataEditable)
		{
            _isDataEditable = isDataEditable;
            //
            InitializeComponent();

            vm = new BatteryViewModel();
            BindingContext = vm;

            // Load the Activity Log report into ListView class.
            vm.LoadBatteryList();
            //
            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () => {
                    var RCItem = new InventoryItemList();
                    // create a new details view with the item
                    var view = new RCInventoryDetailsView(RCItem, App.ItemCategory_BATTERY);
                    //// tell the navigator to show the new view
                    Navigation.PushAsync(view);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", () =>
                {
                    var RCItem = new InventoryItemList();
                    // create a new details view with the item
                    var view = new RCInventoryDetailsView(RCItem, App.ItemCategory_BATTERY);
                    //// tell the navigator to show the new view
                    Navigation.PushAsync(view);
                }, 0, 0);
            }
            //
            ToolbarItems.Add(tbi);
            //
            lblNoOfItems.Text = "No. of Batteries: " + vm.BatteryLV.Count.ToString();
        }

        public void OnSelect(object sender, SelectedItemChangedEventArgs e)
        {
            // get the item selected
            var rcitem = (Model.InventoryItemList)e.SelectedItem;
            if (_isDataEditable)
            {
                // create a new details view with the item
                var view = new RCInventoryDetailsView(rcitem, rcitem.ItemCategory);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            }
            else
            {
                App.selectedBatteryName = rcitem.ItemName;
                App.selectedBatteryID = rcitem.ID;
                Navigation.PopAsync();
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Load the Activity Log report into ListView class.
            vm.LoadBatteryList();
        }
    }
}
