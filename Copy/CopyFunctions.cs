using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Copy
{
    public static class CopyFunctions
    {
        public const long BUFFER_SIZE = 10 * 1024 * 1024;

        public static FileStream GetInputFile( string inputFilename )
        {
            return new FileStream( inputFilename, FileMode.Open, FileAccess.Read, FileShare.Read );
        }

        public static FileStream CreateOutputFile( FileStream inputFile, string outputFilename )
        {
            var stream = new FileStream( outputFilename, FileMode.Create, FileAccess.Write, FileShare.None );
            stream.SetLength( inputFile.Length );

            return stream;
        }

        public static void PartialCopyFile( FileStream fileIn, FileStream fileOut, long offset, long totalBytes, long bufferSize )
        {
            fileIn.Seek( offset, SeekOrigin.Begin );

            int bytesRead;
            byte[] buffer   = new byte[ bufferSize ];
            long bytesLeft  = totalBytes;

            while ( bytesLeft > 0 )
            {
                bytesRead = fileIn.Read( buffer, 0, (int)Math.Min( bytesLeft, bufferSize ) );
                if ( bytesRead == 0 )
                    break;

                fileOut.Write( buffer, 0, bytesRead );

                bytesLeft -= bytesRead;
            }
        }

        public static void PartialCopyFile( FileStream fileIn, FileStream fileOut, long offset, long totalBytes ) => PartialCopyFile( fileIn, fileOut, offset, totalBytes, bufferSize: BUFFER_SIZE  );

        public static void CopyFile( FileStream fileIn, FileStream fileOut, long bufferSize )
        {
            PartialCopyFile( fileIn, fileOut, 0, fileIn.Length, bufferSize );
        }

        public static void CopyFile( FileStream fileIn, FileStream fileOut ) => CopyFile( fileIn, fileOut, bufferSize: BUFFER_SIZE );
    }
}
