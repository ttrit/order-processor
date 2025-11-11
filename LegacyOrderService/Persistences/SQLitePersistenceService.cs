namespace LegacyOrderService.Persistences
{
    public sealed class SQLitePersistenceService : IPersistenceService
    {
        private static readonly Lazy<SQLitePersistenceService> _instance =
            new(() => new SQLitePersistenceService());

        public static SQLitePersistenceService Instance => _instance.Value;

        private readonly string _dbPath;
        public string ConnectionString => $"Data Source={_dbPath}";


        public SQLitePersistenceService()
        {
            _dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\orders.db"));
        }
    }
}
