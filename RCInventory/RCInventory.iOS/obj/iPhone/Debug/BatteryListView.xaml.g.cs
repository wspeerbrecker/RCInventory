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
    
    
    public partial class BatteryListView : ContentPage {
        
        private Label lblNoOfItems;
        
        private ListView ListViewBattery;
        
        private void InitializeComponent() {
            this.LoadFromXaml(typeof(BatteryListView));
            lblNoOfItems = this.FindByName<Label>("lblNoOfItems");
            ListViewBattery = this.FindByName<ListView>("ListViewBattery");
        }
    }
}
