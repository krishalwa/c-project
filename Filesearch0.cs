using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
namespace delegate_sample
{
    public delegate void filehandling(FileInfo file);
    class Filesearch0
    {
        
        private string _FileSpec;
        private string _Lookin;
        private Boolean _SearchsubFolder;
        public filehandling handler = null;
        protected List<FileInfo> files = new List<FileInfo>();

        public Filesearch0(string filespec,string lookin, Boolean searchfolder)
        {
            this._FileSpec = filespec;
            this._Lookin = lookin;
            this._SearchsubFolder = searchfolder;
        
            


            //throw new System.NotImplementedException();
        }

        public List<FileInfo> Execute()
        {
            this.Search(this._Lookin);
            return files;
           // throw new System.NotImplementedException();
        }

        public void Search(string path)
        {
            DirectoryInfo localdirectory = new DirectoryInfo(path);

            try
            {
                if (this._SearchsubFolder)
                {
                    foreach (DirectoryInfo x in localdirectory.GetDirectories())
                    {

                        FileInfo[] nn = x.GetFiles();
                        foreach (FileInfo mm in nn)
                        {
                            handler.Invoke(mm);
                           
                            //Console.WriteLine("file :- [{0}] DIRECTORY : [{1}]", mm.Name,x.Name);
                        }
                        Search(x.FullName);
                       // Console.WriteLine(x.FullName);
                        Console.WriteLine("============================");
                    }

                    //
                    FileInfo[] file = localdirectory.GetFiles();
                    /// Console.WriteLine("=====================");
                    //  foreach(FileInfo xab in file)
                    //  {
                    //
                    //      Console.WriteLine(xab.FullName);
                    //   }
                    //   Console.WriteLine("=====================");///

                    files.AddRange(file);
                }




            }
            catch (UnauthorizedAccessException ex)
            {

                Console.WriteLine("file cannot be accesssed" + ex.ToString());
                Console.ReadKey();
            }
        }
    }
}
