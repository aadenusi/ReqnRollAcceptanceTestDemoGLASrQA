using NUnit.Framework;
using ReqnRollAcceptanceTestDemoGLA.Pages;

namespace ReqnRollAcceptanceTestDemoGLA.StepDefinitions
{
    [Binding]
    public class GLAStepDefinitions
    {
        private readonly WebDriverContext _context;
        private readonly HomePage _homePage;
        private readonly Search _searchPage;
        //private readonly WebDriverWait _wait;
       
        public GLAStepDefinitions(WebDriverContext context)
        {
            _context = context;
            _searchPage = new Search(_context.Driver);
            _homePage = new HomePage(_context.Driver);
        }

        [Given("I am on the GLA website")]
        public void GivenIAmOnTheGLAWebsite()
        {
            var test = _homePage.HomePageTitle.Text;
            Assert.That(test, Does.Contain("Talk London"));
        }

        [When("I click on the search icon")]
        public void WhenIClickOnTheSearchIcon()
        {
            _searchPage.searchButton.Click();
        }

        [When("I search for the text (.*) in the search box")]
        public void WhenISearchForTheTextInTheSearchBox(string Search)
        {
            _searchPage.searchTextField.SendKeys(Search);
            _searchPage.searchTextField.Submit();
        }

        [Then("the search results returned contains (.*)")]
        public void ThenTheSearchResultsReturnedContains(string Search)
        {
            var results = _searchPage.GetSearchResultsTexts();
            Assert.That(results.Count, Is.GreaterThanOrEqualTo(10), "Less than two search results returned.");
            Assert.Multiple(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Assert.That(results[i].ToLower(), Does.Contain(Search.ToLower()),
                        $"Result '{results[i]}' does not contain '{Search}' (case-insensitive)");
                }
            });

        }
    }
}
