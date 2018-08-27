using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace delegate_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Filesearch0 x = new Filesearch0("*",@"C:\Users\thayalini\Desktop\Delegatetesting", true);
           // Filesearch0 x = new Filesearch0("*",, true);


        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Delegatetesting";
        Filesearch0 x = new Filesearch0("*",path, true);


            // Filesearch0 x = new Filesearch0("*",Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+ @"\Delegatetesting", true);
            // Class1 yyy = new Class1();

            // yyy.displayextension handler1 = new yyy.displayextension();

            x.handler = new filehandling(Class1.displayextension);
            x.Execute();
            x.handler = Class1.displayfilename;
            x.Execute();
            Console.ReadKey();
        }
    }
}
