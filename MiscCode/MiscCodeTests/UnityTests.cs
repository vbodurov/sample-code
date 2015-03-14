using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace MiscCodeTests
{
    [TestFixture]
    public class UnityTests
    {
        [Test]
        public void CanCreateInstance()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<IClass, Class1>();
            container.RegisterType<IClass, Class2>("ALT");

            var attr0 = new OneAttribute();
            var attr1 = new OneSubAttribute();

            container.BuildUp(typeof (OneAttribute), attr0);
            container.BuildUp(typeof (OneSubAttribute), attr1);

            Console.WriteLine("0:"+attr0.GetPropType());
            Console.WriteLine("1:"+attr1.GetPropType());

            Assert.That(attr0.GetPropType(), Is.EqualTo(typeof(Class1)));
            Assert.That(attr1.GetPropType(), Is.EqualTo(typeof(Class2)));
        }
        
    }
    public interface IClass { }
    public class Class1 : IClass { }
    public class Class2 : IClass { }

    [AttributeUsage(AttributeTargets.All)]
    public class OneSubAttribute : OneAttribute
    {
        [Dependency("ALT")]
        internal IClass SubProp1 { get; set; }
        internal override IClass Prop1 { get { return SubProp1; } set{} }
    }
    [AttributeUsage(AttributeTargets.All)]
    public class OneAttribute : Attribute
    {
        [Dependency]
        internal virtual IClass Prop1 { get; set; }

        public Type GetPropType()
        {
            return Prop1.GetType();
        }
    }
}