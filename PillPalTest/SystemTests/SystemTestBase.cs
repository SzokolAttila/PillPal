using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalTest.SystemTests
{
    public class SystemTestBase
    {
        protected WebDriverWait? wait;
        protected WebDriver? driver;
        protected LoginDto? admin;
        protected readonly CreateUserDto adminLogin = new () { UserName = "administrator", Password = "aA1?aA1?" };
        protected const string url = "http://vm1.test:5173/";
        protected readonly UserAPIHandler handler = new();
    }
}
