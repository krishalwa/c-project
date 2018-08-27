using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace delegate_sample
{
   static  class Class1
    {
       // public List<FileInfo> filesfor = new List<FileInfo>();
        public static void displayfilename(FileInfo file)
        {
            Console.WriteLine(file.Name);
            //throw new System.NotImplementedException();
        }

        public static void displayfilenameandsize(FileInfo  file)
        {
            Console.WriteLine(file.Name + " length" + file.Length);
           // throw new System.NotImplementedException();
        }

        public static void displayextension(FileInfo file)
        {

            Console.WriteLine(file.Extension);
                //file.CopyTo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\copiesfiles");

         // throw new System.NotImplementedException();
        }
    }
}
