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

           "ISteamEconomy.GetMarketPrices",

           "IWorkshopService.GetItemDailyRevenue"
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
            BeforeLines = new List<string>();

            var pars = m.parameters.Where ( x => x.name != "key" );

            var parameters = string.Join( ", ", pars.Select( x => FormatParameter( x ) ).Where( x => x != null ) );

            foreach ( var p in pars )
            {
                var name = p.name;
                if ( p.type == "uint32" && p.name.Contains( "date" ) )
                {
                    name += "_dt";
                }

                WriteLine( $"/// <param name=\"{name.Replace( "[0]", "[]" ) }\">({p.type}){p.description}</param>" );
            }

            var returnType = "string";

            if ( HasReturnTypes.Contains( $"{parent}.{m.name}" ) )
            {
                returnType = m.name + "Response";
            }

            StartBlock( $"public static {returnType} {m.name}({parameters})" );
            {
                if ( BeforeLines.Count > 0 )
                {
                    WriteLines( BeforeLines );
                    WriteLine();
                }

                if ( m.httpmethod == "GET" )
                {
                    var getParameters = string.Join( "&", pars.Select( x => FormatGetParameter( x ) ).Where( x => x != null ) );
                    WriteLine( $"return Utility.WebGet<{returnType}>( $\"{{Url}}{m.name}/v{m.version.ToString( "0000" )}/?key={{Facepunch.SteamApi.Config.Key}}&format=json&{getParameters}\" );" );
                }
                else if ( m.httpmethod == "POST" )
                {
                    StartBlock( $"var headers = new System.Collections.Generic.Dictionary<string, object>()" );
                    {
                        foreach ( var p in pars )
                        {
                            WriteLine( $"{{ \"{p.name}\", {p.name} }}," );
                        }
                    }
                    EndBlock( ";" );
                    WriteLine();

                    WriteLine( $"return Utility.WebPost<{returnType}>( $\"{{Url}}/{m.name}/v{m.version.ToString( "0000" )}/\", headers );" );
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

            if ( type == "uint" && name.Contains( "date" ) )
            {
                BeforeLines.Add( $"uint {name} = Facepunch.Steamworks.Utility.Epoch.FromDateTime( {name}_dt );" );

                type = "DateTime";
                name += "_dt";
            }

            return $"{type} {name}";
        }


    }
}
