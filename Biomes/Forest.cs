using SAE_Graphe.Biomes;
using System.Runtime.Serialization;
using System.Windows.Media;

namespace SAE_Graph.Biomes {

	public class Forest : Biome {

        public override Brush Color{
            get { return Brushes.DarkGreen; }
        }

        public Forest() : base(1.5f, BiomeType.FOREST) { }
    }
}
