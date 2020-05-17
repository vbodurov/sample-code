using System.Reflection;
using MiscCodeTests.Extensions;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class FieldInfoExtensionsTests
    {
        public string Field1;
        public int Field2;
        public FieldInfo Field1Info => GetType().GetField("Field1");
        public FieldInfo Field2Info => GetType().GetField("Field2");

        [Test]
        public void Will_Create_Setter_For_String_Field()
        {
            // Arrange
            var obj = new FieldInfoExtensionsTests();
            var expectValue = "SET";

            // Act
            var setter = Field1Info.CreateSetter();

            // Assert
            setter(obj, expectValue);
            Assert.That(obj.Field1, Is.EqualTo(expectValue));
        }
        [Test]
        public void Will_Create_Setter_For_Int_Field()
        {
            // Arrange
            var obj = new FieldInfoExtensionsTests();
            var expectValue = 11;

            // Act
            var setter = Field2Info.CreateSetter();

            // Assert
            setter(obj, expectValue);
            Assert.That(obj.Field2, Is.EqualTo(expectValue));
        }
        [Test]
        public void Will_Create_Getter_For_String_Field()
        {
            // Arrange
            var obj = new FieldInfoExtensionsTests();
            var expectValue = "SET";
            obj.Field1 = expectValue;

            // Act
            var getter = Field1Info.CreateGetter();

            // Assert
            var result = getter(obj);
            Assert.That(result, Is.EqualTo(expectValue));
        }
        [Test]
        public void Will_Create_Getter_For_Int_Field()
        {
            // Arrange
            var obj = new FieldInfoExtensionsTests();
            var expectValue = 2342;
            obj.Field2 = expectValue;

            // Act
            var getter = Field2Info.CreateGetter();

            // Assert
            var result = getter(obj);
            Assert.That(result, Is.EqualTo(expectValue));
        }
    }
}