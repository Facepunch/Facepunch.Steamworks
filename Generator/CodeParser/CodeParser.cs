using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Generator
{
    class CodeParser
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
            {
                var r = new Regex( @"enum { (k_i(?:.+)) = ([0-9]+) };" );
                var ma = r.Matches( Content );

                foreach ( Match m in ma )
                {
                    def.CallbackIds.Add( m.Groups[1].Value.Replace( "k_i", "" ).Replace( "Callbacks", "" ), int.Parse( m.Groups[2].Value ) );
                }
            }

            //
            // Associate callbackIds with structs
            //
            foreach ( var t in def.structs )
            {
                var r = new Regex( @"struct "+t.Name+@"\n{ ?\n(?:.)+enum { k_iCallback = (.+) \+ ([0-9]+)", RegexOptions.Multiline | RegexOptions.IgnoreCase );
                var m = r.Match( Content );
                if ( m.Success )
                {
                    var kName = m.Groups[1].Value;
                    var num = m.Groups[2].Value;

                    kName = kName.Replace( "k_i", "CallbackIdentifiers." ).Replace( "Callbacks", "" );

                    t.CallbackId = $"{kName} + {num}";
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

        }
    }
}
