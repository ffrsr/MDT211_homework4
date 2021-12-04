using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW4
{
    class Program
    {
        static void Main(string[] args)
        {
            int InputTown = int.Parse(Console.ReadLine());
            Console.Clear();

            for ( int i = 0; i < InputTown; i++)
            {
                string InputNameTown = Console.ReadLine();
            }
            Console.Clear();

            DirectedWeightedGraph g = new DirectedWeightedGraph();

            g.InsertVertex("A");
            g.InsertVertex("B");
            g.InsertVertex("C");
            g.InsertVertex("D");
            g.InsertVertex("E");
            g.InsertVertex("F");

            g.InsertEdge("A", "F", 14);
            g.InsertEdge("A", "C", 9);
            g.InsertEdge("A", "B", 7);
            g.InsertEdge("B", "C", 10);
            g.InsertEdge("B", "D", 15);
            g.InsertEdge("C", "D", 11);
            g.InsertEdge("C", "F", 2);
            g.InsertEdge("D", "E", 6);
            g.InsertEdge("E", "F", 9);

            g.InsertEdge("F", "A", 14);
            g.InsertEdge("C", "A", 9);
            g.InsertEdge("B", "A", 7);
            g.InsertEdge("C", "B", 10);
            g.InsertEdge("D", "B", 15);
            g.InsertEdge("D", "C", 11);
            g.InsertEdge("F", "C", 2);
            g.InsertEdge("E", "D", 6);
            g.InsertEdge("F", "E", 9);

            g.FindPaths("A");

            Console.ReadLine();
        }
    }

    class Vertex
    {
        public string name;
        public int status;
        public int predecessor;
        public int pathLength;

        public Vertex(string name)
        {
            this.name = name;
        }
    }

    class DirectedWeightedGraph
    {
        public readonly int Max_Vertices = 30;

        int n;
        int e;
        int[,] adj;
        Vertex[] vertexList;

        private readonly int Temporary = 1;
        private readonly int Permanent = 2;
        private readonly int NIL = -1;
        private readonly int Infinity = 99999;

        public DirectedWeightedGraph()
        {
            adj = new int[Max_Vertices, Max_Vertices];
            vertexList = new Vertex[Max_Vertices];
        }

        private void Dijkstra(int s)
        {
            int v, c;
            for (v = 0; v < n; v++)
            {
                vertexList[v].status = Temporary;
                vertexList[v].pathLength = Infinity;
                vertexList[v].predecessor = NIL;
            }
            vertexList[s].pathLength = 0;

            while (true)
            {
                c = TempVertexMinPL();

                if (c == NIL)
                    return;

                vertexList[c].status = Permanent;

                for (v = 0; v < n; v++)
                {
                    if (IsAdjacent(c,v) && vertexList[v].status == Temporary)
                    {
                        if (vertexList[c].pathLength + adj[c,v] < vertexList[v].pathLength) 
                        {
                            vertexList[v].predecessor = c;
                            vertexList[v].pathLength = vertexList[c].pathLength + adj[c, v]; 
                        }
                    }
                }
            }
        }

        private int TempVertexMinPL()
        {
            int min = Infinity;
            int x = NIL;
            for (int v = 0; v < n; v++)
            {
                if (vertexList[v].status == Temporary && vertexList[v].pathLength < min)
                {
                    min = vertexList[v].pathLength;
                    x = v;
                }
            }
            return x;
        }

        public void FindPaths(string source)
        {
            int s = GetIndex(source);

            Dijkstra(s);

            Console.WriteLine("Source Vertex : " + source + "\n");

            for (int v = 0; v < n; v++)
            {
                Console.WriteLine("Destination Vertex : " + vertexList[v].name);
                if(vertexList[v].pathLength == Infinity)
                {                   
                    Console.WriteLine("There is no path from " + source + " to vertex " + vertexList[v].name + "\n"); 
                }
                else
                {
                    FindPath(s, v);
                }
            }
        }

        private void FindPath (int s, int v)
        {
            int i, u;
            int[] path = new int[n];
            int sd = 0;
            int count = 0;

            while (v != s)
            {
                count++;
                path[count] = v;
                u = vertexList[v].predecessor;
                sd += adj[u, v];
                v = u;

            }
            count++;
            path[count] = s;

            Console.Write("Shortest Path is : ");

            for (i = count; i >= 1; i--)
            {
                Console.Write(path[i] + " ");
            }
            Console.WriteLine("\nShortest distance is : " + sd + "\n");

        }

        private int GetIndex(string s)
        {
            for (int i = 0; i < n; i++)
            
                if (s.Equals(vertexList[i].name))
                {
                    return i;
                }
                throw new System.InvalidOperationException("Invalid Vertex");
            
        }

        public void InsertVertex(string name)
        {
            vertexList[n++] = new Vertex(name);
        }

        private bool IsAdjacent (int u, int v)
        {
            return Convert.ToBoolean(adj[u, v] != 0);
        }

        public void InsertEdge(string s1, string s2, int d)
        {
            int u = GetIndex(s1);
            int v = GetIndex(s2);

            if ( u == v )
            {
                throw new System.InvalidOperationException("Not a valild edge");
            }
            if (adj[u, v] != 0)
            {
                Console.WriteLine("Edge already present");
            }
            else 
            {
                adj[u, v] = d;                   
                e++;
            }
        }
        
    }
        
}
