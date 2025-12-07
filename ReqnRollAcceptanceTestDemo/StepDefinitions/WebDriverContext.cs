
namespace ReqnRollAcceptanceTestDemoGLA.StepDefinitions
{
 // Simple context object to share the IWebDriver instance between hooks and step definitions
 public class WebDriverContext
 {
 public IWebDriver Driver { get; set; }

 // Base URI loaded from testsettings.json
 public string BaseUri { get; set; }

 // Credentials loaded from credentials.yml (keyed by user key like "standard_user")
 public Dictionary<string, (string Username, string Password)> Credentials { get; set; } = new();
 }
}
