using OpenQA.Selenium.Support.UI;

namespace ReqnRollAcceptanceTestDemoGLA.Pages
{
    public class Search
    {

        private readonly IWebDriver _driver;
        public Search(IWebDriver driver)
        {
            _driver = driver;
        }
        public IWebElement searchButton => _driver.FindElement(By.CssSelector(".main-nav #block-glasearchicon"));

        public IWebElement searchTextField => _driver.FindElement(By.Id("edit-s"));

        public List<string> GetSearchResultsTexts()
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var resultsElements = wait.Until(d => d.FindElements(By.CssSelector(".card__header .heading")));
            var resultsTexts = new List<string>();
            foreach (var element in resultsElements)
            {
                resultsTexts.Add(element.Text);
            }
            return resultsTexts;
        }
    }
}
