using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AddingNoteToWP
{
    public class WordpressTest : IDisposable
    {
        private const string PageUrl = "https://autotestdotnet.wordpress.com/wp-admin";
        private readonly IWebDriver driver;
        private readonly ExampleComment testComment;
        private readonly ExampleComment testPost;
        private readonly TestCredentials testCredentials;

        public WordpressTest()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            testPost = new ExampleComment();
            testCredentials = new TestCredentials();
        }

        [Fact]
        public void LoginTo_WP_Admin()
        {
            //arange
            var Page = new Page(driver, PageUrl);


            //act
            Page.Login(testCredentials);
            Page.InsertPost(testPost);
            //assert

        }

        public void Dispose()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                throw;
            }
        }




    }

    internal class Page
    {
        private IWebDriver _driver;
        private string _PageUrl;

        public Page(IWebDriver driver, string PageUrl)
        {
            _driver = driver;
            _PageUrl = PageUrl;
            _driver.Navigate().GoToUrl(_PageUrl);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        internal void InsertPost(ExampleComment testPost)
        {

            var LeftMenu = _driver.FindElements(By.ClassName("wp-menu-name"));
            var PostBtnFromLeftMenu = LeftMenu.SingleOrDefault(c => c.Text.Contains("Posts"));
            PostBtnFromLeftMenu.Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);




            var SearchingBTN = _driver.FindElement(By.ClassName("page-title-action"));
            SearchingBTN.Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);



        }




        internal void Login(TestCredentials TestCredentials)
        {
            WaitForClickable(_driver.FindElement(By.Id("usernameOrEmail")),100000);

            var LoginField = _driver.FindElement(By.Id("usernameOrEmail"));
            LoginField.Click();
            LoginField.SendKeys(TestCredentials.Login);

            var ContinueButton = _driver.FindElement(By.ClassName("login__form-action"));
            ContinueButton.Click();

            WaitForClickable(_driver.FindElement(By.Id("password")), 100000);

            var PasswordField = _driver.FindElement(By.Id("password"));
            PasswordField.Click();
            PasswordField.SendKeys(TestCredentials.Password);

            var LogInButton = _driver.FindElement(By.ClassName("login__form-action"));
            ContinueButton.Click();

            //wait
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);

        }



        private void WaitForClickable(IWebElement by, int seconds)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }


    }

    internal class TestCredentials
    {
        public string Login { get; }
        public string Password { get; }

        public TestCredentials()
        {
            Login = "autotestdotnet@gmail.com";
            Password = "codesprinters2018";
        }
    }




    internal class ExampleComment
    {
        public string Name { get; }
        public string Email { get; }
        public string Text { get; }

        public ExampleComment()
        {
            Name = GenerateUserName();
            Email = GenerateEmail();
            Text = GenerateComment();
        }

        private string GenerateComment()
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateEmail()
        {
            var user = Guid.NewGuid().ToString();
            return $"{user}@nonexistent.test.com";
        }

        private string GenerateUserName()
        {
            return "JŚ" + "  " + Guid.NewGuid().ToString();
        }
    }
}
