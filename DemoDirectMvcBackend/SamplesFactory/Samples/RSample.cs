using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using OLAPDemoASP.Code;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web;
using RadarSoft.RadarCube.Web.Analysis;
using RadarSoft.RadarCube.Web.Mvc;

namespace SamplesFactory
{
    public class RSample : Sample
    {
        public RSample(SamplesModel samplesModel)
            : base(samplesModel)
        {
        }

        protected MvcOlapAnalysis _OlapAnalysis;
        public virtual MvcOlapAnalysis OlapAnalysis
        {
            get
            {
                if (_OlapAnalysis == null)
                {
                    _OlapAnalysis = new MvcOlapAnalysis("MvcOlapAnalysis1");

                    _OlapAnalysis.InitOlap += delegate { InitOlapAnalysis(); };

                    var cube = new MvcRCube
                    {
                        DataSet = new Northwind(),
                        ID = "Cube_MvcOlapAnalysis1",
                    };
                    cube.OnCalculateField += Cube_OnCalculateField;
                    _OlapAnalysis.Cube = cube;
                }
                return _OlapAnalysis;
            }
        }

        protected override void InitOlapAnalysis()
        {
            InitSample();
            DoActive();
            OlapAnalysis.BeginUpdate();
            InitLayout();
            OlapAnalysis.EndUpdate();
        }

        protected override void InitSample()
        {
            OlapAnalysis.CallbackController = "OlapAnalysis";
            OlapAnalysis.CallbackAction = "CallbackHandler";
            OlapAnalysis.ExportController = "OlapAnalysis";
            OlapAnalysis.ExportAction = "ExportHandler";

            OlapAnalysis.SetDefaultToolboxSettings();
        }

        protected override void DoActive()
        {
            if (OlapAnalysis.Cube.Active)
            {
                OlapAnalysis.ClearAxesLayout();
            }

            if (OlapAnalysis.Cube.Active == false)
            {
                InitCubeStructure();
                ((MvcRCube)OlapAnalysis.Cube).FactTableName = "Order Details";
                OlapAnalysis.Cube.Active = true;
            }
        }

        protected override void InitLayout()
        {
//            OlapAnalysis.ClearAxesLayout();
        }


        protected virtual void InitCubeStructure()
        {
            var cube = OlapAnalysis.Cube as TOLAPCube;
            var d = cube.DataSet as Northwind;
            if (d == null)
                throw new ApplicationException("The cube's DataSet property must be assigned before setting up the structure");

            // Make attribute hierarchies in the "Customers", "Shippers" and "Products" dimensions 
            cube.AddHierarchy("Customers", d.Customers, "CompanyName", "", "Customers");
            cube.AddHierarchy("Shippers", d.Shippers, "CompanyName", "", "Shippers");
            cube.AddHierarchy("Products", d.Suppliers, "CompanyName", "", "Suppliers");
            TCubeHierarchy H1 = cube.AddHierarchy("Products", d.Products, "ProductName", "", "Products");
            TCubeHierarchy H2 = cube.AddHierarchy("Products", d.Categories, "CategoryName", "", "Categories");


            // Make two composite (multilevel) hierarchies in the "Products" dimension
            cube.MakeUpCompositeHierarchy("Products", "Products by categories", new TCubeHierarchy[] { H2, H1 });
            cube.MakeUpCompositeHierarchy("Products", "Products by suppliers", new string[] { "Suppliers", "Products" });

            // Add BI time hierarchies: "Year", "Quarter", "Month"...
            cube.AddBIHierarchy("Time", d.Orders, "Year", "OrderDate", TBIMembersType.ltTimeYear);
            cube.AddBIHierarchy("Time", d.Orders, "Quarter", "OrderDate", TBIMembersType.ltTimeQuarter);
            cube.AddBIHierarchy("Time", d.Orders, "Month", "OrderDate", TBIMembersType.ltTimeMonthLong);
            // ... and combine them into a single "Date" hierarchy
            cube.MakeUpCompositeHierarchy("Time", "Date", new string[] { "Year", "Quarter", "Month" });

            // The two lines add the calculated hierarchy "Employee Name" into the "Employees" dimension:
            // The "Employee Name" column must be calculated in the TOLAPCube1.OnCalculateField even handler
            cube.AddCalculatedColumn(d.Employees, "Employee Name", typeof(String));
            cube.AddHierarchy("Employees", d.Employees, "Employee Name", "ReportsTo", "Employees");
            // just the same thing might have been done with a single line of code:
            // cube.AddCalculatedHierarchy("Employees", d.Employees, typeof(string), "Employee Name");

            // Add two measures: "Quantity" and "Sales"
            var measure = cube.AddMeasure(d.Order_Details, "Quantity");
            measure.DefaultFormat = "#,###";
            // The "Sales" column must be calculated in the TOLAPCube1.OnCalculateField even handler
            cube.AddCalculatedMeasure(d.Order_Details, typeof(double), "Sales");
        }

        protected void Cube_OnCalculateField(object sender, TCalculateFieldArgs e)
        {
            if (e.ThisTable("Order Details"))
            {
                e.Row["Sales"] = Convert.ToDouble(e.Row["Quantity"]) * Convert.ToDouble(e.Row["UnitPrice"]) * (1 - Convert.ToDouble(e.Row["Discount"]));
            }
            if (e.ThisTable("Employees"))
            {
                e.Row["Employee Name"] = e.Row["FirstName"] + " " + e.Row["LastName"];
            }
        }
    }
}