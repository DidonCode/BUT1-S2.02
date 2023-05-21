using SAE_Graphe.Biomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_Graphe.Graph {

    public class Node {

        public Vertex position;
        public Vertex Position {
            get { return position; }
            set { position = value; }
        }

        public List<Biome> biome;
        public List<Biome> Biome {
            get { return biome; }
            set { biome = value; }
        }

        public Node(Vertex position, List<Biome> biome) { 
            this.position = position;
            this.biome = biome;
        }

		public Node(Vertex position, Biome biome) {
			this.position = position;
			this.biome = new List<Biome>();
            this.biome.Add(biome);
		}

        public void AddBiome(Biome biome) {
            this.biome.Add(biome);
        }

		public bool IsEqual(Node node, float margin) {
            return this.position.IsEqual(node.position, margin);
        }

        public bool IsEqual(Vertex position, float margin) {
            return this.position.IsEqual(position, margin);
        }

        public override string ToString() {
            return "Node{ " + this.position.ToString() + " }";
        }
    }
}
