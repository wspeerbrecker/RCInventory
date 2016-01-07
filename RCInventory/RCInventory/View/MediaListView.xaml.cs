using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace RCInventory.View
{
	public partial class MediaListView : ContentPage
	{
        private static int ItemID;
		public MediaListView (int itemID)
		{
            ItemID = itemID;
			InitializeComponent ();
            //
            // Media Button
            btnMedia.Clicked += (sender, e) =>
            {
                var mItem = new Model.InventoryMedia();
                // create a new details view with the item
                var view = new MediaDetailsView(itemID, mItem);
                // tell the navigator to show the new view
                Navigation.PushAsync(view);
            };
        }

        public void OnSelect(object sender, SelectedItemChangedEventArgs e)
        {
            // get the item selected
            var mItem = (Model.InventoryMedia)e.SelectedItem;
            // create a new details view with the item
            var view = new MediaDetailsView(ItemID, mItem);
            //// tell the navigator to show the new view
            Navigation.PushAsync(view);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //
            ListViewMedia.ItemsSource = App.Database.GetMediaRecs(ItemID);
        }
    }
}
