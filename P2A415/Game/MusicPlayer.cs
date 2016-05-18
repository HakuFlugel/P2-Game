using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;


namespace RPGame {
    public class MusicPlayer {

        List<string> WhereWavFilesAre = new List<string>();
        private bool isPlaying = false;
        private static Random rand = new Random();
        public bool isMute = false;
        Thread _soundThread;
        System.Media.SoundPlayer player;

        public MusicPlayer() {
            string path = "Content";

            string[] wavFiles = Directory.GetFiles(path,"*.wav");

            foreach (var item in wavFiles)
                WhereWavFilesAre.Add(item);

            
        }

        public void toggleMute() {
            isMute = !isMute;
            isPlaying = false;
            
            player?.Stop();

        }


        public void update() {
            Console.WriteLine(isMute.ToString() + " vovovovovo " + isPlaying.ToString());
            if (isMute || isPlaying) {
                
                return;
            }
                

            _soundThread = new Thread(playMusic);
            _soundThread.Start();
            
        }
        [DllImport("winmm.dll")]
        private static extern uint mciSendString(
                string command,
                StringBuilder returnValue,
                int returnLength,
                IntPtr winHandle);

        public void playMusic() {
            


            isPlaying = true;

            string soundFile = WhereWavFilesAre[rand.Next(0, WhereWavFilesAre.Count-1)];

            StringBuilder lengthBuf = new StringBuilder(128);

            mciSendString(string.Format("open \"{0}\" type waveaudio alias wave", soundFile), null, 0, IntPtr.Zero);
            mciSendString("status wave length", lengthBuf, lengthBuf.Capacity, IntPtr.Zero);
            mciSendString("close wave", null, 0, IntPtr.Zero);
            
            int length = 0;
            int.TryParse(lengthBuf.ToString(), out length);


            using (player = new System.Media.SoundPlayer(soundFile)) {
                player.Play();//Sync();
                Thread.Sleep(length);
            }
            isPlaying = false;
        }


    }
}
