using System;
using System.Collections.Generic;
using System.Text;

namespace Copy
{
    public static class CRC32
    {
        private const uint POLYNOMIAL           = 0xEDB88320;
        private static readonly uint[] crctable = new uint[ 256 ];

        public static uint Checksum( byte[] bytes, int offset, int count )
        {
            uint result = 0xFFFFFFFF;

            for ( int i = offset; i < offset + count; i++ )
            {
                byte index  = (byte)( ((result) & 0xFF) ^ bytes[i]) ;
                result      = ( result >> 8 ) ^ crctable[ index ];
            }

            return ~result;
        }

        public static uint Checksum( byte[] bytes )
        {
            return Checksum( bytes, 0, bytes.Length );
        }

        #region Conversion
        public static uint Checksum( string value )
        {
            return Checksum( Encoding.ASCII.GetBytes( value ) );
        }

        public static uint Checksum( uint value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }

        public static uint Checksum( ushort value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }

        public static uint Checksum( float value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }

        public static uint Checksum( long value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }

        public static uint Checksum( ulong value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }

        public static uint Checksum( short value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }

        public static uint Checksum( double value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }

        public static uint Checksum( char value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }

        public static uint Checksum( bool value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }

        public static uint Checksum( int value )
        {
            return Checksum( BitConverter.GetBytes( value ) );
        }
        #endregion

        static CRC32()
        {
            // Init the table
            for ( uint i = 0; i < crctable.Length; i++ )
            {
                uint value = i;

                for ( int j = 0; j < 8; j++ )
                {
                    if ( ( value & 1 ) == 1 )
                    {
                        value = ( value >> 1 ) ^ POLYNOMIAL;
                    }
                    else
                    {
                        value >>= 1;
                    }
                }

                crctable[ i ] = value;
            }
        }

    }
}
