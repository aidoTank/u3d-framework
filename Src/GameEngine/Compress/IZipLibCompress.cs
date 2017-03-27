/***
 * IZipLibCompress.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
	public interface IZipLibCompress
    {
		byte[] Compress(byte[] bytes, int level = 6);

        byte[] UnCompress(byte[] bytes);
	}
}
