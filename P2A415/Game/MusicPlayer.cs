using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;


namespace RPGame {
    public class MusicPlayer {

        List<Tuple<string, int>> WhereWavFilesAre = new List<Tuple<string, int>>();
        private bool isPlaying = false;
        private static Random rand = new Random();
        public bool isMute = false;
        Thread _soundThread;
        System.Media.SoundPlayer player;
        
        public MusicPlayer() {
            string WhereIsLaid = "Content/";

            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "01ANightOfDizzySpells.wav", giveSec(1) + 54));
            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "02Underclocked(underunderclockedmix).wav", (giveSec(3) + 8)));
            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "03ChibiNinja.wav", (giveSec(2) + 3)));
            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "04AllofUs.wav", (giveSec(1) + 57)));
            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "05ComeandFindMe.wav", (giveSec(3) + 19)));
            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "06Searching.wav", (giveSec(2) + 20)));
            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "07We'retheResistors.wav", (giveSec(2) + 21)));
            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "08Ascending.wav", (giveSec(3) + 12)));
            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "09ComeandFindMe-Bmix.wav", (giveSec(3) + 29)));
            WhereWavFilesAre.Add(new Tuple<string, int>(WhereIsLaid + "10Arpanauts.wav", (giveSec(3) + 16)));

        }

        public void toggleMute() {
            isMute = !isMute;
            isPlaying = false;
            
            player?.Stop();

        }
        private int giveSec(int min) {
            return min * 60;
        }


        public void update() {
            Console.WriteLine(isMute.ToString() + " vovovovovo " + isPlaying.ToString());
            if (isMute || isPlaying) {
                
                return;
            }
                

            _soundThread = new Thread(playMusic);
            _soundThread.IsBackground = true;
            _soundThread.Start();
            
        }

        public void playMusic() {
            
            isPlaying = true;

            int soundIndex = rand.Next(0, WhereWavFilesAre.Count);
            
            using (player = new System.Media.SoundPlayer(WhereWavFilesAre[soundIndex].Item1)) {
                player.Play();
                Thread.Sleep(WhereWavFilesAre[soundIndex].Item2 * 1000);
            }
            isPlaying = false;
        }


    }
}
