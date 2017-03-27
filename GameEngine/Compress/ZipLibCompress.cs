/***
 * ZipLibCompress.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
	public class ZipLibCompress : IZipLibCompress
    {
		private ICompressAdapter compress;

		public ZipLibCompress(ICompressAdapter adapter){
			compress = adapter;
		}

        public byte[] Compress(byte[] bytes, int level = 6)
        {
            if (bytes == null) {
                return null;
            }
            if (bytes.Length <= 0) {
                return bytes;
            }
            return compress.Compress(bytes, level);
        }

        public byte[] UnCompress(byte[] bytes)
        {
            if (bytes == null) {
                return null;
            }
            if (bytes.Length <= 0) {
                return bytes;
            }
            return compress.UnCompress(bytes);
        }
	}
}

