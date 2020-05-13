using Knuddels.Popup.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup
{
    interface IComponent
    {
        ComponentType Type { get; }
        int[] Foreground { get; }
        int[] Background { get; }
        string Text { get; }
        Location Location { get; }
    }
}
