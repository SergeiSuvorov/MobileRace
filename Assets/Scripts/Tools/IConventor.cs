namespace Tools
{
    public interface IConventor<T>
    {
        T Parse(string strValue);
    }

}

