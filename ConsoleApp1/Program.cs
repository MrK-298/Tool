

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using ConsoleApp1;
using MongoDB.Bson;

public class Program
{
    public static ExamManager _examManager;
    public static void Main()
    {
        _examManager = new ExamManager();
        using (IWebDriver driver = new ChromeDriver())
        {            
            driver.Navigate().GoToUrl("https://khoahoc.vietjack.com/thi-online/500-cau-trac-nghiem-ngu-phap-tieng-anh-co-dap-an/101093");
            Thread.Sleep(2000);
            IWebElement button1 = driver.FindElement(By.XPath("//*[@id=\"main-content\"]/div[2]/div/div/div[1]/div[1]/a"));
            button1.Click();
            Thread.Sleep(1000);
            IWebElement button2 = driver.FindElement(By.XPath("//*[@id=\"exam-modal_notice\"]/div/div/div[2]/div[2]/a"));
            button2.Click();
            Thread.Sleep(1000);
            var emailInput = driver.FindElement(By.XPath("//*[@id=\"login-box\"]/div/div[2]/div/div[1]/form/div/div[1]/input"));
            emailInput.SendKeys("binbb1324@gmail.com");
            var passwordInput = driver.FindElement(By.XPath("//*[@id=\"login-box\"]/div/div[2]/div/div[1]/form/div/div[2]/input"));
            passwordInput.SendKeys("khoibia123");
            Thread.Sleep(2000);
            IWebElement button3 = driver.FindElement(By.XPath("//*[@id=\"login-box\"]/div/div[2]/div/div[1]/form/div/div[3]/button"));
            button3.Click();
            Thread.Sleep(3000);
            IWebElement button4 = driver.FindElement(By.XPath("//*[@id=\"main-content\"]/div[2]/div/div/div[1]/div[1]/a"));
            button4.Click();
            Thread.Sleep(2000);
            var questionAnswerItems = driver.FindElements(By.CssSelector(".quiz-answer-item"));
            int count = 0;
            foreach (var questionAnswerItem in questionAnswerItems)
            {
                count++;
                var question = questionAnswerItem.FindElement(By.CssSelector(".question-name")).Text;
                var answers = questionAnswerItem.FindElements(By.CssSelector(".anwser-item"));

                var questionDB = new Question
                {
                    Id = ObjectId.GenerateNewId(),
                    questionText = questionAnswerItem.Text,
                };
                _examManager.AddQuestionToExam(questionDB);
                List<Answer> answersList = new List<Answer>();
                foreach (var answer in answers)
                {
                    var answerText = answer.FindElement(By.CssSelector("div")).Text.Trim();
                    var answerStatusElement = answer.FindElement(By.CssSelector(".result-anwser"));
                    var answerStatus = answerStatusElement.GetAttribute("value");
                    bool isCorrect = answerStatus == "Y";
                    var answerDB = new Answer
                    {
                        Id = ObjectId.GenerateNewId(),
                        text = answerText,
                        isTrue = isCorrect
                    };
                    answersList.Add(answerDB);
                }
                _examManager.AddAnswersToQuestion(answersList);
                IWebElement button5 = driver.FindElement(By.XPath("//*[@id=\"next-question\"]"));
                button5.Click();
                Thread.Sleep(2000);
            }
        }
    }
}