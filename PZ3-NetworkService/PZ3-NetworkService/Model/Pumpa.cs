using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PZ3_NetworkService.ViewModel;

namespace PZ3_NetworkService.Model
{
    public class Pumpa : ValidationBase
    {
        public int id;
        public string name;
        public double value;
        public TipoviPumpi type;

        public Pumpa(int ID, string NAME, TipoviPumpi TYPE, double VALUE)
        {
            id = ID;
            name = NAME;
            type = TYPE;
            value = VALUE;
        }

        public Pumpa()
        {

        }

        public int Id
        {
            get { return id; }
            set
            {
                if(id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public double Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged("Value");
                }
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public TipoviPumpi Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }


        public static List<double> proveraIDa = new List<double>();


        protected override void ValidateSelf()
        {

            if (this.id == -1)
            {
                this.ValidationErrors["Id"] = "Id is required!";//samo prvi put kada se pokrene ce upasti ovde, jer je samo onda null posle upada u combination of numbers
            }

            if (this.id <= 0)
            {
                this.ValidationErrors["Id"] = "Id must be greater than 0!";
            }

            if (this.id == -2)
            {
                this.ValidationErrors["Id"] = "Id must be combination of numbers!";
            }

            proveraIDa = MainViewModel.proveraIDa;

            if (proveraIDa.Contains(this.id))
            {
                this.ValidationErrors["Id"] = "Id is busy!";
            }

            if (string.IsNullOrWhiteSpace(this.name))
            {
                this.ValidationErrors["Name"] = "Name is required!";
            }

            if (this.type == null)
            {
                this.ValidationErrors["Type"] = "Type is required.";
            }

        }


    }
}
