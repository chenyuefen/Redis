using Ruanmou.Redis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRedis
{
    /// <summary>
    /// 好友管理 共同好友-可能认识
    /// 
    /// 找出共同好友: 
    /// 关系型数据库：找出2个好友列表，然后再比对一下
    ///     
    /// 二次好友(可能认识)：
    /// 
    /// </summary>
    public class FriendManager
    {
        public static void Show()
        {
            //去重：IP统计去重；添加好友申请；投票限制；点赞；
            //交叉并的使用
            using (RedisSetService service = new RedisSetService())
            {
                service.FlushAll();//清理全部数据
                service.Add("Eleven", "Powell");
                service.Add("Eleven", "Tenk");
                service.Add("Eleven", "spider");
                service.Add("Eleven", "spider");
                service.Add("Eleven", "spider");
                service.Add("Eleven", "aaron");
                service.Add("Eleven", "Linsan");

                service.Add("Powell", "Eleven");
                service.Add("Powell", "Tenk");
                service.Add("Powell", "ywa");
                service.Add("Powell", "Pang");
                service.Add("Powell", "Jeff");

                var result = service.GetIntersectFromSets("Eleven", "Powell");
                var result2 = service.GetDifferencesFromSet("Powell", "Eleven");
                var result3 = service.GetDifferencesFromSet("Eleven", "Powell");
                var result4=service.GetUnionFromSets("Eleven", "Powell");
            }

        }

    }
}
