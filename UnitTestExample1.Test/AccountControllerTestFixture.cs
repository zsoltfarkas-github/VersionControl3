using NUnit.Framework;
using System;
using System.Activities;
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




        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd12345"),
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.Register(email, password);

            // Assert
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }


        [
            Test,
            TestCase("irf@uni-corvinus", "Abcd1234", false),
            TestCase("irf.uni-corvinus.hu", "Abcd1234", false),
            TestCase("irf@uni-corvinus.hu", "abcd1234", false),
            TestCase("irf@uni-corvinus.hu", "ABCD1234", false),
            TestCase("irf@uni-corvinus.hu", "abcdABCD", false),
            TestCase("irf@uni-corvinus.hu", "Ab1234", false),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }

            // Assert
        }

    }
}
