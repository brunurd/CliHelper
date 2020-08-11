using NUnit.Framework;

namespace LavaLeak.CliHelper.Tests
{
    public class ExtensionMethodsTests
    {
        private bool _callbackCalled;

        [SetUp]
        public void SetUp()
        {
            _callbackCalled = false;
        }

        [TearDown]
        public void TearDown()
        {
            Assert.True(_callbackCalled);
            _callbackCalled = false;
        }

        [Test]
        public void GetParametersByNameTest()
        {
            var app = new CliApplication("Test");

            var command = app.RegisterCommand("export", "Export something.", 
                (parameters, flags) =>
            {
                var path = parameters.GetParameterByName("path");
                Assert.AreEqual("/test/test", path.Values[0]);
                _callbackCalled = true;
            });

            command.RegisterParameter("path", "Set the path to export.");

            app.Run(new[] {"export", "--path", "/test/test"});
        }

        [Test]
        public void HasFlagTest()
        {
            var app = new CliApplication("Test");

            var command = app.RegisterCommand("export", "Export something.", 
                (parameters, flags) =>
            {
                var obfuscate = flags.HasFlag("obfuscate");
                Assert.True(obfuscate);
                _callbackCalled = true;
            });

            command.RegisterFlag("obfuscate", "Obfuscate the exported files.", 'o');

            app.Run(new[] {"export", "-o"});
        }
    }
}