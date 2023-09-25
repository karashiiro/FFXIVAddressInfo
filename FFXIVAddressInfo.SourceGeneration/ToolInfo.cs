using System.Diagnostics;
using System.Reflection;

namespace FFXIVAddressInfo.SourceGeneration;

internal static class ToolInfo
{
    internal static readonly Assembly ToolAssembly = typeof(ToolInfo).Assembly;

    internal static readonly string AssemblyName = ToolAssembly.GetName().Name!;

    internal static readonly string AssemblyVersion = FileVersionInfo.GetVersionInfo(ToolAssembly.Location).FileVersion!;
}