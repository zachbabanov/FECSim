using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FECSim
{
    internal class Encoder
    {
        private static readonly byte[] epsilonTable = {
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        byte[][] allKindledData;
        byte[][] ?encodedData;

        public Encoder()
        { 
            allKindledData = new byte[7][];
        }

        public void ProceedFile(string filePath)
        {
            byte[] allFileBytes = System.IO.File.ReadAllBytes(filePath);
            int offset = 0;

            for (int currentPosition = 0; currentPosition < allFileBytes.Length; currentPosition += 8)
            {
                for(int currentByte = 0; currentByte < 7; currentByte++)
                {
                    allKindledData[currentByte][offset] = allFileBytes[currentPosition + currentByte];
                }

                offset++;
            }
        }

        public void Encode()
        {

        }
    }
}
