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
       // private readonly ExampleComment testComment;
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
            string PageUrl_to_veryfi = Page.InsertPost(testPost);
            Page.Logout();

            //assert
            var Page_v = new Page(driver, PageUrl_to_veryfi);
            bool AssertResult = Page_v.CheckAssert(testPost);
            Assert.True(AssertResult);
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

        internal bool CheckAssert(ExampleComment testPost)
        {
            bool AssertResult = false;

            var AssertTitle = _driver.FindElement(By.ClassName("entry-title")).Text;

            var AssertContent = _driver.FindElement(By.ClassName("entry-content"));
            var AssertText = AssertContent.FindElement(By.TagName("p")).Text;

           if (testPost.Text == AssertText && testPost.Title == AssertTitle)
            {
                AssertResult = true;
            }

            return AssertResult;
        }

        internal string InsertPost(ExampleComment testPost)
        {
            var LeftMenu = _driver.FindElements(By.ClassName("wp-menu-name"));
            var PostBtnFromLeftMenu = LeftMenu.SingleOrDefault(c => c.Text.Contains("Posts"));
            PostBtnFromLeftMenu.Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            
            var SearchingBTN = _driver.FindElement(By.ClassName("page-title-action"));
            SearchingBTN.Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
   
            var TitleField = _driver.FindElement(By.Id("title"));
            TitleField.Click();
            TitleField.SendKeys(testPost.Title);

            var TextField = _driver.FindElement(By.Id("content"));
            TextField.Click();
            TextField.SendKeys(testPost.Text);

            WaitForClickable(_driver.FindElement(By.Id("edit-slug-buttons")), 100000);
            var EditBtnWaitFor = _driver.FindElement(By.Id("edit-slug-buttons"));
            var PublishBtnWaitFor = _driver.FindElement(By.Id("publish"));

            var permamentLink = _driver.FindElement(By.Id("edit-slug-box"));
            var permamentLink_s = permamentLink.FindElement(By.TagName("a"));
             string CheckUrl = permamentLink_s.GetAttribute("href");

            PublishBtnWaitFor.Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(4);
                        
            return CheckUrl;            
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

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        internal void Logout()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);

            var Avatar = _driver.FindElement(By.Id("wp-admin-bar-my-account"));
            Avatar.Click();

            var SingOut_find = _driver.FindElement(By.TagName("form"));
            SingOut_find.Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
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
        public string Title { get; }
        public string Text { get; }

        public ExampleComment()
        {
            Name = GenerateUserName();
            Title = GenerateTitle();
            Text = GenerateComment();
        }

        private string GenerateComment()
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateTitle()
        {
            return "JŚ" + " " + Guid.NewGuid().ToString();
        }

        private string GenerateUserName()
        {
            return "JŚ" + " " + Guid.NewGuid().ToString();
        }
    }
}
