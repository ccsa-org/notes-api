using CCSANoteApp.DB.Configurations;
using CCSANoteApp.DB.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace CCSANoteApp.DB
{
    public class SessionFactory
    {
        public SessionFactory(/*DBConfiguration config*/)
        {
            if (_sessionFactory is null)
            {
                //_sessionFactory = BuildSessionFactory(config.ConnectionString);
                _sessionFactory = BuildSessionFactory(_connecttionString);
            }
        }

        public ISession GetSession() => _sessionFactory.OpenSession();

        private readonly ISessionFactory _sessionFactory;

        private ISessionFactory BuildSessionFactory(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(NoteMap).Assembly))
                .ExposeConfiguration(cfg =>
                {
                    new SchemaUpdate(cfg).Execute(true, true);
                });
            return configuration.BuildSessionFactory();
        }

        private readonly string _connecttionString = "Server=itaobong.com;Initial Catalog=notes;Persist Security Info=False;User ID=notes-login;Password=notes-password;Connection Timeout=30;";
    }
}
