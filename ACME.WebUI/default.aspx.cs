using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ACME.Domain.Controllers;

namespace ACME.WebUI
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Arrange
            TimeController timeController = new TimeController();
            string table;

            // Act
            timeController.AddEmployee("RENE=MO10:15-12:00,TU10:00-12:00,TH013:00-13:15,SA14:00-18:00,SU20:00-21:00");
            timeController.AddEmployee("ASTRID=MO10:00-12:00,TH12:00-14:00,SU20:00-21:00");
            table = timeController.GenerateTable();



        }

        protected void LoadOKLinkButton_OnClick(object sender, EventArgs e)
        {
            if (LoadFileUpload.HasFile)
            {
                string folderPathTemp = Server.MapPath("~");
                string textFile = folderPathTemp + "Employees.txt";
                LoadFileUpload.SaveAs(textFile);
                TimeController timeController = new TimeController();
                using (StreamReader file = new StreamReader(textFile))
                {
                    int lineCounter = 0;
                    string lineText;
                    while ((lineText = file.ReadLine()) != null)
                    {
                        lineCounter++;
                        if (lineText.Trim().Length > 1)
                        {
                            timeController.AddEmployee(lineText.Trim());
                        }
                    }
                    file.Close();
                }

                OutputDiv.InnerHtml = timeController.GenerateTable();
            }
        }
    }
}