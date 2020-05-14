using Knuddels.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Popup
{
    class PopupWriter : PacketBuilder
    {
        public PopupWriter(String opcode)
        : base(opcode)
        {
        }

        public void WriteSize(int size)
        {
            Write('A' + size);
        }

        public void WritePopupString(String str)
        {
            WriteString(str);
            Write(0xF5);
        }

        public void WriteFontStyle(int weight, int size)
        {
            if (weight != 'p')
            {
                Write(weight);
            }

            Write('g');
            WriteSize(size);
        }

        public void WriteLayout(int layout)
        {
            Write(layout);
        }

        public void WriteFrameSize(int width, int height)
        {
            Write('s');
            WriteShort(width);
            WriteShort(height);
        }

        public void WriteForeground(int[] color)
        {
            Write('f');
            Write(color);
        }

        public void WriteBackground(int[] color, char op = 'h')
        {
            Write(op);
            Write(color);
        }

        public void WriteBackgroundImage(String image, int position)
        {
            Write('i');
            WritePopupString(image);
            WriteShort(position);
        }

        public void WriteEnd()
        {
            Write(0xE3);
        }
    }
}
