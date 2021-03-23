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
        public string PreviousHash { get; set; }
        public string Data { get; set; }
        public long TimeStamp { get; init; }

        public string Hash()
        {
            string dataAsHash = PreviousHash +
                                TimeStamp +
                                Data;

            return Hasher.Sha256(data: dataAsHash);
        }
        public IBlock Copy()
        {
            return (IBlock)MemberwiseClone();
        }
    }
}
