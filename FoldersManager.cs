using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace ProjetoVader
{
    class FoldersManager
    {
        //para inits e doc config.
        private List<String> folderDirAtalhos;
        private List<String> exec;
        private List<String> dirFile;
        private List<String> allWords;
        private String pcName;
        private String pathRules;
        private static String fileRules = "GrammarRules.xml";
        private String userName;

        public FoldersManager()
        {

            userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            userName = userName.Remove(0, userName.IndexOf("\\") + 1); 
            
            pcName = "";
            //pathRules = @"C:\Users\"+userName+@"\AppData\Local\Vader\";
            pathRules = @"GrammarRules.xml";
            
            //Depois Pegará todos caminhos de atalhos que estiverem no config.txt
            folderDirAtalhos = new List<string>();
           
            //allword found
            allWords = new List<string>();
            getDicionario();
            //Buscar parametros no config.txt
            getConfig();
            //dir file init e execs
            dirFile = new List<string>();
            exec = new List<string>();
            SetExect(".lnk");
            
            AlteraPcNameXmlRules(pcName);
            IncluirAtalhos(exec);
           
        }
        private void AlteraPcNameXmlRules(String _pcName)
        {   
            XmlDocument doc = new XmlDocument();
            doc.Load(pathRules);

            XmlNode commentsElement = doc.SelectSingleNode("//*[@id='PcName']");
 
            commentsElement.LastChild.InnerText = _pcName;
           //doc.Save(Console.Out);
            doc.Save(pathRules);
                                                                        
        }
       
        private void IncluirAtalhos(List<string> _atalhosName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(pathRules);
            XmlNode commentsElement = doc.SelectSingleNode("//*[@id='Programs']");

            List<string> atalhos = new List<string>(_atalhosName);

            commentsElement = commentsElement.ChildNodes.Item(1).ChildNodes.Item(0);
            //Verifica se o atalho ja consta no xml e se já consta remove ele da lista q vai ser salva no xml posteriormente 
            for (int i = 0; i < commentsElement.ChildNodes.Count; i++)
            {
                
                int j = atalhos.IndexOf(commentsElement.ChildNodes.Item(i).InnerText);
                if (j >= 0 )
                {
                    atalhos.RemoveAt(j);
                }
            }

            XmlNode xmlNode;
            foreach (string nomeAtalhos in atalhos)
            {
                xmlNode = commentsElement.ChildNodes.Item(0).Clone();
                xmlNode.InnerText = nomeAtalhos;
                commentsElement.AppendChild(xmlNode);
            }
                        
           // doc.Save(Console.Out);
           doc.Save(pathRules);

        }
        public void SetExect(string extencao)
        {
                List<String> dirFileTemp = new List<string>();
                foreach (string dirAtalhos in folderDirAtalhos)
                {
                    dirFileTemp = GetFiles(dirAtalhos, "*" + extencao);

                    foreach (string fileName in dirFileTemp)
                    {
                        //  fileName = File.Replace(fileName, "").Remove(0, 1);
                        string name = fileName.Remove(0, fileName.IndexOf("\\") + 1);
                        int i = name.IndexOf("\\");
                        while (name.IndexOf("\\") > 0)
                        {
                            name = name.Remove(0, name.IndexOf("\\") + 1);
                        }

                        name = name.Replace(extencao, "");
                        name = name.Replace("Microsoft", "");

                        //separa numero da string....melhorar depois para remover os numeros não impota ordem
                        /*Regex regexObj = new Regex(@"[^\d]");   
                        string resultString = regexObj.Replace(name, "");
                        
                        if(resultString != "")
                        name = name.Replace(resultString, "");

                        */
                        name = name.Trim();
                        name = name.Replace(" ", "_");
                        if (exec.IndexOf(name) < 0 )
                        {                          
                            exec.Add(name);
                            dirFile.Add(fileName);
                        }
                    }
                }

        }
        public string PcName
        {
            get { return pcName; }
        }
        public string PathRules
        {
            get { return pathRules; }
        }
        public List<string> GetAllWords
        { 
        get { return allWords;}
        }
        public List<string> GetExec
        {
            get { return exec; }
        }
        public List<string> GetDirFiles
        {
            get { return dirFile; }
        }
        private void getConfig()
        {
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"config.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                              
                counter++;

                int iniciovalue = line.IndexOf("=");
                iniciovalue = iniciovalue + 1;
                switch(counter)
                { 
                    case 1:
                        pcName = line.Remove(0, iniciovalue);
                        break;
                    default:
                        folderDirAtalhos.Add((line.Remove(0, iniciovalue)).Replace("[user]",userName));
                        // replace [user] pelo usuario logado e remove o valor inicial 
                        break;

                }
            }

            file.Close();
           
        }
        private void getDicionario()
        {
            int counter = 0;
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"palavras.txt");
            while ((line = file.ReadLine()) != null)
            {
               // System.Console.WriteLine(line);

                allWords.Add(line.ToLower());
                counter++;
            }

            file.Close();

        }
        private void GetProgs()
        {

             /*
             DirectoryInfo Dir = new DirectoryInfo(folderDir);
            // Busca automaticamente todos os arquivos em todos os subdiretórios
            try
            {
                FileInfo[] Files = Dir.GetFiles("*.exe", SearchOption.AllDirectories);
                  foreach (FileInfo File in Files)
                    {
                        dirFile.Add(File.FullName);

                        // Retira o diretório iformado inicialmente
                        string FileName = File.FullName.Replace(Dir.FullName, "").Remove(0, 1);
                        FileName = FileName.Remove(FileName.ToUpper().IndexOf(".EXE"), 0);
                        exec.Add(FileName);


                    }
                }
            catch (UnauthorizedAccessException) { }
            //finally { }*/
        }
        private List<string> GetFiles(string path, string pattern)
         {
             var files = new List<string>();

             try
             {   
                 files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                 foreach (var directory in Directory.GetDirectories(path))
                     files.AddRange(GetFiles(directory, pattern));
             }
             catch (UnauthorizedAccessException) { }
             catch (PathTooLongException) { }

             return files;
         }
                        
    }
}
