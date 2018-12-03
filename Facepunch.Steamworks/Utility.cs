using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Facepunch.Steamworks
{
    public static partial class Utility
    {
        static internal uint Swap( uint x )
        {
            return ((x & 0x000000ff) << 24) +
                   ((x & 0x0000ff00) << 8) +
                   ((x & 0x00ff0000) >> 8) +
                   ((x & 0xff000000) >> 24);
        }

        static public uint IpToInt32( this IPAddress ipAddress )
        {
            return Swap( (uint) ipAddress.Address );
        }

        static public IPAddress Int32ToIp( uint ipAddress )
        {
            return new IPAddress( Swap( ipAddress ) );
        }

        static internal class Epoch
        {
            private static readonly DateTime epoch = new DateTime( 1970, 1, 1, 0, 0, 0, DateTimeKind.Utc );

            /// <summary>
            /// Returns the current Unix Epoch
            /// </summary>
            public static int Current
            {
                get
                {
                    return (int)( DateTime.UtcNow.Subtract( epoch ).TotalSeconds );
                }
            }

            /// <summary>
            /// Convert an epoch to a datetime
            /// </summary>
            public static DateTime ToDateTime( decimal unixTime )
            {
                return epoch.AddSeconds( (long)unixTime );
            }

            /// <summary>
            /// Convert a DateTime to a unix time
            /// </summary>
            public static uint FromDateTime( DateTime dt )
            {
                return (uint)( dt.Subtract( epoch ).TotalSeconds );
            }

        }

        internal static string FormatPrice(string currency, ulong price)
        {
            return FormatPrice(currency, price / 100.0);
        }

        public static string FormatPrice(string currency, double price)
        {
            var decimaled = price.ToString("0.00");

            switch (currency)
            {
                case "AED": return $"{decimaled}د.إ";
                case "ARS": return $"${decimaled} ARS";
                case "AUD": return $"${decimaled} AUD";
                case "BRL": return $"R$ {decimaled}";
                case "CAD": return $"${decimaled} CAD";
                case "CHF": return $"Fr. {decimaled}";
                case "CLP": return $"${decimaled} CLP";
                case "CNY": return $"{decimaled}元";
                case "COP": return $"COL$ {decimaled}";
                case "CRC": return $"₡{decimaled}";
                case "EUR": return $"€{decimaled}";
                case "GBP": return $"£{decimaled}";
                case "HKD": return $"HK$ {decimaled}";
                case "ILS": return $"₪{decimaled}";
                case "IDR": return $"Rp{decimaled}";
                case "INR": return $"₹{decimaled}";
                case "JPY": return $"¥{decimaled}";
                case "KRW": return $"₩{decimaled}";
                case "KWD": return $"KD {decimaled}";
                case "KZT": return $"{decimaled}₸";
                case "MXN": return $"Mex$ {decimaled}";
                case "MYR": return $"RM {decimaled}";
                case "NOK": return $"{decimaled} kr";
                case "NZD": return $"${decimaled} NZD";
                case "PEN": return $"S/. {decimaled}";
                case "PHP": return $"₱{decimaled}";
                case "PLN": return $"zł {decimaled}";
                case "QAR": return $"QR {decimaled}";
                case "RUB": return $"₽{decimaled}";
                case "SAR": return $"SR {decimaled}";
                case "SGD": return $"S$ {decimaled}";
                case "THB": return $"฿{decimaled}";
                case "TRY": return $"₺{decimaled}";
                case "TWD": return $"NT$ {decimaled}";
                case "UAH": return $"₴{decimaled}";
                case "USD": return $"${decimaled}";
                case "UYU": return $"$U {decimaled}"; // yes the U goes after $
                case "VND": return $"₫{decimaled}";
                case "ZAR": return $"R {decimaled}";

                // TODO - check all of them https://partner.steamgames.com/doc/store/pricing/currencies

                default: return $"{decimaled} {currency}";
            }
        }

        public static string ReadNullTerminatedUTF8String( this BinaryReader br, byte[] buffer = null )
        {
            if ( buffer == null )
                buffer = new byte[1024];

            byte chr;
            int i = 0;
            while ( (chr = br.ReadByte()) != 0 && i < buffer.Length )
            {
                buffer[i] = chr;
                i++;
            }

            return Encoding.UTF8.GetString( buffer, 0, i );
        }

        public static IEnumerable<T> UnionSelect<T>(
            this IEnumerable<T> first,
            IEnumerable<T> second,
            Func<T, T, T> selector) where T : IEquatable<T>
        {
            var items = new Dictionary<T, T>();

            foreach (var i in first)
            {
                items[i] = i;
            }

            foreach (var i in second)
            {
                T firstValue;
                if (items.TryGetValue(i, out firstValue))
                {
                    items.Remove(i);
                    yield return selector(firstValue, i);
                }
            }
        }
    }
}
