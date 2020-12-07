namespace CliHelper.Tests
{
    using CliHelper.Models;
    using CliHelper.Models.Parameters;
    using NUnit.Framework;

    /// <summary>
    /// Tests of the class <see cref="CliApplication"/>.
    /// </summary>
    [TestFixture]
    public class CliApplicationTests
    {
        private bool _callbackCalled;

        /// <summary>
        /// CliApplication tests setup.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _callbackCalled = false;
        }

        /// <summary>
        /// CliApplication tests tear down.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Assert.True(_callbackCalled);
            _callbackCalled = false;
        }

        /// <summary>
        /// A test of the CliApplication.RegisterCommand method.
        /// </summary>
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

        /// <summary>
        /// A test of the CliApplication.Help to checks if it show the correct output.
        /// </summary>
        [Test]
        public void HelpTest()
        {
            var app = new CliApplication("Test App", new CliOptions(assemblyName: "test"));
            var opCmd = app.RegisterCommand("op", "A mathematical operation.", null);
            opCmd.RegisterParameter("sum", "Sum two numbers.", 's', 2, ParameterType.Number);
            opCmd.RegisterFlag("floor", "Return the greatest integer less or equal the result.", 'f');
            opCmd.RegisterFlag("ceil", "Return the greatest integer greater or equal the result.", 'c');

            const string commandsHelp = @"Test App

Usage: test <command> [options]

Commands:
help | how application commands list.
op   | A mathematical operation.
";

            const string opHelp = @"Test App

Usage: test op [options]

Parameters:
--sum, -s   | Sum two numbers.
--floor, -f | Return the greatest integer less or equal the result.
--ceil, -c  | Return the greatest integer greater or equal the result.
";

            Assert.AreEqual(commandsHelp, app.Help());
            Assert.AreEqual(opHelp, app.Help("op"));
        }
    }
}