using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup.Components
{
    class TextField : IComponent
    {
        public ComponentType Type { get { return ComponentType.TEXT_FIELD; } }
        public int[] Background { get; private set; }
        public int[] Foreground { get; private set; }
        public string Text { get; private set; }
        public Location Location { get; private set; }
        public byte Width { get; private set; }

        public TextField(int width, Location location)
        {
            this.Foreground = new int[] { 0x00, 0x00, 0x00 };
            this.Background = new int[] { 0xFF, 0xFF, 0xFF };
            this.Text = "";
            this.Width = (byte)width;
            this.Location = location;
        }

    }
}
