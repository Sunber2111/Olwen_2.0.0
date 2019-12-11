using System;
using System.Windows;
using GalaSoft.MvvmLight;
using Prism.Commands;

namespace Olwen_2._0._0.ViewModel
{
  
    public class MainViewModel : ViewModelBase
    {
        private int _slide;

        public DelegateCommand<int?> SlideChange
        {
            get;
            private set;
        }

        public int Slide
        {
            get
            {
                return _slide;
            }

            set
            {
                _slide = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            Slide = 0;

            SlideChange = new DelegateCommand<int?>(ChangeSlide);
        }

        private void ChangeSlide(int? obj)
        {
            try
            {
                if(obj!=null)
                {
                    Slide = (int)obj;
                }
            }
            catch
            {
                MessageBox.Show("có lỗi");
            }
        }
    }
}