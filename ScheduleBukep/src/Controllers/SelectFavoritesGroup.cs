using System.Collections.Generic;
using Android.Widget;
using Bukep.Sheduler.Controllers;
using Bukep.Sheduler.logic;
using Bukep.Sheduler.View;
using ScheduleBukepAPI.domain;

namespace Bukep.Sheduler.controllers
{
    internal class SelectFavoritesGroup : SelectItem
    {
        public const string CacheKeyFavoritesGroup = "FavoritesGroup";

        public SelectFavoritesGroup(SelectItemActivity activity) : base(activity)
        {
        }

        public override void Update()
        {
            List<Group> favoritesGroup = CacheHelper.GetAndPutInCached<List<Group>>(
                CacheKeyFavoritesGroup, () => null);

            InitChoice(favoritesGroup, ShowScheduleFavoritesGroup, group => group.NameGroup);
        }

        private void ShowScheduleFavoritesGroup(Group group)
        {
            Toast.MakeText(_view,"SelectFavoritesGroup", ToastLength.Long).Show();
        }
    }
}