using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeParser
    {
        public string Content;

        public CodeParser( string folder )
        {
            foreach ( var file in System.IO.Directory.GetFiles( folder, "*.h", System.IO.SearchOption.AllDirectories ) )
            {
                Content += System.IO.File.ReadAllText( file );
            }

            Content = Content.Replace( "\r\n", "\n" );
            Content = Content.Replace( "\n\r", "\n" );
        }

        internal void ExtendDefinition( SteamApiDefinition def )
        {
            //
            // Get a list of CallbackIds
            //
            def.CallbackIds = new Dictionary<string, int>();

			//v1
            {
                var r = new Regex( @"enum { (k_[i|I](?:.+)) = ([0-9]+) };" );
                var ma = r.Matches( Content );

                foreach ( Match m in ma )
                {
                    def.CallbackIds.Add( m.Groups[1].Value.Substring( 3 ).Replace( "Callbacks", "" ), int.Parse( m.Groups[2].Value ) );
                }
            }

			//
			// Associate callbackIds with structs
			//
			foreach ( var t in def.structs )
            {
                if ( !string.IsNullOrEmpty(  t.CallbackId ) ) continue;

                // Standard style
                {
                    var r = new Regex( @"struct "+t.Name+@"\n{ ?\n(?:.)+enum { k_iCallback = (?:(.+) \+ ([0-9]+)|(.+)) };", RegexOptions.Multiline | RegexOptions.IgnoreCase );
                    var m = r.Match( Content );
                    if ( m.Success )
                    {
                        var kName = m.Groups[1].Value;
                        var num = m.Groups[2].Value;

                        if ( string.IsNullOrEmpty( kName ) )
                        {
                            kName = m.Groups[3].Value;
                            num = "0";
                        }

                        kName = "CallbackIdentifiers." + kName.Substring( 3 ).Replace( "Callbacks", "" );

                        t.CallbackId = $"{kName} + {num}";
                    }
                }

                // New style
                {
                    var r = new Regex( @"DEFINE_CALLBACK\( "+t.Name+@", (.+) \+ ([0-9]+) \)" );
                    var m = r.Match( Content );
                    if ( m.Success )
                    {
                        var kName = m.Groups[1].Value;
                        var num = m.Groups[2].Value;

                        //kName = kName.Replace( "k_i", "CallbackIdentifiers." ).Replace( "Callbacks", "" );
                        kName = "CallbackIdentifiers." + kName.Substring( 3 ).Replace( "Callbacks", "" );

                        t.CallbackId = $"{kName} + {num}";
                    }
                }

				// Even Newer Style
				{
					var r = new Regex( @"STEAM_CALLBACK_BEGIN\( " + t.Name + @", (.+) \+ ([0-9]+) \)" );
					var m = r.Match( Content );
					if ( m.Success )
					{
						var kName = m.Groups[1].Value;
						var num = m.Groups[2].Value;

						//kName = kName.Replace( "k_i", "CallbackIdentifiers." ).Replace( "Callbacks", "" );
						kName = "CallbackIdentifiers." + kName.Substring( 3 ).Replace( "Callbacks", "" );

						t.CallbackId = $"{kName} + {num}";
					}
				}
			}

            //
            // Find defines
            //
            def.Defines = new Dictionary<string, string>();
            {
                var r = new Regex( @"#define ([a-zA-Z_]+) ""(.+)""" );
                var ma = r.Matches( Content );

                foreach ( Match m in ma )
                {
                    def.Defines.Add( m.Groups[1].Value.Replace( "Callbacks", "" ), m.Groups[2].Value );
                }
            }

            //
            // Find missing structs
            //
            {
                var r = new Regex( @"struct ([a-zA-Z]+_t)" );
                var ma = r.Matches( Content );

                foreach ( Match m in ma )
                {
                    var s = def.structs.SingleOrDefault( x => x.Name == m.Groups[1].Value );
                    if ( s == null )
                    {
                        Console.WriteLine( "Missing Struct: " + m.Groups[1].Value );
                    }
                }

                //Console.ReadKey();
            }

			//
			// Change all struct bool fields to bytes (they're technically bytes anyway, and it helps with marshalling)
			//
			{
				foreach ( var s in def.structs )
				{
					foreach ( var f in s.Fields )
					{
						if ( f.Type == "bool" )
							f.Type = "byte";
					}
				}
			}

		}
    }
}
