using System;
using System.Collections.Generic;


namespace WinFormsTest
{
    class RoadMaker
    {
        public static Dictionary<int[], Road> PathDictionary = new Dictionary<int[], Road>();

        public RoadMaker(World world, int[,] weight)
        {
            World = world;
            Weight = (int[,])weight.Clone();
        }

        public World World { get; set; }
        private int[,] Weight { get; set; }

        public void MakeRoad(int[] start, int[] end)
        {
            Start = start;
            End = end;
            string path = "";
            int[] worldSize = new int[2];
            worldSize[0] = World.regions.GetLength(0);
            worldSize[1] = World.regions.GetLength(1);
            RegionAnalyzer(worldSize);
            path = ProcessTiles(path);

            if (path != "")
            {
                Road road = new Road(path);
                road.AddRoadToWorld(World);
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
            startroad.AddPath(Start, Weight);
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
                int newest = item.Value.CheckCardinals(out newRoad, Weight);

                //if (newest == -1)
                //{
                //    PathDictionary.Remove(item.Value.End);
                //}

                if (newest < best)
                {
                    best = newest;
                    key = item.Key;
                    chosenRoad = newRoad;
                }
            }

            string tempPath = PathDictionary[key].Path;
            PathDictionary.Add(chosenRoad, new Road(tempPath));
            PathDictionary[chosenRoad].AddPath(chosenRoad, Weight);
            path = PathDictionary[chosenRoad].Path;
            return chosenRoad;
        }
        private void RegionAnalyzer(int[] size)
        {
            int[] coordinates = new int[2];
            for (int index = 0; index < size[0]; index++)
            {
                for (int jndex = 0; jndex < size[1]; jndex++)
                {
                    coordinates[0] = index;
                    coordinates[1] = jndex;
                    Weight[index, jndex] *= (int)Distance(coordinates, End);
                }
            }
        }
    }
}
