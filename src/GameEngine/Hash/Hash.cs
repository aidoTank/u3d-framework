/***
 * Hash.cs
 * 
 * @author administrator 
 */
namespace GameEngine
{
	public class Hash : IHash
    {
		private string generateSalt;

        public void SetGenerateSalt(string salt){
            if(!string.IsNullOrEmpty(salt)){
                generateSalt = salt;
            }
        }

        public void SetFactor(int factor){
            generateSalt = BCrypt.BCrypt.GenerateSalt(factor);
        }

        public string Make(string password)
        {
            if (string.IsNullOrEmpty(generateSalt)) {
                generateSalt = BCrypt.BCrypt.GenerateSalt(6);
            }
            return BCrypt.BCrypt.HashPassword(password, generateSalt);
        }

		public bool Check(string text, string hash){
			return BCrypt.BCrypt.Verify(text , hash);
		}

        public string HashFile(string path)
        {
            return MD5.ParseFile(path);
        }
    }

}