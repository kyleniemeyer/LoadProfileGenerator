﻿using System.Collections.Generic;
using System.IO;

namespace ReleaseMaker {
    public class LinuxFileCopier:CopierBase {
        public static void CopySimEngineLinuxFiles([JetBrains.Annotations.NotNull] string src, [JetBrains.Annotations.NotNull] string dst)
        {
            List<string> programFiles = new List<string>();
            var srcDi = new DirectoryInfo(src);
            Copy(programFiles, srcDi, src, dst, @"Autofac.dll");
            Copy(programFiles, srcDi, src, dst, @"Automation.dll");
            Copy(programFiles, srcDi, src, dst, @"CalcPostProcessor.dll");
            Copy(programFiles, srcDi, src, dst, @"CalculationController.dll");
            Copy(programFiles, srcDi, src, dst, @"CalculationEngine.dll");
            Copy(programFiles, srcDi, src, dst, @"ChartCreator2.dll");
            Copy(programFiles, srcDi, src, dst, @"Common.dll");
            Copy(programFiles, srcDi, src, dst, @"createdump");
            Copy(programFiles, srcDi, src, dst, @"Database.dll");
            Copy(programFiles, srcDi, src, dst, @"EntityFramework.dll");
            Copy(programFiles, srcDi, src, dst, @"EntityFramework.SqlServer.dll");
            Copy(programFiles, srcDi, src, dst, @"HarfBuzzSharp.dll");
            Copy(programFiles, srcDi, src, dst, @"JetBrains.Annotations.dll");
            Copy(programFiles, srcDi, src, dst, @"libclrjit.so");
            Copy(programFiles, srcDi, src, dst, @"libcoreclr.so");
            Copy(programFiles, srcDi, src, dst, @"libcoreclrtraceptprovider.so");
            Copy(programFiles, srcDi, src, dst, @"libdbgshim.so");
            Copy(programFiles, srcDi, src, dst, @"libhostfxr.so");
            Copy(programFiles, srcDi, src, dst, @"libhostpolicy.so");
            Copy(programFiles, srcDi, src, dst, @"libmscordaccore.so");
            Copy(programFiles, srcDi, src, dst, @"libmscordbi.so");
            Copy(programFiles, srcDi, src, dst, @"libSkiaSharp.so");
            Copy(programFiles, srcDi, src, dst, @"libSystem.IO.Compression.Native.a");
            Copy(programFiles, srcDi, src, dst, @"libSystem.IO.Compression.Native.so");
            Copy(programFiles, srcDi, src, dst, @"libSystem.IO.Ports.Native.so");
            Copy(programFiles, srcDi, src, dst, @"libSystem.Native.a");
            Copy(programFiles, srcDi, src, dst, @"libSystem.Native.so");
            Copy(programFiles, srcDi, src, dst, @"libSystem.Net.Security.Native.a");
            Copy(programFiles, srcDi, src, dst, @"libSystem.Net.Security.Native.so");
            Copy(programFiles, srcDi, src, dst, @"libSystem.Security.Cryptography.Native.OpenSsl.a");
            Copy(programFiles, srcDi, src, dst, @"libSystem.Security.Cryptography.Native.OpenSsl.so");
            Copy(programFiles, srcDi, src, dst, @"Microsoft.CSharp.dll");
            Copy(programFiles, srcDi, src, dst, @"Microsoft.VisualBasic.Core.dll");
            Copy(programFiles, srcDi, src, dst, @"Microsoft.VisualBasic.dll");
            Copy(programFiles, srcDi, src, dst, @"Microsoft.Win32.Primitives.dll");
            Copy(programFiles, srcDi, src, dst, @"Microsoft.Win32.Registry.AccessControl.dll");
            Copy(programFiles, srcDi, src, dst, @"Microsoft.Win32.Registry.dll");
            Copy(programFiles, srcDi, src, dst, @"Microsoft.Win32.SystemEvents.dll");
            Copy(programFiles, srcDi, src, dst, @"mscorlib.dll");
            Copy(programFiles, srcDi, src, dst, @"netstandard.dll");
            Copy(programFiles, srcDi, src, dst, @"Newtonsoft.Json.dll");
            Copy(programFiles, srcDi, src, dst, @"OxyPlot.dll");
            Copy(programFiles, srcDi, src, dst, @"OxyPlot.SkiaSharp.dll");
            Copy(programFiles, srcDi, src, dst, @"PowerArgs.dll");
            Copy(programFiles, srcDi, src, dst, @"simengine2");
            Copy(programFiles, srcDi, src, dst, @"simengine2.deps.json");
            Copy(programFiles, srcDi, src, dst, @"simengine2.dll");
            Copy(programFiles, srcDi, src, dst, @"simengine2.runtimeconfig.json");
            Copy(programFiles, srcDi, src, dst, @"SimulationEngineLib.dll");
            Copy(programFiles, srcDi, src, dst, @"SkiaSharp.dll");
            Copy(programFiles, srcDi, src, dst, @"SkiaSharp.HarfBuzz.dll");
            Copy(programFiles, srcDi, src, dst, @"SQLite.Interop.dll");
            Copy(programFiles, srcDi, src, dst, @"System.AppContext.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Buffers.dll");
            Copy(programFiles, srcDi, src, dst, @"System.CodeDom.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Collections.Concurrent.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Collections.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Collections.Immutable.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Collections.NonGeneric.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Collections.Specialized.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ComponentModel.Annotations.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ComponentModel.Composition.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ComponentModel.Composition.Registration.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ComponentModel.DataAnnotations.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ComponentModel.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ComponentModel.EventBasedAsync.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ComponentModel.Primitives.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ComponentModel.TypeConverter.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Configuration.ConfigurationManager.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Configuration.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Console.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Core.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Data.Common.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Data.DataSetExtensions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Data.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Data.Odbc.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Data.OleDb.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Data.SqlClient.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Data.SQLite.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Data.SQLite.EF6.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.Contracts.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.Debug.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.DiagnosticSource.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.EventLog.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.FileVersionInfo.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.PerformanceCounter.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.Process.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.StackTrace.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.TextWriterTraceListener.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.Tools.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.TraceSource.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Diagnostics.Tracing.dll");
            Copy(programFiles, srcDi, src, dst, @"System.DirectoryServices.AccountManagement.dll");
            Copy(programFiles, srcDi, src, dst, @"System.DirectoryServices.dll");
            Copy(programFiles, srcDi, src, dst, @"System.DirectoryServices.Protocols.dll");
            Copy(programFiles, srcDi, src, dst, @"System.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Drawing.Common.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Drawing.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Drawing.Primitives.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Dynamic.Runtime.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Formats.Asn1.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Globalization.Calendars.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Globalization.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Globalization.Extensions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.Compression.Brotli.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.Compression.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.Compression.FileSystem.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.Compression.ZipFile.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.FileSystem.AccessControl.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.FileSystem.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.FileSystem.DriveInfo.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.FileSystem.Primitives.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.FileSystem.Watcher.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.IsolatedStorage.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.MemoryMappedFiles.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.Packaging.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.Pipes.AccessControl.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.Pipes.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.Ports.dll");
            Copy(programFiles, srcDi, src, dst, @"System.IO.UnmanagedMemoryStream.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Linq.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Linq.Expressions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Linq.Parallel.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Linq.Queryable.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Management.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Memory.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.Http.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.Http.Json.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.HttpListener.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.Mail.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.NameResolution.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.NetworkInformation.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.Ping.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.Primitives.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.Requests.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.Security.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.ServicePoint.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.Sockets.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.WebClient.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.WebHeaderCollection.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.WebProxy.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.WebSockets.Client.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Net.WebSockets.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Numerics.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Numerics.Vectors.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ObjectModel.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Private.CoreLib.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Private.DataContractSerialization.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Private.ServiceModel.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Private.Uri.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Private.Xml.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Private.Xml.Linq.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.Context.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.DispatchProxy.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.Emit.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.Emit.ILGeneration.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.Emit.Lightweight.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.Extensions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.Metadata.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.Primitives.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Reflection.TypeExtensions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Resources.Reader.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Resources.ResourceManager.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Resources.Writer.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Caching.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.CompilerServices.Unsafe.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.CompilerServices.VisualC.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Extensions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Handles.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.InteropServices.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.InteropServices.RuntimeInformation.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Intrinsics.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Loader.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Numerics.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Serialization.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Serialization.Formatters.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Serialization.Json.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Serialization.Primitives.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Runtime.Serialization.Xml.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.AccessControl.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Claims.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.Algorithms.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.Cng.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.Csp.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.Encoding.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.OpenSsl.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.Pkcs.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.Primitives.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.ProtectedData.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.X509Certificates.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Cryptography.Xml.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Permissions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Principal.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.Principal.Windows.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Security.SecureString.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceModel.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceModel.Duplex.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceModel.Http.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceModel.NetTcp.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceModel.Primitives.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceModel.Security.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceModel.Syndication.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceModel.Web.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceProcess.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ServiceProcess.ServiceController.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Speech.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Text.Encoding.CodePages.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Text.Encoding.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Text.Encoding.Extensions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Text.Encodings.Web.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Text.Json.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Text.RegularExpressions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.AccessControl.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.Channels.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.Overlapped.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.Tasks.Dataflow.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.Tasks.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.Tasks.Extensions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.Tasks.Parallel.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.Thread.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.ThreadPool.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Threading.Timer.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Transactions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Transactions.Local.dll");
            Copy(programFiles, srcDi, src, dst, @"System.ValueTuple.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Web.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Web.HttpUtility.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Windows.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Windows.Extensions.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Xml.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Xml.Linq.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Xml.ReaderWriter.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Xml.Serialization.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Xml.XDocument.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Xml.XmlDocument.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Xml.XmlSerializer.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Xml.XPath.dll");
            Copy(programFiles, srcDi, src, dst, @"System.Xml.XPath.XDocument.dll");
            Copy(programFiles, srcDi, src, dst, @"Utf8Json.dll");
            Copy(programFiles, srcDi, src, dst, @"WindowsBase.dll");
            Copy(programFiles, srcDi, src, dst, @"xunit.abstractions.dll");
            Copy(programFiles, srcDi, src, dst, @"xunit.runner.json");
           CheckIfFilesAreCompletelyCopied(src, programFiles);
        }


    }
}