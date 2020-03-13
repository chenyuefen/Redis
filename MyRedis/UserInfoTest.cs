using Ruanmou.Redis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRedis
{
    /// <summary>
    /// 缓存一个用户的信息---string
    /// String：key-value, id--序列化
    ///   修改需求--查询-反序列化-修改-序列化-存储
    ///   
    /// 还有个方案是 多个key-value  
    /// userinfo_123_Account    Administrator 
    /// userinfo_123_Name   Eleven
    /// 数据项就很多(浪费空间)，修改方便
    /// 
    /// Hash-->hashid userinfo_123
    /// 多个key--string类型的value最小值是512byte，即使只保存一个1，也是要占用512空间的，
    /// 而hash是一种zipmap存储，数据紧密排列，可以节约空间
    /// (配置zip的两个属性-字段数+value大小，只要都满足就可以zipmap存储)
    /// 超出就是hash存储
    /// 1 节约空间  2 更新方便
    /// 
    /// 如果实体类型是带Id，可以直接实体存储和读取
    /// 
    /// Hash数据很像关系型数据库的表的一行数据，
    /// 但是其实字段是可以随意定制的，没有严格约束的
    /// 
    /// </summary>
    public class UserInfoTest
    {
        public static void Show()
        {
            UserInfo user = new UserInfo()
            {
                Id = 123,
                Account = "Administrator",
                Address = "武汉市",
                Email = "57265177@qq.com",
                Name = "Eleven",
                Password = "123456",
                QQ = 57265177
            };


            using (RedisStringService service = new RedisStringService())
            {
                //service.Set<string>($"userinfo_{user.Id}", Newtonsoft.Json.JsonConvert.SerializeObject(user));
                service.Set<UserInfo>($"userinfo_{user.Id}", user);
                var userCacheList = service.Get<UserInfo>(new List<string>() { $"userinfo_{user.Id}" });
                var userCache = userCacheList.FirstOrDefault();
                //string sResult = service.Get($"userinfo_{user.Id}");
                //var userCache = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfo>(sResult);
                userCache.Account = "Admin";
                service.Set<UserInfo>($"userinfo_{user.Id}", userCache);
            }
            using (RedisHashService service = new RedisHashService())
            {
                service.FlushAll();
                //反射遍历做一下
                service.SetEntryInHash($"userinfo_{user.Id}", "Account", user.Account);
                service.SetEntryInHash($"userinfo_{user.Id}", "Name", user.Name);
                service.SetEntryInHash($"userinfo_{user.Id}", "Address", user.Address);
                service.SetEntryInHash($"userinfo_{user.Id}", "Email", user.Email);
                service.SetEntryInHash($"userinfo_{user.Id}", "Password", user.Password);
                service.SetEntryInHash($"userinfo_{user.Id}", "Account", "Admin");

                service.StoreAsHash<UserInfo>(user);//含ID才可以的
                var result = service.GetFromHash<UserInfo>(user.Id);

            }
            UserInfo user2 = new UserInfo()
            {
                Id = 234,
                Account = "Administrator2",
                Address = "武汉市2",
                Email = "57265177@qq.com2",
                Name = "Eleven2",
                Password = "1234562",
                QQ = 572651772
            };
            using (RedisHashService service = new RedisHashService())
            {
                service.FlushAll();
                //反射遍历做一下
                service.SetEntryInHash($"userinfo_{user2.Id}", "Account", user2.Account);
                service.SetEntryInHash($"userinfo_{user2.Id}", "Name", user2.Name);
                service.SetEntryInHash($"userinfo_{user2.Id}", "Address", user2.Address);
                service.SetEntryInHash($"userinfo_{user2.Id}", "Email", user2.Email);
                service.SetEntryInHash($"userinfo_{user2.Id}", "Remark", "这里是2号用户");

                service.StoreAsHash<UserInfo>(user);//含ID才可以的
                var result = service.GetFromHash<UserInfo>(user.Id);

            }
        }
    }
    /// <summary>
    /// 用户信息实体
    /// </summary>
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public long QQ { get; set; }
    }
}
