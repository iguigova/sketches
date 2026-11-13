using System;
using System.Collections.Generic;
using System.Text;

namespace RegularPolygons
{
    // The use of interface is more often the preferred approach
    // as it allows for decoupled implementations and better 
    // extensibility. 
    // In this case, it will allow for more shapes (incl. irregular ones) 
    // to be "described" as having area and perimeter. 
    interface IAreable
    {
        string GetDescription();
        double CalcArea();
        double CalcPerimeter();
    }

    class RegularPolygon: IAreable
    {
        // Regular Polygon type lookup table - 
        // the index of the type corresponds to the shape's
        // number of sides. 
        private string[] regularPolygonTypes = { 
            "circle", 
            "undefined", 
            "undefined",
            "triangle", 
            "square", 
            "pentagon"
        };

        protected int number_of_sides;
        protected string type;
        protected double side;

        protected string desc = "A {0} with {1} {2:0.##} u has a perimeter of {3:0.##} u and an area of {4:0.##} u^2";

        public RegularPolygon(string type, double side)
        {
            this.type = type.ToLower();
            this.side = side;
            this.number_of_sides = Array.IndexOf(regularPolygonTypes, this.type);
        }

        public virtual string GetDescription()
        {
            return string.Format(desc, type, "side length", side, CalcPerimeter(), CalcArea());
        }

        public virtual double CalcArea()
        {
            // http://www.mathwords.com/a/area_regular_polygon.htm
            //return (0.25)*number_of_sides*Math.Pow(side, 2)*(Math.Cos(180/number_of_sides)/Math.Sin(180/number_of_sides));
            return Math.Max(0, (0.25) * number_of_sides * Math.Pow(side, 2) * (1 / Math.Tan(Math.PI / number_of_sides)));
        }

        public virtual double CalcPerimeter()
        {
            return Math.Max(0, (number_of_sides * side));
        }
    }

    // Alternatively, Circle does not extend RegularPolygon
    // but only implements IAreable interface; independetly.
    // However, in this case, since we do not expect more features 
    // to be added to it later, the "less coding" approach is chosen.
    // One can argue that a Circle is a regular shape with no sides...
    class Circle: RegularPolygon
    {
        public Circle(string polygon_type, double polygon_size) : base(polygon_type, polygon_size) { }

        public override string GetDescription()
        {
            return string.Format(desc, type, "radius", side, CalcPerimeter(), CalcArea());
        }

        public override double CalcArea()
        {
            return Math.Max(0, Math.PI*Math.Pow(side, 2));
        }

        public override double CalcPerimeter()
        {
            return Math.Max(0, 2*Math.PI*side);
        }     
    }
}
