using System;
using Xunit;

using Blockchain;
using Blockchain.Block;
using System.Collections.Generic;

namespace BlockchainTest
{
    public class BlockchainTest
    {
        [Fact]
        public void BlockAndFirstBlockInChainAreNotEqual()
        {
            BlockChain chain = new("test seed");
            IBlock block = new Block
            {
                Data = "test block",
                TimeStamp = new DateTime().Ticks
            };

            chain.Add(block);

            Assert.NotEqual(block.Hash(), chain[0]);
        }

        [Fact]
        public void ChainShouldNotMutateBlock()
        {
            BlockChain chain = new("test seed");
            string blockData = "test block";
            IBlock block = new Block
            {
                Data = blockData,
                TimeStamp = new DateTime().Ticks
            };

            chain.Add(block);

            Assert.Equal(blockData, block.Data);
        }

        [Fact]
        public void ChainIsEmpty()
        {
            BlockChain chain = new("test seed");

            Assert.True(chain.Empty);
        }

        [Fact]
        public void ChainIsNotEmpty()
        {
            BlockChain chain = new("test seed");
            chain.Add(new Block
            {
                Data = "test",
                TimeStamp = new DateTime().Ticks
            });

            Assert.False(chain.Empty);
        }

        [Fact]
        public void BlockAfterFirstBlockIsNotEqual()
        {
            BlockChain chain = new("test seed");
            IBlock block1 = new Block
            {
                Data = "test block 1",
                TimeStamp = new DateTime().Ticks
            };
            IBlock block2 = new Block
            {
                Data = "test block 2",
                TimeStamp = new DateTime().Ticks
            };

            chain.Add(block1);
            chain.Add(block2);

            Assert.NotEqual(block2.Hash(), chain[1]);
        }

        [Fact]
        public void HashesDontRepeat()
        {
            List<string> hashes = new();
            BlockChain chain = new("test seed");

            chain.Add(new Block
            {
                Data = "0",
                TimeStamp = new DateTime().Ticks
            });

            for (int i = 1; i < 10000; i++)
            {
                chain.Add(new Block
                {
                    Data = i.ToString(),
                    TimeStamp = new DateTime().Ticks
                });

                Assert.DoesNotContain(chain[i], hashes);

                hashes.Add(chain[i]);
            }
        }

        [Fact]
        public void HashesDontRepeatWithSameData()
        {
            List<string> hashes = new();
            BlockChain chain = new("test seed");

            string blockData = "testing123";

            chain.Add(new Block 
            {
                Data = blockData,
                TimeStamp = new DateTime().Ticks
            });

            for (int i = 0; i < 10000; i++)
            {
                chain.Add(new Block
                {
                    Data = blockData,
                    TimeStamp = new DateTime().Ticks
                });

                Assert.DoesNotContain(chain[i], hashes);

                hashes.Add(chain[i]);
            }
        }
    }
}
