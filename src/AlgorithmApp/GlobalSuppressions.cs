// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "This is a console application without localization requirements")]
[assembly: SuppressMessage("Performance", "CA1852:Seal internal types", Justification = "Sealing is not necessary for this application and may limit future extensibility")]
