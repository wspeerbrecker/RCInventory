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
	public partial class RCModelListView : ContentPage
	{
        private bool _isDataEditable;
        RCModelViewModel vm;

		public RCModelListView (bool isDataEditable)
		{

            InitializeComponent();

            vm = new RCModelViewModel();
            BindingContext = vm;

            // Load the Activity Log report into ListView class.
            vm.LoadRCModels();

            _isDataEditable = isDataEditable;
            App.selectedItemName = null;
            App.selectedItemID = 0;
            //
            if (_isDataEditable)
            {
                ToolbarItem tbi = null;
                if (Device.OS == TargetPlatform.iOS)
                {
                    tbi = new ToolbarItem("+", null, () =>
                    {
                        var RCItem = new InventoryItemList();
                        // create a new details view with the item
                        var view = new RCInventoryDetailsView(RCItem, App.ItemCategory_MODEL);
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
                    var view = new RCInventoryDetailsView(RCItem, App.ItemCategory_MODEL);
                    //// tell the navigator to show the new view
                    Navigation.PushAsync(view);
                    }, 0, 0);
                }
                //
                ToolbarItems.Add(tbi);
            }
            //
            lblNoOfModels.Text = "No. of Models: " + vm.RCModelLV.Count.ToString();
        }

        public void OnSelect(object sender, SelectedItemChangedEventArgs e)
        {
            // get the item selected
            var rcitem = (Model.InventoryItemList)e.SelectedItem;
            //
            // If the data is editable then display the detail screen.
            if (_isDataEditable)
            {
                // create a new details view with the item
                var view = new RCInventoryDetailsView(rcitem, rcitem.ItemCategory);
                //// tell the navigator to show the new view
                Navigation.PushAsync(view);
            }
            else
            {
                // Return the Model Name;
                App.selectedItemName = rcitem.ItemName;
                App.selectedItemID = rcitem.ID;
                Navigation.PopAsync();
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Load the Activity Log report into ListView class.
            vm.LoadRCModels();
        }
    }
}
