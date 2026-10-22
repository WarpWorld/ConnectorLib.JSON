global using System;
global using System.Buffers;
global using System.Collections;
global using System.Collections.Generic;
global using System.Collections.Specialized;
global using System.Drawing;
global using System.IO;
global using System.Linq;
global using System.Net;
global using System.Net.Http;
global using System.Net.Sockets;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Runtime.Serialization;
global using System.Text;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Newtonsoft.Json;
global using Newtonsoft.Json.Linq;

[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]
