using Shop.BLL;
using Shop.BLL.Entities;
using Shop.DAL;
using Shop.DAL.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Shops.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Shop.DAL.Entities.Shop> Shops { get; set; } = new ObservableCollection<Shop.DAL.Entities.Shop>();
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public ObservableCollection<Assortment> Assortments { get; set; } = new ObservableCollection<Assortment>();
        public ObservableCollection<Assortment> AssortmentSelectedShop { get; set; } = new ObservableCollection<Assortment>();


        private int _myVar;

        public int MyProperty
        {
            get { return _myVar; }
            set { _myVar = value; }
        }



        DataModel dal = null;
        ShopService bll = null;

        public MainWindowViewModel()
        {
            dal = new DataModel(App.Configuration);
            bll = new ShopService(dal.GetImplementation());
            foreach (var shop in dal.GetShops())
            {
                Shops.Add(shop);
            }

            foreach (var product in dal.GetProducts())
            {
                Products.Add(product);
            }

            foreach (var assortment in dal.GetAssortments())
            {
                Assortments.Add(assortment);
            }
            PackProduct = new PackProduct();
            WhatCanBuy = new WhatCanBuy();
            MinPriceProduct = new MinPriceProduct();
            BuyPackProductInShop = new BuyPackProductInShop();
            BuyPackLowPrice = new BuyPackLowPrice();
        }


        #region props
        private PackProduct _packProduct;
        public PackProduct PackProduct
        {
            get { return _packProduct; }
            set
            {
                _packProduct = value;
                OnPropertyChanged();
            }
        }

        private WhatCanBuy _whatCanBuy;
        public WhatCanBuy WhatCanBuy
        {
            get { return _whatCanBuy; }
            set
            {
                _whatCanBuy = value;
                OnPropertyChanged();
            }
        }

        private MinPriceProduct _minPriceProduct;
        public MinPriceProduct MinPriceProduct
        {
            get { return _minPriceProduct; }
            set
            {
                _minPriceProduct = value;
                OnPropertyChanged();
            }
        }

        private BuyPackProductInShop _buyPackProductInShop;
        public BuyPackProductInShop BuyPackProductInShop
        {
            get { return _buyPackProductInShop; }
            set
            {
                _buyPackProductInShop = value;
                OnPropertyChanged();
            }
        }

        private BuyPackLowPrice _buyPackLowPrice;
        public BuyPackLowPrice BuyPackLowPrice
        {
            get { return _buyPackLowPrice; }
            set
            {
                _buyPackLowPrice = value;
                OnPropertyChanged();
            }
        }


        private string _titleShop;
        public string TitleShop
        {
            get { return _titleShop; }
            set
            {
                _titleShop = value;
                OnPropertyChanged();
            }
        }

        private string _addressShop;
        public string AddressShop
        {
            get { return _addressShop; }
            set
            {
                _addressShop = value;
                OnPropertyChanged();
            }
        }

        private Shop.DAL.Entities.Shop selectedShop;
        public Shop.DAL.Entities.Shop SelectedShop
        {
            get { return selectedShop; }
            set
            {
                selectedShop = value;

                var assortments = Assortments.Where(x => x.Shop.Id == selectedShop.Id).ToList();
                AssortmentSelectedShop.Clear();
                foreach (var assortment in assortments)
                {
                    AssortmentSelectedShop.Add(assortment);
                }
                OnPropertyChanged();
            }
        }

        private string _titleProduct;
        public string TitleProduct
        {
            get { return _titleProduct; }
            set
            {
                _titleProduct = value;
                OnPropertyChanged();
            }
        }

        private double _priceProduct;
        public double PriceProduct
        {
            get { return _priceProduct; }
            set
            {
                _priceProduct = value;
                OnPropertyChanged();
            }
        }

        #endregion



        #region commands
        public RelayCommand CreateShop
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    dal.CreateNewShop(TitleShop, AddressShop);
                    TitleShop = "";
                    AddressShop = "";
                });
            }
        }

        public RelayCommand CreateProduct
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    dal.CreateNewProduct(TitleProduct, PriceProduct);
                    TitleProduct = "";
                    PriceProduct = 0;
                });
            }
        }

        public RelayCommand AddPackProducts
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    bll.AddPackProducts(PackProduct, out string message);
                    if (!string.IsNullOrEmpty(message))
                    {
                        MessageBox.Show(message, "Ошибка");
                    }
                });
            }
        }

        public RelayCommand GetWhatCanBuy
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    var res = bll.GetWhatCanBui(WhatCanBuy);
                    if (string.IsNullOrEmpty(res))
                    {
                        MessageBox.Show("На данную сумму, в данном магазине нечего купить", "Покупка");
                    }
                    else
                    {
                        MessageBox.Show(res, "Покупка");
                    }
                });
            }
        }

        public RelayCommand GetMinPriceProduct
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    var res = bll.GetMinPriveProduct(MinPriceProduct);
                    if (string.IsNullOrEmpty(res))
                    {
                        MessageBox.Show("Данный товар не найден", "Ошибка");
                    }
                    else
                    {
                        MessageBox.Show(res, "Самый дешевый товар");
                    }
                });
            }
        }

        public RelayCommand GetBuyPackProductInShop
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    var res = bll.GetBuyPackProductInShop(BuyPackProductInShop);
                    MessageBox.Show(res, "Результат");
                });
            }
        }

        public RelayCommand GetBuyPackLowPrice
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    var res = bll.GetBuyPackLowPrice(BuyPackLowPrice);
                    MessageBox.Show(res, "Результат");
                });
            }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
}
