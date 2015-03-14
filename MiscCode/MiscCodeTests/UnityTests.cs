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

            Console.WriteLine("0:"+attr0.Prop1.GetType());
            Console.WriteLine("1:"+attr1.Prop1.GetType());

            Assert.That(attr0.Prop1.GetType(), Is.EqualTo(typeof(Class1)));
            Assert.That(attr1.Prop1.GetType(), Is.EqualTo(typeof(Class2)));
        }
        
    }
    [AttributeUsage(AttributeTargets.All)]
    public class OneSubAttribute : OneAttribute
    {
        [Dependency("ALT")]
        internal new IClass Prop1 { get; set; }
    }
    public interface IClass { }
    public class Class1 : IClass { }
    public class Class2 : IClass { }
    [AttributeUsage(AttributeTargets.All)]
    public class OneAttribute : Attribute
    {
        [Dependency]
        internal IClass Prop1 { get; set; }
    }
}