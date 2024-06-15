// See https://aka.ms/new-console-template for more information
//21110437
//EMMANUEL ISAAC RODRIGUEZ MENDEZ
//ALGORITMO DE DIJKSTRA PRACTICA 3
using System;
using System.Collections.Generic;

class Program
{
    // Clase que representa una arista del grafo
    public class Edge
    {
        public int Destination { get; set; }
        public int Weight { get; set; }

        public Edge(int destination, int weight)
        {
            Destination = destination;
            Weight = weight;
        }
    }

    // Clase que representa un nodo del grafo
    public class Node
    {
        public int Id { get; set; }
        public List<Edge> Edges { get; set; }

        public Node(int id)
        {
            Id = id;
            Edges = new List<Edge>();
        }
    }

    public class Graph
    {
        public List<Node> Nodes { get; set; }

        public Graph()
        {
            Nodes = new List<Node>();
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }

        public void AddEdge(int fromNode, int toNode, int weight)
        {
            Nodes[fromNode].Edges.Add(new Edge(toNode, weight));
        }
    }

    static void Main(string[] args)
    {
        // Crear el grafo
        Graph graph = new Graph();
        for (int i = 0; i < 4; i++)
        {
            graph.AddNode(new Node(i));
        }

        // Añadir las aristas (caminos)
        graph.AddEdge(0, 1, 50);  // Camino 1: A -> B (50 min)
        graph.AddEdge(0, 1, 120); // Camino 2: A -> B (120 min)
        graph.AddEdge(0, 2, 20);  // Camino 3: A -> C (20 min)
        graph.AddEdge(2, 1, 20);  // Camino adicional: C -> B (20 min)
        // Aquí, podemos considerar que de C se puede ir a B, sumando el tiempo
        // Ejecutar el algoritmo de Dijkstra desde el nodo de inicio (0) hasta el nodo de fin (1)
        var shortestPath = Dijkstra(graph, 0, 1);

        Console.WriteLine("La ruta más rápida tiene un tiempo de: " + shortestPath + " minutos.");
    }

    public static int Dijkstra(Graph graph, int startNode, int endNode)
    {
        int[] distances = new int[graph.Nodes.Count];
        bool[] visited = new bool[graph.Nodes.Count];

        // Inicializar distancias a infinito, excepto el nodo inicial
        for (int i = 0; i < distances.Length; i++)
        {
            distances[i] = int.MaxValue;
            visited[i] = false;
        }
        distances[startNode] = 0;

        // Cola de prioridad (usar una lista para simplicidad)
        var priorityQueue = new List<Tuple<int, int>>();
        priorityQueue.Add(Tuple.Create(startNode, 0));

        while (priorityQueue.Count > 0)
        {
            // Obtener el nodo con la menor distancia
            priorityQueue.Sort((a, b) => a.Item2.CompareTo(b.Item2));
            var current = priorityQueue[0];
            priorityQueue.RemoveAt(0);

            int currentNode = current.Item1;
            int currentDistance = current.Item2;

            if (visited[currentNode])
                continue;

            visited[currentNode] = true;

            foreach (var edge in graph.Nodes[currentNode].Edges)
            {
                int neighbor = edge.Destination;
                int newDist = currentDistance + edge.Weight;

                if (newDist < distances[neighbor])
                {
                    distances[neighbor] = newDist;
                    priorityQueue.Add(Tuple.Create(neighbor, newDist));
                }
            }
        }

        return distances[endNode];
    }
}