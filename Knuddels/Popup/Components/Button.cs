using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup.Components
{
    class Button : IComponent
    {
        public ComponentType Type { get { return ComponentType.BUTTON; } }
        public int[] Background { get;  set; }
        public int[] Foreground { get;  set; }
        public string Text { get;  set; }
        public Location Location { get;  set; }
        public string Command { get;  set; }
        public bool Styled { get; set; }
        public bool Colored { get; set; }
        public bool Close { get; set; }
        public bool Action { get;  set; }
        public char FontWeight { get; set; }
        public int FontSize { get; set; }

        public Button(string text)
            : this(text, Location.NONE) { }
        public Button(String text, Location location)
        {
            this.Foreground = new int[] { 0x00, 0x00, 0x00 };
            this.Background = new int[] { 0xBE, 0xBC, 0xFB };
            this.Text = text;
            this.Styled = false;
            this.Close = true;
            this.Action = false;
            this.Command = null;
            this.Location = location;
            this.FontWeight = 'b';
            this.FontSize = 16;
        }

        public void SetStyled(bool colored)
        {
            this.Styled = true;
            this.Colored = colored;
        }
    }
}
