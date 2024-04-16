

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using ConsoleApp1;
using MongoDB.Bson;
using ConsoleApp1.Data;
using System.IO;
using System.Net;
using ConsoleApp1.Function;

public class Program
{
    public static ExamManager _examManager;
    public static void Main()
    {
        _examManager = new ExamManager();
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl("https://khoahoc.vietjack.com/thi-online/trac-nghiem-tieng-anh-toeic-part-5-test/102867");
            Thread.Sleep(2000);
            FindExam findExam = new FindExam(_examManager);
            findExam.GoToExam(driver);
            Thread.Sleep(1000);
            findExam.GetExam();       
        }      
    }
}