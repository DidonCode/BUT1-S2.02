using Newtonsoft.Json.Linq;
using SAE_Graph.Other;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace SAE_Graphe.Graph {


	public class Dijkstra {
		private List<Node> nodes;
		private List<Edge> edges;
		private Dictionary<int, float> distance;
		private Dictionary<int, int> previous;

		public Dijkstra() {
		}

		private void Initialize(Node startNode) {
			distance = new Dictionary<int, float>();
			previous = new Dictionary<int, int>();
			foreach(Node node in nodes) {
				distance[this.nodes.IndexOf(node)] = float.MaxValue;
				previous[this.nodes.IndexOf(node)] = -1;
			}
			distance[this.nodes.IndexOf(startNode)] = 0;
		}

		private int GetClosestNode(Dictionary<int, float> distance, HashSet<int> visited) {
			float shortestDistance = float.MaxValue;
			int closestNode = -1;
			foreach(KeyValuePair<int, float> pair in distance) {
				int nodeId = pair.Key;
				float nodeDistance = pair.Value;
				if(nodeDistance < shortestDistance && !visited.Contains(nodeId)) {
					shortestDistance = nodeDistance;
					closestNode = nodeId;
				}
			}
			return closestNode;
		}

		public List<Node> ShortestPath(List<Node> nodes, List<Edge> edges, Node startNode, Node endNode) {
			this.nodes = nodes;
			this.edges = edges;

			Initialize(startNode);
			
			HashSet<int> visited = new HashSet<int>();

			while(visited.Count < nodes.Count) {
				int currentNode = GetClosestNode(distance, visited);
				if(currentNode == -1) {
					break;
				}

				visited.Add(currentNode);

				foreach(Edge edge in this.edges) {
					if(this.nodes.IndexOf(edge.start) == currentNode || this.nodes.IndexOf(edge.end) == currentNode) {
						int neighbourNodeId = this.nodes.IndexOf(edge.start) == currentNode ? this.nodes.IndexOf(edge.end) : this.nodes.IndexOf(edge.start);
						float distanceToNeighbour = distance[currentNode] + edge.weight;
						if(distanceToNeighbour < distance[neighbourNodeId]) {
							distance[neighbourNodeId] = distanceToNeighbour;
							previous[neighbourNodeId] = currentNode;
						}
					}
				}
			}

			List<Node> shortestPath = new List<Node>();
			int node = this.nodes.IndexOf(endNode);
			while(node != -1) {
				shortestPath.Insert(0, nodes.Find(n => this.nodes.IndexOf(n) == node));
				node = previous[node];
			}

			return shortestPath;
		}
	}

}
