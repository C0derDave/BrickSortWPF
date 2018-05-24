using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace BrickSortWPF.ViewModel
{
    public class PartViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }

        /// <summary>
        /// The ID of the part, so, in this case, the actual numerical ID of the part + the part color in order to keep it unique.
        /// </summary>
        public string ID { get; set; }

        public int RequiredQuantity { get; set; }

        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                {
                    _addCommand = new RelayCommand(
                        p => true,
                        p => Add());
                }
                return _addCommand;
            }
        }

        private ICommand _removeCommand;
        public ICommand RemoveCommand
        {
            get
            {
                if (_removeCommand == null)
                {
                    _removeCommand = new RelayCommand(
                        p => true,
                        p => Remove());
                }
                return _removeCommand;
            }
        }

        /// <summary>
        /// Create a PartViewModel.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id">This parameter would be the numerical part ID + the part color.</param>
        /// <param name="requiredQuantity"></param>
        /// <param name="imageURL"></param>
        public PartViewModel(string name, string id, int requiredQuantity, string imageURL)
        {
            Name = name;
            ID = id;
            Quantity = 0;
            RequiredQuantity = requiredQuantity;

            Uri uri = new Uri(imageURL, UriKind.Absolute);
            Image = new BitmapImage(uri);
            OnPropertyChanged(nameof(Image));
        }

        public void Add()
        {
            Quantity++;
        }

        public void Remove()
        {
            if (Quantity > 0)
            {
                Quantity--;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
