using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MiscCodeTests
{
    [TestFixture]
    public class CustomAwaiterTests
    {
        [Test]
        public async void CanAwaitCustom()
        {
            var ca = new CustomAwaitable();

            Console.WriteLine("BEFORE:" + DateTime.Now.ToString() + "|" + Thread.CurrentThread.ManagedThreadId);

            var str = await ca;

            Console.WriteLine("AFTER!:" + str + "|" + Thread.CurrentThread.ManagedThreadId);
        }

        [Test]
        public async void CanAwaitTask()
        {
            Console.WriteLine("BEFORE:" + DateTime.Now.ToString() + "|" + Thread.CurrentThread.ManagedThreadId);

            var str = await Task<string>.Factory.StartNew(() =>
            {
                Console.WriteLine("IN:" + DateTime.Now.ToString() + "|" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(2000);
                return DateTime.Now.ToString();
            });

            Console.WriteLine("AFTER!:" + str + "|" + Thread.CurrentThread.ManagedThreadId);
        }
    }
    public interface IAwaitable<out T>
    {
        IAwaiter<T> GetAwaiter();
    }
    public interface IAwaiter<out T> : INotifyCompletion
    {
        bool IsCompleted { get; }
        T GetResult();
    }
    public class CustomAwaitable : IAwaitable<string>
    {
        public IAwaiter<string> GetAwaiter()
        {
            return new CustomAwaiter();
        }
    }
    public class CustomAwaiter : IAwaiter<string>
    {
        private bool _isComplete;
        public CustomAwaiter()
        {
            Console.WriteLine("IN:" + DateTime.Now.ToString() + "|" + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            _isComplete = true;
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            Console.WriteLine("END:" + DateTime.Now.ToString() + "|" + Thread.CurrentThread.ManagedThreadId);
            if (continuation != null) continuation();
        }

        bool IAwaiter<string>.IsCompleted
        {
            get
            {
                Console.WriteLine("IsCompleted:" + DateTime.Now.ToString() + "|" + Thread.CurrentThread.ManagedThreadId);
                return _isComplete;
            }
        }

        string IAwaiter<string>.GetResult()
        {
            Console.WriteLine("GetResult:" + DateTime.Now.ToString() + "|" + Thread.CurrentThread.ManagedThreadId);
            return DateTime.Now.ToString();
        }
    }

}