using Enplace.Service.Contracts;

namespace Enplace.Service.Database
{
    public class MySqlKeyChain : IDbKeyChain
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string GetConnectionString() => $"Server={Server};Port={Port};Database={Database};User ID={Username};Password={Password};CharSet=utf8;";
    }
}
