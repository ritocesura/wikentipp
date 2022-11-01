namespace Wm22App.Services
{
    public class JwtRefreshTokenCache : IHostedService, IDisposable
    {
        private IJwtAuthService JwtAuthService { get; }
        private Timer Timer { get; set; }

        public JwtRefreshTokenCache(IJwtAuthService jwtAuthService)
        {
            JwtAuthService = jwtAuthService;
            JwtAuthService = jwtAuthService;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            // remove expired refresh tokens from cache every minute
            Timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            JwtAuthService.RemoveExpiredRefreshTokens(DateTime.Now);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            Timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Timer?.Dispose();
        }
    }
}