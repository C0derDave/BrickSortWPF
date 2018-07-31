using BrickSortWPF.BrickSort;
using BrickSortWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BrickSortWPF.ViewModel
{
    public class InventoryViewModel : INotifyPropertyChanged
    {
        private ICommand _sortByQuantityCommand;
        public ICommand SortByQuantityCommand
        {
            get
            {
                if (_sortByQuantityCommand == null)
                {
                    _sortByQuantityCommand = new RelayCommand(
                        p => true,
                        p => SortByQuantity());
                }
                return _sortByQuantityCommand;
            }
        }

        public ObservableCollection<PartViewModel> Parts { get; set; }

        private Inventory _inventory;
        private InventoryDownloader _inventoryDownloader;
    
        public InventoryViewModel()
        {
            Parts = new ObservableCollection<PartViewModel>();
            _inventory = new Inventory();
            _inventoryDownloader = new InventoryDownloader();
        }

        private bool _sortedByQuantity = false;
        private void SortByQuantity()
        {
            IEnumerable<PartViewModel> sortedParts;
            if (!_sortedByQuantity)
                sortedParts = Parts.ToArray().OrderBy(p => p.RequiredQuantity);
            else
                sortedParts = Parts.ToArray().OrderBy(p => p.Color);
            _sortedByQuantity = !_sortedByQuantity;

            Application.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                Parts.Clear();
                foreach (PartViewModel part in sortedParts)
                {
                    Parts.Add(new PartViewModel(part.Name, part.UniqueID, part.Color, part.RequiredQuantity, part.ImageURL) { Quantity = part.Quantity });
                }
            }));
        }

        public async Task LoadSet(string setID)
        {
            await Task.Run(() =>
            {
                _inventory = _inventoryDownloader.DownloadInventoryFromId(setID);
            });
            Thread.Sleep(0);

            Application.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                Parts.Clear();
                foreach(Part part in _inventory.Parts)
                {
                    Parts.Add(new PartViewModel(part.Name, part.GetUniqueID(), part.Color, part.RequiredQuantity, part.ImageURL));
                }
            }));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
