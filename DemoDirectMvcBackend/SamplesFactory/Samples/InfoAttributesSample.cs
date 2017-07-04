using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using RadarSoft.RadarCube.Common;
using RadarSoft.RadarCube.Web.Analysis;
using RadarSoft.RadarCube.Web.Mvc;
using SamplesFactory;
using RadarSoft.RadarCube.Web;
using OLAPDemoASP.Code;

namespace SamplesFactory
{
    public class InfoAttributesSample : BaseGridSample
    {
        public InfoAttributesSample(SamplesModel samplesModel) : base(samplesModel)
        {
        }

        protected override void InitCubeStructure()
        {
            var cube = OlapAnalysis.Cube as TOLAPCube;
            var d = cube.DataSet as Northwind;
            if (d == null)
                throw new ApplicationException("The cube's DataSet property must be assigned before setting up the structure");

            // Make the attribute hierarchy "Customers" in the "Customers" dimension.
            TCubeHierarchy H = cube.AddHierarchy("Customers", d.Customers, "CompanyName", "", "Customers");

            //The source field of an attribute should be taken from the table of its hierarchy.
            TInfoAttribute a = new TInfoAttribute();
            a.DisplayName = "Phone";
            a.SourceField = "Phone";
            a.DisplayMode = AttributeDispalyMode.AsColumn;
            H.InfoAttributes.Add(a);

            a = new TInfoAttribute();
            a.DisplayName = "City";
            a.SourceField = "City";
            a.DisplayMode = AttributeDispalyMode.AsTooltip;
            H.InfoAttributes.Add(a);

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
            cube.AddMeasure(d.Order_Details, "Quantity");
            // The "Sales" column must be calculated in the TOLAPCube1.OnCalculateField even handler
            cube.AddCalculatedMeasure(d.Order_Details, typeof(double), "Sales");
        }


        protected override void InitLayout()
        {
            OlapAnalysis.BeginUpdate();

            THierarchy H = OlapAnalysis.Dimensions.FindHierarchyByDisplayName("Customers");
            OlapAnalysis.PivotingLast(H, TLayoutArea.laRow);

            TMeasure M = OlapAnalysis.Measures.FindByDisplayName("Sales");
            if (M != null)
                OlapAnalysis.Pivoting(M, TLayoutArea.laRow, null);

            OlapAnalysis.EndUpdate();
        }
    }
}