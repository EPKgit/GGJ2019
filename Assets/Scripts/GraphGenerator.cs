using System.Collections.Generic;
using System;

namespace GraphGeneration{
    public static class GraphGenerator{

        private static Random rand;
        private static Random Rand{
            get{
                if(rand == null){
                    rand = new Random();
                }
                return rand;
            }
        }

        /*  main for testing
        public static void Main(){
            Graph graph = Build(4, 8, 20.0f, 20.0f, 2.0f, 2.0f, 0.6f);
            Console.WriteLine("Nodes: {0}, Edges: {1}", graph.Nodes.Count, graph.Edges.Count);
        }
        //*/

        /// TAKES
        /// number of planets wide, number of planets tall, av x dist between planners, av y sist between planets
        /// how much to randomize x dists, how much to randomize y dists, % chance will build each edge (50% => av. degree 3)
        public static Graph Build(int x, int y, float xStep, float yStep, float xRandRange, float yRandRange, float buildChance){
            List<BuildingEdge> edges = new List<BuildingEdge>(); // the edges that survive to the end
            Graph grid = BuildGrid(x,y,xStep,yStep,xRandRange,yRandRange);
            foreach(BuildingEdge e in grid.Edges){
                //figure out if will disconnect
                bool willDisconnect = false;
                foreach(BuildingNode n in e.Nodes){
                    willDisconnect |= n.cc.outgoingEdges <= 0;
                }
                // add the edge
                if(willDisconnect || Rand.NextDouble() < buildChance){ 
                    edges.Add(e);
                    //assuming edges make a strict ordering
                    BuildingCC cc = e.Nodes[0].cc;
                    cc.outgoingEdges += e.Nodes[1].cc.outgoingEdges - 1;
                    e.Nodes[1].cc = cc;
                // delete the edge
                }else{
                    foreach(BuildingNode n in e.Nodes){
                        n.cc.outgoingEdges--;
                    }
                }
            }
            return new Graph{Nodes = grid.Nodes, Edges = edges};
        }

        internal static Graph BuildGrid(int x, int y, float xStep, float yStep, float xRandRange, float yRandRange){
            List<BuildingNode> nodes = new List<BuildingNode>();
            List<BuildingEdge> edges = new List<BuildingEdge>();
            float yPos= 0;
            BuildingNode[,] grid = new BuildingNode[x,y];
            for(int j = 0; j < y; j++){
                float xPos= (j%2 == 0)? 0 : yStep/2; // stagger the rows
                for(int i = 0; i < x; i++){
                    //walk
                    xPos += 2*xRandRange*((float)Rand.NextDouble()) - xRandRange;
                    yPos += 2*yRandRange*((float)Rand.NextDouble()) - yRandRange;
                    //init the node
                    BuildingNode node = new BuildingNode{cc = new BuildingCC(), xPos = xPos, yPos = yPos, prio = nodes.Count};
                    //store it
                    nodes.Add(node);
                    grid[i,j] = node;
                    //build edges
                    if(i > 0){
                        edges.Add(new BuildingEdge(node, grid[i-1,j]));
                    }
                    if(j > 0){
                        edges.Add(new BuildingEdge(node, grid[i,j-1]));
                    }
                    if(j > 0 && i > 0){
                        edges.Add(new BuildingEdge(node, grid[i-1,j-1]));
                    }
                }
            }
            return new Graph{Nodes = nodes, Edges = edges};
        }


        //INNER CLASSES

        // connected component
        internal class BuildingCC{
            internal int outgoingEdges;
            internal BuildingCC(){
                outgoingEdges = 0;
            }
        }

        public class BuildingNode{
            internal BuildingCC cc;
            public float xPos;
            public float yPos;
            internal int prio; //must be unique among nodes to give a strict ordering
        }

        public class BuildingEdge{
            public BuildingNode[] Nodes;
            internal BuildingEdge(BuildingNode a, BuildingNode b){
                Nodes = new BuildingNode[]{a,b};
                foreach(BuildingNode n in Nodes){
                    n.cc.outgoingEdges++;
                }
            }
        }

        public class Graph{
            public List<BuildingNode> Nodes;
            public List<BuildingEdge> Edges;
        }

    }

}