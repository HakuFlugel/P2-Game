﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsTest {

    class ImageToFile {

        public ImageToFile(string[,] arr) {
            using (System.IO.StreamWriter MyFile = new System.IO.StreamWriter(@"")) {
                for (int i = 0; i < arr.Length; i++) {
                    for (int j = 0; j < arr.Length; j++) {
                        MyFile.WriteLine(arr[i, j].ToString());
                    }
                }
                MyFile.Close();
            }
        }
    }
}
