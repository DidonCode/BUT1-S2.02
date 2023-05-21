using SAE_Graphe.Biomes;
using SAE_Graphe.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_Graphe.Game {

    public class Player {

        public Node startPosition;
        public Node endPosition;

		public Player() {
			Biome nullBiome = null;
			this.startPosition = new Node(new Vertex(0, 0), nullBiome);
			this.endPosition = new Node(new Vertex(0, 0), nullBiome);
		}

    }
}
