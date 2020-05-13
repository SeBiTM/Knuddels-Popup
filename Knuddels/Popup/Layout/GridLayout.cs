using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup.Layout
{
    class GridLayout : ILayout
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int HGap { get; set; }
        public int VGap { get; set; }

        public LayoutType Type => LayoutType.GRID_LAYOUT;

        public GridLayout(int rows, int cols)
            : this(rows, cols, 0, 0)
        { }
        public GridLayout(int rows, int cols, int hgap, int vgap)
        {
            this.Rows = rows;
            this.Cols = cols;
            this.HGap = hgap;
            this.VGap = vgap;
        }

    }
}
