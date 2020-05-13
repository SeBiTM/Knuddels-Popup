using Knuddels.Popup.Layout;
using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup.Components
{
    class Panel : IComponent
    {
        public ComponentType Type { get { return ComponentType.PANEL; } }
        public int[] Background { get;  set; }
        public int[] Foreground { get;  set; }
        public string Text { get; private set; }
        public string ID { get; set; }
        public Location Location { get;  set; }
        public List<IComponent> Components;
        public int Width { get;  set; }
        public int Height { get;  set; }
        public string BackgroundImage { get;  set; }
        public ILayout Layout { get;  set; }

        public Panel(Location location)
            : this(null, location) { }
        public Panel(ILayout layout, Location location)
            : this(layout, location, null, 0, 0) { }
        public Panel(ILayout layout)
                    : this(layout, Location.NONE, null, 0, 0) { }

        public Panel(ILayout layout, Location location, String backgroundImage, int height, int width)
        {
            this.Components = new List<IComponent>();
            this.Layout = layout;
            this.Location = location;
            this.BackgroundImage = backgroundImage;
            this.Height = height;
            this.Width = width;
        }
        
        public void AddComponent(IComponent com)
        {
            this.Components.Add(com);
        }
    }
}
