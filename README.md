# ACME: Hour Control

## Introduction

This project reads the data from a text file, this file should include a list of employees with their respective schedule worked, the goal of the project is to determine how many times a couple of employees have coincided in the office.

The content of text file must follow the next format:

    EmployeeName=DayTimeIn-TimeOut,DayTimeIn-TimeOut
    
Example:

    MOSH=MO08:00-17:00,TU10:00-15:00,WE13:00-17:15,TH11:00-13:00,FR12:00-14:00,SA14:00-18:00,SU20:00-21:00
    MARIA=TU10:00-11:00,SA08:00-17:00

The output of the proyect is a list like this:

    MARIA-MOSH: 2
    
## Architecture

This project uses a couple of clases.

The frist one is the Schedule that includes Day, TimeIn and TimeOut.

And the second one is Employee that includes Name and a list of Schedules.

The Project has a web interface to select the text file with the data.

If some data in the text file, fails to be loaded or is incorrect a file called log.txt will be written in the same folder of the main web page.

## Running the project

The project was made in visual studio using c# and asp.net, Visual Studio 2013 and .Net Framework 4.5 are the minimum requeriments to run this application, due to unit testing code.

To compile and start the aplication, download the complete code and open the ACME_HourControl.sln file, and push the F5 key.

When the main page loads click the button "browse..." to select the file, then click the button "OK" to obtain the output.
