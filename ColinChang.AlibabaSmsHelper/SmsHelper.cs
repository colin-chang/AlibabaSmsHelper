using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;


namespace ColinChang.AlibabaSmsHelper
{
    public class SmsHelper : ISmsHelper
    {
        private readonly SmsHelperOptions _options;
        private readonly AlibabaCloud.SDK.Dysmsapi20170525.Client _client;

        public SmsHelper(IOptionsMonitor<SmsHelperOptions> options) : this(options.CurrentValue)
        {
        }

        public SmsHelper(SmsHelperOptions options)
        {
            _options = options;

            var config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                AccessKeyId = _options.AccessKeyId,
                AccessKeySecret = _options.AccessKeySecret,
                Endpoint = _options.Endpoint
            };

            _client = new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
        }


        public Task<SendSmsResponse> SendVerificationCodeSmsAsync(IEnumerable<string> phoneNumbers, string code,
            string templateCode) =>
            SendSmsAsync(phoneNumbers, new Dictionary<string, string> {["code"] = code}, templateCode);

        public async Task<SendSmsResponse> SendSmsAsync(IEnumerable<string> phoneNumbers,
            IDictionary<string, string> variables, string templateCode)
        {
            if (phoneNumbers == null || !phoneNumbers.Any())
                throw new ArgumentException("手机号码错误");

            if (variables == null || !variables.Any())
                throw new ArgumentException("短信通知变量错误");

            if (string.IsNullOrWhiteSpace(templateCode) || !_options.TemplateCodes.ContainsKey(templateCode))
                throw new ArgumentException("模板编号错误");

            var request = new SendSmsRequest
            {
                PhoneNumbers = string.Join(",", phoneNumbers),
                SignName = _options.SignName,
                TemplateCode = _options.TemplateCodes[templateCode],
                TemplateParam = JsonConvert.SerializeObject(variables)
            };

            return await _client.SendSmsAsync(request);
        }

        public async
            Task<IEnumerable<QuerySendDetailsResponseBody.QuerySendDetailsResponseBodySmsSendDetailDTOs.
                QuerySendDetailsResponseBodySmsSendDetailDTOsSmsSendDetailDTO>> QuerySendDetailsAsync(string bizId,
                string phoneNumber, int pageIndex, int pageSize)
        {
            var queryRequest = new QuerySendDetailsRequest
            {
                PhoneNumber = AlibabaCloud.TeaUtil.Common.AssertAsString(phoneNumber),
                BizId = bizId,
                SendDate = AlibabaCloud.DarabonbaTime.Time.Format("yyyyMMdd"),
                CurrentPage = pageIndex,
                PageSize = pageSize
            };
            var queryResponse = await _client.QuerySendDetailsAsync(queryRequest);
            return queryResponse.Body.SmsSendDetailDTOs.SmsSendDetailDTO;
        }
    }
}