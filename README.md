# NM.Tools.Dotnet.MSbuild.BuildXml
.Net tool for making basic MSBuild XML templetes on directory chain.


## How to install
|.Net CLI|
|:---|
|dotnet tool install --global NM.Tools.Dotnet.MSbuild.BuildXml|


## How to use
|.Net CLI|
|:---|
|dotnet **buildxml** \<From\> \<To\> [options]|


## Details
```
Description:
  Make basic MSBuild XML templetes on directory chain.
  It does not overwrite existing files.
  If *.*proj files exist, it does not make any file.

Usage:
  buildxml <From> <To> [options]

Arguments:
  <From>  Directory path or child file path to start making MSBuild XML. Directory of file at the path SHOULD exist.
  <To>    Directory path or child file path to end making MSBuild XML. Directory of file at the path SHOULD exist.

Options:
  -np, --no-props    Do not make Directory.Build.props files
  -nt, --no-targets  Do not make Directory.Build.targets files
```