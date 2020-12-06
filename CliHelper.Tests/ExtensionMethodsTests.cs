namespace CliHelper.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Tests of the class <see cref="ExtensionMethods"/>.
    /// </summary>
    public class ExtensionMethodsTests
    {
        private bool _callbackCalled;

        /// <summary>
        /// A ExtensionMethods tests setup.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _callbackCalled = false;
        }

        /// <summary>
        /// A ExtensionMethods tests tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Assert.True(_callbackCalled);
            _callbackCalled = false;
        }

        /// <summary>
        /// A test for the parameters.GetParametersByName method.
        /// </summary>
        [Test]
        public void GetParametersByNameTest()
        {
            var app = new CliApplication("Test");

            var command = app.RegisterCommand("export", "Export something.", (parameters, flags) =>
            {
                var path = parameters.GetParameterByName("path");
                Assert.AreEqual("/test/test", path.Values[0]);
                _callbackCalled = true;
            });

            command.RegisterParameter("path", "Set the path to export.");

            app.Run(new[] { "export", "--path", "/test/test" });
        }

        /// <summary>
        /// A test for the flags.HasFlag method.
        /// </summary>
        [Test]
        public void HasFlagTest()
        {
            var app = new CliApplication("Test");

            var command = app.RegisterCommand("export", "Export something.", (parameters, flags) =>
            {
                var obfuscate = flags.HasFlag("obfuscate");
                Assert.True(obfuscate);
                _callbackCalled = true;
            });

            command.RegisterFlag("obfuscate", "Obfuscate the exported files.", 'o');

            app.Run(new[] { "export", "-o" });
        }
    }
}