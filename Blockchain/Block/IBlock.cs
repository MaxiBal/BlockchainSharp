using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Block
{
    public interface IBlock
    {
        string PreviousHash { get; set; }
        string Data { get; set; }
        string Hash();
        string Mine(int prefix);
        IBlock Copy();
    }
}
