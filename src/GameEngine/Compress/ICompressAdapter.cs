/***
 * ICompressAdapter.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
	public interface ICompressAdapter
    {
		byte[] Compress(byte[] bytes, int level);

		byte[] UnCompress(byte[] bytes);
	}
}
