using System.Reflection;
using MiscCodeTests.Extensions;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class PropertyExtensionsTests
    {
        string _readOnlyField, _writeOnlyField;
        public string ReadOnly => _readOnlyField;
        public string WriteOnly { set => _writeOnlyField = value; }
        public PropertyInfo ReadOnlyPropertyInfo => GetType().GetProperty("ReadOnly");
        public PropertyInfo WriteOnlyPropertyInfo => GetType().GetProperty("WriteOnly");
        [Test]
        public void Will_Create_Setter_For_Writable_Property()
        {
            // Arrange
            var obj = new PropertyExtensionsTests();
            var expectValue = "SET";

            // Act
            var setter = WriteOnlyPropertyInfo.CreateSetter();

            // Assert
            setter(obj, expectValue);
            Assert.That(obj._writeOnlyField, Is.EqualTo(expectValue));
        }
        [Test]
        public void Will_Not_Create_Setter_For_Read_Only_Property()
        {
            // Arrange
            var obj = new PropertyExtensionsTests();

            // Act
            var setter = ReadOnlyPropertyInfo.CreateSetter();

            // Assert
            Assert.That(setter, Is.Null);
        }
        [Test]
        public void Will_Create_Getter_For_Readable_Property()
        {
            // Arrange
            var obj = new PropertyExtensionsTests();
            var expectValue = "SET";
            obj._readOnlyField = expectValue;

            // Act
            var getter = ReadOnlyPropertyInfo.CreateGetter();

            // Assert
            var result = getter(obj);
            Assert.That(result, Is.EqualTo(expectValue));
        }
        [Test]
        public void Will_Not_Create_Getter_For_Write_Only_Property()
        {
            // Arrange
            var obj = new PropertyExtensionsTests();
            var expectValue = "SET";
            obj._writeOnlyField = expectValue;

            // Act
            var getter = WriteOnlyPropertyInfo.CreateGetter();

            // Assert
            Assert.That(getter, Is.Null);
        }

    }
}