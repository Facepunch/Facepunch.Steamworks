using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Generator
{
	partial class CodeParser
	{
		public class Class
		{
			public string Name;
			public string InterfaceString;

			public class Function
			{
				public string Name;
				public Dictionary<string, string> Arguments = new Dictionary<string, string>();

				public string ReturnType;
				public string CallResult;
			}

			public List<Function> Functions = new List<Function>();			

			internal Function AddFunction( string funcName, string returnType, string args )
			{
				var f = new Function
				{
					Name = funcName,
					ReturnType = returnType
				};

				args = Regex.Replace( args, "", "" );

				foreach ( var arg in args.Split( new[] { ',' }, StringSplitOptions.RemoveEmptyEntries ) )
				{
					var m = Regex.Match( arg.Trim(), @"(.+?[ |\*])?([a-zA-Z0-9_]+?)( = (.+?))?$" );

					var t = m.Groups[1].Value.Trim();
					var n = m.Groups[2].Value.Trim();

					t = Cleanup.ConvertType( t );

					f.Arguments.Add( n, t );
				}

				Functions.Add( f );

				return f;
			}

			public void PostProcess()
			{
				var LastName = "";
				var DuplicateCount = 0;

				for (int i=0; i< Functions.Count; i++ )
				{
					var ThisName = Functions[i].Name;

					if ( Functions[i].Name == LastName)
					{
						DuplicateCount++;
						Functions[i].Name += $"{DuplicateCount + 1}";
					}
					else
					{
						DuplicateCount = 0;
					}

					LastName = ThisName;
				}
			}
		}

	}
}
