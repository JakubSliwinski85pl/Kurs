using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace HelloWeb
{    
    public class HelloWebTests : IDisposable
    {

        private readonly string BlogUrl = "https://autotestdotnet.wordpress.com/";
        private IWebDriver browser;

        public HelloWebTests()
        {
            browser = new ChromeDriver();
        }
                
        [Fact]
        public void CanOpenBlogAndHelloNoteExists()
        {
            browser.Navigate().GoToUrl(BlogUrl);

            // var element = browser.FindElement(By.LinkText("Witamy na warsztatach automatyzacji testów!"));
            var article = browser.FindElement(By.Id("post-3096"));
            var header = article.FindElement(By.TagName("h1"));

            Assert.Equal("Witamy na warsztatach automatyzacji testów!", header.Text);
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
