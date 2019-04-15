using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Generator
{
	partial class CodeParser
	{
		public List<Class> Classes = new List<Class>();

		public void ParseClasses()
		{
			var source = RemoveAnnotations( Content );

			{
				var r = new Regex( @"class ([a-zA-Z]+)[\r|\n]+{[\r|\n]((?s).*?)};" );
				var ma = r.Matches( source );

				foreach ( Match m in ma )
				{
					ProcessClass( m.Groups[0].Value.Trim(), m.Groups[1].Value.Trim(), m.Groups[2].Value.Trim() );
					//def.CallbackIds.Add( m.Groups[1].Value.Substring( 3 ).Replace( "Callbacks", "" ), int.Parse( m.Groups[2].Value ) );
				}
			}

			Console.WriteLine( "OKay" );
		}

		public void ProcessClass( string fulldef, string classname, string inner )
		{
			Console.WriteLine( $"Class: {classname} " );

			var lines = inner.Split( new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries );

			var func = new Regex( @"virtual (.+[ |\*])([a-zA-Z]+?)\((.+?)?\) = 0 ?;" );

			var c = new Class();
			c.Name = classname;

			var interfaceMatch = Regex.Match( Content, $"#define {classname.ToUpper().Substring( 1 )}_INTERFACE_VERSION \"(.+?)\"" );
			if ( interfaceMatch.Success )
			{
				c.InterfaceString = interfaceMatch.Groups[1].Value;
			}

			var lastCallResult = "";

			foreach ( var line in lines )
			{
				if ( line.Trim().Length < 4 ) continue;
				if ( line.Trim().StartsWith( "public:" ) ) continue;
				if ( line.Trim().StartsWith( "//" ) ) continue;

				var callresult = Regex.Match( line, @"STEAM_CALL_RESULT\( (.+?) \)" );
				if ( callresult.Success )
				{
					lastCallResult = callresult.Groups[1].Value.Trim();
					continue;
				}

				var f = func.Match( line );
				if ( f.Success )
				{

					

					var returnType = f.Groups[1].Value.Trim();
					var funcName = f.Groups[2].Value.Trim();
					var args = f.Groups[3].Value.Trim();
					//	Console.WriteLine( $"Function: {funcName} returns {returnType} with args {args}" );


					if ( funcName == "DownloadClanActivityCounts" ) lastCallResult = "DownloadClanActivityCountsResult_t";

					if ( funcName.Contains( ' ' ) || funcName.Contains( '*' ) )
						throw new System.Exception( "Parsing Error!" );

					var fnc = c.AddFunction( funcName, returnType, args );

					fnc.CallResult = lastCallResult;
					lastCallResult = null;
				}
				else
				{
					Console.WriteLine( $"Unknown Line: {line}" );
				}
			}


			Classes.Add( c );
		}

		public string RemoveAnnotations( string str )
		{
			str = Regex.Replace( str, @"STEAM_OUT_ARRAY_CALL\((.+?)\)", "" );
			str = Regex.Replace( str, @"STEAM_PRIVATE_API\((.+)\)", "$1" );
			str = Regex.Replace( str, @"STEAM_ARRAY_COUNT\((.+?)\) ", "" );
			str = Regex.Replace( str, @"STEAM_OUT_STRUCT\(\) ", "" );
			str = Regex.Replace( str, @"STEAM_OUT_STRUCT\((.+?)\) ", "" );

			

			return str;
		}

	}
}
