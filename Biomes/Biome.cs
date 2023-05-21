using SAE_Graph.Biomes;
using SAE_Graphe.Biomes;
using SAE_Graphe.Graph;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Media;
using static SAE_Graphe.Biomes.Biome;

namespace SAE_Graphe.Biomes {

	[Serializable]
	[KnownType(typeof(Forest))]
	[KnownType(typeof(Village))]
	[KnownType(typeof(Ocean))]
	[KnownType(typeof(Volcano))]
	[KnownType(typeof(Montaigne))]
	[KnownType(typeof(Plain))]
	public abstract class Biome {

		public enum BiomeType {FOREST, VILLAGE, OCEAN, VOLCANO, PLAIN, MONTAIGNE}

        public const float maxSpeed = 2;
        public const float minSpeed = 0;

		[DataMember]
		public float speed;
        public float Speed {
            get { return speed; }
        }

		public BiomeType type;
		[DataMember]
		public BiomeType Type {
            get { return type; }
        }

        public abstract Brush Color {
            get;
        }

		[DataMember]
		public List<Vertex> points;
        public List<Vertex> Point {
            get { return points; }
            set { points = value; }
        }

        public Biome(float speed, BiomeType type) {
            this.speed = speed;
            if(speed < minSpeed) this.speed = minSpeed;
            if(speed > maxSpeed) this.speed = maxSpeed;
            
            this.type = type;
            this.points = new List<Vertex>();
        }

        public static Biome GetBiome(BiomeType type) {
            switch (type) {
                case BiomeType.FOREST:
                    return new Forest();

                case BiomeType.VILLAGE:
                    return new Village();

                case BiomeType.OCEAN:
                    return new Ocean();

                case BiomeType.VOLCANO:
                    return new Volcano();

				case BiomeType.MONTAIGNE:
					return new Montaigne();

				case BiomeType.PLAIN:
					return new Plain();

				default:
                    return new Forest();
            }
        }
		public override string ToString() {
            return "Biome{ Name: " + this.Type.ToString() + " }";
		}
	}
}
