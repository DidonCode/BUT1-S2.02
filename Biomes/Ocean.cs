using SAE_Graphe.Biomes;
using System.Windows.Media;

namespace SAE_Graph.Biomes {

    public class Ocean : Biome {

        public override Brush Color {
            get { return Brushes.LightSteelBlue; }
        }

        public Ocean() : base(0.5f, BiomeType.OCEAN) { }
    }
}
