﻿using AngleSharp.Io;
using Core.Network.Methods;
using Masuit.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Network.Methods.User;
using static Core.RuntimeObject.Room;

namespace Core.RuntimeObject
{
    public class Room
    {
        #region private Properties
        private static List<RoomInfo> roomInfos = new List<RoomInfo>();
        #endregion

        #region Public Method

        public static long GetRoomId(long Uid)
        {
            return _GetRoomId(Uid);
        }

        public static string GetTitle(long Uid)
        {
            return _GetTitle(Uid);
        }

        #endregion

        #region private Method

        private static long _GetRoomId(long Uid)
        {
            RoomInfo roomInfo = roomInfos.Where(x => x.uid == Uid).FirstOrDefault();
            if (roomInfo == null || roomInfo.room_id < 0)
            {
                UserInfo userInfo = User.GetUserInfo(Uid);
                if (userInfo == null)
                {
                    return -1;
                }
                else
                {
                    RoomInfo newRoomInfo = new RoomInfo()
                    {
                        uid = userInfo.data.mid,
                        room_id = userInfo.data.live_room.roomid,
                        uname = userInfo.data.name,
                        url = $"https://live.bilibili.com/{userInfo.data.live_room.roomid}",
                        roomStatus = new ExpansionType<int> { Value = userInfo.data.live_room.liveStatus, ExpirationTime = DateTime.Now.AddSeconds(5) },
                        title = new ExpansionType<string> { Value = userInfo.data.live_room.title, ExpirationTime = DateTime.Now.AddSeconds(30) },
                        cover_from_user = new ExpansionType<string> { Value = userInfo.data.live_room.cover, ExpirationTime = DateTime.Now.AddMinutes(10) },
                        face = userInfo.data.face,
                        sex = userInfo.data.sex,
                        sign = userInfo.data.sign,
                        level = userInfo.data.level,
                    }; ;
                    if (roomInfo == null)
                    {
                        roomInfos.Add(newRoomInfo);
                    }
                    else
                    {
                        for (int i = 0; i < roomInfos.Count; i++)
                        {
                            if (roomInfos[i].uid == newRoomInfo.uid)
                            {
                                roomInfos[i] = newRoomInfo;
                            }
                        }
                    }
                    return userInfo.data.live_room.roomid;
                }
            }
            else
            {
                return roomInfo.room_id;
            }
        }

        private static string _GetTitle(long Uid)
        {
            RoomInfo roomInfo = roomInfos.Where(x => x.uid == Uid).FirstOrDefault();
            if (roomInfo == null || string.IsNullOrEmpty(roomInfo.title.Value) || roomInfo.title.ExpirationTime < DateTime.Now)
            {
                UserInfo userInfo = User.GetUserInfo(Uid);
                if (userInfo == null)
                {
                    return string.Empty;
                }
                else
                {
                    RoomInfo newRoomInfo = new RoomInfo()
                    {
                        uid = userInfo.data.mid,
                        room_id = userInfo.data.live_room.roomid,
                        uname = userInfo.data.name,
                        url = $"https://live.bilibili.com/{userInfo.data.live_room.roomid}",
                        roomStatus = new ExpansionType<int> { Value = userInfo.data.live_room.liveStatus, ExpirationTime = DateTime.Now.AddSeconds(5) },
                        title = new ExpansionType<string> { Value = userInfo.data.live_room.title, ExpirationTime = DateTime.Now.AddSeconds(30) },
                        cover_from_user = new ExpansionType<string> { Value = userInfo.data.live_room.cover, ExpirationTime = DateTime.Now.AddMinutes(10) },
                        face = userInfo.data.face,
                        sex = userInfo.data.sex,
                        sign = userInfo.data.sign,
                        level = userInfo.data.level,
                    }; ;
                    if (roomInfo == null)
                    {
                        roomInfos.Add(newRoomInfo);
                    }
                    else
                    {
                        for (int i = 0; i < roomInfos.Count; i++)
                        {
                            if (roomInfos[i].uid == newRoomInfo.uid)
                            {
                                roomInfos[i] = newRoomInfo;
                            }
                        }
                    }
                    return userInfo.data.live_room.title;
                }
            }
            else
            {
                return roomInfo.title.Value;
            }
        }

        #endregion

        #region internal Class

        private class RoomInfo
        {

