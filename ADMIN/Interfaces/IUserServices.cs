using PuntoDeVenta_API.ADMIN.Models;

namespace PuntoDeVenta_API.ADMIN.Interfaces
{
    public interface IUserServices
    {
        public Task<List<UserModel>> GetUsers();
        public Task<UserModel> GetASingleUser(int id);
        public Task<bool> DeleteAUser(int id);
        public Task<UserModel> ValidateUserLogin(UserModel credentials);
        public Task<bool> CreateANewUser(UserModel newUser);
    }
}
