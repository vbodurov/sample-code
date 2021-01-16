using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using MiscCodeTests.Extensions;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class MiscTests
    {
        [Test]
        public void Test1()
        {
            var s = "CiBjZjg0YTZmYjE4YWU0NmRlYmQwODk1NzNjMjlmMDU0ZBIEU2l0ZRokODg5YTM2MWMtNzYzOC1lYTExLTg0NTQtMDAwM2ZmNjA5NTNjIhRUZXhhc19UZXN0X1NpdGUyX0FDQyokMThjMjY0OWMtZjZmYi1lOTExLWI4NjItMDAwM2ZmNzEwOWFlMhhTdWJzY3JpYmVyIFRleGFzIFRlc3Rpbmc6JDc5ZWM0NGQ4LTA2NGMtNDc3OS04MjNhLTY1NjcxOWRjNTcyMUIWUzFYNm5SYkJTOWVQc1V3eG1zdXlaQVAB";
            var str = Encoding.UTF8.GetString(Convert.FromBase64String(s));
            Console.WriteLine(str);

            foreach (var ch in str)
            {
                Console.WriteLine(ch + "|" + ((int)ch) + "|" + char.IsControl(ch));
            }
        }
        
    }
}

