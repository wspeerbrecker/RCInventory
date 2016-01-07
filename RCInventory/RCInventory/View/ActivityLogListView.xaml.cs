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
	public partial class ActivityLogListView : ContentPage
	{
        ActivityLogListViewModel vm;
        private int _ItemID;

        public ActivityLogListView(int ItemID)
        {
            _ItemID = ItemID;
            InitializeComponent();

            vm = new ActivityLogListViewModel();
            BindingContext = vm;

            // Load the Activity Log report into ListView class.
            vm.LoadActivityLogs();

            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () => {
                    var ALogRec = new ActivityLogList();
                    // create a new details view with the item
                    var view = new ActivityLogDetailsView(ALogRec);
                    // tell the navigator to show the new view
                    Navigation.PushAsync(view);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.Android)
            { // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", () =>
                {
                    var ALogRec = new ActivityLogList();
                    // create a new details view with the item
                    var view = new ActivityLogDetailsView(ALogRec);
                    // tell the navigator to show the new view
                    Navigation.PushAsync(view);
                }, 0, 0);
            }
            //
            ToolbarItems.Add(tbi);
            //
            lblNoOfItems.Text = "No. of Activities: " + vm.ActivityLogsLV.Count.ToString();
        }

        public void OnSelect(object sender, SelectedItemChangedEventArgs e)
        {
            // get the item selected
            var ALogRec = (ActivityLogList)e.SelectedItem;
            // create a new details view with the item
            var view = new ActivityLogDetailsView(ALogRec);
            // tell the navigator to show the new view
            Navigation.PushAsync(view);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            // Load the Activity Log report into ListView class.
            vm.LoadActivityLogs();
        }
    }
}
