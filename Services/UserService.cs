using be_learn_dotnet.Models;
using be_learn_dotnet.Repositories;

namespace be_learn_dotnet.Services
{
  public class UserService(UserRepository repository)
  {
    private readonly UserRepository _repository = repository;

    public IEnumerable<UserModel> GetAll() => _repository.GetAll();

    public (IEnumerable<UserModel> Data, int TotalData, int TotalPage, int From, int To) GetPaged(int page, int size)
    {
      var users = _repository.GetPaged(page, size, out int totalData, out int totalPage, out int from, out int to);
      return (users, totalData, totalPage, from, to);
    }

    public UserModel? GetById(int id) => _repository.GetById(id);

    public UserModel? Create(UserModel user)
    {
      int id = _repository.Create(user);
      return _repository.GetById(id);
    }

    public UserModel? Update(int id, UserModel user)
    {
      user.Id = id;
      var success = _repository.Update(user);
      return success ? _repository.GetById(id) : null;
    }

    public bool Delete(int id) => _repository.Delete(id);
  }
}
