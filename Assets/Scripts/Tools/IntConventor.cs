using System;

namespace Tools
{
    public class IntConventor : IConventor<int>
    {
        public int Parse(string strValue)
        {
            return Int32.Parse(strValue);
        }
    }

}

