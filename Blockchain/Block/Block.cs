using Blockchain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Block
{
    public class Block : IBlock
    {
        public string DataHash { get; private set; }
        public string PreviousHash { get; set; }
        public string Data { get; set; }
        public long TimeStamp { get; init; }
        public int Nonce { get; private set; }

        public string Hash()
        {
            string dataAsHash = PreviousHash +
                                TimeStamp +
                                Nonce +
                                Data;

            return Hasher.Sha256(data: dataAsHash);
        }

        public string Mine(int prefix)
        {
            string prefixString = new string(new char[prefix]).Replace('\0', '0');
            while (DataHash.Substring(0, prefix) != prefixString)
            {
                Nonce++;
                DataHash = Hash();
            }

            return DataHash;
        }

        public IBlock Copy()
        {
            return (IBlock)MemberwiseClone();
        }
    }
}
