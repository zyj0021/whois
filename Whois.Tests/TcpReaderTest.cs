﻿using System;
using System.Collections;
using System.Text;
using NUnit.Framework;
using Whois.Extensions;

namespace Whois
{
    /// <summary>
    /// These tests will only pass if your connected to the Internet
    /// </summary>
    [TestFixture]
    public class TcpReaderTest
    {
        [Test]
        public void TestReadWhoisForCogworksCoUk()
        {
            ArrayList result;

            using (var reader = new TcpReader())
            {
                result = reader.Read("whois.nic.uk", 43, "cogworks.co.uk");
            }

            // Just check the domain name is in the response
            Assert.Greater(result.IndexOfLineContaining("cogworks.co.uk"), -1);
        }

        [Test]
        [Ignore]
        public void TestReadWhoisForSapoPt()
        {
            ArrayList result;
            var encoding = Encoding.GetEncoding("ISO-8859-1");

            using (var reader = new TcpReader(encoding))
            {
                result = reader.Read("whois.dns.pt", 43, "sapo.pt");
            }

            // Just check the domain name is in the response
            Assert.Greater(result.IndexOfLineContaining("sapo.pt"), -1);
        }

        [Test]
        public void TestReadWhoisForUolComBr()
        {
            ArrayList result;
            var encoding = Encoding.GetEncoding("ISO-8859-1");

            using (var reader = new TcpReader(encoding))
            {
                result = reader.Read("registro.br", 43, "uol.com.br");
            }

            // Just check the domain name is in the response
            Assert.Greater(result.IndexOfLineContaining("uol.com.br"), -1);
        }

        [Test]
        public void TestReadWhoisForUnknownDomain()
        {
            ArrayList result;

            using (var reader = new TcpReader())
            {
                result = reader.Read("whois.nic.uk", 43, "invalid domain");
            }

            // SHould never be registered (as invalid)
            Assert.AreEqual(result.IndexOfLineContaining("Registered on:"), -1);
        }

        [Test]
        public void TestReadWhenInvalidHost()
        {
            try
            {
                using (var reader = new TcpReader())
                {
                    reader.Read("invalid domain", 43, "invalid domain");
                }

                Assert.Fail("Should of thrown an exception!");
            }
            catch (ApplicationException)
            {
                // Should thrown an exception
            }
            catch (Exception)
            {
                Assert.Fail("Thrown an unexpected exception!");
            }
        }
    }
}