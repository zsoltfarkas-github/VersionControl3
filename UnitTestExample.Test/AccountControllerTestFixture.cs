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
        [
            Test,
            TestCase("abcd1234", false),
            TestCase("irf@uni-corvinus", false),
            TestCase("irf.uni-corvinus.hu", false),
            TestCase("irf@uni-corvinus.hu", true)
        ]      
        public void TestValidateEmail(string email, bool expectedResult)
        {
            // arrange
            var accountcontroller = new AccountController();

            // act
            var actualResult = accountcontroller.ValidateEmail(email);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [
            Test,
            TestCase("Egyjelszo", false),
            TestCase("EGYJELSZO1", false),
            TestCase("egyjelszo1", false),
            TestCase("Egyjel1", false),
            TestCase("Egyjelszo1", true)
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            // arrange
            var accountcontroller = new AccountController();

            // act
            var actualResult = accountcontroller.ValidatePassword(password);

            // assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}
