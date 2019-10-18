# ClientAssignment
Building instructions:
1. Install Visual Studio 2019
2. Make sure .NET Framework 4.7.2 Targeting Pack is installed in your Visual Studio
3. Click Solution->Restore Nuget Packages
4. Build/Run the project (ctrl+F5 or F5)

Running instructions:
1. Make sure `Visual C++ 2015` is installed (`x86` or `x64` depending on your build). Download link - https://www.microsoft.com/en-us/download/details.aspx?id=52685
2. Run ClientAssignment.exe


P.S. The CefSharp ChromiumWebBrowser component based on the chromium engine was used, because the standard component WebBrowser in WPF is based on the Explorer engine and has problems with displaying complex markup as well as visual rendering problems.
