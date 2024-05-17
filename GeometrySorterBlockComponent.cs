using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Runtime.InteropServices;


//░▒█▀▀█░█▀▀░▄▀▀▄░█▀▄▀█░█▀▀░▀█▀░█▀▀▄░█░░█░▒█▀▀▀█░▄▀▀▄░█▀▀▄░▀█▀░█▀▀░█▀▀▄░▒█▀▀▄░█░░▄▀▀▄░█▀▄░█░▄
//░▒█░▄▄░█▀▀░█░░█░█░▀░█░█▀▀░░█░░█▄▄▀░█▄▄█░░▀▀▀▄▄░█░░█░█▄▄▀░░█░░█▀▀░█▄▄▀░▒█▀▀▄░█░░█░░█░█░░░█▀▄
//░▒█▄▄▀░▀▀▀░░▀▀░░▀░░▒▀░▀▀▀░░▀░░▀░▀▀░▄▄▄▀░▒█▄▄▄█░░▀▀░░▀░▀▀░░▀░░▀▀▀░▀░▀▀░▒█▄▄█░▀▀░░▀▀░░▀▀▀░▀░▀



namespace GeometrySorterBlock
{
    public class GeometrySorterBlockComponent : GH_Component
    {
        
        public GeometrySorterBlockComponent()
          : base("GeometrySorterBlock", "GSB",
            "Geometry Sorter Component To Separate List Of Geometries And Put Each In Branches",
            "Sets", "List")
        {
        }

        
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            
            pManager.AddGeometryParameter("Geometries", "G", "List of Geometries",GH_ParamAccess.list);
            
        }

        
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            
            pManager.AddGeometryParameter("SortedTree", "S", "Sorted tree of data", GH_ParamAccess.tree);
            

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            var geometries = new List<GeometryBase>();


            if (!DA.GetDataList(0, geometries) && (geometries.Count == 0 && geometries == null))
            {

                AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "No Data To Process , Input A list Of Geometries");
                return;// Don't forget to return after adding a warning message
            }
            

            // Filter out non-GeometryBase elements
            DA.SetDataTree(0, GeometriesSorter(geometries));
            
           

        }
        public DataTree<GeometryBase> GeometriesSorter(List<GeometryBase> Geometries)
        {
            // Create a dictionary to hold sorted geometries
            var sortedGeometries = new Dictionary<string, List<GeometryBase>>();

            // Iterate through the list of geometries
            foreach (var geometry in Geometries)
            {
                // Get the type of the geometry
                string type = geometry.GetType().Name;

                // If the type is not yet in the dictionary, add it
                if (!sortedGeometries.ContainsKey(type))
                {
                    sortedGeometries[type] = new List<GeometryBase>();
                }

                // Add the geometry to the correct list based on its type
                sortedGeometries[type].Add(geometry);

            }

            // Convert the dictionary to a tree structure for output
            DataTree<GeometryBase> tree = new DataTree<GeometryBase>();
            int i = 0;
            foreach (var keyValuePair in sortedGeometries)
            {
                // Create a path for each type
                GH_Path path = new GH_Path(i);
                // Add the geometries to the tree under this path
                tree.AddRange(keyValuePair.Value, path);
                i++;
            }
            return tree;
        }



        public override GH_Exposure Exposure => GH_Exposure.primary;

        
        protected override System.Drawing.Bitmap Icon => Properties.Resources.sort_G_03;

        
        public override Guid ComponentGuid => new Guid("4C404F87-31AE-4891-8231-C44BBDC75BC8");
    }
}