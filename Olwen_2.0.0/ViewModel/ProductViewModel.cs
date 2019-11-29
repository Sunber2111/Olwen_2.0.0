using GalaSoft.MvvmLight;
using Prism.Commands;
using Olwen_2._0._0.Model;
using Olwen_2._0._0.Repositories.Implements;
using Olwen_2._0._0.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Olwen_2._0._0.DataModel;
using System.Windows.Media.Imaging;
using Olwen_2._0._0.View.Components;
using Olwen_2._0._0.View.DialogsResult;
using MaterialDesignThemes.Wpf;
using Olwen_2._0._0.DependencyInjection;

namespace Olwen_2._0._0.ViewModel
{
    public class ProductViewModel : ViewModelBase
    {
        private IAsyncRepository<ProductCategory> categories_Repo;
        private IProductRepository product_Repo;

        private ObservableCollection<ProductCategory> _listCatelogys;
        private ObservableCollection<ProductModel> _listProducts;
        private ObservableCollection<string> _listKey1, _listKey2;
        private BitmapImage _imgProduct;
        private string _productName, _description, _price, _priceSales, _quantity, _productID;
        private string _proCateID, _nameCate, _desCate;
        private string _sKey1, _sKey2;
        private string _seaKey1, _seaKey2;
        static DialogContent dc = new DialogContent();
        static DialogOk dialog = new DialogOk();
        private const string DialogHostId = "RootDialogHostId";
        public DialogSession DialogSession { get; set; }

        public ObservableCollection<ProductCategory> ListCatelogys
        {
            get
            {
                return _listCatelogys;
            }

            set
            {
                _listCatelogys = value;
                RaisePropertyChanged("");
            }
        }

        public DelegateCommand LoadedCommand
        {
            get;
            private set;
        }

        public DelegateCommand AddPCCommand
        {
            get;
            private set;
        }

        public DelegateCommand<int?> UpdateCategoryCommand
        {
            get;
            private set;
        }

        public DelegateCommand<int?> DeleteCategoryCommand
        {
            get;
            private set;
        }

        public DelegateCommand<string> SubmitCommand
        {
            get;
            private set;
        }

        public ObservableCollection<ProductModel> ListProducts
        {
            get
            {
                return _listProducts;
            }

            set
            {
                _listProducts = value;
                RaisePropertyChanged("ListProducts");
            }
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }

            set
            {
                _productName = value;
                RaisePropertyChanged("ProductName");
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
                RaisePropertyChanged("Description");
            }
        }

