using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using MelonLoader;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("FckWorldCheck")]
[assembly: AssemblyDescription("Allows to bypass world checks used by different VRChat mods")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("FckWorldCheck")]
[assembly: AssemblyCopyright("Copyright © 2021 Xavi")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("0b7b27d8-ccbb-41ab-895d-6dfbe07c0e27")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.0")]
[assembly: AssemblyFileVersion("1.1.0")]

[assembly: MelonInfo(typeof(FckWorldCheck.FckWorldCheck), "FckWorldCheck", "1.1.0", "Xavi")]
[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonPriority(-100)]

[assembly: MelonOptionalDependencies("emmVRCLoader", "VRChatUtilityKit", "SeatMod")]