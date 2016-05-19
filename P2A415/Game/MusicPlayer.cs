using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;
using System.Media;
using System.Drawing;


namespace RPGame {
    public class MusicPlayer {

        List<Tuple <Tuple<string, int>, Tuple<string,string>>> musicList = new List<Tuple<Tuple<string,int>, Tuple<string,string>>>();
        private bool isPlaying = false;
        public bool isMuted = false;

        int soundIndex;


        private static Random rand = new Random();
        Thread _soundThread;
        SoundPlayer player = new SoundPlayer();
        
        public MusicPlayer() {
            const string folder = "Content/";
            musicList.Add(new Tuple<Tuple<string,int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "01ANightOfDizzySpells.wav", toSec(1) + 54),
                new Tuple<string, string>("Eric Skiff", "A Night Of Dizzy Spells")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "02Underclocked(underunderclockedmix).wav", toSec(3) + 8),
                new Tuple<string, string>("Eric Skiff", "Underclocked")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "03ChibiNinja.wav", (toSec(2) + 3)),
                new Tuple<string, string>("Eric Skiff", "Chibi Ninja")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "04AllofUs.wav", (toSec(1) + 57)),
                new Tuple<string, string>("Eric Skiff", "All of Us")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "05ComeandFindMe.wav", (toSec(3) + 19)),
                new Tuple<string, string>("Eric Skiff", "Come and Find Me")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "06Searching.wav", (toSec(2) + 20)),
                new Tuple<string, string>("Eric Skiff", "Searching")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "07We'retheResistors.wav", (toSec(2) + 21)),
                new Tuple<string, string>("Eric Skiff", "We're the Resistors")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "08Ascending.wav", (toSec(3) + 12)),
                new Tuple<string, string>("Eric Skiff", "Ascending")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "09ComeandFindMe-Bmix.wav", (toSec(3) + 29)),
                new Tuple<string, string>("Eric Skiff", "Come and Find me - Bmix")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "10Arpanauts.wav", (toSec(3) + 16)),
                new Tuple<string, string>("Eric Skiff", "Arpanauts")));
            musicList.Add(new Tuple<Tuple<string, int>, Tuple<string, string>>(
                new Tuple<string, int>(folder + "VITAS-The7thElement.wav", (42)),
                new Tuple<string, string>("VITAS", "The 7th Element ClickMix")));

            //musicList.Add(new Tuple<string, int>(folder + "01ANightOfDizzySpells.wav", toSec(1) + 54));
            //musicList.Add(new Tuple<string, int>(folder + "02Underclocked(underunderclockedmix).wav", (toSec(3) + 8)));
            //musicList.Add(new Tuple<string, int>(folder + "03ChibiNinja.wav", (toSec(2) + 3)));
            //musicList.Add(new Tuple<string, int>(folder + "04AllofUs.wav", (toSec(1) + 57)));
            //musicList.Add(new Tuple<string, int>(folder + "05ComeandFindMe.wav", (toSec(3) + 19)));
            //musicList.Add(new Tuple<string, int>(folder + "06Searching.wav", (toSec(2) + 20)));
            //musicList.Add(new Tuple<string, int>(folder + "07We'retheResistors.wav", (toSec(2) + 21)));
            //musicList.Add(new Tuple<string, int>(folder + "08Ascending.wav", (toSec(3) + 12)));
            //musicList.Add(new Tuple<string, int>(folder + "09ComeandFindMe-Bmix.wav", (toSec(3) + 29)));
            //musicList.Add(new Tuple<string, int>(folder + "10Arpanauts.wav", (toSec(3) + 16)));
            //musicList.Add(new Tuple<string, int>(folder + "VITAS-The7thElement.wav", (42)));

        }

        public void toggleMute() {
            isMuted = !isMuted;
        }
        private int toSec(int min) {
            return min * 60;
        }


        public void update() {

            if (isMuted && isPlaying) {
                player?.Stop();
                isPlaying = false;
            } else if (!isMuted && !isPlaying) {
                _soundThread = new Thread(playMusic);
                _soundThread.IsBackground = true;
                _soundThread.Start();
            }

        }

        public void playMusic() {
            
            isPlaying = true;

            soundIndex = rand.Next(0, musicList.Count);

            player.Stop();
            player.SoundLocation = musicList[soundIndex].Item1.Item1;
            player.Play();
            Thread.Sleep(musicList[soundIndex].Item1.Item2 * 1000);

            isPlaying = false;
        }

//        public void draw(Graphics gfx, Game game) {
//            Font font;
//            Font timefont;
//            int fontLarge = 16;
//            int timeFontlarge = 12;
//            SolidBrush background = new SolidBrush(Color.FromArgb(128, Color.Black));
//            String text = $@" {musicList[soundIndex].Item2.Item2}
//By: {musicList[soundIndex].Item2.Item1}";
//            string timetext = (musicList[soundIndex].Item1.Item2 / 60).ToString() + ":" + (musicList[soundIndex].Item1.Item2 % 60).ToString();
//            SizeF uiSize = new SizeF(200,50);
//            while (true) {

//                font = new Font("Arial", fontLarge, FontStyle.Regular);
//                timefont = new Font("Arial", timeFontlarge, FontStyle.Regular);

//                SizeF size = gfx.MeasureString(text, font);
//                SizeF timefontSize = gfx.MeasureString(timetext, timefont);
//                if (size.Width < uiSize.Width && size.Height < uiSize.Height)
//                    break;
//                else
//                    fontLarge--;
//            }
            
            

//            const int padding = 4;
            

//            RectangleF uiRect = new RectangleF(game.Width - padding - uiSize.Width, game.Height - padding - uiSize.Height, uiSize.Width, uiSize.Height );
//            RectangleF textRect = new RectangleF(uiRect.X + padding, uiRect.Y + padding, size.Width, size.Height);
//            gfx.FillRectangle(background, uiRect);

//            gfx.DrawString(text, font, Brushes.WhiteSmoke, textRect);
//        }
    }
}
