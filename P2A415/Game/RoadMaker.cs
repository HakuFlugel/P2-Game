using System;
using System.Collections.Generic;
using System.Collections;

/* 
 * TODO: lige nu bruger den bare dijkstra, dvs. den prøver alle de korteste veje fra start, indtil den når i mål. 
 * Dette kan betyde at den prøver alle 32*32*(16*16 i øjeblikket) tiles, hvis den skal igennem hele banen.
 * Problemet med at vægte afstanden sammen med er at den så bare foretrækker at gå mod siden først, og derefter op ad.
 * 
 * Alternativt: Afstanden kunne divideres med vægten af et bjerg, hvilket betyder at den ikke prøver tiles,
 * der er helt håbløst lang væk, men stadig spreder sig en del.
 * Kunne også dividere med mindre...
 * 
 * Note: Se på vægtforhold mellem bjerge og åbne tiles...
 */


namespace RPGame {
    public class RoadMaker {

        public struct coords {
            public int x;
            public int y;

            public coords(int x, int y) {
                this.x = x;
                this.y = y;
            }

            public static coords operator +(coords a, coords b) {
                return new coords(a.x + b.x, a.y + b.y);
            }

            public static bool operator ==(coords a, coords b) {
                return a.x == b.x && a.y == b.y;
            }
            public static bool operator !=(coords a, coords b) {
                return !(a == b);
            }
            public override bool Equals(object obj) {
                return base.Equals(obj); // TODO: skal det være det samme som ==
            }
            public override int GetHashCode() {
                return base.GetHashCode();
            }

        }

        public World world { get; set; }
        private int[,] weights;
        private int[,] shortestPathCost;
        private List<NeighborEntry> neighbors;

        private bool foundEnd = false;

        public coords end { get; set; } // TODO: verify length = 2
        public coords start { get; set; }

        //public static Dictionary<int[], Road> PathDictionary = new Dictionary<int[], Road>();

        public RoadMaker(World world, int[,] weights) {
            this.world = world;
            this.weights = weights;//(int[,])weights.Clone();
        }



        private static double calculateDistance(coords a, coords b) {
            int dx, dy;

            dx = a.x - b.x; // x diff
            dy = a.x - b.x; // y diff
            double distance = Math.Sqrt(dx * dx + dy * dy); // original
            //double distance = (dx * dx) * (dy * dy); // original

            //double distance = dx*dy;

            return distance+1;
        }

//        private class ReverseComparer<T> : IComparer<T> where T : IComparable<T> {
//            public int Compare(T obj1, T obj2) {
//                return -((obj1).CompareTo(obj2));
//            }
//        }

        public struct NeighborEntry : IComparable {
            public int pathCost;
            public int dist;
            public coords tile;

            public NeighborEntry(int pathCost, coords tile, int dist) {
                this.pathCost = pathCost;
                this.tile = tile;
                this.dist = dist;
            }

            public int CompareTo(object obj) {
                NeighborEntry other = (NeighborEntry)obj;
                return (this.pathCost + this.dist).CompareTo(other.pathCost + other.dist);
                // TODO: kan det betale sig at invertere dette, så de laveste elementer er sidst i listen(arrayet), hvorved der er mindre der skal flyttes under .Insert()
            }
        }


        public void addNeighbor(int prevCost, coords tile) {
            try {
                Console.WriteLine(tile.x + "," + tile.y + ": " + prevCost);
                int curCost = prevCost + weights[tile.x, tile.y]; // TODO: skal der ændres på hvordan vi vægter fx afstand?

                if (curCost < shortestPathCost[tile.x, tile.y]) {

                    int curDist = (int)(calculateDistance(tile, end)/64);

                    shortestPathCost[tile.x, tile.y] = curCost;
                    NeighborEntry newNeighbor = new NeighborEntry(curCost, tile, curDist);

                    int ret = neighbors.BinarySearch(newNeighbor);

                    neighbors.Insert(ret < 0 ? ~ret : ret, newNeighbor);

                    if (tile == end) {
                        Console.WriteLine("Hit end");
                        foundEnd = true;
                    }
                }
            } catch (IndexOutOfRangeException) {
                // Do nothing
            }
        }

