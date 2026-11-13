using System;
using System.IO;
using System.Text;

namespace RegularPolygons
{
    class Broker
    {
        public void Process(string filename)
        {
            // Assumptions: 
            //  . ErrorHandling is not a priority and is reduced to a minimum
            //  . <filename> is a path to a well-formatted text file
            //  . Polygons are created sequentially and are in memory only long enough to output the required data
            //  . Each line contains two tokens - the polygon type and the polygon side length
            if (File.Exists(filename))
            {
                using (var inputfile = new StreamReader(filename))
                {
                    using (var outputfile = new StreamWriter(filename + ".output"))
                    {
                        string inputline;
                        string outputline;

                        string[] tokens;
                        string polygon_type;
                        double polygon_size;
                        IAreable polygon;

                        while ((inputline = inputfile.ReadLine()) != null)
                        {
                            tokens = inputline.Split(',');
                            polygon_type = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(tokens[0]);
                            polygon_size = Convert.ToDouble(tokens[1]);

                            try
                            {
                                polygon = (IAreable)System.Activator.CreateInstance(Type.GetType("RegularPolygons." + polygon_type), new object[] { polygon_type, polygon_size });
                            }
                            catch
                            {
                                polygon = new RegularPolygon(polygon_type, polygon_size);
                            }

                            outputline = polygon.GetDescription();
                            outputfile.WriteLine(outputline);
                            Console.WriteLine(outputline);
                        }
                    }
                }
            }
        }
    }
}
