namespace CliHelper.Tests.Models.Parameters
{
    using CliHelper.Models.Parameters;
    using NUnit.Framework;

    /// <summary>
    /// Tests of the class <see cref="ParameterWithValues"/>.
    /// </summary>
    public class ParameterWithValuesTests
    {
        /// <summary>
        /// Test the method ParameterWithValues.GetValue with a null parameter.
        /// </summary>
        [Test]
        public void GetValueWithNullParameterTest()
        {
            ParameterWithValues parameter = null;
            var value = ParameterWithValues.GetValue(parameter);
            Assert.AreEqual(null, value);
        }

        /// <summary>
        /// Test the GetValue method expecting real values.
        /// </summary>
        [Test]
        public void GetValueSuccessTest()
        {
            ParameterWithValues parameter = new ParameterWithValues(null, new[] { "foo", "bar" });
            var value1 = ParameterWithValues.GetValue(parameter);
            var value2 = ParameterWithValues.GetValue(parameter, 1);
            var value3 = parameter.GetValue();
            var value4 = parameter.GetValue(1);
            Assert.AreEqual("foo", value1);
            Assert.AreEqual("bar", value2);
            Assert.AreEqual("foo", value3);
            Assert.AreEqual("bar", value4);
        }

        /// <summary>
        /// Test the GetValue method with bad arguments.
        /// </summary>
        [Test]
        public void GetValueFailTest()
        {
            ParameterWithValues parameter = new ParameterWithValues(null, new[] { "foo", "bar" });
            var value1 = ParameterWithValues.GetValue(parameter, 2);
            var value2 = parameter.GetValue(2);
            Assert.AreEqual(null, value1);
            Assert.AreEqual(null, value2);
        }
    }
}