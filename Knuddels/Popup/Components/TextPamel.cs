using Knuddels.Popup.Layout;
using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup.Components
{
    class TextPanel : IComponent
    {
        public ComponentType Type { get { return ComponentType.TEXT_PANEL; } }
        public int[] Background { get; private set; }
        public int[] Foreground { get; private set; }
        public string Text { get; private set; }
        public Location Location { get; private set; }
        public int BackgroundPosition { get; private set; }
        public int Width { get; private set; }
    public int Height { get; private set; }
    public string BackgroundImage { get; private set; }
    public ILayout Layout { get; private set; }

        public string Id { get; set; }
        public string UpdateId {get; set; }

       public TextPanel(String text, int width, int height, Location loc)
            : this(text, width, height, new int[] { 0, 0, 0 }, new int[] { 0xBE, 0xBC, 0xFB }, "pics/cloudsblue.gif", 17, loc) { }
        public TextPanel(String text, int width, int height, int[] foreground, int[] background, String backgroundImage, int pos, Location loc)
        {
            this.Foreground = foreground;
            this.Background = background;
            this.Text = text;
            this.Location = loc;
            this.BackgroundImage = backgroundImage;
            this.BackgroundPosition = pos;
            this.Height = height;
            this.Width = width;
            Layout = new BorderLayout();
        }
    }
}
