using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup.Components
{
    class Label : IComponent
    {
        public ComponentType Type { get { return ComponentType.LABEL; } }
        public int[] Background { get;  set; }
        public int[] Foreground { get;  set; }
        public string Text { get;  set; }
        public Location Location { get;  set; }
        public int Size { get;  set; }
        public int Style { get;  set; }

        public Label(String text, Location loc)
            : this(text, loc, 12) { }
        public Label(String text, Location loc, int size)
            : this(text, loc, size, 'p', new int[] { 0xBE, 0xBC, 0xFB }) { }
        public Label(String text, Location loc, int size, char style)
            : this(text, loc, size, style, new int[] { 0xBE, 0xBC, 0xFB }) { }
        public Label(String text, Location loc, int size, char style, int[] background)
        {
            this.Foreground = new int[] { 0x00, 0x00, 0x00 };
            this.Background = background;
            this.Text = text;
            this.Location = loc;
            this.Size = size;
            this.Style = style;
        }

    }
}
