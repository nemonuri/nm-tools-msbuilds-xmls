namespace NM.Tools.Dotnet.MSbuild.BuildXml;

internal class MainCommand : RootCommand
{   
    public MainCommand() : base(SR.MainCommandDiscription)
    {
        (var v1, var v2, var v3, var v4) = (
                this.AddAndPass(new FromArgument()),
                this.AddAndPass(new ToArgument()),
                this.AddAndPass(new NoPropsOption()),
                this.AddAndPass(new NoTargetsOption())
            );
        this.SetHandler(InvokeImpl, v1,v2,v3,v4);
    }


    private void InvokeImpl(FileSystemInfo fromDirOrFile,
                            FileSystemInfo toDirOrFile,
                            bool noProps,
                            bool noTargets)
    {
        DirectoryInfo fromDir = (fromDirOrFile is FileInfo fromFile ? fromFile.Directory : (DirectoryInfo)fromDirOrFile) ?? throw new ArgumentNullException(nameof(fromDirOrFile));
        DirectoryInfo toDir = (toDirOrFile is FileInfo toFile ? toFile.Directory : (DirectoryInfo)toDirOrFile) ?? throw new ArgumentNullException(nameof(toDirOrFile));

        (var top, var bottom) = fromDir.FullName.Length <= toDir.FullName.Length ? (fromDir, toDir) : (toDir, fromDir);
        if (!bottom.FullName.StartsWith(top.FullName))
        {
            throw new InvalidOperationException("One should sub-directory of another");
        }

        //---main work---
        Console.WriteLine(@"See https://aka.ms/dotnet/msbuild/customize for more details on customizing your build");
        
        DirectoryInfo? curDir = bottom;

        while (true)
        {
            if (curDir == null) break;
            if (top.Parent?.FullName == curDir.FullName) break;

            var aggResult = curDir.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly).Aggregate(new AggregatedResult(), EnumeratedFileAggregateImpl);

            if (!noProps &&
                !aggResult.IsPropsLikeExist)
            {
                var p =Path.Combine(curDir.FullName, SR.DirectoryBuildProps);
                File.WriteAllText(p, SR.DirectoryBuildPropsBasicTemplate);
                Console.WriteLine($"File created: {p}");
            }
            if (!noTargets &&
                !aggResult.IsTargetsLikeExist)
            {
                var p = Path.Combine(curDir.FullName, SR.DirectoryBuildTargets);
                File.WriteAllText(p, SR.DirectoryBuildTargetsBasicTemplate);
                Console.WriteLine($"File created: {p}");
            }

            curDir = curDir.Parent;
        }
        //---|

        Console.WriteLine("Finished.");
    }

    private AggregatedResult EnumeratedFileAggregateImpl(AggregatedResult growingSeed, FileInfo fileInfo)
    {
        if (fileInfo.Extension.EndsWith("proj"))
        {
            return new(IsPropsLikeExist:true, 
                       IsTargetsLikeExist:true);
        }

        if (fileInfo.Name == SR.DirectoryBuildProps)
        {
            return growingSeed with { IsPropsLikeExist=true };
        }

        if (fileInfo.Name == SR.DirectoryBuildTargets)
        {
            return growingSeed with { IsTargetsLikeExist=true };
        }

        return growingSeed;
    }

    private record struct AggregatedResult(bool IsPropsLikeExist, 
                                           bool IsTargetsLikeExist);
}

internal class NoPropsOption : Option<bool>
{
    public NoPropsOption():base(["--no-props", "-np"], SR.NoPropsOptionDiscription)
    {

    }
}

internal class NoTargetsOption : Option<bool>
{
    public NoTargetsOption():base(["--no-targets", "-nt"], SR.NoTargetsOptionDiscription)
    {
        
    }
}



internal class FromArgument : Argument<FileSystemInfo>
{
    public FromArgument():base("From", SR.FromArgumentDiscription)
    {
        this.ExistingOnly();
    }
}

internal class ToArgument : Argument<FileSystemInfo>
{
    public ToArgument():base("To", SR.ToArgumentDiscription)
    {
        this.ExistingOnly();
    }
}