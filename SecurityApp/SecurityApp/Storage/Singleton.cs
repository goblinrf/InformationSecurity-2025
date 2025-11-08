using System.Text;
using System.Xml.Linq;

namespace Storage
{
	public class Singleton
	{
		private static readonly Lazy<Singleton> _instance =
			new Lazy<Singleton>(() => new Singleton());

		private readonly string _encryptedFile = "Users.xml.enc";
		private readonly string _tempFile = Path.GetTempFileName();
		private readonly byte[] _key = Encoding.UTF8.GetBytes("12345678");

		public List<User> Users { get; private set; }

		public static Singleton Instance => _instance.Value;

		public void SaveUsers()
		{
			string tempPath = Path.GetTempFileName();

			try
			{
				SaveData(Users, tempPath, "Users", x => x.GetXElement);
				DES_ECB.EncryptECB(_key, tempPath, _encryptedFile);
			}
			finally
			{
				if (File.Exists(tempPath))
					File.Delete(tempPath);
			}
		}

		private Singleton()
		{
			if (!File.Exists(_encryptedFile))
			{
				InitializeDefaultAdmin();
				SaveUsers();
			}
			else
			{
				LoadUsers();
			}
		}

		private void LoadUsers()
		{
			string tempPath = Path.GetTempFileName();

			try
			{
				DES_ECB.DecryptECB(_key, _encryptedFile, tempPath);
				Users = LoadData(tempPath, "User", x => User.Create(x)) ?? new List<User>();
			}
			finally
			{
				if (File.Exists(tempPath))
					File.Delete(tempPath);
			}
		}

		private void InitializeDefaultAdmin()
		{
			Users = new List<User>
			{
				new User
				{
					Username = "ADMIN",
					Password = Hasher.GenerateMd4Hash(string.Empty),
					HasAdministratorRights = true,
					IsAccountLocked = false,
					HasStrongPassword = false,
					MinimumPasswordLength = 8,
					PasswordExpirationPeriodMonths = 3,
					LastPasswordUpdate = DateTime.UtcNow,
				}
			};
		}

		private static List<T>? LoadData<T>(string filename, string xmlNodeName, Func<XElement, T> selectFunction)
		{
			if (!File.Exists(filename))
				return new List<T>();

			var doc = XDocument.Load(filename);
			return doc.Root?.Elements(xmlNodeName)
					 .Select(selectFunction)
					 .Where(x => x != null)
					 .ToList() ?? new List<T>();
		}

		private static void SaveData<T>(List<T> data, string filename, string xmlNodeName, Func<T, XElement> selectFunction)
		{
			if (data == null)
				return;

			var elements = data.Select(selectFunction).ToArray();
			new XDocument(new XElement(xmlNodeName, elements)).Save(filename);
		}
	}
}
