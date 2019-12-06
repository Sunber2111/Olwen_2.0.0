﻿using GalaSoft.MvvmLight;
using MaterialDesignThemes.Wpf;
using Olwen_2._0._0.DataModel;
using Olwen_2._0._0.Model;
using Olwen_2._0._0.Repositories.Implements;
using Olwen_2._0._0.Repositories.Interfaces;
using Olwen_2._0._0.View.Components;
using Olwen_2._0._0.View.DialogsResult;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olwen_2._0._0.ViewModel
{
    public class StoreViewModel:ViewModelBase
    {
        private IProductRepository product_repo;
        private IAsyncRepository<Store> store_repo;
        private IAsyncRepository<Employee> emp_repo;
        private StoreDetailRepository sd_repo;
        private const string DialogHostId = "RootDialogHost4";
        private ObservableCollection<Store> _listStore;
        private ObservableCollection<ProductStoreModel> _listProSto;
        private ObservableCollection<Employee> _listManager;
        private Employee _manager;
        private Store _selectedSto;
        private string _storeID, _name, _address, _description;
        static DialogContent dc = new DialogContent();
        static DialogOk dialog = new DialogOk();

        public DelegateCommand<int?> DeleteStoreCommand
        {
            get;
            private set;
        }

        public DelegateCommand<int?> UpdateProCommand
        {
            get;
            private set;
        }

        public DelegateCommand AddStore
        {
            get;
            private set;
        }

        public DelegateCommand LoadingCommand
        {
            get;
            private set;
        }

        public DelegateCommand<int?> DeleteProCommand
        {
            get;
            private set;
        }

        public DelegateCommand<string> SubmitCommand
        {
            get;
            private set;
        }

        public DelegateCommand<int?> UpdateStoreCommand
        {
            get;
            private set;
        }

        public ObservableCollection<Store> ListStore
        {
            get
            {
                return _listStore;
            }

            set
            {
                _listStore = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ProductStoreModel> ListProSto
        {
            get
            {
                return _listProSto;
            }

            set
            {
                _listProSto = value;
                RaisePropertyChanged();
            }
        }

        public Store SelectedSto
        {
            get
            {
                return _selectedSto;
            }

            set
            {
                _selectedSto = value;
                RaisePropertyChanged();
                if(_selectedSto!=null)
                {
                    ListProSto = new ObservableCollection<ProductStoreModel>(product_repo.GetAllProductStoreByStoreID(_selectedSto.StoreID));
                }
            }
        }

        public string StoreID
        {
            get
            {
                return _storeID;
            }

            set
            {
                _storeID = value;
                RaisePropertyChanged();
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        public string Address
        {
            get
            {
                return _address;
            }

            set
            {
                _address = value;
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }

        public Employee Manager
        {
            get
            {
                return _manager;
            }

            set
            {
                _manager = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Employee> ListManager
        {
            get
            {
                return _listManager;
            }

            set
            {
                _listManager = value;
                RaisePropertyChanged();
            }
        }


        public StoreViewModel()
        {
            product_repo = new ProductRepository();
            store_repo = new BaseAsyncRepository<Store>();
            emp_repo = new BaseAsyncRepository<Employee>();
            sd_repo = new StoreDetailRepository();

            LoadingCommand = new DelegateCommand(CreateData);
            UpdateStoreCommand = new DelegateCommand<int?>(ShowInfo);
            SubmitCommand = new DelegateCommand<string>(Submit);
            DeleteStoreCommand = new DelegateCommand<int?>(DeleteStore);
            AddStore = new DelegateCommand(AddNewStore);
            UpdateProCommand = new DelegateCommand<int?>(UpdateQty);
            DeleteProCommand = new DelegateCommand<int?>(DeletePro);
        }

        private async void DeletePro(int? obj)
        {
            try
            {
                dc = new DialogContent() { Content = "Bạn muốn xóa sản phẩm này ?", Tilte = "Thông Báo" };
                var dialogYS = new DialogYesNo() { DataContext = dc };
                var result = (bool)await DialogHost.Show(dialogYS, DialogHostId);
                if (result)
                {
                    var sd = new StoreDetail()
                    {
                        StoreID = SelectedSto.StoreID,
                        ProductID = (int)obj,
                    };
                    if (sd_repo.DeletePro(sd))
                    {
                        dc = new DialogContent() { Content = "Xóa Thành Công", Tilte = "Thông Báo" };
                        dialog = new DialogOk() { DataContext = dc };
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        await DialogHost.Show(dialog, DialogHostId);
                        ListProSto.Remove(ListProSto.SingleOrDefault(t => t.ProductID == (int)obj));
                    }
                    else
                    {
                        dc = new DialogContent() { Content = "Xóa Thất Bại", Tilte = "Thông Báo" };
                        dialog = new DialogOk() { DataContext = dc };
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        await DialogHost.Show(dialog, DialogHostId);
                    }
                }
            }
            catch
            {
                dc.Content = "Có Lỗi";
                dc.Tilte = "Thông Báo";
                dialog = new DialogOk() { DataContext = dc };
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(dialog, DialogHostId);
            }
        }

        private async void UpdateQty(int? obj)
        {
            try
            {
                var sd = new StoreDetail()
                {
                    StoreID = SelectedSto.StoreID,
                    ProductID = (int)obj,
                    Quantity = ListProSto.SingleOrDefault(t=>t.ProductID==(int)obj).Quantity
                };
                if (sd_repo.UpdateQty(sd))
                {
                    dc = new DialogContent() { Content = "Cập Nhật Thành Công", Tilte = "Thông Báo" };
                    dialog = new DialogOk() { DataContext = dc };
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    await DialogHost.Show(dialog, DialogHostId);
                }
                else
                {
                    dc = new DialogContent() { Content = "Cập Nhật Thất Bại", Tilte = "Thông Báo" };
                    dialog = new DialogOk() { DataContext = dc };
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    await DialogHost.Show(dialog, DialogHostId);
                }
            }
            catch
            {
                dc.Content = "Có Lỗi";
                dc.Tilte = "Thông Báo";
                dialog = new DialogOk() { DataContext = dc };
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(dialog, DialogHostId);
            }
        }

        private async void AddNewStore()
        {
            try
            {
                StoreID = Name = Description = Address = null;
                Manager = null;
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(new StoreProfile(), DialogHostId);
            }
            catch
            {
                dc.Content = "Có Lỗi";
                dc.Tilte = "Thông Báo";
                dialog = new DialogOk() { DataContext = dc };
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(dialog, DialogHostId);
            }
        }

        private async void DeleteStore(int? obj)
        {
            try
            {
                dc = new DialogContent() { Content = "Bạn muốn xóa Kho này ?", Tilte = "Thông Báo" };
                var dialogYS = new DialogYesNo() { DataContext = dc };
                var result = (bool)await DialogHost.Show(dialogYS, DialogHostId);
                if (result)
                {
                    if (obj != null)
                    {
                        if (await store_repo.Remove((int)obj))
                        {
                            ListStore.Remove(ListStore.SingleOrDefault(t => t.StoreID == (int)obj));
                            dc = new DialogContent() { Content = "Xóa Thành Công", Tilte = "Thông Báo" };
                            dialog = new DialogOk() { DataContext = dc };
                            await DialogHost.Show(dialog, DialogHostId);
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
            catch 
            {
                dc.Content = "Có Lỗi";
                dc.Tilte = "Thông Báo";
                dialog = new DialogOk() { DataContext = dc };
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(dialog, DialogHostId);
            }
        }

        private async void Submit(string obj)
        {
            try
            {
                var newstore = new Store()
                {
                    Name = Name,
                    Description = Description,
                    Address = Address,

                };

                if (Manager != null)
                    newstore.EmpID_Control = Manager.EmpID;


                if (obj == null)
                {
                    //Create new store
                    var objresult = await store_repo.Add(newstore);

                    if (objresult != null)
                    {
                        dc = new DialogContent() { Content = "Thêm Thành Công", Tilte = "Thông Báo" };
                        dialog = new DialogOk() { DataContext = dc };
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        await DialogHost.Show(dialog, DialogHostId);
                        ListStore.Add(objresult);
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
                    //update store

                    newstore.StoreID = Convert.ToInt32(obj);

                    if (await store_repo.Update(newstore))
                    {
                        dc = new DialogContent() { Content = "Cập Nhật Thành Công", Tilte = "Thông Báo" };
                        dialog = new DialogOk() { DataContext = dc };
                        DialogHost.CloseDialogCommand.Execute(null, null);
                        await DialogHost.Show(dialog, DialogHostId);
                        ListStore = new ObservableCollection<Store>(await store_repo.GetAllAsync());
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
            catch
            {
                dc.Content = "Có Lỗi";
                dc.Tilte = "Thông Báo";
                dialog = new DialogOk() { DataContext = dc };
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(dialog, DialogHostId);
            }
        }

        private async void ShowInfo(int? obj)
        {
            try
            {
                var store = ListStore.SingleOrDefault(t => t.StoreID == (int)obj);
                StoreID = store.StoreID.ToString();
                Name = store.Name;
                Description = store.Description;
                Address = store.Address;
                if (store.EmpID_Control != null)
                    Manager = ListManager.SingleOrDefault(t => t.EmpID == store.EmpID_Control);
                else
                    Manager = null;
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(new StoreProfile(), DialogHostId);
            }
            catch
            {
                dc.Content = "Có Lỗi";
                dc.Tilte = "Thông Báo";
                dialog = new DialogOk() { DataContext = dc };
                DialogHost.CloseDialogCommand.Execute(null, null);
                await DialogHost.Show(dialog, DialogHostId);
            }
        }

        private async void CreateData()
        {
            ListStore = new ObservableCollection<Store>(await store_repo.GetAllAsync());
            ListManager = new ObservableCollection<Employee>(await emp_repo.GetAllAsync());
            SelectedSto = ListStore.First();
        }
    }
}
