﻿using Xunit;

namespace TGIT.ACME.Protocol.Model.Tests.Model_Initialization
{
    public class Nonce
    {
        [Fact]
        public void Ctor_Populates_All_Properties()
        {
            var token = "ABC";
            var sut = new Model.Nonce(token);

            Assert.Equal(token, sut.Token);
        }
    }
}
