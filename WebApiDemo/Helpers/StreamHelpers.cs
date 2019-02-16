using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Helpers
{
    public static class StreamHelpers
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            var memoStream = new MemoryStream();
            stream.CopyTo(memoStream);
            return memoStream.ToArray();
        }
    }
}
