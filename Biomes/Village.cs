using SAE_Graphe.Biomes;
using System.Windows.Media;

namespace SAE_Graph.Biomes {

    public class Village : Biome {

        public override Brush Color {
            get { return Brushes.Green; }
        }

        public Village() : base(1, BiomeType.VILLAGE) { }
    }
}
