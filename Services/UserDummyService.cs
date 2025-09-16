using be_learn_dotnet.Data;
using be_learn_dotnet.Models;

namespace be_learn_dotnet.Services
{
  public class UserDummyService
  {
    public List<UserDummyModel> GetAll(int page, int size, out int totalData, out int totalPage, out int from, out int to)
    {
      totalData = UserDummyData.Users.Count;
      totalPage = (int)Math.Ceiling(totalData / (double)size);

      var data = UserDummyData.Users
          .Skip((page - 1) * size)
          .Take(size)
          .ToList();

      from = ((page - 1) * size) + 1;
      to = Math.Min(from + size - 1, totalData);

      return data;
    }

    public UserDummyModel? GetById(int id) =>
        UserDummyData.Users.FirstOrDefault(u => u.Id == id);

    public UserDummyModel Create(UserDummyModel newUser)
    {
      newUser.Id = UserDummyData.Users.Max(u => u.Id) + 1;
      UserDummyData.Users.Add(newUser);
      return newUser;
    }

    public UserDummyModel? Update(int id, UserDummyModel updatedUser)
    {
      var user = UserDummyData.Users.FirstOrDefault(u => u.Id == id);
      if (user == null) return null;

      if (!string.IsNullOrEmpty(updatedUser.Name)) user.Name = updatedUser.Name;
      if (!string.IsNullOrEmpty(updatedUser.Email)) user.Email = updatedUser.Email;

      return user;
    }

    public bool Delete(int id)
    {
      var user = UserDummyData.Users.FirstOrDefault(u => u.Id == id);
      if (user == null) return false;

      UserDummyData.Users.Remove(user);
      return true;
    }
  }
}
