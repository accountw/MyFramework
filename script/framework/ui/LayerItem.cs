using System.Collections.Generic;

namespace Framework.UI
{
    public class LayerItem
    {
        public int startZ = 0;
        public int z = 0;
        public List<BaseView> panels = new List<BaseView>();

        public LayerItem(int startZ, int z)
        {
            this.startZ = startZ;
            this.z = z;
        }
    }
}