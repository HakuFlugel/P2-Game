using System;
using System.Collections.Generic;


namespace WinFormsTest
{
    class RoadMaker
    {
        public static Dictionary<int[], Road> PathDictionary = new Dictionary<int[], Road>();

        public RoadMaker(World region)
        {
            EndRegion = region;
            TempRegion = new World();
            TempRegion = EndRegion; //todo: ændre til value copy og ikke reference copy
        }

        public World EndRegion { get; set; }
        public World TempRegion { get; set; }

        public void MakeRoad(int[] start, int[] end)
        {
            Start = start;
            End = end;
            string path = "";
            int[] gridSize = new int[2];
            gridSize[0] = TempRegion.regions.GetLength(0);
            gridSize[1] = TempRegion.regions.GetLength(1);
            path = ProcessTiles(path);

            if (path != "")
            {
                Road road = new Road(path);
                road.AddRoadToGrid(EndRegion); //todo: somehow add road to region
            }
            else
                Console.WriteLine("could not create road");
        }

        public int[] End { get; set; }

        public int[] Start { get; set; }

        private decimal Distance(int[] itemStart, int[] itemEnd)
        {
            int[] xy = new int[2];

            xy[0] = itemStart[0] - itemEnd[0]; // x diff
            xy[1] = itemStart[1] - itemEnd[1]; // y diff
            decimal distance = (decimal)Math.Sqrt(xy[0] * xy[0] + xy[1] * xy[1]);

            return distance;
        }

        private string ProcessTiles(string path)
        {
            Road startroad = new Road(path);
            startroad.AddPath(Start, TempRegion);
            PathDictionary.Add(startroad.End, startroad);
            int[] newEnd = new int[2];

            while (End[0] != newEnd[0] || End[1] != newEnd[1])
            {
                newEnd = ChooseNewRoad(out path);
                Console.WriteLine(path);
            }
            return path;
        }

        private int[] ChooseNewRoad(out string path)
        {
            int best = 10000;
            int[] key = new int[2];
            int[] chosenRoad = new int[2];
            foreach (KeyValuePair<int[], Road> item in PathDictionary)
            {
                int[] newRoad;
                int newest = item.Value.CheckCardinals(TempRegion, out newRoad);
                if (newest < best)
                {
                    best = newest;
                    key = item.Key;
                    chosenRoad = newRoad;
                }
            }

            string tempPath = PathDictionary[key].Path;
            PathDictionary.Add(chosenRoad, new Road(tempPath));
            PathDictionary[chosenRoad].AddPath(chosenRoad, TempRegion);
            path = PathDictionary[chosenRoad].Path;
            return chosenRoad;
        }
    }
}
