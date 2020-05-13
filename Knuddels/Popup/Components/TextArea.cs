using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup.Components
{
    class TextArea : IComponent
    {
        public ComponentType Type { get { return ComponentType.TEXT_AREA; } }
        public int[] Background { get; private set; }
        public int[] Foreground { get; private set; }
        public string Text { get; private set; }
        public Location Location { get; private set; }

        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int Scrollbars { get; private set; }
        public bool Editable { get; private set; }

        public TextArea(int rows, int columns, Location location)
        {
            this.Foreground = new int[] { 0x00, 0x00, 0x00 };
            this.Background = new int[] { 0xFF, 0xFF, 0xFF };
            this.Text = "";
            this.Editable = true;
            this.Scrollbars = 1;
            this.Rows = (byte)rows;
            this.Cols = (byte)columns;
            this.Location = location;
        }

    }
}