            /// <summary>
            /// 直播间房间号(直播间实际房间号)
            /// </summary>
            internal long room_id { get; set; } = -1;
            /// <summary>
            /// 主播mid
            /// </summary>
            internal long uid { get; set; } = -1;
            /// <summary>
            /// 描述(Local值)
            /// </summary>
            internal string Description { get; set; } = "";
            /// <summary>
            /// 是否自动录制(Local值)
            /// </summary>
            internal bool IsAutoRec { set; get; }
            /// <summary>
            /// 是否开播提醒(Local值)
            /// </summary>
            internal bool IsRemind { set; get; }
            /// <summary>
            /// 是否录制弹幕(Local值)
            /// </summary>
            internal bool IsRecDanmu { set; get; }
            /// <summary>
            /// 特殊标记(Local值)
            /// </summary>
            internal bool Like { set; get; }
            /// <summary>
            /// 该房间录制完成后会执行的Shell命令
            /// </summary>
            internal string Shell { set; get; } = "";
            /// <summary>
            /// 是否持久化储存，用于判断是否需要写到房间配置文件
            /// </summary>
            internal bool IsPersisting = false;
            /// <summary>
            /// 标题
            /// </summary>
            internal ExpansionType<string> title = new ExpansionType<string> { ExpirationTime = DateTime.UnixEpoch, Value = string.Empty };
            /// <summary>
            /// 主播简介
            /// </summary>
            internal string description = "";
            /// <summary>
            /// 关注数
            /// </summary>
            internal ExpansionType<int> attention = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 直播间在线人数
            /// </summary>
            internal ExpansionType<int> online = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 开播时间(未开播时为-62170012800,live_status为1时有效)
            /// </summary>
            internal ExpansionType<long> live_time = new ExpansionType<long> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 直播状态(1为正在直播，2为轮播中)
            /// </summary>
            internal ExpansionType<int> live_status = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 直播间房间号(直播间短房间号，常见于签约主播)
            /// </summary>
            internal int short_id = 0;
            /// <summary>
            /// 直播间分区id
            /// </summary>
            internal ExpansionType<int> area = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 直播间分区名
            /// </summary>
            internal string area_name = "";
            /// <summary>
            /// 直播间新版分区id
            /// </summary>
            internal ExpansionType<int> area_v2_id = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 直播间新版分区名
            /// </summary>
            internal ExpansionType<string> area_v2_name = new ExpansionType<string> { ExpirationTime = DateTime.UnixEpoch, Value = string.Empty };
            /// <summary>
            /// 直播间父分区名
            /// </summary>
            internal ExpansionType<string> area_v2_parent_name = new ExpansionType<string> { ExpirationTime = DateTime.UnixEpoch, Value = string.Empty };
            /// <summary>
            /// 直播间父分区id
            /// </summary>
            internal ExpansionType<int> area_v2_parent_id = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 用户名
            /// </summary>
            internal string uname = "";
            /// <summary>
            /// 主播头像url
            /// </summary>
            internal string face = "";
            /// <summary>
            /// 系统tag列表(以逗号分割)
            /// </summary>
            internal string tag_name = "";
            /// <summary>
            /// 用户自定义tag列表(以逗号分割)
            /// </summary>
            internal string tags = "";
            /// <summary>
            /// 直播封面图
            /// </summary>
            internal ExpansionType<string> cover_from_user = new ExpansionType<string> { ExpirationTime = DateTime.UnixEpoch, Value = string.Empty };
            /// <summary>
            /// 直播关键帧图
            /// </summary>
            internal ExpansionType<string> keyframe = new ExpansionType<string> { ExpirationTime = DateTime.UnixEpoch, Value = string.Empty };
            /// <summary>
            /// 直播间封禁信息
            /// </summary>
            internal ExpansionType<string> lock_till = new ExpansionType<string> { ExpirationTime = DateTime.UnixEpoch, Value = string.Empty };
            /// <summary>
            /// 直播间隐藏信息
            /// </summary>
            internal string hidden_till = "";
            /// <summary>
            /// 直播类型(0:普通直播，1：手机直播)
            /// </summary>
            internal ExpansionType<int> broadcast_type = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 是否p2p
            /// </summary>
            internal ExpansionType<int> need_p2p = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 是否隐藏
            /// </summary>
            internal ExpansionType<bool> is_hidden = new ExpansionType<bool> { ExpirationTime = DateTime.UnixEpoch, Value = false };
            /// <summary>
            /// 是否锁定
            /// </summary>
            internal ExpansionType<bool> is_locked = new ExpansionType<bool> { ExpirationTime = DateTime.UnixEpoch, Value = false };
            /// <summary>
            /// 是否竖屏
            /// </summary>
            internal ExpansionType<bool> is_portrait = new ExpansionType<bool> { ExpirationTime = DateTime.UnixEpoch, Value = false };
            /// <summary>
            /// 是否加密
            /// </summary>
            internal ExpansionType<bool> encrypted = new ExpansionType<bool> { ExpirationTime = DateTime.UnixEpoch, Value = false };
            /// <summary>
            /// 加密房间是否通过密码验证(encrypted=true时才有意义)
            /// </summary>
            internal ExpansionType<bool> pwd_verified = new ExpansionType<bool> { ExpirationTime = DateTime.UnixEpoch, Value = false };
            /// <summary>
            /// 未知
            /// </summary>
            internal int room_shield = 0;
            /// <summary>
            /// 是否为特殊直播间(0：普通直播间 1：付费直播间)
            /// </summary>
            internal ExpansionType<int> is_sp = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 特殊直播间标志(0：普通直播间 1：付费直播间 2：拜年祭直播间)
            /// </summary>
            internal ExpansionType<int> special_type = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 直播间状态(0:无房间 1:有房间)
            /// </summary>
            internal ExpansionType<int> roomStatus = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 轮播状态(0：未轮播 1：轮播)
            /// </summary>
            internal ExpansionType<int> roundStatus = new ExpansionType<int> { ExpirationTime = DateTime.UnixEpoch, Value = 0 };
            /// <summary>
            /// 直播间网页url
            /// </summary>
            internal string url = "";
            /// <summary>
            /// 用户等级
            /// </summary>
            internal int level = 0;
            /// <summary>
            /// 主播性别
            /// </summary>
            internal string sex = "";
            /// <summary>
            /// 主播简介
            /// </summary>
            internal string sign = "";
            /// <summary>
            /// 下载标识符
            /// </summary>
            internal bool IsDownload = false;
            /// <summary>
            /// 当前Host地址
            /// </summary>
            internal ExpansionType<string> Host = new ExpansionType<string> { ExpirationTime = DateTime.UnixEpoch, Value = "" };
            /// <summary>
            /// 当前模式（1:FLV 2:HLS）
            /// </summary>
            internal int CurrentMode = 0;
        }
        internal class ExpansionType<T>
        {
            internal DateTime ExpirationTime;
            internal T? Value;
        }

        #endregion


    }
}
