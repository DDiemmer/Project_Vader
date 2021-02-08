using WindowsInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace ProjetoVader
{
    class Actionkey
    {
        //string de ação
        
     
        public void Executa(string action,string oq)
        {
            //possiveis casos 
            // "fechar"  "minimizar", "maximizar", "pesquisar" "bloquear", "trocar"    "voltar"   "escolher" selecionar 
            // guia/tela    tela/janela/tudo                   computador   guia/tela   guia/tela    tela
            if (action.ToLower() != "escolher" && action.ToLower() != "selecionar" && action.ToLower() != "voltar" && action.ToLower() != "avançar")
            {
                if (InputSimulator.IsKeyDown(VirtualKeyCode.LMENU))
                {
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.LMENU);
                }
            }
            switch (action.ToLower())
            { 
                case "fechar":
                    if (oq.ToLower() == "guia")
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.F4);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                    }
                    else
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LMENU);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.F4);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LMENU);
                    }

                    break;
                case "trocar":
                    if (oq.ToLower() == "guia")
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                    }
                    else
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LSHIFT);
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LMENU);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LMENU);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LSHIFT);
                    }

                    break;
                case "voltar":
                    if ((oq.ToLower() == "tela"  || oq.ToLower() == "janela")  && InputSimulator.IsKeyDown(VirtualKeyCode.LMENU))
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LSHIFT);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LSHIFT);
                    }
                    else if (oq.ToLower() == "guia")
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LSHIFT);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LSHIFT);
                    }
                    else
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LMENU);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LMENU);
                    }

                    break;
                case "escolher":
                    if ((oq.ToLower() == "tela" || oq.ToLower() == "janela") && !InputSimulator.IsKeyDown(VirtualKeyCode.LMENU))
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LMENU);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                    }
                    else if(InputSimulator.IsKeyDown(VirtualKeyCode.LMENU))
                    {
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LMENU);
                    }
                    break;
                case "selecionar":
                    if ((oq.ToLower() == "tela" || oq.ToLower() == "janela") && !InputSimulator.IsKeyDown(VirtualKeyCode.LMENU))
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LMENU);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                    }
                    else if ((oq.ToLower() == "tudo") && !InputSimulator.IsKeyDown(VirtualKeyCode.CONTROL))
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_A);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                    }
                    else if (InputSimulator.IsKeyDown(VirtualKeyCode.LMENU))
                    {
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LMENU);
                        
                    }
                    break;
                case "avançar":
                    if ((oq.ToLower() == "tela" || oq.ToLower() == "janela") && InputSimulator.IsKeyDown(VirtualKeyCode.LMENU))
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                    //InputSimulator.SimulateKeyUp(VirtualKeyCode.LMENU);
                    else if (oq.ToLower() == "guia")
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                    }
                    break;
                case "maximizar":
                    if (oq.ToLower() == "tudo")
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LWIN);
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LSHIFT);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_M);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LSHIFT);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LWIN);
                    }
                    else 
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LWIN);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.UP);
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LWIN);
                    }
                    break;
                case "minimizar":
                    if (oq.ToLower() == "tudo")
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LWIN);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_M);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.LWIN);
                    }
                    else
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LWIN);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.DOWN);
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.LWIN);
                    }
                    break;
                case "bloquear":
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.LMENU);
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.DELETE);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.LMENU);
                    break;
                case "escrever":

                  // InputSimulator.SimulateKeyPress(VirtualKeyCode.LWIN);
                   // InputSimulator.SimulateTextEntry(oq);
                    
                    InputSimulator.SimulateTextEntry(oq);

                    
                    break;
                case "pesquisar":

                     InputSimulator.SimulateKeyPress(VirtualKeyCode.LWIN);
                    // InputSimulator.SimulateTextEntry(oq);

                    //InputSimulator.SimulateTextEntry(oq);

                    break;
                      case "criar_nova":
                    if (oq.ToLower() == "guia")
                    {
                        InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                        InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_T);
                        InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                    }
                        break;
                    default:

                    break;
            
            }
            
        
    
        }
        public void Action(string action)
        {
             /* Esque Enter  Rome  Endi  Peige daum  Peige ãpi  Tabi  printi iscrim  colar  copiar  recortar */
            switch (action.ToLower())
            {
                case "esque":
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.ESCAPE);
                break;
                case "enter":
                InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
                break;
                case "rome":
                InputSimulator.SimulateKeyPress(VirtualKeyCode.HOME);
                break;
                case "endi":
                InputSimulator.SimulateKeyPress(VirtualKeyCode.END);
                break;
                case "peige_daum":
                InputSimulator.SimulateKeyPress(VirtualKeyCode.NEXT);
                break;
                case "peige_ãpi":
                InputSimulator.SimulateKeyPress(VirtualKeyCode.PRIOR);
                break;
                case "tabi":
                InputSimulator.SimulateKeyPress(VirtualKeyCode.TAB);
                break;
                case "printi_iscrim":
                InputSimulator.SimulateKeyPress(VirtualKeyCode.SNAPSHOT);
                break;
                case "colar":
                InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_V);
                InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                break;
                case "copiar":
                InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_C);
                InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                break;
                case "recortar":
                InputSimulator.SimulateKeyDown(VirtualKeyCode.CONTROL);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.VK_X);
                InputSimulator.SimulateKeyUp(VirtualKeyCode.CONTROL);
                break;
                default:
                    break;
            }
        
        }
    }
}
