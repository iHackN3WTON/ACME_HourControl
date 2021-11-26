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

        }

        protected void LoadOKLinkButton_OnClick(object sender, EventArgs e)
        {
            if (LoadFileUpload.HasFile)
            {
                string folderPathTemp = Server.MapPath("~");
                string textFile = folderPathTemp + "Employees.txt";
                LoadFileUpload.SaveAs(textFile);
                string logPath = Server.MapPath("~\\log.txt");
                TimeController timeController = new TimeController(logPath);
                using (StreamReader file = new StreamReader(textFile))
                {
                    int lineCounter = 0;
                    string lineText;
                    while ((lineText = file.ReadLine()) != null)
                    {
                        lineCounter++;
                        if (lineText.Trim().Length > 1)
                        {
                            timeController.AddEmployee(lineText.Trim(), lineCounter);
                        }
                    }
                    file.Close();
                }
                File.Delete(textFile);

                OutputDiv.InnerHtml = timeController.GenerateTable();
            }
        }
    }
}