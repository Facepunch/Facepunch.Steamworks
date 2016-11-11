using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
    public partial class CodeWriter
    {
        public static string[] HasReturnTypes = new string[]
        {
           "ISteamApps.GetAppBetas",
           "ISteamApps.GetAppBuilds",
        };

        private void WebApi()
        {
            foreach ( var o in webdef.apilist.interfaces )
            {
                //
                // Skip any that end in numbers
                if ( char.IsNumber( o.name.Last() ) )
                    continue;

                WriteLine( $"/// <summary>" );
                WriteLine( $"/// {o.name}" );
                WriteLine( $"/// </summary>" );
                StartBlock( $"public partial class {o.name}" );
                {
                    WriteLine( $"private const string Url = \"http://api.steampowered.com/{o.name}/\";" );
                    WriteLine();


                    foreach ( var m in o.methods.OrderBy( x => x.name ) )
                    {
                        var LatestVersion = o.methods
                                            .Where( x => x.name == m.name )
                                            .Max( x => x.version );

                        // Skip this method if it's not the latest
                        if ( LatestVersion != m.version )
                            continue;

                        WebApiMethod( o.name, m );
                    }
                }
                EndBlock();
                WriteLine();
                
            }
        }

        private void WebApiMethod( string parent, WebApiDefinition.ApiList.Interface.Method m )
        {
            var pars = m.parameters.Where ( x => x.name != "key" );

            var parameters = string.Join( ", ", pars.Select( x => FormatParameter( x ) ).Where( x => x != null ) );

            foreach ( var p in pars )
            {
                WriteLine( $"/// <param name=\"{p.name.Replace( "[0]", "[]" ) }\">{p.description}</param>" );
            }

            var returnType = "string";

            if ( HasReturnTypes.Contains( $"{parent}.{m.name}" ) )
            {
                returnType = m.name + "Response";
            }

            StartBlock( $"public static {returnType} {m.name}({parameters})" );
            {
                if ( m.httpmethod == "GET" )
                {
                    var getParameters = string.Join( "&", pars.Select( x => FormatGetParameter( x ) ).Where( x => x != null ) );
                    WriteLine( $"string url = $\"{{Url}}{m.name}/v{m.version.ToString( "0000" )}/?key={{Facepunch.SteamApi.Config.Key}}&format=json&{getParameters}\";" );
                    WriteLine( $"return Utility.WebGet<{returnType}>( url );" );
                }
                else
                {
                    WriteLine( $"string url = $\"{{Url}}/{m.name}/v{m.version.ToString( "0000" )}/\";" );
                    WriteLine( $"return url;" );
                }

            }
            EndBlock();
            WriteLine();
        }

        private object FormatGetParameter( WebApiDefinition.ApiList.Interface.Method.Parameter x )
        {
            return $"{x.name}={{{x.name}}}";
        }

        private object FormatParameter( WebApiDefinition.ApiList.Interface.Method.Parameter x )
        {
            var name = x.name;

            var type = ToManagedType( x.type );

            if ( type == "{message}" ) type = "string";
            if ( type == "rawbinary" ) type = "byte[]";

            if ( name.EndsWith( "[0]" ) )
            {
                name = name.Replace( "[0]", "" );
                type += "[]";
            }

            return $"{type} {name}";
        }


    }
}
