using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyValWpf
{
    public class Values
    {
        string firstValue;
        string secondValue;
        int id;

       


        public Values()
        { 
        
        }

        public Values(string val1,string val2)
        {
            FirstValue = val1;
            SecondValue = val2;
        }

        public Values(string val1, string val2,int id)
        {
            FirstValue = val1;
            SecondValue = val2;
            Id = id;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public String SecondValue
        {
            get { return secondValue; }
            set { secondValue = value; }
        }

        public String FirstValue
        {
            get { return firstValue; }
            set { firstValue = value; }
        }
    }
}
