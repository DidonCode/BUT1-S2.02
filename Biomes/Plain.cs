using SAE_Graphe.Biomes;
using System.Windows.Media;

namespace SAE_Graph.Biomes {

    public class Plain : Biome { 
        public override Brush Color {
            get { return Brushes.LightGreen; }
        }

        public Plain() : base(2, BiomeType.PLAIN) { }
    }
}
