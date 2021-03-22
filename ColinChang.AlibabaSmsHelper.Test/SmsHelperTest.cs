using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ColinChang.AlibabaSmsHelper.Test
{
    public class SmsHelperTest
    {
        private readonly ISmsHelper _smsHelper;

        public SmsHelperTest()
        {
            var options = new SmsHelperOptions
            {
                AccessKeyId = "",
                AccessKeySecret = "",
                Endpoint = "dysmsapi.aliyuncs.com",
                SignName = "",
                TemplateCodes = new Dictionary<string, string> {["VerificationCode"] = ""}
            };
            _smsHelper = new SmsHelper(options);
        }

        [Fact]
        public async Task SendVerificationCodeSmsAndQuerySendDetailsTestAsync()
        {
            var phoneNumber = "";
            var response =
                await _smsHelper.SendVerificationCodeSmsAsync(new[] {phoneNumber}, "123456", "VerificationCode");
            Assert.Equal("OK", response.Body.Code);

            var dtos = await _smsHelper.QuerySendDetailsAsync(response.Body.BizId, phoneNumber);
            Assert.All(dtos, dto =>
            {
                if (dto.SendStatus.HasValue && dto.SendStatus.Value != 3)
                    throw new Exception();
            });
        }

        [Fact]
        public ValueTask SendSmsTestAsync() => ValueTask.CompletedTask;
    }
}