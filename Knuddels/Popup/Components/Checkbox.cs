using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup.Components
{
    class Checkbox : IComponent
    {
        public ComponentType Type { get { return ComponentType.CHECKBOX; } }
        public int[] Background { get; private set; }
        public int[] Foreground { get; private set; }
        public string Text { get; private set; }
        public Location Location { get; private set; }

        public bool Disabled { get; private set; }
        public bool Selected { get; private set; }
        public byte Group { get; private set; }

        public Checkbox(string text, bool selected, Location location)
        {
            this.Foreground = new int[] { 0x00, 0x00, 0x00 };
            this.Background = new int[] { 0xBE, 0xBC, 0xFB };
            this.Text = text;
            this.Disabled = false;
            this.Selected = selected;
            this.Group = 0;
            this.Location = location;
        }
    }
}
