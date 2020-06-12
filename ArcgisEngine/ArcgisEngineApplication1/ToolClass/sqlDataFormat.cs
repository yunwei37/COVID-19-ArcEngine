using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ToolClass.PublicClass
{
    class sqlDataFormat
    {

        public static string dataFormat(string str)
        {
            str = str.Replace(":00:00","");
            return str;
        }
    }


}
