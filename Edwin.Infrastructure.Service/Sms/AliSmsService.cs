using System;
using System.Collections.Generic;
using System.Text;

using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Aliyun.Acs.Core.Exceptions;

using Microsoft.Extensions.Options;

namespace Edwin.Infrastructure.Service.Sms
{
    public class AliSmsService : ISmsService
    {
        private const string product = "Dysmsapi";
        private const string domain = "dysmsapi.aliyuncs.com";
        private const string outId = "netSwaggersms";

        private SmsConfigure _configure;

        public AliSmsService(IOptions<SmsConfigure> options)
        {
            _configure = options.Value;
        }

        public string Send(string name, string info, params string[] phoneNumbers)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", _configure.Id, _configure.Secret);

            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);

            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();

            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为1000个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = string.Join(",", phoneNumbers);
                //必填:短信签名-可在短信控制台中找到
                request.SignName = "一幕";
                //必填:短信模板-可在短信控制台中找到
                request.TemplateCode = _configure.SmsType[name];
                //可选:模板中的变量替换JSON串,如模板内容为"亲爱的${name},您的验证码为${code}"时,此处的值为
                request.TemplateParam = info;
                //可选:outId为提供给业务方扩展字段,最终在短信回执消息中将此值带回给调用者
                request.OutId = outId;
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                return sendSmsResponse.BizId;
            }
            catch (ServerException e)
            {
                throw e;
            }
            catch (ClientException e)
            {
                throw e;
            }
        }
    }
}
