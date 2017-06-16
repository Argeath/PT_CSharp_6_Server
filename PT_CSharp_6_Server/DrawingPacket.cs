using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PT_CSharp_6
{
    class DrawingPacket
    {
        public float posX;
        public float posY;

        public DrawingPacket()
        {
        }


        public static DrawingPacket FromBytes(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter binForm = new BinaryFormatter();
                stream.Write(bytes, 0, bytes.Length);
                stream.Seek(0, SeekOrigin.Begin);

                DrawingPacket obj = new DrawingPacket();
                obj = (DrawingPacket) binForm.Deserialize(stream);

                return obj;
            }
        }
    }

    class DrawingResponsePacket : DrawingPacket
    {
        public byte clientId;

        public DrawingResponsePacket() { }

        public DrawingResponsePacket(DrawingPacket p, byte id)
        {
            posX = p.posX;
            posY = p.posY;
            clientId = id;
        }

        public byte[] ToBytes()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                return stream.ToArray();
            }
        }
    }
}
