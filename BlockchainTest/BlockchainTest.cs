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
            BlockChain chain = new("test prefix");
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
            BlockChain chain = new("test prefix");
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
            BlockChain chain = new("test prefix");

            Assert.True(chain.Empty);
        }

        [Fact]
        public void ChainIsNotEmpty()
        {
            BlockChain chain = new("test prefix");
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
            BlockChain chain = new("test prefix");
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
            BlockChain chain = new("test prefix");

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
            BlockChain chain = new("test prefix");

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

        [Fact]
        public void ChainClears()
        {
            BlockChain chain = new("prefix");
            for (int i = 0; i < 10; i++)
            {
                chain.Add(new Block
                {
                    Data = i.ToString()
                });
            }
            chain.Clear();
            Assert.True(chain.Empty);
        }

        [Fact]
        public void CheckEqualsFunction()
        {
            BlockChain chain = new("test prefix");

            Block end = new()
            {
                Data = "10"
            };

            for (int i = 0; i < 10; i++)
            {
                chain.Add(new Block
                {
                    Data = i.ToString()
                });
            }

            chain.Add(end);

            Assert.True(chain.Equals(end, 10));
        }

        [Fact]
        public void ChainContainsIsTrue()
        {
            BlockChain chain = new("test prefix");

            Block contain = new()
            {
                Data = "1000"
            };

            for (int i = 0; i < 10; i++)
            {
                chain.Add(new Block
                {
                    Data = i.ToString()
                });
            }

            chain.Add(contain);

            Assert.True(chain.Contains(contain));
        }

        [Fact]
        public void ChainContainsIsFalse()
        {
            BlockChain chain = new("test prefix");

            Block contain = new()
            {
                Data = "1000"
            };

            for (int i = 0; i < 10; i++)
            {
                chain.Add(new Block
                {
                    Data = i.ToString()
                });
            }

            Assert.False(chain.Contains(contain));
        }

        [Fact]
        public void LongData()
        {
            BlockChain chain = new("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            Block b = new()
            {
                Data = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
            };
            chain.Add(b);
        }

        [Fact]
        public void UnicodeData()
        {
            BlockChain chain = new("test seed");
            Block b = new()
            {
                Data = "ьяаоьяаоьяаоь"
            };
            chain.Add(b);
        }
    }
}
