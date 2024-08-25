using NUnit.Framework;
using System;
using System.Net;
using Newtonsoft.Json;
using www778878net.net;
namespace Tests
{
    [TestFixture]
    public class UpInfo78Tests
    {
        [Test]
        public void TestToUrlEncode_DefaultValues()
        {
            // Arrange1
            var upInfo = new UpInfo78();

            // Act
            var urlEncoded = upInfo.ToUrlEncode();

            // Assert
            Assert.AreEqual("sid=", urlEncoded);
        }

        [Test]
        public void TestToUrlEncode_WithCustomValues()
        {
            // Arrange
            UpInfo78.pcid = "PC123";
            var upInfo = new UpInfo78
            {
                uname = "testuser",
                cid = "cidguest",
                bcid = "cidvps",
                sid = "session123",
                cache = "cache123",
                mid = "custom_mid",
              
                order = "name",
                desc = 1,
                getnumber = 50,
                getstart = 10,
                Midpk = 12345,
                pars = new string[] { "param1", "param2" },
                cols = new string[] { "col1", "col2" }
            };

            // Act
            var urlEncoded = upInfo.ToUrlEncode();

            // Assert
            Assert.AreEqual("sid=session123&uname=testuser&bcid=28401227-bd00-a20f-c561-ddf0def881d9&cache=cache123&mid=custom_mid&pcid=PC123&order=name%20desc&getnumber=50&getstart=10&midpk=12345&pars=%5B%22param1%22%2C%22param2%22%5D&cols=[\"col1\",\"col2\"]", urlEncoded);
        }
    }
}