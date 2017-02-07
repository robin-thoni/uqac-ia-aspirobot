using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace uqac_ia_aspirobot.Common
{
    public class ArStreamString
    {
        private readonly Stream ioStream;
        private readonly UnicodeEncoding streamEncoding;
        private readonly IList<string> _writtenStrings = new List<string>();
        private readonly IList<string> _readStrings = new List<string>();

        public ArStreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            int len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            var str = streamEncoding.GetString(inBuffer);
            _readStrings.Add(str);
            return str;
        }

        public int ReadInt()
        {
            var str = ReadString();
            return int.Parse(str);
        }

        public T ReadEnum<T>()
        {
            var str = ReadString();
            return (T)Enum.Parse(typeof(T), str);
        }

        public int Write(string outString)
        {
            _writtenStrings.Add(outString);
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue)
            {
                len = (int)UInt16.MaxValue;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }

        public int Write(int outInt)
        {
            return Write(outInt.ToString());
        }

        public int Write(Enum outEnum)
        {
            return Write(outEnum.ToString());
        }
    }
}