        public void generatePath(coords start, coords end) {

            // TODO: er koordinater inden for verden

            this.start = start;
            this.end = end;

            int xSize = world.regions.GetLength(0) * 32;
            int ySize = world.regions.GetLength(1) * 32;

            shortestPathCost = new int[xSize, ySize];
            for (int x = 0; x < xSize; x++) {
                for (int y = 0; y < ySize; y++) {
                    shortestPathCost[x, y] = int.MaxValue;
                }
            }

            neighbors = new List<NeighborEntry>();
            neighbors.Add(new NeighborEntry(0, start, (int)calculateDistance(start, end)));
            shortestPathCost[start.x, start.y] = 0;
//            int iiii = 1;

            foundEnd = false;
            while (!foundEnd) {
//                Console.WriteLine(iiii++ + "             " + iiii);
                NeighborEntry firstNeightbor = neighbors[0];
                coords first = firstNeightbor.tile;
                int firstCost = shortestPathCost[first.x, first.y];

                addNeighbor(firstCost, new coords(first.x+1, first.y));
                addNeighbor(firstCost, new coords(first.x-1, first.y));
                addNeighbor(firstCost, new coords(first.x, first.y+1));
                addNeighbor(firstCost, new coords(first.x, first.y-1));

                neighbors.Remove(firstNeightbor);
            }

            Console.WriteLine("Making path from end to start");
            // Follow the path backwards, always taking the cheapest possible cost
            coords[] direction = { // TODO: kan vi på nogen måde gøre den her const/readonly??????
                new coords( 1,0),
                new coords(-1,0),
                new coords(0, 1),
                new coords(0,-1)
            };

            coords tile = end;

            if (world[tile.x, tile.y] != (int)World.GeneratedTile.Town) {
                world[tile.x, tile.y] = (int)World.GeneratedTile.Path;
            }
            weights[tile.x, tile.y] = 1;//-=weights[tile.x, tile.y]/2;


            while (tile != start) {
                coords cheapestTile = tile;
                int cheapestCost = int.MaxValue;
//
//                try { 
//                    cheapestTile = tile + direction[0];
//                    cheapestCost = shortestPathCost[cheapestTile.x, cheapestTile.y];
//                } catch (IndexOutOfRangeException) {
//                    // Do nothing
//                }

                for (int i = 0; i < direction.Length; i++) {
                    try { // TODO: lav range-check istedet
                        coords newTile = tile + direction[i];
                        int directionCost = shortestPathCost[newTile.x, newTile.y];
                        Console.Write(newTile.x + "," + newTile.y + ": ");
                        Console.WriteLine(directionCost + "<" + cheapestCost + (directionCost<cheapestCost));
                        if (directionCost < cheapestCost) {
                            cheapestCost = directionCost;
                            cheapestTile = newTile;
                        }
                    } catch (IndexOutOfRangeException) {
                        // Do nothing
                        Console.WriteLine("out of range " + i);
                    }
                }

                if (cheapestTile == tile) {
                    throw new Exception("Couldn't find a tile closer to the path starting point. Not sure how that's possible though");
                }

                tile = cheapestTile;
                Console.WriteLine(tile.x + "---" + tile.y);
                if (world[tile.x, tile.y] != (int)World.GeneratedTile.Town) {
                    world[tile.x, tile.y] = (int)World.GeneratedTile.Path;
                }
                weights[tile.x, tile.y] = 1;//-=weights[tile.x, tile.y]/2;

            }
            
            Console.WriteLine("Done making path");

            // TODO: reverse search for cheapest path, greedy


        }
    }
}
