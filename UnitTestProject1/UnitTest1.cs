using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Books;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAddDeleteBook()
        {
            var b = new Book { BookName = Guid.NewGuid().ToString() };
            DbCtx.dbCtx.Books.Add(b);
            DbCtx.dbCtx.SaveChanges();

            var b1 = DbCtx.dbCtx.Books.FirstOrDefault((x) => x.BookId == b.BookId);
            Assert.IsNotNull(b1, "cant find inserted book!");
            Assert.AreEqual(b.BookName, b1.BookName, "incorrectly!");

            DbCtx.dbCtx.Books.Remove(b);
            DbCtx.dbCtx.SaveChanges();

            b1 = DbCtx.dbCtx.Books.FirstOrDefault((x) => x.BookId == b.BookId);

            Assert.IsNull(b1, "cant remove inserted book!");

        }
    }
}
