using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using RCInventory.Model;

namespace RCInventory.Data
{
    public class InventoryDatabase
    {
        static object locker = new object();

        SQLiteConnection database;

        string DatabasePath
        {
            get
            {
                var sqliteFilename = "InventorySQLite.db3";
#if __IOS__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				var path = Path.Combine(libraryPath, sqliteFilename);
#else
#if __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				var path = Path.Combine(documentsPath, sqliteFilename);
#else
                // WinPhone
                var path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, sqliteFilename); ;
#endif
#endif
                return path;
            }
        }
        string FolderPath
        {
            get
            {
#if __IOS__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				var path = libraryPath;
#else
#if __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				var path = documentsPath;
#else
                // WinPhone
                var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
#endif
#endif
                return path;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Tasky.DL.TaskDatabase"/> TaskDatabase. 
        /// if the database doesn't exist, it will create the database and all the tables.
        /// </summary>
        /// <param name='path'>
        /// Path.
        /// </param>
        public InventoryDatabase()
        {
            database = new SQLiteConnection(DatabasePath);
            // Create the tables
            database.CreateTable<InventoryItem>();
            database.CreateTable<InventoryMedia>();
            database.CreateTable<ActivityLog>();
            database.CreateTable<ListData>();
        }
 
        //
        // InventoryItem table
        public IEnumerable<InventoryItem> GetAllItems(string sItemCategory)
        {
            lock (locker)
            {
                string sSQL = string.Format("SELECT * FROM [InventoryItem] WHERE [ItemCategory] = '{0}' ORDER BY [ItemType]", sItemCategory);
                return database.Query<InventoryItem>(sSQL);
                //
            }
        }
        // Manufacturer List
        public IEnumerable<ListItem> GetManufacturersList(string sItemCategory)
        {
            lock (locker)
            {
                string sSQL = string.Format("SELECT DISTINCT [Manufacturer] as ListDesc FROM [InventoryItem] WHERE [ItemCategory] = '{0}' ORDER BY [Manufacturer]", sItemCategory);
                return database.Query<ListItem>(sSQL);
                //
            }
        }
        public InventoryItem GetItemRec(int id)
        {
            lock (locker)
            {
                return database.Table<InventoryItem>().FirstOrDefault(x => x.ID == id);
            }
        }
        public int SaveItem(InventoryItem item)
        {
            lock (locker)
            {
                if (item.ID != 0)
                {
                    database.Update(item);
                    return item.ID;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }
        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<InventoryItem>(id);
            }
        }
        //
        // InventoryMedia table
        public IEnumerable<InventoryMedia> GetMediaRecs(int id)
        {
            lock (locker)
            {
                string sSQL = string.Format("SELECT * FROM [InventoryMedia] WHERE [ItemID] = {0}", id);
                return database.Query<InventoryMedia>(sSQL);
                //
            }
        }
        public int SaveMedia(InventoryMedia MediaRec)
        {
            lock (locker)
            {
                if (MediaRec.ID != 0)
                {
                    database.Update(MediaRec);
                    return MediaRec.ID;
                }
                else
                {
                    return database.Insert(MediaRec);
                }
            }
        }
        public int DeleteMedia(int id)
        {
            lock (locker)
            {
                return database.Delete<InventoryMedia>(id);
            }
        }
        //
        // TimeLog table
        public IEnumerable<ActivityLog> GetAllActivities()
        {
            lock (locker)
            {
                string sSQL = "SELECT * FROM [ActivityLog] ORDER BY [LogDateTime]";
                return database.Query<ActivityLog>(sSQL);
                //
            }
        }
        public IEnumerable<ActivityLog> GetActivityRecs(int id)
        {
            lock (locker)
            {
                string sSQL = string.Format("SELECT * FROM [ActivityLog] WHERE [ItemID] = {0} ORDER BY [LogDateTime]", id);
                return database.Query<ActivityLog>(sSQL);
                //
            }
        }
        public int SaveActivityLog(ActivityLog ActivityLogRec)
        {
            lock (locker)
            {
                if (ActivityLogRec.ID != 0)
                {
                    database.Update(ActivityLogRec);
                    return ActivityLogRec.ID;
                }
                else
                {
                    return database.Insert(ActivityLogRec);
                }
            }
        }
        public int DeleteActivityLog(int id)
        {
            lock (locker)
            {
                return database.Delete<ActivityLog>(id);
            }
        }
        //
        // ListData table
        public IEnumerable<ListData> GetListByType(string sListType)
        {
            lock (locker)
            {
                string sSQL = string.Format("SELECT * FROM [ListData] WHERE [ListType] = '{0}' ORDER BY [ListDesc]", sListType);
                return database.Query<ListData>(sSQL);
                //
            }
        }
        public IEnumerable<ListData> GetListRec(int id)
        {
            lock (locker)
            {
                string sSQL = string.Format("SELECT * FROM [ListData] WHERE [Id] = {0}", id);
                return database.Query<ListData>(sSQL);
                //
            }
        }
        public int SaveListRec(ListData ListRec)
        {
            lock (locker)
            {
                if (ListRec.ID != 0)
                {
                    database.Update(ListRec);
                    return ListRec.ID;
                }
                else
                {
                    return database.Insert(ListRec);
                }
            }
        }
        public int DeleteListRec(int id)
        {
            lock (locker)
            {
                return database.Delete<ListData>(id);
            }
        }
    }
}
