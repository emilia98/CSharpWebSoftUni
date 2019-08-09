using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MvcFramework.Tests
{
    public class TestSumming
    {
        [Fact]
        public void Summing()
        {
            Assert.Equal(4, 2 + 3);
        }

        [Theory]
        [InlineData(2 + 2, 4)]
        [InlineData(2 + 3, 4)]
        public void Summing2(int result, int expected)
        {
            Assert.Equal(expected, result);
        }
    }
}
