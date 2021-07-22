using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;


///tuttoriais e exemplos
///Exemplo de como reconhecer numeros 
///https://msdn.microsoft.com/en-us/library/hh378386(v=office.14).aspx
///https://msdn.microsoft.com/en-us/library/hh378420(v=office.14).aspx
///Para xml rules 
///https://msdn.microsoft.com/en-us/library/ms873284.aspx
///ler xml
///http://stackoverflow.com/questions/26824756/speech-recognition-result-semantic-containskey-is-always-false-when-reading-gra
///xml exemplo
///https://msdn.microsoft.com/en-us/library/hh378505(v=office.14).aspx

namespace ProjetoVader
{
    class Vader
    {
        private static Speecher speech;
        //variaveis de apoio
        private static string pcName;
        private static string pathRules;
        private static List<string> execs;
        // controle de pastas 
        private static FoldersManager folderManager;
        //Manipula acoes key
        private static Actionkey actionKey;
        //Classe q executa comandos do windowns 
        private static WinCommands winC;
        //contructor
        public Vader()
        {
            
            winC = new WinCommands();
            //inicializa key class
            actionKey = new Actionkey();
            //inicializa foldermanager
            folderManager = new FoldersManager();
            //seta pc name
            pcName = folderManager.PcName;
            winC.SetExecs = folderManager.GetExec;
            winC.SetDirFiles = folderManager.GetDirFiles;
            pathRules = "";
            pathRules = folderManager.PathRules;
            //Inicializa Voz de retorno para conversas 
            speech = new Speecher();
            //inicia vader listening
            IniciaListening();
                       
            
        }
        //inicia a escuta */
        static void IniciaListening()
        {
           
            using (var sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("pt-BR")))
            {
                sre.SetInputToDefaultAudioDevice();
                  // @"GrammarRules.xml"
                var g = new Grammar(pathRules);
                
                sre.LoadGrammarAsync(g);
                

                sre.RecognizeCompleted += sre_RecognizeCompleted;
                sre.SpeechRecognitionRejected += sre_SpeechRecognitionRejected;
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.LoadGrammarCompleted +=sre_LoadGrammarCompleted;  
 
                sre.RecognizeAsync(RecognizeMode.Multiple);


               

                Process[] processlist = Process.GetProcesses();

                foreach (Process process in processlist)
                {
                    if (!String.IsNullOrEmpty(process.MainWindowTitle))
                    {
                        Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                    }
                }
                
                foreach (KeyValuePair<IntPtr, string> window in OpenWindowGetter.GetOpenWindows())
                {
                    IntPtr handle = window.Key;
                    string title = window.Value;

                    Console.WriteLine("{0}: {1}", handle, title);
                }
                Console.ReadLine();

            }
        
        }
        static void sre_LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {
            Console.WriteLine("carregado...");
            speech.Sintetizar("carregado...");
            
        }
 
        static void sre_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine("Ignorado");
        }

        static void sre_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Result == null)
                return;

            Console.WriteLine("Reconhecido: " + e.Result.Text);
        }
        // Create a simple handler for the SpeechRecognized event.
        static void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("Ouvi");

            if (e.Result == null)
                return;

           
            if (e.Result.Words[0].Confidence >= 0.5 && e.Result.Confidence >= 0.5)
            {

                Console.WriteLine("Reconhecido: " + e.Result.Text + "      Chance:" + e.Result.Confidence.ToString() + "  Nome: " + e.Result.Words[0].Confidence.ToString());
                string lastWord = e.Result.Words[e.Result.Words.Count - 1].Text;
                 string action = "";
                
                if(e.Result.Words.Count > 1)
                action = e.Result.Words[1].Text.ToLower();

                speech.Sintetizar("ou quei");

                if (action != "")
                {
                    switch (e.Result.Semantics["comando"].Value.ToString())
                    {
                        case "Teclado":
                            actionKey.Executa(action, lastWord);
                            break;
                        case "Buttons":
                            actionKey.Action(lastWord);
                            break;
                        case "Windowns":
                            winC.ExecutarAbrir(lastWord);
                            break;
                        default:
                            winC.ExecutarAbrir(lastWord);
                            break;

                    }
                }
             
            }
            else
            {
                Console.WriteLine("provavel que fosse : " + e.Result.Text + "      Chance:" + e.Result.Confidence.ToString() + "  Nome: " + e.Result.Words[0].Confidence.ToString() + " ,mas não era....");
                //speech.Sintetizar("pode repetir? por favor ! ");
            }
        }
    }
}
