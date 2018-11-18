


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Collections;

namespace vendingMachine

{
    [Serializable()]    //Set this attribute to all the classes that want to serialize
    public class Employee :IComparable,IComparer //derive your class from ISerializable
    {
        private double p_price;
       

        private string p_name = "";
        private int p_qnty;
        private int arraynumber;

       
        //public comparefield comparedon = comparefield.qnty;

       

        //Default constructor
        public Employee(string pnname)
        {
            this.p_name = pnname;
        }

        public Employee()
        {
            this.p_name ="  ";
        }

        public int Arraynumber
        {
            get { return arraynumber; }
            set { arraynumber = value; }


        }


        public Employee(string name,double price,int qnty)
        {
            this.p_name = name;
            this.p_price = price;
            this.p_qnty = qnty;
            



        }

        public string P_name
        {
            get { return p_name; }
            set { p_name= value; }

        }

        public double P_price
        {
            get { return p_price; }
            set { p_price = value; }
        }

        public int P_qnty
        {
            get { return p_qnty; }
            set { p_qnty = value; }
        }



        //Deserialization constructor.
        public Employee(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
          //  EmpId = (int)info.GetValue("EmployeeId", typeof(int));
           // EmpName = (String)info.GetValue("EmployeeName", typeof(string));
          //  EmpStatus = (Boolean)info.GetValue("EmployeeStatus", typeof(Boolean));
        }

        

        public int Compare(object x, object y)
        {
            

                Employee xxx = (Employee)x;
                Employee yyyy = (Employee)y;
                return xxx.p_qnty.CompareTo(yyyy.p_qnty);
                //throw new NotImplementedException();
            


            //throw new NotImplementedException();
        }
       // public float p_name { get; set; }

        

        public int CompareTo(object obj)
        {
            if (obj is Employee)
            {
                Employee temp = (Employee)obj;

                return this.p_name.CompareTo(temp.p_name);
            }
            throw new ArgumentException("object is not a Department");
        }

        //Serialization function.
        // public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        // {
        //You can use any custom name for your name-value pair. But make sure you
        // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
        // then you should read the same with "EmployeeId"
        // info.AddValue("EmployeeId", EmpId);
        //  info.AddValue("EmployeeName", EmpName);
        //  info.AddValue("EmployeeStatus", EmpStatus);
        //  }


        // method for checking available stock 


    }
}
