using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ColinChang.AlibabaSmsHelper.HostedSample
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISmsHelper _smsHelper;

        public Worker(ISmsHelper smsHelper, ILogger<Worker> logger)
        {
            _logger = logger;
            _smsHelper = smsHelper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var phoneNumber = "17620455468";
            var response =
                await _smsHelper.SendVerificationCodeSmsAsync(new[] {phoneNumber}, "123456", "VerificationCode");
            if (!string.Equals("OK", response.Body.Code))
            {
                _logger.LogError(response.Body.Message);
                return;
            }

            var dtos = await _smsHelper.QuerySendDetailsAsync(response.Body.BizId, phoneNumber);
            if (dtos.Any(dto => dto.SendStatus.HasValue && dto.SendStatus.Value != 3))
                _logger.LogError("有短信发送失败");

            _logger.LogInformation("finished successfully");
        }
    }
}