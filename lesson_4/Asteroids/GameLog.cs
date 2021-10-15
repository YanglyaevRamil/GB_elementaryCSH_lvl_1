using System;
using System.Collections.Generic;
using System.IO;

namespace Asteroids
{
    static class GameLog
    {
        public static List<Message> LogGame = new List<Message>();
        public static void GetLogConsoleGame()
        {

            foreach (Message m in LogGame)
            {
                Console.WriteLine($"{m.Info}, Energy Ship = {m.EnergyShip}, Score = {m.Score}");
            }

            Console.ReadLine();
        }

        public static void SetLogGame(string info, int en, int sc)
        {
            LogGame.Add(new Message(info, en, sc));
        }

        public static void WriteLogFile(string dir)
        {
            File.WriteAllText(dir, String.Empty);
            FileStream fs = new FileStream(dir, FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (Message m in LogGame)
                {
                    sw.WriteLine($"{m.Info}, Energy Ship = {m.EnergyShip}, Score = {m.Score}");
                }
            }
        }
    }

    public class Message
    {
        public string Info { set; get; }
        public int EnergyShip { set; get; }
        public int Score { set; get; }

        public Message(string Info, int EnergyShip, int Score)
        {
            this.Info = Info;
            this.EnergyShip = EnergyShip;
            this.Score = Score;
        }
    }
}

