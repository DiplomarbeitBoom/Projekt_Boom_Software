using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datatemplates.CustomObjects
{
    public class CGraph: BidirectionalGraph<Object, CEdge>
    {
        public CGraph()
        {

        }

        public CGraph(bool allowParallelEdges)
            : base (allowParallelEdges) {}

        public CGraph(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }
    }
}
