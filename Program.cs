using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vendingMachine
{
    static class Program
    {
        internal static Coins Coins
        {
            get => default(Coins);
            set
            {
            }
        }

        public static Employee Employee
        {
            get => default(Employee);
            set
            {
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // following line handle unhandled exceptions
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(HandleErrors);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form());
        }

        
        //method for unhandled exceptions

        private static void HandleErrors(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Error" + e.Exception.Message);
            //throw new NotImplementedException();
        }
    }
   
}
