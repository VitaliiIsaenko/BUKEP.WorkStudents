using System;
using System.Collections.Generic;
using Android.Widget;
using Bukep.Sheduler.Controllers;

namespace Bukep.Sheduler.View
{
    internal class ItemSelector<TItem>
    {
        private readonly ItemAdapter<TItem> _itemAdapter;
        private readonly Action<TItem> _selectedItem;
        private readonly List<Spinner> _spinners;

        public ItemSelector(ItemAdapter<TItem> itemAdapter, Action<TItem> selectedItem, List<Spinner> spinners)
        {
            _itemAdapter = itemAdapter;
            _selectedItem = selectedItem;
            _spinners = spinners;
        }

        public void SelectedItemInSpinner(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var posotion = e.Position;
            if (posotion == 0) return;
            TItem item = _itemAdapter.GetObject(posotion);
            _selectedItem.Invoke(item);

            UpdateEnabledSpinners((Spinner) sender);
        }

        /// <summary>
        /// Включает следующий выпадающий список от выбранного, и выключает все последующие
        /// </summary>
        /// <param name="selectedSpinner">Выбранный выпадающий список</param>
        private void UpdateEnabledSpinners(Spinner selectedSpinner)
        {
            var index = _spinners.FindIndex(spinner => spinner.Equals(selectedSpinner));
            _spinners[index++].Enabled = true;
            for (var i = index + 2; i < _spinners.Count; i++)
            {
                _spinners[i].Enabled = false;
            }
        }
    }
}