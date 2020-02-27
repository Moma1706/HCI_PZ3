using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PZ3_NetworkService.Model
{
    public class TipoviPumpi : BindableBase
    {
        public string nameTip;
        public string imgSrc;

        public string NameTip
        {
            get { return nameTip; }
            set
            {
                nameTip = value;
            }
        }

        public string ImgSrc
        {
            get { return imgSrc; }
            set
            {
                imgSrc = value;
            }
        }

        public override string ToString()
        {
            return NameTip;
        }
    }
}
