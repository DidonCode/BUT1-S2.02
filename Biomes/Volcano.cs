using SAE_Graphe.Biomes;
using System.Windows.Media;

namespace SAE_Graph.Biomes {

    public class Volcano : Biome { 
        public override Brush Color {
            get { return Brushes.Red; }
        }

        public Volcano() : base(0.8f, BiomeType.VOLCANO) { }
    }
}
