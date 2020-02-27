using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PZ3_NetworkService.Model;

namespace PZ3_NetworkService.ViewModel
{
    public class NetworkDataViewModel : BindableBase
    { 
        public MyICommand AddNewPump { get; set; }
        public MyICommand DeletePump { get; set; }
        public MyICommand SearchSvePumpe { get; set; }
        public MyICommand SearchStareVr { get; set; }
        public static List<double> proveraIDa { get; set; }
        public static ObservableCollection<Pumpa> Pumpe { get; set; }
        public static ObservableCollection<TipoviPumpi> Tipovi { get; set; }
        public static ObservableCollection<Pumpa> SearchovanePumpe { get; set; }
        private Pumpa currentPumpa = new Pumpa();
        private Pumpa selektovanaPumpa = new Pumpa();
        public bool srchName;//
        public bool srchType;//
        public string tekstIzSrch;//
        public string srcClr;
        public bool prviPutPritisnutSearch = false;
        public bool prviPutPritisnutExitSearch = false;
        ObservableCollection<Pumpa> preuzimaSearchovane = new ObservableCollection<Pumpa>();
        ObservableCollection<Pumpa> vracaStareVrednosti = new ObservableCollection<Pumpa>();
        ObservableCollection<Pumpa> delVr = new ObservableCollection<Pumpa>();
        private string currId;

        public NetworkDataViewModel()
        {
           
            Pumpe = MainViewModel.Pumpe;
            Tipovi = MainViewModel.Tipovi;
            AddNewPump = new MyICommand(AddOn);
            DeletePump = new MyICommand(Delete, CanDelete);
            SearchSvePumpe = new MyICommand(Pretrazi);
            SearchStareVr = new MyICommand(VratiStare);
            proveraIDa = MainViewModel.proveraIDa;
            SrcClr = "Gray";

        }

        public Pumpa CurrentPumpa
        {
            get { return currentPumpa; }
            set
            {
                currentPumpa = value;
                OnPropertyChanged("CurrentPumpa");
            }
        }

        public Pumpa SelektovanaPumpa
        {
            get { return selektovanaPumpa; }
            set
            {
                selektovanaPumpa = value;
                DeletePump.RaiseCanExecuteChanged();
            }
        }


        public string CurrId
        {
            get { return currId; }
            set
            {
                currId = value;
                OnPropertyChanged("CurrId");

            }
        }


        public void idProvera()
        {
            if (CurrId != null)
            {
                try
                {
                    CurrentPumpa.Id = Int32.Parse(CurrId);
                }
                catch
                {
                    CurrentPumpa.Id = -2;
                }
            }
            else
                CurrentPumpa.Id = -1;
        }

        public void AddOn()
        {
            idProvera();

            CurrentPumpa.Validate();
            if (CurrentPumpa.IsValid)
            {
                Pumpe.Add(new Pumpa
                {
                    Id = CurrentPumpa.Id,
                    Name = CurrentPumpa.Name,
                    Type = CurrentPumpa.Type

                });
                proveraIDa.Add(CurrentPumpa.Id);
                CurrId = "";
                CurrentPumpa.Name = "";
                CurrentPumpa.Id = 0;
                CurrentPumpa.Type = null;
            }
        }


        public bool CanDelete()
        {
            return SelektovanaPumpa != null;
        }

        public void Delete()
        {
            proveraIDa.Remove(SelektovanaPumpa.Id);
            Pumpe.Remove(SelektovanaPumpa);           
        }


        public bool SrchName
        {
            get { return srchName; }
            set
            {
                srchName = value;
                OnPropertyChanged("SrchName");
            }
        }

        public bool SrchType
        {
            get { return srchType; }
            set
            {
                srchType = value;
                OnPropertyChanged("SrchType");
            }
        }

        public string TekstIzSrch
        {
            get { return tekstIzSrch; }
            set
            {
                tekstIzSrch = value;
                OnPropertyChanged("TekstIzSrch");
            }
        }

        public string SrcClr
        {
            get { return srcClr; }
            set
            {
                srcClr = value;
                OnPropertyChanged("SrcClr");
            }
        }

        public void Pretrazi() 
        {

            if (TekstIzSrch == "" || TekstIzSrch == null || (SrchName == false && SrchType == false))
            {
                SrcClr = "Red";
            }
            else
            {
                SrcClr = "Gray";

                if (prviPutPritisnutSearch == false)
                {
                    foreach (Pumpa item in Pumpe)
                    {
                        vracaStareVrednosti.Add(item);
                    }
                    prviPutPritisnutSearch = true;
                    prviPutPritisnutExitSearch = true;
                }
                if (SrchName == true)
                {
                    foreach (Pumpa item in Pumpe)
                    {
                        if (item.Name == TekstIzSrch)
                        {
                            preuzimaSearchovane.Add(item);
                        }
                    }
                }
                else if (SrchType == true)
                {
                    foreach (Pumpa item in Pumpe)
                    {
                        if (item.Type.NameTip == TekstIzSrch)
                        {
                            preuzimaSearchovane.Add(item);
                        }
                    }
                }

                Pumpe.Clear();
                foreach (Pumpa item in preuzimaSearchovane)
                {
                    Pumpe.Add(item);
                }
                preuzimaSearchovane.Clear();

            }


            
        }

        public void VratiStare()
        {
            if (prviPutPritisnutExitSearch == true)
            {
                Pumpe.Clear();
                foreach (Pumpa item in vracaStareVrednosti)
                {
                    Pumpe.Add(item);
                }
                vracaStareVrednosti.Clear();
                prviPutPritisnutSearch = false;
                prviPutPritisnutExitSearch = false;
            }
        }
    }
}
