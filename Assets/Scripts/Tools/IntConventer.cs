using System;

namespace Tools
{
    public class IntConventer : IConventer<int>
    {
        public int Parse(string strValue)
        {
            int result;
            if (Int32.TryParse(strValue, out result))
                return Int32.Parse(strValue);
            else
                return 0;
        }
    }

}

