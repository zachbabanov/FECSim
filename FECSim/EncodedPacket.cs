namespace FECSim
{
    /// <summary>
    /// Class for tranfroming already encoded data from Ecnoder class
    /// to a List of byte arrays. Each one of thoose are ready-to-send
    /// byte array for Sink/Source classes
    /// </summary>
    public class EncodedPacket
    {
        private readonly List<byte[]> encodedPackets;
        private readonly int numberOfSequences;
        private readonly int sequenceLenght;

        public EncodedPacket(int sequenceLenght, int packetLenght)
        {
            this.sequenceLenght = sequenceLenght;

            if (packetLenght / sequenceLenght < 1)
                while (packetLenght / sequenceLenght < 1)
                    packetLenght *= 2;

            numberOfSequences = packetLenght / sequenceLenght >= 8 ? 8 : packetLenght / sequenceLenght < 2 ? 1 : 2;
            encodedPackets = new List<byte[]>();
        }

        /// <summary>
        /// Splits the encoded data from Encoder class(encodedSequencce object reffered)
        /// to a List<byte[]> of packets, where each byte array representing single packet
        /// containing one or more encoded sequences from Encoder
        /// </summary>
        /// <param name="encodedSequence">Encoded data from Encoder class</param>
        public void SplitSequenceToPackets(byte[,] encodedSequence)
        {
            int numberOfPackets = 255 % numberOfSequences == 0 ? 255 / numberOfSequences : 255 / numberOfSequences + 1;

            for (int currentPacketIndex = 0; currentPacketIndex < numberOfPackets; currentPacketIndex++)
            {
                byte[] currentPacket = new byte[sequenceLenght * numberOfSequences];

                for (int currentSequence = 0; currentSequence < numberOfSequences; currentSequence++)
                    Enumerable.Range(0, encodedSequence.GetLength(0))
                        .Select(x => encodedSequence[x, currentSequence + currentPacketIndex * (numberOfSequences - 1)])
                        .ToArray().CopyTo(currentPacket, currentSequence * sequenceLenght);

                encodedPackets.Add(currentPacket);
            }
        }

        public byte[,] FormatPacketToSequence(byte[] packetRecieved, int singleSequenceLength)
        {
            int amountOfDimensions = packetRecieved.Length / singleSequenceLength;
            byte[,] encodedSequence = new byte[packetRecieved.Length / singleSequenceLength, singleSequenceLength];

            for (int currentSequence = 0; currentSequence < packetRecieved.Length / singleSequenceLength; currentSequence++)
            {
                for (int currentByte = 0; currentByte < singleSequenceLength; currentByte++)
                {
                    encodedSequence[currentSequence, currentByte] = packetRecieved[(amountOfDimensions * currentByte) + currentSequence];
                }
            }

            return encodedSequence;
        }

        /// <summary>
        /// Gets a single packet by index
        /// </summary>
        /// <param name="packetIndex">Index in List<byte[]> of packets</param>
        /// <returns>Byte array representing a packet of data if packet exists,
        /// empty array if packet is null or index out of range</returns>
        public byte[] GetPacket(int packetIndex)
        {
            try { return encodedPackets.ElementAt(packetIndex); }
            catch (ArgumentNullException) { return Array.Empty<byte>(); }
            catch (ArgumentOutOfRangeException) { return Array.Empty<byte>(); }
        }

        /// <summary>
        /// Gets number of sequences in single packet and lenght of each sequence
        /// </summary>
        /// <returns>Byte array of two bytes, where first is number of sequences and second is lenght</returns>
        public byte[] GetPacketInfo()
        {
            byte[] packetInfo = { Convert.ToByte(numberOfSequences), Convert.ToByte(sequenceLenght) };
            return packetInfo;
        }
    }
}