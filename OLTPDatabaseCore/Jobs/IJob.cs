namespace OLTPDatabaseCore.Jobs
{
    public interface IJob
    {
        public string Name { get; }

        public DateTime? LastRunDatetime { get; set; }

        public Task RunAsync();
    }
}
