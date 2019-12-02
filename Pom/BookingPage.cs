using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TP.Utilities;
using Table = TechTalk.SpecFlow.Table;

namespace TP.Pom
{
    public class BookingPage : BasePage
    {
        private readonly By _recordRowsLocator = By.XPath("//*[@class='row'  and @id]");
        private readonly By _saveButton = By.XPath("//input[@value=' Save 'and@type='button']");
        private readonly string url = "http://hotel-test.equalexperts.io/";

        public void Open()
        {
            Driver.NavigateTo(url);
        }
        
        public string GetMainHeading()
        {
            return GetVisibleElement(By.XPath("/html/body/div[1]/div[1]/h1")).Text;
        }

        public void CreateBooking(BookingRecord bookingRecord)
        {
            EnterText(By.Id("firstname"), bookingRecord.FirstName);
            EnterText(By.Id("lastname"), bookingRecord.Surname);
            EnterText(By.Id("totalprice"), bookingRecord.Price);
            SelectValue(By.Id("depositpaid"), bookingRecord.Deposit);
            EnterText(By.Id("checkin"), bookingRecord.CheckIn);
            EnterText(By.Id("checkout"), bookingRecord.CheckOut);

            Click(_saveButton);
        }

        public void WaitUntilRowHasBeenAdded(int PreviousCount)
        {
            baseWait.Until(driver =>
            {
                return GetCurrentRowsOnPage() > PreviousCount;
            });
        }

        public int GetCurrentRowsOnPage()
        {
            var allRows = baseDriver.FindElements(_recordRowsLocator);
            return allRows.Count;
        }

        public List<BookingRecord> FindMatchingBookings(BookingRecord MatchingRecord)
        {
            // an empty list, to add matching records into.
            var matchedBookingRecords = new List<BookingRecord>();
            var allRows = baseDriver.FindElements(_recordRowsLocator);

            foreach (var row in allRows)
            {
                var id = row.GetAttribute("id");

                var ExistingBooking = new BookingRecord
                {
                    Id = id,
                    FirstName = row.FindElement(By.XPath($"./div[1]/p"))?.Text,
                    Surname = row.FindElement(By.XPath($"./div[2]/p"))?.Text,
                    Price = row.FindElement(By.XPath($"./div[3]/p"))?.Text,
                    Deposit = row.FindElement(By.XPath($"./div[4]/p"))?.Text,
                    CheckIn = row.FindElement(By.XPath($"./div[5]/p"))?.Text,
                    CheckOut = row.FindElement(By.XPath($"./div[6]/p"))?.Text,
                };

                if (ExistingBooking.Equals(MatchingRecord))
                {
                    matchedBookingRecords.Add(ExistingBooking);
                }
            }

            return matchedBookingRecords;
        }

        public void DeleteMatchingBookings(BookingRecord matchingRecord)
        {
            List<BookingRecord> matchedBookingRecords = FindMatchingBookings(matchingRecord);
            BookingRecord bookingRecordToDelete = matchedBookingRecords.First();

            var deleteButtonLocator = By.XPath($"//div[@id={bookingRecordToDelete.Id}]/*/input[@value='Delete']");

            Find(deleteButtonLocator).Click();
            //waiting until Delete button of the deleted row gets invisible
            baseWait.Until(ExpectedConditions.InvisibilityOfElementLocated(deleteButtonLocator));
        }
    }
}


