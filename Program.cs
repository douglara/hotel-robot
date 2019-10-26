using ai.humanrobotics.api.csharp;
using ai.humanrobotics.messaging.Types;
using System;

namespace ai.humanrobotics.examples
{
    class Program
    {
        public const string API_KEY = "9106b136-1424-4b2a-be0a-6e313dc2a80d";
        public const string ROBOT_ADDRESS = "192.168.100.38";
        public static IRobios robios = RobiosApi.Get(API_KEY, ROBOT_ADDRESS);

        static void Main(string[] args)
        {
            // Run dialog
            Welcome(robios);
            //robios.Close();
        }

        static void Welcome(IRobios robios)
        {
            robios.Say("Olá eu sou o Robios e estou aqui para te ajudar!");
            CheckinOrBook(robios); 
        }

        static void CheckinOrBook(IRobios robios)
        {
            robios.OnUserTextReceived += RecognizedWelcome;
            robios.Say("Você gostaria de fazer seu check-in ou fazer uma reserva?");
            robios.Delay(3000);
            robios.Ask("");
        }
        static void CheckIn()
        {
            robios.OnUserTextReceived -= RecognizedWelcome;
            robios.OnUserTextReceived += RecognizedText;
            robios.Say("Para fazer seu check-in por favor diga seu nome completo começando com meu nome é");
            robios.Delay(3000);
            robios.Ask("");
        }

        static void NotUnderstanding()
        {
            robios.Say("Desculpe não entendi, vamos começar de novo!");
            CheckinOrBook(robios);
        }


        static void RecognizedText(string fala)
        {
            Console.WriteLine(fala);
        }
        static void RecognizedWelcome(string fala)
        {
            Console.WriteLine(fala);
            if (fala.Contains("check-in"))
            {
                CheckIn();
            }
            else if(fala.Contains("reserva"))
            {
                robios.Say("Vamos agilizar essa reserva");
            }
            else
            {
                NotUnderstanding();
            }
        }
    }
}
