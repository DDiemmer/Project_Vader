using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Speech.Synthesis;
using System.Threading;

namespace ProjetoVader
{
    public class Speecher
    {
        
        protected  SpeechSynthesizer Speaker { get; set; }

        public Speecher()
        {
            Speaker = new SpeechSynthesizer();

            var z = "";
            foreach (var item in IdiomasDisponiveis().Where(item => item.Contains("pt-BR")))
            {
                z = item;
                return;
            }
            Speaker.SelectVoice(z);
        }


        private IEnumerable<string> IdiomasDisponiveis()
        {
            return Speaker.GetInstalledVoices().Select(voice => voice.VoiceInfo.Culture.ToString()).ToList();
        }
       
        public void Sintetizar(string Texto)
        {
            Speaker.Speak(Texto);
        }

        public void Pausar()
        {
            Speaker.Pause();
        }

        public void Continuar()
        {
            Speaker.Resume();
        }
    }
}