using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace vendingMachine
{
    [Serializable()]
    class Coins : ISerializable
    {
        private decimal fivep;
        private decimal tenp;
        private decimal twntyp;
        private decimal fiftyp;
        private decimal onepound;

        private List<Coins> Coinsadded = new List<Coins>();
        public List<Coins> coinstoadd
        {
            get{ return Coinsadded; }
            set { Coinsadded = value; }

        }



        public decimal Fivep
        {
            get { return fivep; }
            set { fivep = value; }
        }
        public decimal Tenp
        {
            get { return tenp;  }
            set { tenp = value; }
        }

        public decimal Twntyp
        {
            get { return twntyp; }
            set { twntyp = value; }

        }

         public decimal Fiftyp
        {
            get { return fiftyp; }
            set { fiftyp = value; }
        }

         public decimal Onepound
        {
            get { return onepound; }
            set { onepound = value; }
        }

        public Coins()
        { }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("fivepence", Fivep);
            info.AddValue("tenpence", Tenp);
            info.AddValue("tewentypence", Twntyp);
            info.AddValue("fiftypence",Fiftyp);
            info.AddValue("Onepund", Onepound);
        }
        public Coins(SerializationInfo info, StreamingContext context)
        {
            Fivep = (decimal)info.GetValue("fivepence", typeof(decimal));
            Tenp = (decimal)info.GetValue("tenpence", typeof(decimal));
            Twntyp = (decimal)info.GetValue("tewentypence", typeof(decimal));
            Fiftyp = (decimal)info.GetValue("fiftypence", typeof(decimal));
            Onepound = (decimal)info.GetValue("Onepund", typeof(decimal));
          //  throw new NotImplementedException();
        }

    }
}
