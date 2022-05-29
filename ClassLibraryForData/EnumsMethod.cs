using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForData
{
    public class EnumsMethod
    {
        public enum MethodCategory
        {
            categoryPython = 1,
            categoryKalabin = 2
        }

        public enum MethodResponse
        {
            responseString = 1,
            responseLinear = 2,
            responseDouble = 3
        }

        public enum MethodNormalize
        {
            normalizeDouble = 1,
            normalizeMinMax = 2
        }
    }
}
