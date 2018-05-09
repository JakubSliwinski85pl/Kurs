using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace HelloWeb
{    
    
    public class HelloWebTests : IDisposable
    {

        private readonly string BlogUrl = "https://autotestdotnet.wordpress.com/";
        private IWebDriver browser;

        public HelloWebTests()
        {
            browser = new ChromeDriver();
            browser.Manage().Window.Maximize();
           
        }

        private string GenerateEmail()
        {
            var user = Guid.NewGuid().ToString();
            return $"{user}@nonexistent.test.com";
        }

        private string GenerateText()
        {
            var randomText = Guid.NewGuid().ToString();
            return $"{randomText}";
        }
        
        [Fact]
        public void CanOpenBlogAndHelloNoteExists()
        {
            browser.Navigate().GoToUrl(BlogUrl);
                      
            var article = browser.FindElement(By.Id("post-3096"));
            var footer = article.FindElement(By.TagName("footer"));

            var commentsLink = footer.FindElement(By.ClassName("comments-link"));

            commentsLink.Click();

            var textBox1 = browser.FindElement(By.Name("comment"));
            textBox1.Click();
            string randomText = GenerateText();
            textBox1.SendKeys(randomText);

            var email_textBox1 = browser.FindElement(By.Id("email"));
            email_textBox1.SendKeys(GenerateEmail());

            var user_textBox1 = browser.FindElement(By.Id("author"));
            user_textBox1.SendKeys("JŚ");


            var submi_btn1 = browser.FindElement(By.Name("submit"));
            submi_btn1.Click();
            
            var check_comment = browser.FindElement(By.ClassName("comment-content"));
            
            var p_fromComment = check_comment.FindElements(By.TagName("p"));
            int ilosc_p =  p_fromComment.Count();
            string x  = p_fromComment[0].Text;
                 
            var check_body = browser.FindElement(By.ClassName("comment-body"));
            var replyBtn = check_body.FindElement(By.PartialLinkText("Reply"));
            replyBtn.Click();
                        
            WaitForClickable(browser.FindElement(By.ClassName("submit")), 100);
            
            var textBox2 = browser.FindElement(By.Name("comment"));
            textBox2.Click();
            string randomReplayText = GenerateText();
            textBox2.Clear();
            textBox2.SendKeys(randomReplayText);

            var email_textBox2 = browser.FindElement(By.Id("email"));
            email_textBox2.Clear();
            email_textBox2.SendKeys(GenerateEmail());

            var user_textBox2 = browser.FindElement(By.Id("author"));
            user_textBox2.Clear();
            user_textBox2.SendKeys("JŚ");

            var submi_btn2 = browser.FindElement(By.ClassName("submit"));
            submi_btn2.Click();

            var komentarzDoKomentarza = browser.FindElement(By.ClassName("children"));
            var komentarzDoKomentarza_elementP = komentarzDoKomentarza.FindElements(By.TagName("p"));
            var TekstPierwszegoElemntu_p = komentarzDoKomentarza_elementP[0].Text;


            Assert.Equal(randomReplayText, TekstPierwszegoElemntu_p);
        }
        
        private void WaitForClickable(By by, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        private void WaitForClickable(IWebElement element, int seconds)
        {
            var wait = new WebDriverWait(browser, TimeSpan.FromSeconds(seconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }
        
        public void Dispose()
        {
          try
                {
                    browser.Quit();
                }
                catch (Exception) { }
          }

    }
}
