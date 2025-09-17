using Dapper;
using be_learn_dotnet.Models;
using System.Data;

namespace be_learn_dotnet.Repositories
{
  public class UserRepository(IDbConnection db)
  {
    private readonly IDbConnection _db = db;

    public IEnumerable<UserModel> GetAll()
    {
      string sql = "SELECT * FROM Users";
      return _db.Query<UserModel>(sql);
    }

    public IEnumerable<UserModel> GetPaged(int page, int size, out int totalData, out int totalPage, out int from, out int to)
    {
      // hitung total data
      totalData = _db.ExecuteScalar<int>("SELECT COUNT(*) FROM users");

      // hitung total halaman
      totalPage = (int)Math.Ceiling(totalData / (double)size);

      // hitung offset
      int offset = (page - 1) * size;

      // ambil data dengan pagination
      var users = _db.Query<UserModel>(
        "SELECT * FROM users ORDER BY id LIMIT @Size OFFSET @Offset",
        new { Size = size, Offset = offset }
      ).ToList();

      // hitung from/to
      from = offset + 1;
      to = offset + users.Count;

      return users;
    }

    public UserModel? GetById(int id)
    {
      string sql = "SELECT * FROM Users WHERE Id = @Id";
      return _db.QueryFirstOrDefault<UserModel>(sql, new { Id = id });
    }

    public int Create(UserModel user)
    {
      string sql = "INSERT INTO Users (Name, Email, Age) VALUES (@Name, @Email, @Age); SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, user);
    }

    public bool Update(UserModel user)
    {
      string sql = "UPDATE Users SET Name = @Name, Email = @Email, Age = @Age WHERE Id = @Id";
      int rows = _db.Execute(sql, user);
      return rows > 0;
    }

    public bool Delete(int id)
    {
      string sql = "DELETE FROM Users WHERE Id = @Id";
      int rows = _db.Execute(sql, new { Id = id });
      return rows > 0;
    }
  }
}
