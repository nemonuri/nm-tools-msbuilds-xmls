namespace NM.Tools.Dotnet.MSbuild.BuildXml;

internal static class CommandExtension
{
    /// <summary>
    /// It just <see cref="Command.AddArgument">AddArgument</see> + return,<br/>
    /// for syntax sugar. 
    /// </summary>
    public static Argument<T> AddAndPass<T>(this Command self, Argument<T> argument)
    {
        self.AddArgument(argument);
        return argument;
    }

    /// <summary>
    /// It just <see cref="Command.AddOption">AddOption</see> + return,<br/>
    /// for syntax sugar. 
    /// </summary>
    public static Option<T> AddAndPass<T>(this Command self, Option<T> option)
    {
        self.AddOption(option);
        return option;
    }
}
