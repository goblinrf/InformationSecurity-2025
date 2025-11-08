using Contracts;

namespace Storage
{
	public class Storage
	{
		private readonly Singleton source;

		public Storage()
		{
			source = Singleton.Instance;
		}

		public UserViewModel? Delete(UserBindingModel model)
		{
			foreach (var user in source.Users)
			{
				if (user.Id == model.Id)
				{
					source.Users.Remove(user);
					source.SaveUsers();
					return user.GetViewModel;
				}
			}
			return null;
		}

		public UserViewModel? GetElement(UserSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Username) && string.IsNullOrEmpty(model.Id))
			{
				return null;
			}

			foreach (var user in source.Users)
			{
				bool idMatch = string.IsNullOrEmpty(model.Id) || user.Id == model.Id;
				bool usernameMatch = string.IsNullOrEmpty(model.Username) || user.Username == model.Username;
				bool passwordMatch = string.IsNullOrEmpty(model.Password) || user.Password == model.Password;

				if (idMatch && usernameMatch && passwordMatch)
				{
					return user.GetViewModel;
				}
			}
			return null;
		}

		public UserViewModel? GetElementWithPassword(UserSearchModel model)
		{
			if ((string.IsNullOrEmpty(model.Username) && string.IsNullOrEmpty(model.Id)) ||
				string.IsNullOrEmpty(model.Password))
			{
				return null;
			}

			foreach (var user in source.Users)
			{
				bool idMatch = string.IsNullOrEmpty(model.Id) || user.Id == model.Id;
				bool usernameMatch = string.IsNullOrEmpty(model.Username) || user.Username == model.Username;
				bool passwordMatch = string.IsNullOrEmpty(model.Password) || user.Password == model.Password;

				if (idMatch && usernameMatch && passwordMatch)
				{
					return user.GetViewModel;
				}
			}
			return null;
		}

		public List<UserViewModel> GetFilteredList(UserSearchModel model)
		{
			if (string.IsNullOrEmpty(model.Username) && string.IsNullOrEmpty(model.Password))
			{
				return new List<UserViewModel>();
			}

			var result = new List<UserViewModel>();
			foreach (var user in source.Users)
			{
				bool usernameMatch = string.IsNullOrEmpty(model.Username) || user.Username.Contains(model.Username);
				bool passwordMatch = string.IsNullOrEmpty(model.Password) || user.Username.Contains(model.Password);

				if (usernameMatch && passwordMatch)
				{
					result.Add(user.GetViewModel);
				}
			}
			return result;
		}

		public List<UserViewModel> GetFullList()
		{
			var result = new List<UserViewModel>();
			foreach (var user in source.Users)
			{
				result.Add(user.GetViewModel);
			}
			return result;
		}

		public UserViewModel? Insert(UserBindingModel model)
		{
			var newUser = User.Create(model);
			if (newUser == null)
			{
				return null;
			}
			source.Users.Add(newUser);
			source.SaveUsers();
			return newUser.GetViewModel;
		}

		public UserViewModel? Update(UserBindingModel model)
		{
			foreach (var user in source.Users)
			{
				if (user.Id == model.Id)
				{
					user.Update(model);
					source.SaveUsers();
					return user.GetViewModel;
				}
			}
			return null;
		}
	}
}
