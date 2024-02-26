namespace ACME.Helper;

public class TypeUtil
{
    public static Dictionary<string, Type> _tiposDeDato = new Dictionary<string, Type>()
    {
        { "int", typeof(int)},
        { "date", typeof(DateTime) },
        { "nvarchar", typeof(string) },
        { "varchar", typeof(string) },
    };
}
