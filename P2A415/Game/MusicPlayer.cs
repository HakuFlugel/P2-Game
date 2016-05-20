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

        List<Tuple <string, int, string,string>> musicList = new List<Tuple<string,int,string,string>>();
        private bool isPlaying = false;
        public bool isMuted = false;
        public bool isNewMusic = true;

        int soundIndex;


        private static Random rand = new Random();
        Thread _soundThread;
        SoundPlayer soundplayer = new SoundPlayer();
        
        public MusicPlayer() {
            const string folder = "Content/";
            musicList.Add(
                new Tuple<string, int, string, string>(
                folder + "01ANightOfDizzySpells.wav", toSec(1) + 54,"Eric Skiff", "A Night Of Dizzy Spells"));

            musicList.Add(new Tuple<string, int, string, string>(
                folder + "02Underclocked(underunderclockedmix).wav", toSec(3) + 8,
                "Eric Skiff", "Underclocked"));
            musicList.Add(new Tuple<string, int,string, string>(
                folder + "03ChibiNinja.wav", (toSec(2) + 3),
                "Eric Skiff", "Chibi Ninja"));
            musicList.Add(new Tuple<string, int, string, string>(
                folder + "04AllofUs.wav", (toSec(1) + 57),
                "Eric Skiff", "All of Us"));
            musicList.Add(new Tuple<string, int, string, string>(
                folder + "05ComeandFindMe.wav", (toSec(3) + 19),
                "Eric Skiff", "Come and Find Me"));
            musicList.Add(new Tuple<string, int, string, string>(
                folder + "06Searching.wav", (toSec(2) + 20),
                "Eric Skiff", "Searching"));
            musicList.Add(new Tuple<string, int, string, string>(
                folder + "07We'retheResistors.wav", (toSec(2) + 21),
               "Eric Skiff", "We're the Resistors"));
            musicList.Add(new Tuple<string, int, string, string>(
                folder + "08Ascending.wav", (toSec(3) + 12),
                "Eric Skiff", "Ascending"));
            musicList.Add(new Tuple<string, int, string, string>(
                folder + "09ComeandFindMe-Bmix.wav", (toSec(3) + 29),
                "Eric Skiff", "Come and Find me - Bmix"));
            musicList.Add(new Tuple<string, int, string, string>(
                folder + "10Arpanauts.wav", (toSec(3) + 16),
                "Eric Skiff", "Arpanauts"));
            musicList.Add(new Tuple<string, int, string, string>(
                folder + "VITAS-The7thElement.wav", (42),
                "VITAS", "The 7th Element ClickMix"));

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

            soundIndex = rand.Next(0, musicList.Count);

            soundplayer.Stop();
            soundplayer.SoundLocation = musicList[soundIndex].Item1;
            isNewMusic = true;
            soundplayer.Play();
            Thread.Sleep(musicList[soundIndex].Item2 * 1000);

            isPlaying = false;
        }

        public void draw(Graphics gfx, Game game) {
            if (isMuted)
                return;
            if (isNewMusic) {
                drawBox(gfx,game);
                isNewMusic = true;
                Thread t = new Thread(() => {

                    Thread.Sleep(5000);
                    isNewMusic = false;

                });
                t.IsBackground = true;
                t.Start();
            }
        }

        private void drawBox(Graphics gfx, Game game) { 

            Font font, timefont;
            int fontLarge = 16, timeFontlarge = 12;

            SizeF playedSize, authorSize, timefontSize, uiSize = new SizeF(250, 100);
            SolidBrush background = new SolidBrush(Color.FromArgb(128, Color.Black));

            string played = musicList[soundIndex].Item4, author ="By " +  musicList[soundIndex].Item3,
            timetext = (musicList[soundIndex].Item2 / 60) + ":" + (musicList[soundIndex].Item2 % 60).ToString("00");

            


            while (true) {

                font = new Font("Arial", fontLarge, FontStyle.Regular);
                timefont = new Font("Arial", timeFontlarge, FontStyle.Regular);
                authorSize = gfx.MeasureString(author, font);
                playedSize = gfx.MeasureString(played, font);
                timefontSize = gfx.MeasureString(timetext, timefont);
                if ((playedSize.Width < uiSize.Width || authorSize.Width + timefontSize.Width < uiSize.Width) && playedSize.Height + authorSize.Height < uiSize.Height)
                    break;
                else { fontLarge--; timeFontlarge--; }
                    

            }

            

            const int padding = 4;
            

            RectangleF uiRect = new RectangleF(game.Width - padding - uiSize.Width, game.Height - padding - uiSize.Height, uiSize.Width, uiSize.Height );
            RectangleF textRect = new RectangleF(uiRect.X + padding, uiRect.Y + padding, playedSize.Width, playedSize.Height);
            RectangleF authorRect = new RectangleF(textRect.X, textRect.Y + padding + playedSize.Height, authorSize.Width, authorSize.Height);
            RectangleF timeRect = new RectangleF(authorRect.X + authorSize.Width, authorRect.Y + fontLarge-timeFontlarge, timefontSize.Width, timefontSize.Height);
            gfx.FillRectangle(background, uiRect);

            gfx.DrawString(played, font, Brushes.WhiteSmoke, textRect);
            gfx.DrawString(author, font, Brushes.WhiteSmoke, authorRect);
            gfx.DrawString(timetext, timefont, Brushes.WhiteSmoke, timeRect);

    }
}
}
