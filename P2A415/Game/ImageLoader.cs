using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace RPGame {
    public static class ImageLoader {

        public class ImageResource {
            public ulong usedCount = 0;
            public Bitmap texture;

            public ImageResource(Bitmap texture) {
                this.texture = texture;
            }
        }

        public static Dictionary<string, ImageResource> imageResources = new Dictionary<string, ImageResource>();

        public static Bitmap Load(string file) {
            if (!imageResources.ContainsKey(file)) {

                if (!File.Exists(file)) 
                    throw new FileNotFoundException();

                imageResources.Add(file, new ImageResource((Bitmap)Bitmap.FromFile(file)));
            }
            imageResources[file].usedCount++;
            return imageResources[file].texture;
        }

        public static void Unload(string file) {
            if (!imageResources.ContainsKey(file)) {
                throw new InvalidOperationException();
            }

            if (--imageResources[file].usedCount < 1)
                imageResources.Remove(file);
        }
    }
}