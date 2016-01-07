using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using RCInventory.Model;

namespace RCInventory.View
{
    class RCTabPages : TabbedPage
    {
        public RCTabPages()
        {
            this.Title = "";
            Children.Add(new RCModelListView(true) { Title = "RC MODELS" });

            Children.Add(new BatteryListView(true) { Title = "BATTERIES" });

            Children.Add(new ActivityLogListView(0) { Title = "ACTIVITY LOG" });

            this.SelectedItem = Children[0];
        }

    }
}
