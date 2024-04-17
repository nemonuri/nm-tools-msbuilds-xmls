namespace NM.Tools.Dotnet.MSbuild.BuildXml;

internal static partial class SR
{
   public const string DirectoryBuildProps = "Directory.Build.props";
   public const string DirectoryBuildTargets = "Directory.Build.targets";

   public const string DirectoryBuildPropsBasicTemplate = 
"""
<Project>
  <Import Project="$([MSBuild]::GetPathOfFileAbove('Directory.Build.props', '$(MSBuildThisFileDirectory)../'))"
          Condition="$([MSBuild]::GetPathOfFileAbove('Directory.Build.props', '$(MSBuildThisFileDirectory)../')) != ''" />
  <PropertyGroup>


  </PropertyGroup>
</Project>
"""
;

    public const string DirectoryBuildTargetsBasicTemplate = 
"""
<Project>

  <Import Project="$([MSBuild]::GetPathOfFileAbove('Directory.Build.targets', '$(MSBuildThisFileDirectory)../'))"
          Condition="$([MSBuild]::GetPathOfFileAbove('Directory.Build.targets', '$(MSBuildThisFileDirectory)../')) != ''" />
</Project>
"""
;

    public const string MainCommandDiscription =
"""
Make basic MSBuild XML templetes on directory chain.
It does not overwrite existing files.
If *.*proj files exist, it does not make any file.
"""
;
    public const string ShouldExistDiscriptionSnippet = "Directory of file at the path SHOULD exist.";
    public const string FromArgumentDiscription = "Directory path or child file path to start making MSBuild XML."+" "+ShouldExistDiscriptionSnippet;
    public const string ToArgumentDiscription = "Directory path or child file path to end making MSBuild XML."+" "+ShouldExistDiscriptionSnippet;

    public const string NoPropsOptionDiscription = $"Do not make {SR.DirectoryBuildProps} files";
    public const string NoTargetsOptionDiscription = $"Do not make {SR.DirectoryBuildTargets} files";

}
