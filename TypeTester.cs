using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace UserDataUtils
{
    public class TypeTesterComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public TypeTesterComponent()
          : base("TypeTesterComponent", "TUD",
              "Tests type.",
              "Speckle", "User Data Utils")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //pManager.AddGenericParameter("Object", "O", "Object to attach user data to.", GH_ParamAccess.item);
            pManager.AddGeometryParameter("Geo", "G", "Geometry to attach user data to.", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Test Result", "T", "Object with user data.", GH_ParamAccess.item);
        }


        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //object obj1 = null;
            //DA.GetData(0, ref obj1);

            object obj2 = null;
            DA.GetData(0, ref obj2);

            Debug.WriteLine("////////////////////////////SOLVE RUN///");

            var theValue = obj2.GetType().GetProperty("Value").GetValue(obj2, null);
            GeometryBase geometry = null;

            if (theValue is Circle)
                geometry = ((Circle)theValue).ToNurbsCurve() as GeometryBase;
            else if (theValue is Line)
                geometry = ((Line)theValue).ToNurbsCurve() as GeometryBase;
            else if (theValue is Point3d)
                geometry = new Point((Point3d)theValue) as GeometryBase;
            else
                geometry = theValue as GeometryBase;

            DA.SetData(0, geometry);

            //GH_Goo<GeometryBase> xx = obj2 as GH_Goo<GeometryBase>;
            //if (xx != null)
            //    DA.SetData(0, xx.Value.GetType().ToString());
            //else
            //    DA.SetData(0, "mep");
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return Properties.Resources.SetUserData;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{964ab773-17bd-4c5f-aea4-d2e773121912}"); }
        }
    }
}
