using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Blockchain.Block;

namespace Blockchain
{
    /// <summary>
    /// A readonly Collection to hold Blocks
    /// </summary>
    public class BlockChain : IReadOnlyCollection<string>
    {
        private readonly LinkedList<string> Blocks;
        private readonly string Prefix;
        
        public string this[int index] { get => Blocks.ElementAt(index); }

        /// <summary>
        /// Returns the number of blocks in the chain
        /// </summary>
        public int Count => Blocks.Count;
        /// <summary>
        /// Returns whether or not the chain is empty
        /// </summary>
        public bool Empty => Blocks.Count == 0;

        /// <summary>
        /// Returns if the chain is readonly
        /// </summary>
        public static bool IsReadOnly => true;

        /// <summary>
        /// Constructs a BlockChain object
        /// </summary>
        /// <param name="prefix">the string to prefix every block data with</param>
        public BlockChain(string prefix)
        {
            Prefix = prefix;
            Blocks = new LinkedList<string>();
        }

        /// <summary>
        /// Checks if the block is equal to the block at <paramref name="index"/> 
        /// </summary>
        /// <param name="block">the block to compare</param>
        /// <param name="index">the index in the chain</param>
        /// <returns>whether or not the block is equal to the block at <paramref name="index"/></returns>
        public bool Equals(IBlock block, int index)
        {
            // create a copy to avoid mutation
            IBlock b = block.Copy();
            b.PreviousHash = this[index - 1];
            b.Data = Prefix + block.Data;
            return b.Hash() == this[index];
        }

        public void Add(IBlock block_)
        {
            // create a copy to avoid mutation
            IBlock block = block_.Copy();
            // if the block is the root block set the previous hash to an empty string
            block.PreviousHash = Blocks.Count == 0 ? string.Empty : Blocks.Last.Value;
            // add the Prefix to the beginning of the hash
            block.Data = Prefix + block.Data;
            // insert the block in the list
            Blocks.AddLast(block.Hash());
        }

        /// <summary>
        /// Clears the contents of the chain
        /// </summary>
        public void Clear()
        {
            // clear the LinkedList
            Blocks.Clear();
        }

        /// <summary>
        /// Copies the hash at <paramref name="arrayIndex"/> to <paramref name="array"/>
        /// </summary>
        /// <param name="array">the array to copy to</param>
        /// <param name="arrayIndex">the index to copy to/from</param>
        public void CopyTo(string[] array, int arrayIndex)
        {
            array[arrayIndex] = Blocks.ElementAt(arrayIndex);
        }

        public bool Contains(IBlock block)
        {
            // create copy of block to avoid mutation
            IBlock b = block.Copy();

            b.Data = Prefix + block.Data;

            if (this[0] == b.Hash())
                return true;

            for (int i = 1; i < Blocks.Count; i++)
            {
                b.PreviousHash = this[i - 1];
                if (b.Hash() == this[i])
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>the enumerator for the chain</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return Blocks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Blocks.GetEnumerator();
        }
    }
}
