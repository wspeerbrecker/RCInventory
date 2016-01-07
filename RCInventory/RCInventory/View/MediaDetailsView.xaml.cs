using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Media.Plugin;
using RCInventory.Model;

namespace RCInventory.View
{
	public partial class MediaDetailsView : ContentPage
	{

        public Model.InventoryMedia Model
        {
            get { return (Model.InventoryMedia)BindingContext; }
            set
            {
                BindingContext = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Initializes a new instance of class.
        /// </summary>
        /// <param name="model">Instance we want to display</param>
        public MediaDetailsView(int ItemID, InventoryMedia model)
        {
            string SelectedFilename = "";
            // Bind our BindingContext
            Model = model;

            NavigationPage.SetHasNavigationBar(this, true);

            InitializeComponent();
            //
            // Select Photo Button
            btnSelectPhoto.Clicked += async (sender, e) =>
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Pick Photo", ":( Pick Photo not avaialble.", "OK");
                    return;
                }
                var file = await CrossMedia.Current.PickPhotoAsync();
                if (file == null)
                    return;
                //
                imgPhoto.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    SelectedFilename = file.Path;
                    file.Dispose();
                    //
                    return stream;
                });
            };
            btnTakePhoto.Clicked += async (sender, args) =>
            {

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Media.Plugin.Abstractions.StoreCameraMediaOptions
                {

                    Directory = "Sample",
                    Name = "rcinventory.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                imgPhoto.Source = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    file.Dispose();
                    model.Filename = file.Path;
                    return stream;
                });
            };
            //
            // Save Button
            btnSaveM.Clicked += (sender, e) =>
            {
                model.ItemID = ItemID;
                model.Filename = SelectedFilename;
                model.MediaType = "Image";
                model.DateTaken = DTPicker.Date;
                model.DefaultMedia = true;
                int mediaID = App.Database.SaveMedia(Model);
                Navigation.PopAsync();
            };
            //
            // Cancel Button
            btnCancelM.Clicked += (sender, e) =>
            {
                Navigation.PopAsync();
            };
            //
            // Delete Button
            btnDeleteM.Clicked += (sender, e) =>
            {
                App.Database.DeleteMedia(Model.ID);
                Navigation.PopAsync();
            };
        }
    }
}
