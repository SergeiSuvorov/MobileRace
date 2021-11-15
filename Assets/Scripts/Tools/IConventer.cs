namespace Tools
{
    public interface IConventer<T>
    {
        T Parse(string strValue);
    }

}

