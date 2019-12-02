Feature: Booking CRUD Test
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
	Given I open the hotel booking page

@mytag
Scenario Outline: CreateBooking
	Given I am on the booking form page
	And I make a booking for "<firstname>","<lastname>","<price>","<deposit>","<checkin>","<checkout>"
	Then A row on the form has been added for the newly created booking
  Examples: 
	| firstname  | lastname   | price | deposit  | checkin    | checkout   |
	| Jason      | Bourne     | 50    | false    | 2019-11-24 | 2019-12-20 |
	| Michael    | Knight     | 150   | true     | 2019-12-24 | 2019-12-25 |


@mytag
Scenario: DeleteBooking
	Given I am on the booking form page
	And I make a booking for "Bobby","Moore","70","true","2019-12-24","2019-12-27"
	Then A row on the form has been added for the newly created booking
    And I delete the booking made
	Then confirm the booking has been deleted 