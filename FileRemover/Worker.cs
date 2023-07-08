namespace FileRemover
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _Config;

        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _Config = config;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var path = _Config.GetConnectionString("PoolPath").ToString();

                // Delete file from FilePool
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(path);
                var directories = directory.GetFiles();
                foreach (var subDirectory in directories)
                {
                    subDirectory.Delete();
                }

                _logger.LogInformation("FilePool Deleted! Worker running at: {time}", DateTimeOffset.Now);
                // Repeat Task every 10 mins
                await Task.Delay(600000, stoppingToken);
            }
        }

    }
}