using System;
using Android.Support.Design.Widget;
using Android.Widget;

namespace Bukep.Sheduler.View
{
    /// <summary>
    /// Используется для работы с общей логикой навигации в Activity.
    /// </summary>
    //TODO: delete this
    public abstract class NavigationActivity : BaseActivity
    {
        /// <summary>
        /// Инициализация всех навигационных View. 
        /// </summary>
        protected void InitNavigationView()
        {
            InitToolbar();
            //InitBottomNavigationView();
        }

        protected virtual void InitBottomNavigationView()
        {
            var bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            if (bottomNavigation == null)
            {
                throw new Exception("Failed execute InitBottomNavigationView(). Check BottomNavigationView in Layout.");
            }
            bottomNavigation.NavigationItemSelected += ClickBottomNavigation;
        }

        private void ClickBottomNavigation(object sender,
            BottomNavigationView.NavigationItemSelectedEventArgs args)
        {
            var itemName = "";
            switch (args.Item.ItemId)
            {
                case Resource.Id.menu_list_task:
                    itemName = "menu_list_task";
                    break;
                case Resource.Id.menu_teachers:
                    itemName = "menu_teachers";
                    break;
                case Resource.Id.menu_favorites:
                    itemName = "menu_favorites";
                    break;
            }
            Toast.MakeText(this, "Click " + itemName, ToastLength.Long).Show();
        }

        protected virtual void InitToolbar()
        {
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar == null)
            {
                throw new Exception("Failed execute InitToolbar(). Check Toolbar in Layout.");
            }
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = GetString(Resource.String.ApplicationName);
        }
    }
}