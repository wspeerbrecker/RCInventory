using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using RCInventory.Model;

namespace RCInventory.View
{
	public partial class ListDataDetailsView : ContentPage
	{
        public ListData Model
        {
            get { return (ListData)BindingContext; }
            set
            {
                BindingContext = value;
                OnPropertyChanged();
            }
        }

		public ListDataDetailsView(string sType, ListData model)
        {
            Model = model;
            //
			InitializeComponent ();
            //
            // Save Button
            btnSaveLD.Clicked += (sender, e) =>
            {
                // If a new List record then insert it.
                if (Model.ID == 0)
                {
                    ListData ListRec = new ListData();
                    ListRec.ListType = sType;
                    ListRec.ListDesc = Model.ListDesc;
                    App.Database.SaveListRec(ListRec);
                }
                else
                {
                    // Else update the existing record.
                    App.Database.SaveListRec(Model);
                }
                Navigation.PopAsync();
            };
            //
            // Cancel Button
            btnCancelLD.Clicked += (sender, e) =>
            {
                Navigation.PopAsync();
            };
            //
            // Delete button
            btnDeleteLD.Clicked += (sender, e) =>
            {
                if (Model.ID != 0)
                {
                    App.Database.DeleteListRec(Model.ID);
                    Navigation.PopAsync();
                }
            };
        }
	}
}
