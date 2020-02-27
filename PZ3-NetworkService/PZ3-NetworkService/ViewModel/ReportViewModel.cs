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
    public class ReportViewModel : BindableBase
    {
        public static ObservableCollection<Pumpa> Pumpe { get; set; }
        public MyICommand Show { get; set; }
        private string report;
        public string provera;
        public string danas;

        public ReportViewModel()
        {
            Pumpe = MainViewModel.Pumpe;

            Show = new MyICommand(S);

        }

        public string Report
        {
            get { return report; }
            set
            {
                report = value;
                OnPropertyChanged("Report");
            }
        }

        public void S()
        {
            Report = "";
            foreach (Pumpa r in Pumpe)
            {
                Report += "Id " + r.id + ":" + "\n";
                using (StreamReader streamReader = new StreamReader("Log.txt"))
                {
                    string s;
                    while ((s = streamReader.ReadLine()) != null)
                    {
                        string[] line = s.Split(' ', '\t');
                        if (Int32.Parse(line[1]) == r.Id)
                        {

                            provera = line[5];
                            danas = DateTime.Now.ToString("M/d/yyyy");
                            if (provera == danas)
                            {
                                Report += "\t" + line[5] + " " + line[6] + " " + line[7] + ", CHANGED STATE: " + line[3] + "\n";
                                
                            }
                        }
                    }
                }
            }
        }
    }
}
