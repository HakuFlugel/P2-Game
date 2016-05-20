using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;
using System.Media;


namespace RPGame {
    public class MusicPlayer {

        List<Tuple<string, int>> musicList = new List<Tuple<string, int>>();
        private bool isPlaying = false;
        public bool isMuted = false;


        private static Random rand = new Random();
        Thread _soundThread;
        SoundPlayer soundplayer = new SoundPlayer();
        
        public MusicPlayer() {
            const string folder = "Content/";

            musicList.Add(new Tuple<string, int>(folder + "01ANightOfDizzySpells.wav", toSec(1) + 54));
            musicList.Add(new Tuple<string, int>(folder + "02Underclocked(underunderclockedmix).wav", (toSec(3) + 8)));
            musicList.Add(new Tuple<string, int>(folder + "03ChibiNinja.wav", (toSec(2) + 3)));
            musicList.Add(new Tuple<string, int>(folder + "04AllofUs.wav", (toSec(1) + 57)));
            musicList.Add(new Tuple<string, int>(folder + "05ComeandFindMe.wav", (toSec(3) + 19)));
            musicList.Add(new Tuple<string, int>(folder + "06Searching.wav", (toSec(2) + 20)));
            musicList.Add(new Tuple<string, int>(folder + "07We'retheResistors.wav", (toSec(2) + 21)));
            musicList.Add(new Tuple<string, int>(folder + "08Ascending.wav", (toSec(3) + 12)));
            musicList.Add(new Tuple<string, int>(folder + "09ComeandFindMe-Bmix.wav", (toSec(3) + 29)));
            musicList.Add(new Tuple<string, int>(folder + "10Arpanauts.wav", (toSec(3) + 16)));
            musicList.Add(new Tuple<string, int>(folder + "VITAS-The7thElement.wav", (42)));

        }

        public void toggleMute() {
            isMuted = !isMuted;
        }
        private int toSec(int min) {
            return min * 60;
        }


        public void update() {

            if (isMuted && isPlaying) {
                soundplayer?.Stop();
                isPlaying = false;
            } else if (!isMuted && !isPlaying) {
                _soundThread = new Thread(playMusic);
                _soundThread.IsBackground = true;
                _soundThread.Start();
            }

        }

        public void playMusic() {
            
            isPlaying = true;

            int soundIndex = rand.Next(0, musicList.Count);

            soundplayer.Stop();
            soundplayer.SoundLocation = musicList[soundIndex].Item1;
            soundplayer.Play();
            Thread.Sleep(musicList[soundIndex].Item2 * 1000);

            isPlaying = false;
        }


    }
}
