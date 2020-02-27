using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PZ3_NetworkService.ViewModel
{
    public class HelpViewModel : BindableBase
    {  
        public MyICommand IjednoIdrugo { get; set; }

        public string boja1;
        public static bool prviSetBoja = false;
        public bool x1 = false;

        public HelpViewModel()
        {
            IjednoIdrugo = new MyICommand(ijid);
            if (IsToolTipVisible == false)
            {
                Boja1 = "Gray";
                prviSetBoja = true;
            }
        }

        public string Boja1
        {
            get { return boja1; }
            set
            {
                boja1 = value;
                OnPropertyChanged("Boja1");
            }
        }


        public void ijid()
        {
            Style style = new Style(typeof(ToolTip));
            style.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            style.Seal();


            if (IsToolTipVisible == false)
            {
                foreach (Window window in Application.Current.Windows)
                {

                    window.Resources.Remove(typeof(ToolTip)); //show
                    IsToolTipVisible = true;
                    Boja1 = "Green";
                }
            }
            else
            {
                foreach (Window window in Application.Current.Windows)
                {
                    window.Resources.Add(typeof(ToolTip), style); //hide
                    IsToolTipVisible = false;
                    Boja1 = "Red";
                }           
            }
        }
        
    }
}
