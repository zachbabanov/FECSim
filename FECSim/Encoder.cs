namespace FECSim
{
    public readonly struct Epsilon
    {
        /// <summary>
        /// This table contains members of Galois Field in plain of 8
        /// in hexadecimal format, wherein generatin polinom is equal to 
        /// P(x) = x^8 + x^4 + x^3 + x^2 + x^0 = 0x10111000
        /// </summary>
        public static readonly List<byte> Table = new() {
            0x80, 0x40, 0x20, 0x10, 0x08, 0x04, 0x02, 0x01,
            0xb8, 0x5c, 0x2e, 0x17, 0xb3, 0xe1, 0xc8, 0x64,
            0x32, 0x19, 0xb4, 0x5a, 0x2d, 0xae, 0x57, 0x93,
            0xf1, 0xc0, 0x60, 0x30, 0x18, 0x0c, 0x06, 0x03,
            0xb9, 0xe4, 0x72, 0x39, 0xa4, 0x52, 0x29, 0xac,
            0x56, 0x2b, 0xad, 0xee, 0x77, 0x83, 0xf9, 0xc4,
            0x62, 0x31, 0xa0, 0x50, 0x28, 0x14, 0x0a, 0x05,
            0xba, 0x5d, 0x96, 0x4b, 0x9d, 0xf6, 0x7b, 0x85,
            0xfa, 0x7d, 0x86, 0x43, 0x99, 0xf4, 0x7a, 0x3d,
            0xa6, 0x53, 0x91, 0xf0, 0x78, 0x3c, 0x1e, 0x0f,
            0xbf, 0xe7, 0xcb, 0xdd, 0xd6, 0x6b, 0x8d, 0xfe,
            0x7f, 0x87, 0xfb, 0xc5, 0xda, 0x6d, 0x8e, 0x47,
            0x9b, 0xf5, 0xc2, 0x61, 0x88, 0x44, 0x22, 0x11,
            0xb0, 0x58, 0x2c, 0x16, 0x0b, 0xbd, 0xe6, 0x73,
            0x81, 0xf8, 0x7c, 0x3e, 0x1f, 0xb7, 0xe3, 0xc9,
            0xdc, 0x6e, 0x37, 0xa3, 0xe9, 0xcc, 0x66, 0x33,
            0xa1, 0xe8, 0x74, 0x3a, 0x1d, 0xb6, 0x5b, 0x95,
            0xf2, 0x79, 0x84, 0x42, 0x21, 0xa8, 0x54, 0x2a,
            0x15, 0xb2, 0x59, 0x94, 0x4a, 0x25, 0xaa, 0x55,
            0x92, 0x49, 0x9c, 0x4e, 0x27, 0xab, 0xed, 0xce,
            0x67, 0x8b, 0xfd, 0xc6, 0x63, 0x89, 0xfc, 0x7e,
            0x3f, 0xa7, 0xeb, 0xcd, 0xde, 0x6f, 0x8f, 0xff,
            0xc7, 0xdb, 0xd5, 0xd2, 0x69, 0x8c, 0x46, 0x23,
            0xa9, 0xec, 0x76, 0x3b, 0xa5, 0xea, 0x75, 0x82,
            0x41, 0x98, 0x4c, 0x26, 0x13, 0xb1, 0xe0, 0x70,
            0x38, 0x1c, 0x0e, 0x07, 0xbb, 0xe5, 0xca, 0x65,
            0x8a, 0x45, 0x9a, 0x4d, 0x9e, 0x4f, 0x9f, 0xf7,
            0xc3, 0xd9, 0xd4, 0x6a, 0x35, 0xa2, 0x51, 0x90,
            0x48, 0x24, 0x12, 0x09, 0xbc, 0x5e, 0x2f, 0xaf,
            0xef, 0xcf, 0xdf, 0xd7, 0xd3, 0xd1, 0xd0, 0x68,
            0x34, 0x1a, 0x0d, 0xbe, 0x5f, 0x97, 0xf3, 0xc1,
            0xd8, 0x6c, 0x36, 0x1b, 0xb5, 0xe2, 0x71
        };

        /*private static void Calculate(string path)
        {
            byte[] table = new byte[256];

            byte start = 0b_10000000;
            byte based = 0b_10111000;

            table[0] = start;

            File.WriteAllText(path, "");
            File.AppendAllText(path, "0x" + Convert.ToString(table[0], 16) + ", ");
            for (int i = 1; i < table.Length - 1; i++)
            {
                if (table[i - 1] % 0b_10 == 1)
                {
                    table[i] = (byte)(table[i - 1] >> 1);
                    table[i] ^= based;
                }
                else
                    table[i] = (byte)(table[i - 1] >> 1);
                File.AppendAllText(path, "0x" + Convert.ToString(table[i], 16) + ", ");
            }
        }*/
    }

    /// <summary>
    /// Single position based sector for encoding adder chain,
    /// where 2^position is power of epsilion either index of Galois Field member 
    /// </summary>
    public class Cell
    {
        public byte _output;
        public bool wasFirstHunge = false;

        public readonly int _position;

        public Cell(int position)
        {
            _position = position;
        }

        /// <summary>
        /// Cause of data based conditions in processing first step in each sector
        /// must be calculated on input byte data. After that, calculations continuesly
        /// forwarding basing on previous step
        /// </summary>
        public void StartHinge(byte data)
        {
            /*int inputAndPostionSum = (int)((Epsilon.Table.IndexOf(data) + Math.Pow(2, _position)) % 255);
            _output = Epsilon.Table[inputAndPostionSum];*/

            _output = data;
            wasFirstHunge = true;
        }

        public void Step()
        {
            if (!wasFirstHunge)
                throw new Exception("First Hinge of Cell was not started before cycling");

            int containIndex = (int)((Epsilon.Table.IndexOf(_output) + Math.Pow(2, _position)) % 255);
            _output = Epsilon.Table[containIndex];
        }
    }

    /// <summary>
    /// Adder for multiply sector or other adders combination
    /// </summary>
    public class Adder
    {
        private readonly byte[] _inputs;
        public byte _output;

        public Adder(int numberOfInputs)
        {
            _inputs = new byte[numberOfInputs];
        }

        /// <summary>
        /// Get outputs of each cell and set them as inputs of adder
        /// </summary>
        public void GetInputs(Cell[] cells)
        {
            int indexOfCurrentCell = 0;

            foreach (Cell cell in cells)
            {
                _inputs[indexOfCurrentCell] = cell._output;
                indexOfCurrentCell++;
            }
        }

        /// <summary>
        /// Get output of each adder and set them as inputs of this adder 
        /// </summary>
        public void GetInputs(Adder[] adders)
        {
            int indexOfCurrentAdder = 0;

            foreach (Adder adder in adders)
            {
                _inputs[indexOfCurrentAdder] = adder._output;
                indexOfCurrentAdder++;
            }
        }

        public byte Summarize()
        {
            _output = 0;

            foreach (byte input in _inputs)
                _output ^= input;

            return _output;
        }
    }

    /// <summary>
    /// Full encode chain of n sectors
    /// </summary>
    public class Encoder
    {
        private readonly Cell[] EncodeCells;
        private readonly Adder Adder;

        private int MAXOffset;

        private byte[]? readBuffer;
        public byte[,] encodedSequence;

        public Encoder(int numberOfCells)
        {
            EncodeCells = new Cell[numberOfCells];
            for (int currentCell = 0; currentCell < numberOfCells; currentCell++)
                EncodeCells[currentCell] = new Cell(currentCell);

            Adder = new(numberOfCells);
            Adder.GetInputs(EncodeCells);

            encodedSequence = new byte[numberOfCells, 255];
        }

        /// <summary>
        /// Read all bytes of file specified by path in buffer.
        /// Also sets MAXOffset as number of octets readed bytes
        /// </summary>
        public void Read(string filePath)
        {
            readBuffer = File.ReadAllBytes(filePath);
            MAXOffset = readBuffer.Length % EncodeCells.Length == 0 ?
                readBuffer.Length / EncodeCells.Length : readBuffer.Length / EncodeCells.Length + 1;

            encodedSequence = new byte[MAXOffset, 255];
        }

        private byte[,] SplitReadBuffer()
        {
            byte[,] splitedReadBuffer = new byte[MAXOffset, EncodeCells.Length];

            if (readBuffer == null)
                throw new NullReferenceException();

            int currentOctet = 0;
            int currentByteOfOctet = 0;
            foreach (byte b in readBuffer)
            {
                splitedReadBuffer[currentOctet, currentByteOfOctet] = b;
                currentByteOfOctet++;

                if (currentByteOfOctet % 8 == 0)
                {
                    currentByteOfOctet = 0;
                    currentOctet++;
                }
            }

            return splitedReadBuffer;
        }

        /// <summary>
        /// Encoding byte[] got from Read() into encodedSequence[,] spliteted by octets 
        /// with overhead up to 255 bytes for each octet
        /// </summary>
        public void Encode()
        {
            byte[,] splitedReadBuffer = SplitReadBuffer();

            //offset here is Octet in splited data
            for (int currentOffset = 0; currentOffset < MAXOffset; currentOffset++)
            {
                //for each new octet of bytes we need to set sector for default position to calculate from blank
                for (int currentCell = 0; currentCell < EncodeCells.Length; currentCell++)
                    EncodeCells[currentCell].wasFirstHunge = false;

                //repeating encoding for overhead to 255 bytes for each octet
                for (int currentIteration = 0; currentIteration < 255; currentIteration++)
                {
                    for (int currentByte = 0; currentByte < EncodeCells.Length; currentByte++)
                    {
                        if (!EncodeCells[currentByte].wasFirstHunge)
                            EncodeCells[currentByte].StartHinge(splitedReadBuffer[currentOffset, currentByte]);
                        else
                            EncodeCells[currentByte].Step();
                    }

                    Adder.GetInputs(EncodeCells);
                    encodedSequence[currentOffset, currentIteration] = Adder.Summarize();
                }
            }
        }
    }
}