using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;

[assembly: AssemblyTitle("Elysium.Test.exe")]
[assembly: AssemblyDescription("Test WPF Metro style application")]
[assembly: AssemblyProduct("Elysium")]
[assembly: AssemblyCopyright("Copyright © Aleksandr Vishnyakov & Codeplex community 2011-2013")]

[assembly: SecurityRules(SecurityRuleSet.Level2)]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(false)]

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]

[assembly: NeutralResourcesLanguage("en", UltimateResourceFallbackLocation.MainAssembly)]

#if NETFX4
[assembly: AssemblyVersion("2.0.357.4")]
[assembly: AssemblyFileVersion("2.0.357.4")]
#elif NETFX45
[assembly: AssemblyVersion("2.0.333.4")]
[assembly: AssemblyFileVersion("2.0.333.4")]
#endif
[assembly: AssemblyInformationalVersion("2.0 SP4")]
