using SAE_Graphe.Biomes;
using System.Windows.Media;

namespace SAE_Graph.Biomes {

    public class Montaigne : Biome {

        public override Brush Color {
            get { return Brushes.DarkGray; }
        }

        public Montaigne() : base(0.7f, BiomeType.MONTAIGNE) { }
    }
}
