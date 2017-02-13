using System;
using System.IO;
using System.Text;

namespace uqac_ia_aspirobot.Common
{
    public class ArStreamString
    {
        private readonly Stream _ioStream;
        private readonly UnicodeEncoding _streamEncoding;

        public ArStreamString(Stream ioStream)
        {
            _ioStream = ioStream;
            _streamEncoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            var len = _ioStream.ReadByte() * 256;
            len += _ioStream.ReadByte();
            var inBuffer = new byte[len];
            _ioStream.Read(inBuffer, 0, len);

            var str = _streamEncoding.GetString(inBuffer);
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
            var outBuffer = _streamEncoding.GetBytes(outString);
            var len = outBuffer.Length;
            if (len > ushort.MaxValue)
            {
                len = ushort.MaxValue;
            }
            _ioStream.WriteByte((byte)(len / 256));
            _ioStream.WriteByte((byte)(len & 255));
            _ioStream.Write(outBuffer, 0, len);
            _ioStream.Flush();

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