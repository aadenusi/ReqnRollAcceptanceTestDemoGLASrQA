
namespace ReqnRollAcceptanceTestDemoGLA.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement HomePageTitle => _driver.FindElement(By.CssSelector("#block-talk-london-account-menu .sr-only"));
        public IWebElement HomePageTitleLoggedIn => _driver.FindElement(By.CssSelector(".card__body .heading"));            
    }
}
