using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Speech.Synthesis;
namespace SpeechToText
{
    public partial class Form1 : Form
    {
        //set the recognizer and grammar
        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
        Grammar grammer = new DictationGrammar();
        Boolean flag = false;
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {   
            //set the recognizer and grammar
            recognizer.SetInputToDefaultAudioDevice();
            recognizer.LoadGrammar(grammer);
            //listen to the speech while flag is on
            while(flag)
            {
                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }  
        }

        private void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            System.Console.WriteLine(e.Result.Text);
            richTextBox1.Text += e.Result.Text + "\r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //sets the flag to true or false and changes the button text
            if(flag==false)
            {
                flag = true;
                button1.Text = "Stop listening";
                recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
            else
            {
                flag = false;
                button1.Text = "Start listening";
                recognizer.RecognizeAsyncStop();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //speak the text in the richtextbox
            var synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.SpeakAsync(richTextBox1.Text);

        }
    }
}
