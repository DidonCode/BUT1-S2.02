using SAE_Graph.Other;
using SAE_Graphe.Biomes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SAE_Graphe.Graph
{

    public class Edge {

        public Node start;
        public Node Start {
            get { return start; }
            set { start = value; }
        }

        public Node end;
        public Node End {
            get { return end; }
            set { end = value; }
        }

        public float weight;
        public float Weight {
            get { return weight; } 
            set { weight = value; }
        }

        public Biome biome;
        public Biome Biome {
            get { return biome; }
            set { biome = value; }
        }

        public Edge(Node start, Node end, Biome biome) {
            this.start = start;
            this.end = end;
            this.biome = biome;
            CalculeWeight();
		}

        public void CalculeWeight() {
            if (biome == null) {
                this.weight = Maths.CalculeDistance(start.position, end.position) * (Biome.maxSpeed + 0.1f);
            } else {
                this.weight = Maths.CalculeDistance(start.position, end.position) * (Biome.maxSpeed - biome.speed + 0.1f);
            }
		}

        public bool IsEqual(Edge edge, float margin) {
            return (this.start.IsEqual(edge.start, margin) && this.end.IsEqual(edge.end, margin)) || (this.start.IsEqual(edge.end, margin) && this.end.IsEqual(edge.start, margin));
        }

		public override string ToString() {
            if(this.biome == null) {
				return "Edge{ Start " + this.start.ToString() + "; End " + this.end.ToString() + "; Weight: " + this.weight + "; Biome: NULL }";
			}
            return "Edge{ Start " + this.start.ToString() + "; End " + this.end.ToString() + "; Weight: " + this.weight + "; Biome: " + this.biome.ToString() + " }";
		}
	}
}
