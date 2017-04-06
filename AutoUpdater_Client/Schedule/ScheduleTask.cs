using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoUpdater_Client.Schedule
{
    //该计划任务的逻辑模仿了linux的crontab的语法：
    //    cron文件语法:
    //          分     小时    日       月       星期      命令
    //        0-59   0-23   1-31   1-12     0-6     command     (取值范围,0表示周日一般一行对应一个任务)

    //4.记住几个特殊符号的含义:
    //        "*"代表取值范围内的数字,
    //        "/"代表"每",
    //        "-"代表从某个数字到某个数字,
    //        ","分开几个离散的数字
    //    举例如下：
    //5       *        *           *      *     ls              指定每小时的第5分钟执行一次ls命令
    //30     5       *           *      *     ls              指定每天的 5:30 执行ls命令
    //30     7       8          *      *      ls              指定每月8号的7：30分执行ls命令
    //30     5       8          6     *      ls              指定每年的6月8日5：30执行ls命令
    //30     6       *           *     0      ls              指定每星期日的6:30执行ls命令[注：0表示星期天，1表示星期1，以此类推，也可以用英文来表示，sun表示星期天，mon表示星期一等。]
    //30     3      10,20     *     *      ls     每月10号及20号的3：30执行ls命令[注：“，”用来连接多个不连续的时段]
    //25     8-11 *            *     *      ls       每天8-11点的第25分钟执行ls命令[注：“-”用来连接连续的时段]
    //*/15   *        *            *     *      ls          每15分钟执行一次ls命令 [即每个小时的第0 15 30 45 60分钟执行ls命令 ]
    //30    6      */10         *      *      ls       每个月中，每隔10天6:30执行一次ls命令[即每月的1、11、21、31日是的6：30执行一次ls命令。 ]
    class ScheduleTask
    {
        //分钟以及判断位
        int _minute = 0;
        bool _minuteJudge = false;
        //小时以及判断位
        int _hour = 0;
        bool _hourJudge = false;
        //日期以及判断位
        int _day = 0;
        bool _dayJudge = true;
        //月份以及判断位
        int _month = 0;
        bool _monthJudge = true;
        //星期以及判断位
        int _dayOfWeek = 1;
        bool _dayOfWeekJudge = true;
        //临时存放规则的字符串数组
        string[] ruleTmp;

        public ScheduleTask(string rule)
        {
            try
            {
                if (isRuleVaild(rule))
                {
                    setParams(rule);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //判断规则字符串是否符合规定，如果不符合则采用默认配置
        public bool isRuleVaild(string rule) {
            ruleTmp = null;
            int[] monthOfYear = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int year = DateTime.Now.Year;
            //判断当前年份是否为闰年
            if ((year % 400 == 0) || (year % 100 != 0 && year % 4 == 0))
                monthOfYear[1]++;
            try {
                ruleTmp = rule.Split(' ');
                if (ruleTmp.Length != 5)
                    return false;
                else if (Convert.ToInt16(ruleTmp[0]) < 1 || Convert.ToInt16(ruleTmp[0]) > 59)
                    return false;
                else if (Convert.ToInt16(ruleTmp[1]) < 0 || Convert.ToInt16(ruleTmp[1]) > 23)
                    return false;
                else if (ruleTmp[3] != "*" && (Convert.ToInt16(ruleTmp[3]) < 1 || Convert.ToInt16(ruleTmp[3]) > 12
                    || (Convert.ToInt16(ruleTmp[2]) > monthOfYear[Convert.ToInt16(ruleTmp[3]) - 1] && ruleTmp[2] != "*")))
                    return false;
                else if (ruleTmp[2] != "*" && (Convert.ToInt16(ruleTmp[2]) < 1 || Convert.ToInt16(ruleTmp[2]) > 31))
                    return false;
                else if (ruleTmp[4] != "*" && (Convert.ToInt16(ruleTmp[4]) < 0 || Convert.ToInt16(ruleTmp[4]) > 6))
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        //设定类内部成员
        public void setParams(string rule)
        {
            _minute = Convert.ToInt16(ruleTmp[0]);
            _hour = Convert.ToInt16(ruleTmp[1]);
            if (ruleTmp[2] != "*")
            {
                _dayJudge = false;
                _day = Convert.ToInt16(ruleTmp[2]);
            }
            if (ruleTmp[3] != "*")
            {
                _monthJudge = false;
                _month = Convert.ToInt16(ruleTmp[3]);
            }
            if (ruleTmp[4] != "*")
            {
                _dayOfWeekJudge = false;
                _dayOfWeek = Convert.ToInt16(ruleTmp[4]);
            }
        }

        //判断当前时间是否符合规则
        public bool isRuleMatches()
        {
            int minute = DateTime.Now.Minute, hour = DateTime.Now.Hour;
            int day = DateTime.Now.Day, month = DateTime.Now.Month, dayOfWeek = Convert.ToInt16(DateTime.Now.DayOfWeek);
            if (_minute != minute || _hour != hour || (!_dayJudge && day != _day)
                || (!_monthJudge && month != _month) || (!_dayOfWeekJudge && dayOfWeek != _dayOfWeek))
                return false;
            return true;
        }
    }
}
