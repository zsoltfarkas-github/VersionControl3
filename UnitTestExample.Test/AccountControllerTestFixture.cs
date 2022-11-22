using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [Test]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // arrange
            var accountcontroller = new AccountController();

            // act
            var actualResult = accountcontroller.ValidateEmail(email);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
