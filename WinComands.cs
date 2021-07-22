using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetoVader
{
    class WinCommands
    {
        private List<String> exec;
        private List<String> dirFile;

        public WinCommands()
        {

        }
        public List<string> SetExecs
        {
            set { exec = value; }
        }
        public List<string> SetDirFiles
        {
            set { dirFile = value; }
        }
        public void ExecutarAbrir(string progs)
        {
            switch (progs)
            {
                case "calculadora":
                    System.Diagnostics.Process.Start("calc");
                    break;
                case "notas":
                    System.Diagnostics.Process.Start("notepad");
                    break;
                case "notepad":
                    System.Diagnostics.Process.Start("notepad");
                    break;
                case "documentos":
                    System.Diagnostics.Process.Start("Explorer", @"C:\Users\"+System.Security.Principal.WindowsIdentity.GetCurrent().Name+@"\Documents");
                    break;
                case "computador":
                    System.Diagnostics.Process.Start("Explorer");
                    break;
                case "paint":
                    System.Diagnostics.Process.Start("mspaint");
                    break;
                default:
                   //tratar aqui programa x executaveis 
                    
                    int i = exec.IndexOf(progs);
                      try
                      {
                          System.Diagnostics.Process.Start("Explorer", dirFile[i]);
                      }
                      catch(Exception e)
                      { 
                      ///
                      }
                    break;

            }


        }
    }
}
