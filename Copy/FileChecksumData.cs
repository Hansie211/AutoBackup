using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Copy
{
    public class FileChecksumData
    {
        private const long BLOCKSIZE = 10 * 1024 * 1024;

        public string Filename { get; }
        public uint[] Checksums { get; }

        public uint this[ int index ] { get => Checksums[ index ]; set => Checksums[ index ] = value; }
        public int Count => Checksums.Length;

        private FileChecksumData( string filename, uint[] checksums )
        {
            Filename    = filename;
            Checksums   = checksums;
        }

        public FileChecksumData( string filename ) : this( filename, GetChecksums(filename) ) { }

        public static FileChecksumData Load( string filename, uint[] checksums )
        {
            return new FileChecksumData( filename, checksums );
        }

        private static uint[] GetChecksums( string filename )
        {
            using ( var fs = new FileStream( filename, FileMode.Open, FileAccess.Read, FileShare.Read ) )
            {
                long checksumCount  = (fs.Length + BLOCKSIZE - 1) / BLOCKSIZE;
                uint[] result       = new uint[ checksumCount ];
                byte[] buffer       = new byte[ BLOCKSIZE ];

                int index = 0;

                while( fs.Position < fs.Length )
                {
                    int bytesRead   = fs.Read( buffer, 0, (int)Math.Min( fs.Length - fs.Position, BLOCKSIZE ) );
                    result[ index ] = CRC32.Checksum( buffer, 0, bytesRead );
                }

                return result;
            }
        }
    }
}
