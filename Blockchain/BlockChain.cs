using Blockchain.Block;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace Blockchain
{
    /// <summary>
    /// A readonly Collection to hold Blocks
    /// </summary>
    public class BlockChain : IReadOnlyCollection<string>
    {
        private readonly LinkedList<string> Blocks;
        private readonly string Seed;

        public string this[int index] { get => Blocks.ElementAt(index); }

        public int Count => Blocks.Count;

        public bool Empty => Blocks.Count == 0;

        public static bool IsReadOnly => true;

        public BlockChain(string seed)
        {
            Seed = seed;
            Blocks = new LinkedList<string>();
        }

        public bool Equals(IBlock block, int index)
        {
            block.PreviousHash = this[index - 1];
            return block.Hash() == this[index];
        }

        public void Add(IBlock block_)
        {
            IBlock block = block_.Copy();
            block.PreviousHash = Blocks.Count == 0 ? "" : Blocks.Last.Value;
            // add the seed to the beginning of the hash
            block.Data = Seed + block.Data;
            Blocks.AddLast(block.Hash());
        }

        public void Clear()
        {
            Blocks.Clear();
        }

        public void CopyTo(string[] array, int arrayIndex)
        {
            array[arrayIndex] = Blocks.ElementAt(arrayIndex);
        }

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
