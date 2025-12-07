using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ReqnRollAcceptanceTestDemoGLA.StepDefinitions
{
 [Binding]
 public class Hooks
 {
 private readonly WebDriverContext _context;

 public Hooks(WebDriverContext context)
 {
 _context = context;
 }

 [BeforeScenario]
 public void BeforeScenario()
 {
 // Load testsettings.json for baseUri and optional view mode
 try
 {
 var settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "testsettings.json");
 if (File.Exists(settingsPath))
 {
 var json = File.ReadAllText(settingsPath);
 using var doc = JsonDocument.Parse(json);
 if (doc.RootElement.TryGetProperty("baseUri", out var baseUriElement))
 {
 _context.BaseUri = baseUriElement.GetString();
 }
 }
 }
 catch
 {
 // ignore, leave BaseUri null
 }

 // Load credentials.yml
 try
 {
 var credPath = Path.Combine(Directory.GetCurrentDirectory(), "credentials.yml");
 if (File.Exists(credPath))
 {
 var yaml = File.ReadAllText(credPath);
 var deserializer = new DeserializerBuilder()
 .WithNamingConvention(CamelCaseNamingConvention.Instance)
 .Build();
 var raw = deserializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(yaml);
 foreach (var kv in raw)
 {
 var userKey = kv.Key;
 var dict = kv.Value;
 var username = dict.ContainsKey("username") ? dict["username"] : string.Empty;
 var password = dict.ContainsKey("password") ? dict["password"] : string.Empty;
 _context.Credentials[userKey] = (username, password);
 }
 }
 }
 catch
 {
 // ignore
 }

 // instantiate driver once per scenario
 // Support configurable view: use environment variable TEST_VIEW (desktop|mobile) or testsettings.json "view"
 var view = Environment.GetEnvironmentVariable("TEST_VIEW");
 var mobileDevice = Environment.GetEnvironmentVariable("MOBILE_DEVICE");

 // fallback to testsettings.json if env var not provided
 try
 {
 if (string.IsNullOrEmpty(view))
 {
 var settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "testsettings.json");
 if (File.Exists(settingsPath))
 {
 var json = File.ReadAllText(settingsPath);
 using var doc = JsonDocument.Parse(json);
 if (doc.RootElement.TryGetProperty("view", out var viewElement))
 {
 view = viewElement.GetString();
 }

 if (string.IsNullOrEmpty(mobileDevice) && doc.RootElement.TryGetProperty("mobileDevice", out var deviceElement))
 {
 mobileDevice = deviceElement.GetString();
 }
 }
 }
 }
 catch
 {
 // ignore
 }
            var options = new ChromeOptions();
options.AddArgument("--start-maximized");
_context.Driver = new ChromeDriver(options);

            // navigate to base uri if present
            if (!string.IsNullOrEmpty(_context.BaseUri))        
 {      
 _context.Driver.Navigate().GoToUrl(_context.BaseUri);
 }

 // Dismiss cookies popup if present
 DismissCookiesPopup();
 }

 private void DismissCookiesPopup()
 {
 try
 {
 var wait = new WebDriverWait(_context.Driver, TimeSpan.FromSeconds(5));
 var acceptButton = wait.Until(d => d.FindElements(By.Id("ccc-notify-accept")).FirstOrDefault());
 if (acceptButton != null && acceptButton.Displayed)
 {
 acceptButton.Click();
 }
 }
 catch (WebDriverTimeoutException)
 {
 // Popup not present, nothing to do
 }
 }

 [AfterScenario]
 public void AfterScenario()
 {
 try
 {
 _context.Driver?.Quit();
 _context.Driver = null;
 }
 catch
 {
 // ignore errors during cleanup
 }
 }
 }
}
