using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knuddels.Tools
{
    class PacketBuilder
    {
        private StringBuilder buffer;

        public PacketBuilder()
        : this("")
        {
        }
        public PacketBuilder(string opcode)
        {
            this.buffer = new StringBuilder(opcode);
        }

        public void WriteChars(char[] v)
        {
            foreach (char c in v)
                this.Write(c);
        }
        public void WriteInt(int v)
        {
            this.buffer.Append(v);
        }
        public void Write(int v)
        {
            this.buffer.Append((char)v);
        }
        public void Write(int[] v)
        {
            foreach (int i in v)
                Write(i);
        }
        public void Write(byte[] v)
        {
            foreach (byte b in v)
                Write(b);
        }
        public void WriteNull()
        {
            this.Write(0x00);
        }
        public void WriteShort(int v)
        {
            Write((v >> 8) & 0xFF);
            Write(v & 0xFF);
        }
        public void WriteString(String v)
        {
            this.WriteChars(v.ToCharArray());
        }

        public override string ToString()
        {
            return this.buffer.ToString();
        }
    }
}
