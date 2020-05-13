using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup.Components
{
    class Choice : IComponent
    {
        public ComponentType Type { get { return ComponentType.CHOICE; } }
        public int[] Background { get; private set; }
        public int[] Foreground { get; private set; }
        public string Text { get; private set; }
        public Location Location { get; private set; }

        public int SelectedIndex { get; private set; }
        public String[] Items { get; private set; }
        public bool Disabled { get; private set; }
        public string Selected { get; private set; }
        public int Fontsize { get; private set; }

        public Choice(string[] items, Location loc)
            : this(items, null, loc) { }
        public Choice(string[] items, string selected, Location loc)
            : this(items, selected, new int[] { 0x00, 0x00, 0x00 }, new int[] { 0xFF, 0xFF, 0xFF }, -1, false, loc) { }
        public Choice(string[] items, string selected, int[] foreground, int[] background, int fontsize, bool disabled, Location loc)
        {
            this.Items = items;
            this.Selected = selected;
            this.Foreground = foreground;
            this.Background = background;
            this.Fontsize = fontsize;
            this.Disabled = disabled;
            this.Location = loc;
        }
    }
}
