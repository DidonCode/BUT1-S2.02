using SAE_Graphe.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SAE_Graph.Obstacles {

    public class Obstacle {

        public List<Vertex> points;
        public List<Vertex> Points {
            get { return points; }
            set { points = value; }
        }
		public Brush Color {
			get { return Brushes.Gray; }
		}

		public Obstacle(List<Vertex> points) {
            this.points = points;
        }

    }
}
