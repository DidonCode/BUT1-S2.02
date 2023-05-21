using SAE_Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SAE_Graphe.Graph {

	[DataContract]
	public class Vertex {

		[DataMember]
		public int x;
        public int X {
            get {  return x; } 
            set { x = value; }
        }

		[DataMember]
		public int y;
        public int Y {
            get { return y; }
            set { y = value; }
        }

        public Vertex(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public Vertex Add(Vertex v) {
            this.x += v.x;
            this.y += v.y;

            return this;
        }

        public Vertex Add(int x, int y) {
            this.x += x;
            this.y += y;

            return this;
        }

        public Vertex AddX(int x) {
            this.x += x;

            return this;
        }

        public Vertex AddY(int y) {
            this.y += y;

            return this;
        }

        public bool IsEqual(Vertex vertex, float margin) {
            if(this.x >= vertex.x - margin && this.x <= vertex.x + margin) {
                if(this.y >= vertex.y - margin && this.y <= vertex.y + margin) {
                    return true;
                }
            }
            return false;
        }

		public override string ToString() {
            return "Vertex{ x: " + this.x + "; y: " + this.y + " }";
		}
	}
}
