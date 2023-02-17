 
    // SimplerAES
using System.Security.Cryptography;
using System.Text;
 byte[] key; 
 byte[] vector;
 byte[] buffer;
string buffer2;
ICryptoTransform encryptor;
ICryptoTransform decryptor;
UTF8Encoding encoder;

 key = new byte[32]{
		123, 217, 79, 11, 24, 2, 85, 45, 114, 184,
		27, 112, 37, 112, 222, 209, 241, 24, 175, 144,
		173, 53, 196, 19, 24, 26, 17, 218, 131, 236,
		53, 209};
       vector = new byte[16]
	{
		146, 64, 171, 161, 2, 3, 113, 119, 231, 121,
		221, 112, 79, 32, 114, 16
	};
    buffer = File.ReadAllBytes(@"C:\Users\pablo\AppData\LocalLow\Lazy Bear Games\Punch Club\save_2.dat");
    buffer2 = File.ReadAllText(@"C:\Users\pablo\Documents\pun\savaDecrypted.txt");
CreateEncoders();
#pragma warning disable 0618
 File.WriteAllText(@"C:\Users\pablo\Documents\pun\savaDecrypted.txt",GetString(Transform(buffer,decryptor)));
#pragma warning restore 0618

//Console.WriteLine(buffer2);
File.WriteAllBytes(@"C:\Users\pablo\Documents\pun\savaecrypted.txt",(Transform(GetBytes((buffer2)),encryptor)));



	 byte[] GetBytes(string str)
	{
		byte[] array = new byte[str.Length * 2];
		Buffer.BlockCopy(str.ToCharArray(), 0, array, 0, array.Length);
		return array;
	}
string GetString(byte[] bytes)
{
	char[] array = new char[bytes.Length / 2];
	Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
	return new string(array);
}

  void CreateEncoders()
{
	RijndaelManaged rijndaelManaged = new RijndaelManaged();
	encryptor = rijndaelManaged.CreateEncryptor(key, vector);
	decryptor = rijndaelManaged.CreateDecryptor(key, vector);
	encoder = new UTF8Encoding();
}

 byte[] Decrypt(byte[] buffer)
	{
		return Transform(buffer, decryptor);
	}

 byte[] Transform(byte[] buffer, ICryptoTransform transform)
	{
		MemoryStream memoryStream = new MemoryStream();
		using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
		{
			cryptoStream.Write(buffer, 0, buffer.Length);
		}
		return memoryStream.ToArray();
	}
