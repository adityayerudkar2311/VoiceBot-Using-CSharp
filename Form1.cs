using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Runtime.Remoting.Messaging;

namespace Voice_Bot
{
    public partial class Form1 : Form
    {

        SpeechSynthesizer S = new SpeechSynthesizer();
        Choices list = new Choices();
        Boolean wake = true;

        public Form1()
        {
            SpeechRecognitionEngine rec = new SpeechRecognitionEngine();

            list.Add(new string[] { "hello", "how are you", "what time is it", "what is today", "open google", "wake", "sleep" });

            Grammar gr=new Grammar(new GrammarBuilder(list));

            try
            {
                rec.RequestRecognizerUpdate();
                rec.LoadGrammar(gr);
                rec.SpeechRecognized += rec_SpeachRecognized;
                rec.SetInputToDefaultAudioDevice();
                rec.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch {return; }

            S.SelectVoiceByHints(VoiceGender.Female);
            S.Speak("hello , my name is Voice Bot");
            InitializeComponent();
        }
        public void Say(String h)
        {
            S.Speak(h);
        }

        private void rec_SpeachRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            String r = e.Result.Text;

            if (r == "wake") wake = true;
            if (r == "sleep") wake = false;

            if (wake == true)
            {



                if (r == "hello")
                {
                    Say("hi");
                }
                if (r == "what time is it")
                {
                    Say(DateTime.Now.ToString("h:mm tt"));
                }
                if (r == "what is today")
                {
                    Say(DateTime.Now.ToString("M/d/yyyy"));
                }

                if (r == "how are You")
                {
                    Say("great, and you");
                }
                if (r == "open chrome")
                {
                    Process.Start("https://www.google.com");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
