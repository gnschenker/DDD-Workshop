namespace DDD_Sample.Infrastructure.Configuration
{
    public class Settings : SettingsBase, INHibernateSettings
    {
        public Settings()
        {
            ConnectionString = FromConnectionStrings("SampleDB");
            CurrentSessionContextClass = FromAppSetting("NhibernateSessionContext");
            DefaultSchema = FromAppSetting("DefaultSchema");
            AdonetBatchSize = FromAppSetting("AdonetBatchSize");
        }

        public string ConnectionString { get; private set; }
        public string CurrentSessionContextClass { get; private set; }
        public string DefaultSchema { get; private set; }
        public string AdonetBatchSize { get; private set; }
    }
}