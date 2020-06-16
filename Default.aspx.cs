using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Zadanie6
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateData();
                lblMessage.Text = "Aktualne dane bazy danych!";
            }
        }

        private void PopulateData()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                gvData.DataSource = dc.Tables.ToList();
                gvData.DataBind();
            }
        }




        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (FileUpload1.PostedFile.ContentType == "application/xml" || FileUpload1.PostedFile.ContentType == "text/xml")
            {
                try
                {
                    string fileName = Path.Combine(Server.MapPath("~/UploadDocuments"), Guid.NewGuid().ToString() + ".xml");
                    FileUpload1.PostedFile.SaveAs(fileName);

                    XDocument xDoc = XDocument.Load(fileName);
                    List<Table> emList = xDoc.Descendants("Employee").Select(d =>
                        new Table
                        {
                            //EmployeeID = d.Element("EmployeeID").Value,
                            CompanyName = d.Element("CompanyName").Value,
                            ContactName = d.Element("ContactName").Value,
                            ContactTitle = d.Element("ContactTitle").Value,
                            EmployeeAddress = d.Element("EmployeeAddress").Value,
                            PostalCode = d.Element("PostalCode").Value
                        }).ToList();

                    // Update Data Here
                    using (MyDatabaseEntities dc = new MyDatabaseEntities())
                    {
                        foreach (var i in emList)
                        {
                            var v = dc.Tables.Where(a => a.EmployeeID.Equals(i.EmployeeID)).FirstOrDefault();
                            if (v != null)
                            {
                                v.EmployeeID = i.EmployeeID;
                                v.CompanyName = i.CompanyName;
                                v.ContactName = i.ContactName;
                                v.ContactTitle = i.ContactTitle;
                                v.EmployeeAddress = i.EmployeeAddress;
                                v.PostalCode = i.PostalCode;
                            }
                            else
                            {
                                dc.Tables.Add(i);
                            }
                        }

                        dc.SaveChanges();
                    }

                    // Populate update data
                    PopulateData();
                    lblMessage.Text = "Pomyślnie załadowano dane";
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                List<Table> emList = dc.Tables.ToList();
                if (emList.Count > 0)
                {
                    var xEle = new XElement("Employees",
                        from emp in emList
                        select new XElement("Employee",
                            new XElement("EmployeeID", emp.EmployeeID),
                            new XElement("CompanyName", emp.CompanyName),
                            new XElement("ContactName", emp.ContactName),
                            new XElement("ContactTitle", emp.ContactTitle),
                            new XElement("EmployeeAddress", emp.EmployeeAddress),
                            new XElement("PostalCode", emp.PostalCode)
                            ));
                    HttpContext context = HttpContext.Current;
                    context.Response.Write(xEle);
                    context.Response.ContentType = "application/xml";
                    context.Response.AppendHeader("Content-Disposition", "attachment; filename=EmployeeData.xml");
                    context.Response.End();
                }
            }
        }
    }
}