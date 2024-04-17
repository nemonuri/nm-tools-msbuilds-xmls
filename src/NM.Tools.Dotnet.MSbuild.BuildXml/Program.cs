namespace NM.Tools.Dotnet.MSbuild.BuildXml;

internal class Program
{
    public static void Main(string[] args)
    {
        var r = new MainCommand();
        r.Invoke(args);
    }
}