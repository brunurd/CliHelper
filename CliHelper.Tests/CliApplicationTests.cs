using NUnit.Framework;

namespace LavaLeak.CliHelper.Tests
{
    [TestFixture]
    public class CliApplicationTests
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
        public void RegisterCommandTest()
        {
            var app = new CliApplication("Test");
            var command = app.RegisterCommand("op", "A mathematical operation.", (parameters, flags) =>
            {
                Assert.AreEqual(1, parameters.Length);
                Assert.AreEqual(2, flags.Length);

                Assert.AreEqual("sum", parameters[0].Parameter.Key.Name);
                Assert.AreEqual('s', parameters[0].Parameter.Key.Shortcut);
                Assert.AreEqual(2, parameters[0].Values.Length);

                Assert.AreEqual("floor", flags[0].Key.Name);
                Assert.AreEqual('f', flags[0].Key.Shortcut);

                Assert.AreEqual("ceil", flags[1].Key.Name);
                Assert.AreEqual('c', flags[1].Key.Shortcut);
                _callbackCalled = true;
            });
            command.RegisterParameter("sum", "Sum two numbers.", 's', 2, ParameterType.Number);
            command.RegisterFlag("floor", "Return the greatest integer less or equal the result.", 'f');
            command.RegisterFlag("ceil", "Return the greatest integer greater or equal the result.", 'c');
            app.Run(new[]
            {
                "op",
                "--sum",
                "12.234",
                "23.122",
                "-f",
                "-c",
            });
        }
    }
}