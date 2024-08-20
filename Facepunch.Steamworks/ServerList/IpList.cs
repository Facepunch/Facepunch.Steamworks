using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Steamworks.ServerList
{
	public class IpList : Internet
	{
		public List<string> Ips = new List<string>();
		bool wantsCancel;

		public IpList( IEnumerable<string> list )
		{
			Ips.AddRange( list );
		}

		public IpList( params string[] list )
		{
			Ips.AddRange( list );
		}

		public override async Task<bool> RunQueryAsync( float timeoutSeconds = 10 )
		{
			int blockSize = 16;
			int pointer = 0;

			var ips = Ips.ToArray();

			wantsCancel = false;

			while ( !wantsCancel )
			{
				var sublist = ips.Skip( pointer ).Take( blockSize ).ToList();
				if ( sublist.Count == 0 )
					break;

				using ( var list = new ServerList.Internet() )
				{
					list.AddFilter( "or", sublist.Count.ToString() );

					foreach ( var server in sublist )
					{
						list.AddFilter( "gameaddr", server );
					}

					await list.RunQueryAsync( timeoutSeconds );

					Responsive.AddRange( list.Responsive );
					Responsive = Responsive.Distinct().ToList();
					Unresponsive.AddRange( list.Unresponsive );
					Unresponsive = Unresponsive.Distinct().ToList();
				}

				pointer += sublist.Count();

				InvokeChanges();
			}

			return true;
		}

		// note: Cancel doesn't get called in Dispose because request is always null for this class
		public override void Cancel()
		{
			wantsCancel = true;
		}

		public override void Dispose()
		{
			base.Dispose();

			wantsCancel = true;
		}
	}
}
