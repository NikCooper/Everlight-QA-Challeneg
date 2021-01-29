using OpenQA.Selenium;
using log4net;

namespace Everlight.Core
{
    public abstract class PageObjectBase
    {
        protected PageObjectBase(IWebDriver driver)
        {
            Driver = driver;
        }

        public static readonly ILog Logger = LogManager.GetLogger(typeof(PageObjectBase));


        public IWebDriver Driver { get; set; }


    }
}