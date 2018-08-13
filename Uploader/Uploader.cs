using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Support.UI;

namespace ArdourUploader
{
    public class Uploader
    {
        public void LoadSoundcloud()
        {
            IWebDriver driver = new ChromeDriver(@"C:\Users\ciara\source\repos\ProToolsUploader\packages\Selenium.WebDriver.ChromeDriver.2.41.0\driver\win32");

            driver.Url = "http://www.soundcloud.com";
        }
    }
}
