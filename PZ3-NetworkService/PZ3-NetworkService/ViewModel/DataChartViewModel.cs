using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PZ3_NetworkService.Model;

namespace PZ3_NetworkService.ViewModel
{
    public class DataChartViewModel : BindableBase
    {
        public static ObservableCollection<Pumpa> Pumpe { get; set; }
        public MyICommand Show { get; set; }
        private List<string> textboxes = new List<string>();
        private List<double> rectangles = new List<double>();
        public Pumpa selectedObject;
        public string provera;
        public string danas;
        private Dictionary<Pumpa, Dictionary<DateTime, double>> prikazaniRectangles = new Dictionary<Pumpa, Dictionary<DateTime, double>>();

        public Dictionary<Pumpa, Dictionary<DateTime, double>> PrikazaniRectangles
        {
            get { return prikazaniRectangles; }
            set
            {
                prikazaniRectangles = value;
                OnPropertyChanged("PrikazaniRectangles");
            }
        }

        public List<double> Rectangles
        {
            get { return rectangles; }
            set
            {
                rectangles = value;
                OnPropertyChanged("Rectangles");
            }
        }
        public List<string> Textboxes
        {
            get { return textboxes; }
            set
            {
                textboxes = value;
                OnPropertyChanged("Textboxes");
            }
        }

        public Pumpa SelectedObject
        {
            get { return selectedObject; }
            set
            {
                selectedObject = value;

                OnPropertyChanged("SelectedObject");
            }
        }



        public DataChartViewModel()
        {
            Pumpe = MainViewModel.Pumpe;
            Show = new MyICommand(OnShow);
        }


        public void OnShow()
        {
            List<double> pomr = new List<double>();
            List<string> pomt = new List<string>();
            for (int i = 0; i < 10; ++i)
            {
                Rectangles.Add(0);
                pomr.Add(0);
                Textboxes.Add(" ");
                pomt.Add(" ");
            }

            if (SelectedObject != null)
            {

                Pumpa izbraniObj = new Pumpa(SelectedObject.Id, SelectedObject.Name, SelectedObject.Type, SelectedObject.Value);

                if (!PrikazaniRectangles.ContainsKey(izbraniObj))
                {
                    PrikazaniRectangles.Add(izbraniObj, new Dictionary<DateTime, double>());
                }
                else
                {
                    PrikazaniRectangles[izbraniObj].Clear();
                }


                string[] s = null;
                s = File.ReadAllLines("Log.txt");
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    string[] line = s[i].Split(' ', '\t');
                    if (Int32.Parse(line[1]) == izbraniObj.Id)
                    {
                        string date = line[6];//uzme vreme
                        int id = Int32.Parse(line[1]);
                        if (DateTime.Compare(Convert.ToDateTime(line[5]), DateTime.Today) < 0)
                        {
                            continue;
                        }
                        double value = -1;

                        if (Double.TryParse(line[3], out double ouut))
                        {
                            value = Double.Parse(line[3]);//uzme value
                        }

                        if (PrikazaniRectangles[izbraniObj].Count == 10)
                        {
                            break;
                        }
                        else
                        {
                            PrikazaniRectangles[izbraniObj].Add(Convert.ToDateTime(date), value);
                        }
                    }
                }

                
                int idx = 0;
                foreach (var item in PrikazaniRectangles[izbraniObj])
                {
                    Textboxes[idx] = item.Key.ToString();
                    OnPropertyChanged("Textboxes");
                    Rectangles[idx] = (item.Value /4);
                    OnPropertyChanged("Rectangles");
                    idx++;

                }


                if (PrikazaniRectangles[izbraniObj].Count == 10)
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        pomr[i] = Rectangles[9 - i];
                        pomt[i] = Textboxes[9 - i];
                    }
                }
                else
                {

                    for (int i = 0; i < PrikazaniRectangles[izbraniObj].Count; i++)
                    {
                        pomr[i] = Rectangles[PrikazaniRectangles[izbraniObj].Count - 1 - i];
                        pomt[i] = Textboxes[PrikazaniRectangles[izbraniObj].Count - 1 - i];
                    }

                    for (int i = PrikazaniRectangles[izbraniObj].Count; i < 10; i++)
                    {
                        pomr[i] = 0;
                        pomt[i] = "";
                    }

                }
                Rectangles = pomr;
                Textboxes = pomt;
            }

        }

    }


}