        public string Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
                RaisePropertyChanged("Price");
            }
        }

        public string PriceSales
        {
            get
            {
                return _priceSales;
            }

            set
            {
                _priceSales = value;
                RaisePropertyChanged("PriceSales");
            }
        }

        public string Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _quantity = value;
                RaisePropertyChanged("Quantity");

            }
        }

        public string ProductID
        {
            get
            {
                return _productID;
            }

            set
            {
                _productID = value;
                RaisePropertyChanged("ProductID");
            }
        }

        public string ProCateID
        {
            get
            {
                return _proCateID;
            }

            set
            {
                _proCateID = value;
                RaisePropertyChanged("ProCateID");
            }
        }

        public string NameCate
        {
            get
            {
                return _nameCate;
            }

            set
            {
                _nameCate = value;
                RaisePropertyChanged("NameCate");
            }
        }

        public string DesCate
        {
            get
            {
                return _desCate;
            }

            set
            {
                _desCate = value;
                RaisePropertyChanged("DesCate");
            }
        }

        public BitmapImage ImgProduct
        {
            get
            {
                return _imgProduct;
            }

            set
            {
                _imgProduct = value;
                RaisePropertyChanged("ImgProduct");
            }
        }

        public ObservableCollection<string> ListKey1
        {
            get
            {
                return _listKey1;
            }

            set
            {
                _listKey1 = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> ListKey2
        {
            get
            {
                return _listKey2;
            }

            set
            {
                _listKey2 = value;
                RaisePropertyChanged();
            }
        }

        public string SKey1
        {
            get
            {
                return _sKey1;
            }

            set
            {
                _sKey1 = value;
                RaisePropertyChanged();
            }
        }

        public string SKey2
        {
            get
            {
                return _sKey2;
            }

            set
            {
                _sKey2 = value;
                RaisePropertyChanged();
            }
        }

        public string SeaKey1
        {
            get
            {
                return _seaKey1;
            }

            set
            {
                _seaKey1 = value;
                RaisePropertyChanged();
                if (!string.IsNullOrEmpty(_seaKey1))
                {
                    if (SKey1 != null)
                    {
                        if (ListKey1.IndexOf(SKey1) == 1)
                        {
                            ListCatelogys = new ObservableCollection<ProductCategory>(categories_Repo.GetFilter(t => t.Name.Contains(SeaKey1)));
                        }
                        else if (ListKey1.IndexOf(SKey1) == 0)
                        {
                            if (SeaKey1.IsNum())
                            {
                                ListCatelogys = new ObservableCollection<ProductCategory>(categories_Repo.GetFilter(t => t.CatelogyID == Convert.ToInt32(SeaKey1)));
                            }
                        }

                    }
                }
                else
                    ListCatelogys = new ObservableCollection<ProductCategory>(categories_Repo.GetAll());
            }
        }

        public string SeaKey2
        {
            get
            {
                return _seaKey2;
            }

            set
            {
                _seaKey2 = value;
                RaisePropertyChanged();
            }
        }

        public ProductViewModel()
        {
            categories_Repo = new BaseAsyncRepository<ProductCategory>();
            product_Repo = new ProductRepository();

            LoadedCommand = new DelegateCommand(CreateData);
            AddPCCommand = new DelegateCommand(ShowNewCategory);
            DeleteCategoryCommand = new DelegateCommand<int?>(DeleteCategory);
            UpdateCategoryCommand = new DelegateCommand<int?>(ShowDataCagory);
            SubmitCommand = new DelegateCommand<string>(SubmitCategory, CanSubmit);

        }

        private bool CanSubmit(string arg)
        {
            //if (string.IsNullOrEmpty(NameCate))
            //    return false;
            //else
            return true;
        }

        private async void SubmitCategory(string obj)
        {
            try
            {
                var newcate = new ProductCategory()
                {
                    Name = NameCate,
                    Description = DesCate
                };
                if (string.IsNullOrEmpty(obj))
                {
                    //Create new category
                    var objresult = await categories_Repo.Add(newcate);
                    if (objresult != null)
                    {
                        dc = new DialogContent() { Content = "Thêm Thành Công", Tilte = "Thông Báo" };
                        dialog = new DialogOk() { DataContext = dc };
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        await DialogHost.Show(dialog, DialogHostId);

                        ListCatelogys.Add(objresult);
                    }
                    else
                    {
                        dc = new DialogContent() { Content = "Thêm Thất Bại", Tilte = "Thông Báo" };
                        dialog = new DialogOk() { DataContext = dc };
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        await DialogHost.Show(dialog, DialogHostId);
                    }

                }
                else
                {
                    //update category

                    newcate.CatelogyID = Convert.ToInt32(ProCateID);

                    if (await categories_Repo.Update(newcate))
                    {
                        dc = new DialogContent() { Content = "Cập Nhật Thành Công", Tilte = "Thông Báo" };
                        dialog = new DialogOk() { DataContext = dc };
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        await DialogHost.Show(dialog, DialogHostId);
                        ListCatelogys = new ObservableCollection<ProductCategory>(categories_Repo.GetAll());
                    }
                    else
                    {
                        dc = new DialogContent() { Content = "Cập Nhật Thất Bại", Tilte = "Thông Báo" };
                        dialog = new DialogOk() { DataContext = dc };
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        await DialogHost.Show(dialog, DialogHostId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // Paralell Async
        private void CreateData()
        {
            List<Task> tasks = new List<Task>();

            tasks.Add(new Task(() => { ListCatelogys = new ObservableCollection<ProductCategory>(categories_Repo.GetAll()); }));

            tasks.Add(new Task(() => { ListProducts = new ObservableCollection<ProductModel>(product_Repo.GetAllProductModels()); }));

            tasks.Add(new Task(() => { ListKey1 = new ObservableCollection<string>() { "CategoryID", "Category Name" }; }));

            tasks.Add(new Task(() => { ListKey2 = new ObservableCollection<string>() { "ProductID", "Product Name" }; }));

            tasks.ForEach(a => a.Start());

        }

        private async void ShowDataCagory(int? obj)
        {
            ProCateID = DesCate = NameCate = null;
            if (obj != null)
            {
                var item = ListCatelogys.SingleOrDefault(t => t.CatelogyID == obj);

                if (item != null)
                {
                    ProCateID = item.CatelogyID.ToString();
                    DesCate = item.Description;
                    NameCate = item.Name;
                }

            }
            await MaterialDesignThemes.Wpf.DialogHost.Show(new CategoryProfile(), DialogHostId);
        }

        private async void DeleteCategory(int? obj)
        {
            try
            {
                dc = new DialogContent() { Content = "Bạn muốn xóa loại này ?", Tilte = "Thông Báo" };
                var dialogYS = new DialogYesNo() { DataContext = dc };
                var result = (bool)await DialogHost.Show(dialogYS, DialogHostId);
                if (result)
                {
                    if (obj != null)
                    {
                        if (await categories_Repo.Remove((int)obj))
                        {
                            dc = new DialogContent() { Content = "Xóa Thành Công", Tilte = "Thông Báo" };
                            dialog = new DialogOk() { DataContext = dc };
                            await DialogHost.Show(dialog, DialogHostId);
                            ListCatelogys.Remove(ListCatelogys.SingleOrDefault(t => t.CatelogyID == (int)obj));
                        }
                        else
                        {
                            dc = new DialogContent() { Content = "Xóa Thất Bại", Tilte = "Thông Báo" };
                            dialog = new DialogOk() { DataContext = dc };
                            await DialogHost.Show(dialog, DialogHostId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private async void ShowNewCategory()
        {
            DesCate = NameCate = ProCateID = null;
            await DialogHost.Show(new CategoryProfile(), DialogHostId);

        }


    }
}
