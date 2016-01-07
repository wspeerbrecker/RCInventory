using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using RCInventory.Model;

namespace RCInventory.View
{
	public partial class ListDataListView : ContentPage
	{
        private static string _sListType;
        private bool EditMode = false;

        public ListDataListView (string sType, bool isDataEditable)
		{
            InitializeComponent();
            _sListType = sType;
            labelListTypeAndMode.Text = "List Type: " + _sListType;
            // If the user is allowed to edit the data then add the menu options.
            if (isDataEditable)
            {
                ToolbarItem tbi = null;
                if (Device.OS == TargetPlatform.iOS)
                {
                    ListData ListRec = new ListData();
                    tbi = new ToolbarItem("+", null, () =>
                    {
                        // create a new details for a specific type.
                        var view = new ListDataDetailsView(_sListType, ListRec);
                        //// tell the navigator to show the new view
                        Navigation.PushAsync(view);
                    }, 0, 0);
                    ToolbarItems.Add(tbi);
                    //
                    tbi = new ToolbarItem("Edit", null, () =>
                    {
                        SetListTypeAndMode();
                    }, 0, 0);
                    ToolbarItems.Add(tbi);
                }
                if (Device.OS == TargetPlatform.Android)
                { // BUG: Android doesn't support the icon being null
                    tbi = new ToolbarItem("+", "plus", () =>
                    {
                        ListData ListRec = new ListData();
                    // create a new details for a specific type.
                    var view = new ListDataDetailsView(_sListType, ListRec);
                    //// tell the navigator to show the new view
                    Navigation.PushAsync(view);
                    }, 0, 0);
                    ToolbarItems.Add(tbi);
                    //
                    tbi = new ToolbarItem("Edit", null, () =>
                    {
                        SetListTypeAndMode();
                    }, 0, 0);
                    ToolbarItems.Add(tbi);
                }
                //
            }
        }
        private void SetListTypeAndMode()
        {
            string sModeText = " - In Edit Mode";
            if (EditMode == false)
            { EditMode = true; }
            else
            {
                EditMode = false;
                sModeText = " - In Select Mode";
            }
            labelListTypeAndMode.Text = "List Type: " + _sListType + sModeText;
        }
        public void OnSelect(object sender, SelectedItemChangedEventArgs e)
        {
            if (EditMode)
            {
                // Get the item selected
                var rcLD = (ListData)e.SelectedItem;
                // Display the Details view.
                var view = new ListDataDetailsView(_sListType, rcLD);
                Navigation.PushAsync(view);
            }
            else
            {
                // get the item selected
                var ListD = (ListData)e.SelectedItem;
                switch (_sListType)
                {
                    case "ITEMTYPE":
                        App.selectedItemType = ListD.ListDesc;
                        break;
                    case "ACTIVITYTYPE":
                        App.selectedActivityType = ListD.ListDesc;
                        break;
                    case "MANUFACTURER":
                        // get the item selected
                        var ListI = (ListItem)e.SelectedItem;
                        App.selectedManufacturer = ListI.ListDesc;
                        break;
                }
                // tell the navigator to show the new view
                Navigation.PopAsync();
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            if (_sListType == "MANUFACTURER")
            { ListDataList.ItemsSource = App.Database.GetManufacturersList("RCMODEL"); }
            else { ListDataList.ItemsSource = App.Database.GetListByType(_sListType); }
        }

    }
}
