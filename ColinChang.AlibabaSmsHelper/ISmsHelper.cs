using System.Collections.Generic;
using System.Threading.Tasks;
using AlibabaCloud.SDK.Dysmsapi20170525.Models;

namespace ColinChang.AlibabaSmsHelper
{
    /// <summary>
    /// 阿里云短信助手
    /// </summary>
    public interface ISmsHelper
    {
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="phoneNumbers">手机号集合</param>
        /// <param name="code">验证码</param>
        /// <param name="templateCode">模板号</param>
        /// <returns></returns>
        Task<SendSmsResponse> SendVerificationCodeSmsAsync(IEnumerable<string> phoneNumbers, string code,
            string templateCode);

        /// <summary>
        /// 发送短信通知
        /// </summary>
        /// <param name="phoneNumbers">手机号集合</param>
        /// <param name="variables">通知变量字典</param>
        /// <param name="templateCode">模板号</param>
        /// <returns></returns>
        Task<SendSmsResponse> SendSmsAsync(IEnumerable<string> phoneNumbers, IDictionary<string, string> variables,
            string templateCode);

        /// <summary>
        /// 查询短信发送详情
        /// </summary>
        /// <param name="bizId">短信发送请求</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns>发送结果集合</returns>
        Task<IEnumerable<QuerySendDetailsResponseBody.QuerySendDetailsResponseBodySmsSendDetailDTOs.
            QuerySendDetailsResponseBodySmsSendDetailDTOsSmsSendDetailDTO>> QuerySendDetailsAsync(string bizId,
            string phoneNumber, int pageIndex = 1, int pageSize = 10);
    }
}