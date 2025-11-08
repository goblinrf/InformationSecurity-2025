using Contracts;
using System.Xml.Linq;

namespace Storage
{
	public class User : IUser
	{
		public string Id { get; private set; } = Guid.NewGuid().ToString();
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public bool HasAdministratorRights { get; set; }
		public bool IsAccountLocked { get; set; }
		public bool HasStrongPassword { get; set; }
		public int MinimumPasswordLength { get; set; }
		public int PasswordExpirationPeriodMonths { get; set; }
		public DateTime LastPasswordUpdate { get; set; }

		public UserViewModel GetViewModel => new()
		{
			Id = Id,
			Username = Username,
			Password = Password,
			HasAdministratorRights = HasAdministratorRights,
			IsAccountLocked = IsAccountLocked,
			HasStrongPassword = HasStrongPassword,
			PasswordExpirationPeriodMonths = PasswordExpirationPeriodMonths,
			LastPasswordUpdate = LastPasswordUpdate,
			MinimumPasswordLength = MinimumPasswordLength,
		};
		public XElement GetXElement =>
				new XElement("User",
				new XAttribute("Id", Id),
				new XElement("Username", Username),
				new XElement("Password", Password),
				new XElement("HasAdministratorRights", HasAdministratorRights),
				new XElement("IsAccountLocked", IsAccountLocked),
				new XElement("HasStrongPassword", HasStrongPassword),
				new XElement("MinimumPasswordLength", MinimumPasswordLength),
				new XElement("PasswordExpirationPeriodMonths", PasswordExpirationPeriodMonths),
				new XElement("LastPasswordUpdate", LastPasswordUpdate.ToString("O"))
			);

		public static User? Create(UserBindingModel model)
		{
			if (model == null)
			{
				return null;
			}
			return new User()
			{
				Id = model.Id,
				Username = model.Username,
				Password = model.Password,
				HasAdministratorRights = model.HasAdministratorRights,
				IsAccountLocked = model.IsAccountLocked,
				HasStrongPassword = model.HasStrongPassword,
				MinimumPasswordLength = model.MinimumPasswordLength,
				PasswordExpirationPeriodMonths = model.PasswordExpirationPeriodMonths,
				LastPasswordUpdate = model.LastPasswordUpdate,
			};
		}

		public static User? Create(XElement element)
		{
			if (element == null)
			{
				return null;
			}
			return new User
			{
				Id = element.Attribute("Id")?.Value ?? Guid.NewGuid().ToString(),
				Username = element.Element("Username")?.Value ?? string.Empty,
				Password = element.Element("Password")?.Value ?? string.Empty,
				HasAdministratorRights = bool.Parse(element.Element("HasAdministratorRights")?.Value ?? "false"),
				IsAccountLocked = bool.Parse(element.Element("IsAccountLocked")?.Value ?? "false"),
				HasStrongPassword = bool.Parse(element.Element("HasStrongPassword")?.Value ?? "false"),
				MinimumPasswordLength = int.Parse(element.Element("MinimumPasswordLength")?.Value ?? "0"),
				PasswordExpirationPeriodMonths = int.Parse(element.Element("PasswordExpirationPeriodMonths")?.Value ?? "0"),
				LastPasswordUpdate = DateTime.TryParse(element.Element("LastPasswordUpdate")?.Value, out var dt) ? dt : DateTime.UtcNow
			};
		}

		public void Update(UserBindingModel model)
		{
			if (model == null)
			{
				return;
			}
			Username = model.Username;
			Password = model.Password;
			HasAdministratorRights = model.HasAdministratorRights;
			IsAccountLocked = model.IsAccountLocked;
			HasStrongPassword = model.HasStrongPassword;
			MinimumPasswordLength = model.MinimumPasswordLength;
			PasswordExpirationPeriodMonths = model.PasswordExpirationPeriodMonths;
			LastPasswordUpdate = model.LastPasswordUpdate;
		}
		
	}
}
