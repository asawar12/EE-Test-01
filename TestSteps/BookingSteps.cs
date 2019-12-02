using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using TP.Pom;

namespace TP.TestSteps
{
    [Binding]
    public class BookingSteps
    {
        //declare variables
        private readonly BookingPage _bookingPage;
        private readonly string NewlyCreatedBookingRecord = "Newly_Created_Booking";
        
        public BookingSteps()
        {
            _bookingPage = new BookingPage();
        }

        [Given(@"I open the hotel booking page")]
        public void GivenIHaveOpenedTheBookingPage()
        {
            //We dont know how many records are on the server, and they are being rendered dynamically. 
            //Currently no easy way to predict when the page has finished loading - so for now test purposes I've used sleep. 
            //Would need to consult a developer for a more comprehensive solution.

            _bookingPage.Open();
            System.Threading.Thread.Sleep(10000);
        }

        [Given(@"I am on the booking form page")]
        public void ThenIAmOnTheBookingPage() 
        {
            Assert.AreEqual("Hotel booking form", _bookingPage.GetMainHeading());
        }
                        
        [Given(@"I make a booking for ""(.*)"",""(.*)"",""(.*)"",""(.*)"",""(.*)"",""(.*)""")]
        public void GivenIEnterDataForTheFollowingFields(string firstName, string lastname, string price, string deposit, string startDate, string endDate)
        {
            //retrieving number of rows on the page so that we can compare that later on to find
            // whether a row has been added or not
            int CurrentRowCount = _bookingPage.GetCurrentRowsOnPage();
            
            //Generating a random number to create a unique record
            Random random = new Random();

            var NewBooking = new BookingRecord
            {
                FirstName = firstName,
                Surname =  "surname" + random.Next(10, 1000),
                Price = price,
                Deposit = deposit,
                CheckIn = startDate,
                CheckOut = endDate,
            };
            
            _bookingPage.CreateBooking(NewBooking);

            _bookingPage.WaitUntilRowHasBeenAdded(CurrentRowCount);
            
            FeatureContext.Current[NewlyCreatedBookingRecord] = NewBooking;
        }

       [Then(@"A row on the form has been added for the newly created booking")]
        public void ThenABookingHasBeenMadeFor()
        {
            var TheCreatedRecord = (BookingRecord) FeatureContext.Current[NewlyCreatedBookingRecord];
            List<BookingRecord> matchedBookings = _bookingPage.FindMatchingBookings(TheCreatedRecord);
            Assert.AreEqual(1, matchedBookings.Count, "failed because could not find surname " + TheCreatedRecord.Surname);
        }
                
        [Then(@"I delete the booking made")]
        public void ThenIDeleteTheBookingMade()
        {
            BookingRecord recordToBeDeleted = (BookingRecord)FeatureContext.Current[NewlyCreatedBookingRecord];
            _bookingPage.DeleteMatchingBookings(recordToBeDeleted);  
        }

        [Then(@"confirm the booking has been deleted")]
        public void ThenConfirmTheBookingHasBeenDeleted()
        {
            var matchingRecord = (BookingRecord)FeatureContext.Current[NewlyCreatedBookingRecord];
            var matchedBookings = _bookingPage.FindMatchingBookings(matchingRecord);
            Assert.AreEqual(0, matchedBookings.Count);
        }
    }
}
