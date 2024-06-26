﻿using Core.Network;
using System;
using System.Drawing;
using System.Net;

namespace Core.Account
{
    /// <summary>
    /// 账号信息模板
    /// </summary>
    public class AccountInformation
    {
        #region Public Fields

        /// <summary>
        /// 登陆状态，是否有效
        /// </summary>
        public bool State { set; get; } = false;

        /// <summary>
        /// Access_Token（使用二维码登录时此项为空）
        /// </summary>
        public string AccessToken;

        /// <summary>
        /// Buvid/local_id
        /// </summary>
        public string Buvid { set; get; }

        /// <summary>
        /// 验证码图片（仅当需要验证码验证时有值）
        /// </summary>
        public Bitmap CaptchaPic = null;

        /// <summary>
        /// Cookies集合实例
        /// </summary>
        public CookieCollection Cookies { set; get; }

        /// <summary>
        /// csrf_token
        /// </summary>
        public string CsrfToken { set; get; }

        /// <summary>
        /// 设备标识
        /// </summary>
        //public string DeviceGuid { set; get; }

        /// <summary>
        /// device_id/bili_local_id
        /// </summary>
        //public string DeviceId { set; get; }

        /// <summary>
        /// 加密过的密码（使用二维码登录时此项为空）
        /// </summary>
        public string EncryptedPassword;

        /// <summary>
        /// Access_Token有效期（使用二维码登录时此项为空）
        /// </summary>
        public DateTime Expires_AccessToken;

        /// <summary>
        /// Cookies有效期
        /// </summary>
        public DateTime Expires_Cookies { set; get; }

        /// <summary>
        /// 指示是否登录成功
        /// </summary>
        public LoginStatusEnum LoginStatus = LoginStatusEnum.None;

        /// <summary>
        /// 密码（使用二维码登录时此项为空）
        /// </summary>
        public string Password;

        /// <summary>
        /// Refresh_Token（使用二维码登录时此项为空）
        /// </summary>
        public string RefreshToken;

        /// <summary>
        /// Cookies字符串
        /// </summary>
        public string strCookies { set; get; }

        /// <summary>
        /// 手机号（仅当需要手机验证的时候有值）
        /// </summary>
        public string Tel;

        /// <summary>
        /// 用户数字id
        /// </summary>
        public string Uid { set; get; }

        /// <summary>
        /// 手机验证链接（仅当需要手机验证的时候有值）
        /// </summary>
        public string Url;

        /// <summary>
        /// 用户名（使用二维码登录时此项为空）
        /// </summary>
        public string UserName;

        #endregion Public Fields

        #region Public Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountInformation()
        {
            Buvid = $"{Guid.NewGuid().ToString("D").ToUpper()}{Guid.NewGuid().ToString("D").Substring(0,4).ToUpper()}infoc";
        }

        #endregion Public Constructors

        #region Public Enums

        /// <summary>
        /// 登录状态枚举
        /// </summary>
        public enum LoginStatusEnum
        {
            /// <summary>
            /// 设备安全验证
            /// </summary>
            NeedSafeVerify = -4,

            /// <summary>
            /// 手机验证
            /// </summary>
            NeedTelVerify = -3,

            /// <summary>
            /// 图片验证码
            /// </summary>
            NeedCaptcha = -2,

            /// <summary>
            /// 密码错误
            /// </summary>
            WrongPassword = -1,

            /// <summary>
            /// 未登录
            /// </summary>
            None,

            /// <summary>
            /// 二维码登录
            /// </summary>
            ByQrCode,

            /// <summary>
            /// 密码登录
            /// </summary>
            ByPassword,

            /// <summary>
            /// 短信登录
            /// </summary>
            BySMS
        }

        #endregion Public Enums
    }
}