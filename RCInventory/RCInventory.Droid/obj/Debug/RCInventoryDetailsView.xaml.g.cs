//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RCInventory.View {
    using System;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    
    
    public partial class RCInventoryDetailsView : ContentPage {
        
        private Button btnCancelSC;
        
        private Button btnSaveSC;
        
        private Button btnDeleteSC;
        
        private Label ItemCategory;
        
        private Label labelItemType;
        
        private Entry entryItemType;
        
        private Entry entryItemName;
        
        private Entry entryManufacturer;
        
        private Button btnListManuf;
        
        private Label lblBatteryNo;
        
        private Entry EntryBatteryNo;
        
        private Label lblDefaultTime;
        
        private Entry EntryMinutes;
        
        private Label lblColon;
        
        private Entry EntrySeconds;
        
        private DatePicker DTPicker;
        
        private Button btnNoOfFlights;
        
        private Label labelNoOfFlights;
        
        private Button btnGallery;
        
        private Label lblNoOfItems;
        
        private Image imgPhoto;
        
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(RCInventoryDetailsView));
            btnCancelSC = this.FindByName<Button>("btnCancelSC");
            btnSaveSC = this.FindByName<Button>("btnSaveSC");
            btnDeleteSC = this.FindByName<Button>("btnDeleteSC");
            ItemCategory = this.FindByName<Label>("ItemCategory");
            labelItemType = this.FindByName<Label>("labelItemType");
            entryItemType = this.FindByName<Entry>("entryItemType");
            entryItemName = this.FindByName<Entry>("entryItemName");
            entryManufacturer = this.FindByName<Entry>("entryManufacturer");
            btnListManuf = this.FindByName<Button>("btnListManuf");
            lblBatteryNo = this.FindByName<Label>("lblBatteryNo");
            EntryBatteryNo = this.FindByName<Entry>("EntryBatteryNo");
            lblDefaultTime = this.FindByName<Label>("lblDefaultTime");
            EntryMinutes = this.FindByName<Entry>("EntryMinutes");
            lblColon = this.FindByName<Label>("lblColon");
            EntrySeconds = this.FindByName<Entry>("EntrySeconds");
            DTPicker = this.FindByName<DatePicker>("DTPicker");
            btnNoOfFlights = this.FindByName<Button>("btnNoOfFlights");
            labelNoOfFlights = this.FindByName<Label>("labelNoOfFlights");
            btnGallery = this.FindByName<Button>("btnGallery");
            lblNoOfItems = this.FindByName<Label>("lblNoOfItems");
            imgPhoto = this.FindByName<Image>("imgPhoto");
        }
    }
}
