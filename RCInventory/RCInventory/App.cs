using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using RCInventory.Data;
using RCInventory.View;
using RCInventory.Model;

namespace RCInventory
{
	public class App : Application
	{
        static InventoryDatabase database;
        public static InventoryDatabase Database
        {
            get
            {
                // /data/user/0/TodoShared.Android/files/TodoSQLite.db3
                database = database ?? new InventoryDatabase();

                return database;
            }
        }
        public static string selectedItemType;
        public static string selectedItemName;
        public static int selectedItemID;
        public static string selectedManufacturer;
        public static string selectedBatteryName;
        public static int selectedBatteryID;
        public static string selectedActivityType;

        public const string ItemCategory_MODEL = "RCMODEL";
        public const string ItemCategory_BATTERY = "BATTERY";

        public App()
        {
            MainPage = new NavigationPage(new RCTabPages());
            //
            IEnumerable < ListData > itemTypeList = App.Database.GetListByType("ITEMTYPE");
            if (itemTypeList.Count<ListData>() == 0)
            { AddModelTypes(); }
            // Add default list of Acitivity Types e.g., Time Tracking report, Crash report, Maintenance report, etc.
            IEnumerable<ListData> activityTypeList = App.Database.GetListByType("ACTIVITYTYPE");
            if (activityTypeList.Count<ListData>() == 0)
            { AddActivityTypes(); }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        private static void AddModelTypes()
        {

            ListData ListRec = new ListData();
            ListRec.ListType = "ITEMTYPE";
            ListRec.ListDesc = "HELICOPTER";
            int iItemID = App.Database.SaveListRec(ListRec);
            //
            ListRec = new ListData();
            ListRec.ListType = "ITEMTYPE";
            ListRec.ListDesc = "AIRPLANE";
            iItemID = App.Database.SaveListRec(ListRec);
            //
            ListRec = new ListData();
            ListRec.ListType = "ITEMTYPE";
            ListRec.ListDesc = "QUADCOPTER";
            iItemID = App.Database.SaveListRec(ListRec);
            //
            ListRec = new ListData();
            ListRec.ListType = "ITEMTYPE";
            ListRec.ListDesc = "CAR";
            iItemID = App.Database.SaveListRec(ListRec);
            //
            ListRec = new ListData();
            ListRec.ListType = "ITEMTYPE";
            ListRec.ListDesc = "TRUCK";
            iItemID = App.Database.SaveListRec(ListRec);
        }
        private static void AddActivityTypes()
        {

            ListData ListRec = new ListData();
            ListRec.ListType = "ACTIVITYTYPE";
            ListRec.ListDesc = "TIME TRACKING REPORT";
            int iItemID = App.Database.SaveListRec(ListRec);
            //
            ListRec = new ListData();
            ListRec.ListType = "ACTIVITYTYPE";
            ListRec.ListDesc = "CRASH REPORT";
            iItemID = App.Database.SaveListRec(ListRec);
            //
            ListRec = new ListData();
            ListRec.ListType = "ACTIVITYTYPE";
            ListRec.ListDesc = "MAINTENANCE/REPAIR REPORT";
            iItemID = App.Database.SaveListRec(ListRec);
        }
    }
}
