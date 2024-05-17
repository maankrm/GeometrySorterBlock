using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace GeometrySorterBlock
{
    public class GeometrySorterBlockInfo : GH_AssemblyInfo
    {
        public override string Name => "GeometrySorterBlock";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => Properties.Resources.sort_G_03;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "This Concept To Get List Of 3D Geometries Then Put Each Type In A Branch";

        public override Guid Id => new Guid("D825004F-E009-4459-8DD2-CBF5F1B328D3");

        //Return a string identifying you or your company.
        public override string AuthorName => "Parastorm lab - maan abdulkareem ";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "maanfordesign@gmail.com";
    }
}