using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsTest
{
    class Road
    {

        public Road(string path)
        {
            Path = path;
        }

        public string Path { get; private set; }

        public int Lenght => ParsePath().Length / 2;

        public int[] End { get; private set; }

        private int[] ParsePath()

        {
            string[] pathSection = Path.Split(';');

            int[] parse = new int[pathSection.Length * 2]; //pathsection is a pair of coordinates

            int jndex = 0;

            foreach (string path in pathSection)
            {
                string[] temppath = path.Split(',');

                parse[jndex] = int.Parse(temppath[0]);
                parse[jndex + 1] = int.Parse(temppath[1]);

                jndex += 2;
            }

            return parse;
        }

        public void AddPath(int[] coordinates, World region)
        {
            if (Path.Length == 0)
                Path = string.Join(",", coordinates);
            else
                Path = Path + ";" + string.Join(",", coordinates);

            End = coordinates;

            region.guidgrid[coordinates[0], coordinates[1]] = 9999; //todo: change weight

        }

        public void AddRoadToGrid(World region)
        {
            int[] path = ParsePath();

            for (int i = 0; i < path.Length; i += 2)
            {
                region[path[i], path[i + 1]] = "road"; //todo: fix plox
            }
        }

        public int CheckCardinals(World region, out int[] newPath)
        {
            int direction = -1; // error number
            int[] cardinals = new int[4];
            newPath = new int[2];
            //0 north, 1 east, 2 south, 3 west
            try
            {
                cardinals[0] = region.guidgrid[End[0], End[1] + 1]; //todo: fetch weight
            }
            catch (IndexOutOfRangeException)
            {
                cardinals[0] = 10000;
            }
            try
            {
                cardinals[1] = region.guidgrid[End[0] + 1, End[1]]; //todo: fetch weight
            }
            catch (IndexOutOfRangeException)
            {
                cardinals[1] = 10000;
            }
            try
            {
                cardinals[2] = region.guidgrid[End[0], End[1] - 1]; //todo: fetch weight
            }
            catch (IndexOutOfRangeException)
            {
                cardinals[2] = 10000;
            }
            try
            {
                cardinals[3] = region.guidgrid[End[0] - 1, End[1]]; //todo: fetch weight
            }
            catch (IndexOutOfRangeException)
            {
                cardinals[3] = 10000;
            }

            int smallest = 9999; //just a high number
            for (int index = 0; index < 4; index++)
            {
                if (cardinals[index] < smallest)
                {
                    smallest = cardinals[index];
                    direction = index;
                }
            }
            newPath = ChooseNewPath(direction);

            return smallest;
        }

        private int[] ChooseNewPath(int direction)
        {
            int[] newpath = new int[2];
            switch (direction)
            {
                case 0:
                    newpath[0] = End[0];
                    newpath[1] = End[1] + 1;
                    break;
                case 1:
                    newpath[0] = End[0] + 1;
                    newpath[1] = End[1];
                    break;
                case 2:
                    newpath[0] = End[0];
                    newpath[1] = End[1] - 1;
                    break;
                case 3:
                    newpath[0] = End[0] - 1;
                    newpath[1] = End[1];
                    break;
            }
            return newpath;
        }
    }
}
