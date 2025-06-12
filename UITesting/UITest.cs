using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

using Xunit;

namespace UITesting;

public class UITest : IDisposable
{
    private readonly IWebDriver driver;
    private readonly WebDriverWait wait_driver;

    public UITest()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless"); // ← najważniejsze!
        options.AddArgument("--disable-gpu");
        options.AddArgument("--window-size=1920,1080"); // opcjonalnie: ustaw rozmiar "niewidzialnego" okna

        driver = new ChromeDriver(options);
        wait_driver = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    }

    public void Dispose()
    {
        driver.Quit();
        driver.Dispose();
    }

    [Fact]
    public void Create_GET_ReturnsCreateView()
    {
        driver.Navigate().GoToUrl("https://localhost:7213");
        Assert.Contains("Welcome to our Library Page", driver.PageSource);
    }

    [Fact]
    public void Create_POST_CreatesNewAuthor()
    {
        driver.Navigate().GoToUrl("https://localhost:7213/author/create");
        var nameInput = driver.FindElement(By.Id("name"));
        var sureNameInput = driver.FindElement(By.Id("last-name"));
        var submitButton = driver.FindElement(By.CssSelector("input[type='submit']"));
        nameInput.SendKeys("TestName");
        sureNameInput.SendKeys("TestSureName");
        submitButton.Click();
        // driver.Navigate().GoToUrl("https://localhost:7213/author");
        wait_driver.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h3")));
        Assert.Contains("TestName", driver.PageSource);
    }
}