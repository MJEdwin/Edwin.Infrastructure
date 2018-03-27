using System;
using System.Collections.Generic;
using System.Text;

namespace Edwin.Infrastructure.Service.Sms
{
    public interface ISmsService
    {
        /// <summary>
        /// 发送Sms信息
        /// </summary>
        /// <param name="info">信息</param>
        /// <param name="phoneNumbers">电话号码</param>
        /// <returns>短信回执Id</returns>
        string Send(string info, params string[] phoneNumbers);
    }
}
