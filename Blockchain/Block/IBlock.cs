using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.Block
{
    /// <summary>
    /// An interface to implement blocks
    /// </summary>
    public interface IBlock
    {
        /// <summary>
        /// The previous hash in the block
        /// </summary>
        string PreviousHash { get; set; }
        /// <summary>
        /// The data of the block
        /// </summary>
        string Data { get; set; }
        /// <summary>
        /// The hashing function of the block
        /// </summary>
        /// <returns><c>Data</c> hashed</returns>
        string Hash();
        IBlock Copy();
    }
}
