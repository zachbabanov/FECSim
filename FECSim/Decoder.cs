using System.Text;

namespace FECSim
{
    public class Decoder
    {
        public readonly struct Lambda
        {
            public static readonly List<int> Table = new()
            {
                252, 251,  45,  98,   1, 0, 254, 253,
                249, 247,  90, 196,   2, 0, 253, 251,
                243, 239, 180, 137,   4, 0, 251, 247,
                231, 223, 105,  19,   8, 0, 247, 239,
                207, 191, 210,  38,  16, 0, 239, 223,
                159, 127, 165,  76,  32, 0, 223, 191,
                 63, 254, 75,  152,  64, 0, 191, 127,
                126, 253, 150,  49, 128, 0, 127, 254
            };
        }

        public byte Decode(byte[] encodedPacket, int bytePosition)
        {
            byte summ = 0;

            for (int currentByte = 0; currentByte < 8; currentByte++)
            {
                if (encodedPacket[currentByte] != 0)
                    summ ^= Epsilon.Table.ElementAt((Epsilon.Table.IndexOf(encodedPacket[currentByte]) + Lambda.Table.ElementAt(currentByte + (8 * bytePosition))) % 255);
                else
                    summ ^= 0;
            }

            return (summ);
        }

        public byte[] Decode(byte[,] encodedSequence)
        {
            byte[] decodedData = new byte[encodedSequence.Length];

            for (int i = 0; i < encodedSequence.Length / encodedSequence.GetLength(1); i++)
            {
                byte[] singleSequence = new byte[encodedSequence.GetLength(1)];
                for (int j = 0; j < encodedSequence.GetLength(1); j++)
                {
                    singleSequence[j] = encodedSequence[i, j];
                }

                byte[] sqc = new byte[encodedSequence.GetLength(1)];
                for (int j = 0; j < encodedSequence.GetLength(1); j++)
                {
                    sqc[j] = Decode(singleSequence, j);
                }

                sqc.CopyTo(decodedData, encodedSequence.GetLength(1) * i);
            }

            return decodedData;
        }
    }
}
