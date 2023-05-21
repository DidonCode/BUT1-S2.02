using SAE_Graph.Obstacles;
using SAE_Graph.Other;
using SAE_Graphe.Biomes;
using SAE_Graphe.Graph;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SAE_Graphe.Game
{

    public class Map {

        public List<Biome> biomes;
        public List<Biome> Biomes {
            get { return biomes; }
            set { biomes = value; }
        }

        public List<Obstacle> obstacles;
        public List<Obstacle> Obstacles {
            get { return obstacles; }
            set { obstacles = value; }
        }

        public List<Node> nodes;
        public List<Node> Nodes {
            get { return nodes; }
            set { nodes = value; }
        }

        public List<Edge> edges;
        public List<Edge> Edges {
            get { return edges; }
            set { edges = value; }
        }

        public int precision;
        public int Precision {
            get { return precision; }
            set { precision = value; }
        }

        public Dijkstra dijkstra;
        public Dijkstra Dijkstra {
            get { return dijkstra; }
            set { dijkstra = value; }
        }

        public Player player;
        public Player Player {
            get { return player; }
            set { player = value; }
        }

        public Map() { 
            this.biomes = new List<Biome>();
            this.nodes = new List<Node>();
            this.edges = new List<Edge>();
            this.obstacles = new List<Obstacle>();

            this.dijkstra = new Dijkstra();
            this.player = new Player();
        }

        public void GenerateGraph() {
            this.nodes.Clear();
            this.edges.Clear();

            foreach(Biome biome in this.biomes) {
                List<Vertex> points = biome.points;

                foreach(Vertex point in points) {
                    int count = 0;
                    foreach(Node node in this.nodes) {
                        if(node.IsEqual(point, 0)) {
                            count++;
                            node.AddBiome(biome);

						}
                    }
                    if (count == 0) {
                        this.nodes.Add(new Node(point, biome));
                    }
                }

                for(int i = 0; i < points.Count; i++) {
                    int count = 0;
                    if(i >= points.Count - 1) {
                        Node start = null;
                        Node end = null;
                        foreach(Node node in this.nodes) {
                            if(node.IsEqual(points[i], 0)) {
                                start = node;
                            }
                            if(node.IsEqual(points[0], 0)) {
                                end = node;
                            }
                        }

                        if(start == null || end == null) return;

                        Edge newEdge = new Edge(start, end, biome);
                        foreach(Edge edge in this.edges) {
                            if(!edge.IsEqual(newEdge, 10)) {
                                count++;
                            }
                        }
                        if(count >= this.edges.Count) {
                            this.edges.Add(newEdge);
                        }
                    } else {
                        Node start = null;
                        Node end = null;
                        foreach (Node node in this.nodes) {
                            if (node.IsEqual(points[i], 0)) {
                                start = node;
                            }
                            if (node.IsEqual(points[i + 1], 0)) {
                                end = node;
                            }
                        }

                        if (start == null || end == null) return;

                        Edge newEdge = new Edge(start, end, biome);
                        foreach(Edge edge in this.edges) {
                            if(!edge.IsEqual(newEdge, 10)) {
                                count++;
                            }
                        }
                        if(count >= this.edges.Count) {
                            this.edges.Add(newEdge);
                        }
                    }
                }
            }
        }

        public void GenerateGraphPrecision() {
            for(int i = 1; i < this.precision; i++) {
                List<Node> newNodes = new List<Node>();
                List<Edge> newEdges = new List<Edge>();

                foreach(Edge edge in this.edges) {
                    Vertex positionNode = Maths.CalculeMiddle(edge.start.position, edge.end.position);

                    Node middleNode = new Node(positionNode, edge.biome);
                    newNodes.Add(middleNode);

                    Edge edge1 = new Edge(middleNode, edge.end, edge.biome);
					edge1.Weight = edge.weight / 2;
					newEdges.Add(edge1);

                    edge.weight = edge.weight / 2;
                    edge.end = middleNode;
				}

                foreach(Node node in newNodes) {
                    this.nodes.Add(node);
                }

                foreach(Edge edge in newEdges) {
                    this.edges.Add(edge);
                }
            }
        }

        public void GenerateJunction() {
            foreach(Node node1 in this.Nodes) {
                foreach(Node node2 in this.Nodes) {
                    if (node1 != node2) {
                        bool haveCommunBiome = false;
                        Biome communBiome = null;
                        foreach (Biome biome in node1.biome) {
                            if (node2.biome.Contains(biome)) {
                                haveCommunBiome = true;
                                communBiome = biome;

                                break;
                            }
                        }

                        if (haveCommunBiome && communBiome != null) {
                            Edge newEdge = new Edge(node1, node2, communBiome);
                            int count = 0;

                            foreach (Edge edge in this.edges) {
                                if (edge.IsEqual(newEdge, 1)) {
                                    count++;
                                }
                            }

                            if (count == 0 && Maths.CrossVertex(communBiome.Point, newEdge)) {
                                this.edges.Add(newEdge);
                            }
                        }
                    }
                }
            }

/*            if (this.nodes.Count > 0) {

                Node? bestNode = null;
				float minDistance = float.MaxValue;
				foreach (Node node in this.Nodes) {
                    float distance = Maths.CalculeDistance(node.position, this.player.position);

					if (distance < minDistance) {
                        minDistance = distance;
						bestNode = node;
                    }
				}

				Biome? playerBiome = null;
                Node playerNode = new Node(this.player.position, playerBiome);

                this.nodes.Add(playerNode);
                this.edges.Add(new Edge(bestNode, playerNode, playerBiome));
			}*/
        }		
        
        public void GeneretePlayer() {
            int maxEdge = 5;
            int edge = 0;

			List<Tuple<float, Node>> startDistancesNodes = new List<Tuple<float, Node>>();
			List<Tuple<float, Node>> endDistancesNodes = new List<Tuple<float, Node>>();
			foreach(Node node in this.nodes) {
                if(node != this.player.startPosition && node != this.player.endPosition) {
                    float startDistance = Maths.CalculeDistance(node.position, this.player.startPosition.position);
                    float endDistance = Maths.CalculeDistance(node.position, this.player.endPosition.position);

                    startDistancesNodes.Add(new Tuple<float, Node>(startDistance, node));
                    endDistancesNodes.Add(new Tuple<float, Node>(endDistance, node));
                }
			}

            if(startDistancesNodes.Count > 0) {

                startDistancesNodes.Sort((tuple1, tuple2) => tuple1.Item1.CompareTo(tuple2.Item1));

                if(startDistancesNodes.Count < maxEdge){
                    edge = startDistancesNodes.Count;
				}else{
                    edge = maxEdge;   
                }

                for(int i = 0; i < edge; i++) {
                    this.edges.Add(new Edge(startDistancesNodes[i].Item2, this.player.startPosition, null));
                }

				this.nodes.Add(this.player.startPosition);
			}

            //----------------\\

            if(endDistancesNodes.Count > 0) {

                endDistancesNodes.Sort((tuple1, tuple2) => tuple1.Item1.CompareTo(tuple2.Item1));

				if(endDistancesNodes.Count < maxEdge) {
					edge = endDistancesNodes.Count;
				} else {
					edge = maxEdge;
				}

				for(int i = 0; i < edge; i++) {
                    this.edges.Add(new Edge(endDistancesNodes[i].Item2, this.player.endPosition, null));
                }
				this.nodes.Add(this.player.endPosition);
			}

		}

		public void AddBiome(Biome biome) {
            if(biome != null) {
                this.biomes.Add(biome);
            }
        }

        public void RemoveBiome(Biome biome) {
            if(biome != null) {
                this.biomes.Remove(biome);
            }
        }

        public void AddObstacle(Obstacle obstacle) {
            if(obstacle != null) {
                this.obstacles.Add(obstacle);
            }
        }

        public void RemoveObstacle(Obstacle obstacle) {
            if(obstacle != null) {
                this.obstacles.Remove(obstacle);
            }
        }

		public override string ToString() {
            string content = "";

            content += "--------------MAP-------------- \n";

			content += "---------Dijstra--------- \n";
            content += this.dijkstra.ToString();

			content += "---------Nodes--------- \n";

            foreach(Node node in this.nodes) {
                content += node.ToString() + "\n";
            }

			content += "---------Edges--------- \n";

			foreach(Edge edge in this.edges) {
				content += edge.ToString() + "\n";
			}

			content += "---------Biomes-------- \n";

			foreach(Biome.BiomeType type in (Biome.BiomeType[])Enum.GetValues(typeof(Biome.BiomeType))) {
                int count = 0;

                foreach(Biome biome in this.biomes) {
                    if(biome.Type == type) {
                        count++;
                    }
                }

                content += type.ToString() + ": " + count + " \n";
            }

			content += "---------Obstacle------ \n";
            content += "OBSTACLE: " + this.obstacles.Count + "\n";

			content += "-------------------------------";

			return content;
		}


	}
}
