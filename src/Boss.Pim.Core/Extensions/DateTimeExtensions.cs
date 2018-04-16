﻿using System;
using Abp.Timing;

namespace Boss.Pim.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime? Today(this DateTime? now)
        {
            return now?.Date.AddDays(1).AddMilliseconds(-1);
        }
        public static DateTime Today(this DateTime now)
        {
            return now.Date.AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// 将日期数组 转为时间范围
        /// </summary>
        /// <param name="dataArr"></param>
        /// <returns></returns>
        public static DateTimeRange ToDateTimeRange(this DateTime[] dataArr)
        {
            if (dataArr != null && dataArr.Length >= 2)
            {
                return new DateTimeRange
                {
                    StartTime = dataArr[0],
                    EndTime = dataArr[1]
                };
            }
            return null;
        }


        /// <summary>
        /// 时间戳 1970-1-1
        /// </summary>
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Unix时间戳转为Datetime
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime GetDateTimeFromUnixTimeStamp(this int unixTimeStamp)
        {
            return UnixEpoch.AddSeconds(unixTimeStamp).ToLocalTime();
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式(单位秒)
        /// </summary>
        /// <param name="time">DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ToUnixTimestamp(this DateTime time)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
    }
}