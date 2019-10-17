using System;
using Xunit;
using static WordGuessGame.Program;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("string", ViewWords());
        }
    }
}
