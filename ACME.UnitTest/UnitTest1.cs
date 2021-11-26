using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ACME.Domain;
using ACME.Domain.Controllers;
using ACME.Domain.Models;

namespace ACME.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestValidateDay()
        {
            // Arrange
            TimeController timeController = new TimeController();

            // Act
            bool MOValid = timeController.ValidateDay("MO");
            bool MAValid = timeController.ValidateDay("MA");
            bool frValid = timeController.ValidateDay("fr");

            // Assert
            Assert.AreEqual(true,MOValid,"MO is not valid");
            Assert.AreEqual(false,MAValid,"MA is valid");
            Assert.AreEqual(true,frValid,"fr is invalid");
        }

        [TestMethod]
        public void TestAddEmployee()
        {
            // Arrange
            TimeController timeController = new TimeController();

            // Act
            timeController.AddEmployee("RENE=MO10:00-12:00,TU10:00-12:00,TH01:00-03:00,SA14:00-18:00,SU20:00- 21:00");

            // Assert
            Assert.AreEqual("RENE",timeController.Employees[0].Name,"Name RENE wrong");
            Assert.AreEqual(5,timeController.Employees[0].Schedules.Count,"Invalid count of schedules");
            Assert.AreEqual(new DateTime(2000,1,1,1,0,0),timeController.Employees[0].Schedules[2].TimeIn,"Invalid time in of schedule 3");
        }

        [TestMethod]
        public void TestGenerateTable()
        {
            // Arrange
            TimeController timeController = new TimeController();
            string table;

            // Act
            timeController.AddEmployee("RENE=MO10:15-12:00,TU10:00-12:00,TH013:00-13:15,SA14:00-18:00,SU20:00-21:00");
            timeController.AddEmployee("ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00");
            table = timeController.GenerateTable();

            // Assert
            Assert.AreEqual("ASTRID-RENE: 3<br>",table,"Table wrong");
        }
    }
}
