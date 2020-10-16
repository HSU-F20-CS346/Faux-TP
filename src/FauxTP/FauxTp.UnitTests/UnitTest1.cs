using NUnit.Framework;

namespace FauxTp.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Tests to see if a file can be requested properly.
        /// </summary>
        [Test] 
        public void FileRequest()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests to see if the file is sent properly to to recipient
        /// </summary>
        [Test]
        public void FileSend()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests to see if the file is received properly from the sender
        /// </summary>
        public void FileReceive()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests whether the authentication fails when a sedn or receive is sent through.
        /// </summary>
        public void AuthenticationFail()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests to see what happens if the client sends a request while the server is off.
        /// </summary>
        [Test]
        public void ServerOff()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests to see what happens when the Server stops sending mid file
        /// </summary>
        [Test]
        public void ServerPowerFailSend()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests to see what happens when the Server stops receiving mid file
        /// </summary>
        [Test]
        public void ServerPowerFailReceive()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests to see what happens when the Client stops sending mid file
        /// </summary>
        [Test]
        public void ClientPowerFailSend()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests to see what happens when the Client stops sending mid file
        /// </summary>
        [Test]
        public void ClientPowerFailReceive()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests to see if there are non-standard chars in the filename
        /// </summary>
        public void NonStandardChars()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests what happens when a file is requested that doesn't exist
        /// </summary>
        [Test]
        public void RequestingGhostFile()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Tests to see if overwriting a file works
        /// </summary>
        public void OverwritingFile()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test for maximum filename length
        /// </summary>
        [Test]
        public void FileNameTooLong()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test to see if the file extension is a valid file extension.
        /// </summary>
        [Test]
        public void ValidFileExtension()
        {
            Assert.Fail();
        }
    }
}