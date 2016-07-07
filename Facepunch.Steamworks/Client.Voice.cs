using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facepunch.Steamworks
{
    public partial class Client : IDisposable
    {
        private Voice _voice;

        public Voice Voice
        {
            get
            {
                if ( _voice == null )
                    _voice = new Voice { client = this };

                return _voice;
            }
        }
    }

    public class Voice
    {
        internal Client client;

        public uint OptimalSampleRate
        {
            get { return client._user.GetVoiceOptimalSampleRate(); }
        }
    }
}
