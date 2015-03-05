using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datatemplates
{
    public abstract class Rootable
    {
        public Boolean isRoot;
        public void SetAsRoot(){
            isRoot = true;
        }
        public void UnsetAsRoot() {
            isRoot = false;
        }
    }
}
