using Ruanmou.Redis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyRedis
{
    /// <summary>
    /// ServiceStack API封装测试  五大结构理解
    /// String：
    /// Hash
    /// Set
    /// ZSet
    /// List
    /// </summary>
    public class ServiceStackTest
    {
        public static void Show()
        {
            Student student_1 = new Student()
            {
                Id = 11,
                Name = "Eleven"
            };
            Student student_2 = new Student()
            {
                Id = 12,
                Name = "Twelve",
                Remark = "123423245"
            };

            // string key-value
            Console.WriteLine("*****************************************");
            {
                //using (RedisStringService service = new RedisStringService())
                //{
                //    service.Set<string>("student1", "梦的翅膀");
                //    Console.WriteLine(service.Get("student1"));

                //    service.Append("student1", "20180802");
                //    Console.WriteLine(service.Get("student1"));

                //    Console.WriteLine(service.GetAndSetValue("student1", "程序错误"));
                //    Console.WriteLine(service.Get("student1"));

                //    service.Set<string>("student2", "王", DateTime.Now.AddSeconds(5));
                //    Thread.Sleep(5100);
                //    Console.WriteLine(service.Get("student2"));

                //    service.Set<int>("Age", 32);
                //    Console.WriteLine(service.Incr("Age"));
                //    Console.WriteLine(service.IncrBy("Age", 3));
                //    Console.WriteLine(service.Decr("Age"));
                //    Console.WriteLine(service.DecrBy("Age", 3));
                //}
            }


            // hash key-keys[]-value
            Console.WriteLine("*****************************************");
            {
                //using (RedisHashService service = new RedisHashService())
                //{
                //    service.SetEntryInHash("student", "id", "123456");
                //    service.SetEntryInHash("student", "name", "张xx");
                //    service.SetEntryInHash("student", "remark", "高级班的学员");

                //    var keys = service.GetHashKeys("student");
                //    var values = service.GetHashValues("student");
                //    var keyValues = service.GetAllEntriesFromHash("student");
                //    Console.WriteLine(service.GetValueFromHash("student", "id"));

                //    service.SetEntryInHashIfNotExists("student", "name", "太子爷");
                //    service.SetEntryInHashIfNotExists("student", "description", "高级班的学员2");

                //    Console.WriteLine(service.GetValueFromHash("student", "name"));
                //    Console.WriteLine(service.GetValueFromHash("student", "description"));
                //    service.RemoveEntryFromHash("student", "description");
                //    Console.WriteLine(service.GetValueFromHash("student", "description"));
                //}
            }

            // set 无序,不重复
            Console.WriteLine("*****************************************");
            {
                using (RedisSetService service = new RedisSetService())
                {
                    //service.FlushAll();//清理全部数据

                    //service.Add("advanced", "111");
                    //service.Add("advanced", "112");
                    //service.Add("advanced", "114");
                    //service.Add("advanced", "114");
                    //service.Add("advanced", "115");
                    //service.Add("advanced", "115");
                    //service.Add("advanced", "113");

                    //var result = service.GetAllItemsFromSet("advanced");

                    //var random = service.GetRandomItemFromSet("advanced");//随机获取
                    //service.GetCount("advanced");//独立的ip数
                    //service.RemoveItemFromSet("advanced", "114");

                    //{
                    //    service.Add("begin", "111");
                    //    service.Add("begin", "112");
                    //    service.Add("begin", "115");

                    //    service.Add("end", "111");
                    //    service.Add("end", "114");
                    //    service.Add("end", "113");

                    //    var result1 = service.GetIntersectFromSets("begin", "end");
                    //    var result2 = service.GetDifferencesFromSet("begin", "end");
                    //    var result3 = service.GetUnionFromSets("begin", "end");
                    //    //共同好友   共同关注
                    //}
                }
            }
            //Zset 可设置分数
            Console.WriteLine("*****************************************");
            {
                //using (RedisZSetService service = new RedisZSetService())
                //{
                //    service.FlushAll();//清理全部数据

                //    service.Add("advanced", "1");
                //    service.Add("advanced", "2");
                //    service.Add("advanced", "5");
                //    service.Add("advanced", "4");
                //    service.Add("advanced", "7");
                //    service.Add("advanced", "5");
                //    service.Add("advanced", "9");

                //    var result1 = service.GetAll("advanced");
                //    var result2 = service.GetAllDesc("advanced");

                //    service.AddItemToSortedSet("Sort", "BY", 123234);
                //    service.AddItemToSortedSet("Sort", "走自己的路", 123);
                //    service.AddItemToSortedSet("Sort", "redboy", 45);
                //    service.AddItemToSortedSet("Sort", "大蛤蟆", 7567);
                //    service.AddItemToSortedSet("Sort", "路人甲", 9879);
                //    service.AddRangeToSortedSet("Sort", new List<string>() { "123", "花生", "加菲猫" }, 3232);
                //    var result3 = service.GetAllWithScoresFromSortedSet("Sort");

                //    //交叉并
                //}
            }

            // list 
            Console.WriteLine("*****************************************");
            {
                //using (RedisListService service = new RedisListService())
                //{
                //    service.FlushAll();

                //    service.Add("article", "eleven1234");
                //    service.Add("article", "kevin");
                //    service.Add("article", "大叔");
                //    service.Add("article", "C卡");
                //    service.Add("article", "触不到的线");
                //    service.Add("article", "程序错误");
                //    service.RPush("article", "eleven1234");
                //    service.RPush("article", "kevin");
                //    service.RPush("article", "大叔");
                //    service.RPush("article", "C卡");
                //    service.RPush("article", "触不到的线");
                //    service.RPush("article", "程序错误");

                //    var result1 = service.Get("article");
                //    var result2 = service.Get("article", 0, 3);
                //    //可以按照添加顺序自动排序；而且可以分页获取

                //    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                //    //栈
                //    service.FlushAll();
                //    service.Add("article", "eleven1234");
                //    service.Add("article", "kevin");
                //    service.Add("article", "大叔");
                //    service.Add("article", "C卡");
                //    service.Add("article", "触不到的线");
                //    service.Add("article", "程序错误");

                //    for (int i = 0; i < 5; i++)
                //    {
                //        Console.WriteLine(service.PopItemFromList("article"));
                //        var result3 = service.Get("article");
                //    }
                //    Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                //    // 队列：生产者消费者模型   
                //    service.FlushAll();
                //    service.RPush("article", "eleven1234");
                //    service.RPush("article", "kevin");
                //    service.RPush("article", "大叔");
                //    service.RPush("article", "C卡");
                //    service.RPush("article", "触不到的线");
                //    service.RPush("article", "程序错误");

                //    for (int i = 0; i < 5; i++)
                //    {
                //        Console.WriteLine(service.PopItemFromList("article"));
                //        var result4 = service.Get("article");
                //    }
                //    //分布式缓存，多服务器都可以访问到，多个生产者，多个消费者，任何产品只被消费一次
                //}

                #region 生产者消费者
                using (RedisListService service = new RedisListService())
                {
                    service.Add("test", "这是一个学生Add1");
                    service.Add("test", "这是一个学生Add2");
                    service.Add("test", "这是一个学生Add3");

                    service.LPush("test", "这是一个学生LPush1");
                    service.LPush("test", "这是一个学生LPush2");
                    service.LPush("test", "这是一个学生LPush3");
                    service.LPush("test", "这是一个学生LPush4");
                    service.LPush("test", "这是一个学生LPush5");
                    service.LPush("test", "这是一个学生LPush6");

                    service.RPush("test", "这是一个学生RPush1");
                    service.RPush("test", "这是一个学生RPush2");
                    service.RPush("test", "这是一个学生RPush3");
                    service.RPush("test", "这是一个学生RPush4");
                    service.RPush("test", "这是一个学生RPush5");
                    service.RPush("test", "这是一个学生RPush6");

                    List<string> stringList = new List<string>();
                    for (int i = 0; i < 10; i++)
                    {
                        stringList.Add(string.Format($"放入任务{i}"));
                    }
                    service.Add("task", stringList);

                    Console.WriteLine(service.Count("test"));
                    Console.WriteLine(service.Count("task"));
                    var list = service.Get("test");
                    list = service.Get("task", 2, 4);

                    Action act = new Action(() =>
                    {
                        while (true)
                        {
                            Console.WriteLine("************请输入数据**************");
                            string testTask = Console.ReadLine();
                            service.LPush("test", testTask);
                        }
                    });
                    act.EndInvoke(act.BeginInvoke(null, null));
                }
                #endregion

                #region 发布订阅:观察者，一个数据源，多个接受者，只要订阅了就可以收到的，能被多个数据源共享
                Task.Run(() =>
                {
                    using (RedisListService service = new RedisListService())
                    {
                        service.Subscribe("Eleven", (c, message, iRedisSubscription) =>
                        {
                            Console.WriteLine($"注册{1}{c}:{message}，Dosomething else");
                            if (message.Equals("exit"))
                                iRedisSubscription.UnSubscribeFromChannels("Eleven");
                        });//blocking
                    }
                });
                Task.Run(() =>
                {
                    using (RedisListService service = new RedisListService())
                    {
                        service.Subscribe("Eleven", (c, message, iRedisSubscription) =>
                        {
                            Console.WriteLine($"注册{2}{c}:{message}，Dosomething else");
                            if (message.Equals("exit"))
                                iRedisSubscription.UnSubscribeFromChannels("Eleven");
                        });//blocking
                    }
                });
                Task.Run(() =>
                {
                    using (RedisListService service = new RedisListService())
                    {
                        service.Subscribe("Twelve", (c, message, iRedisSubscription) =>
                        {
                            Console.WriteLine($"注册{3}{c}:{message}，Dosomething else");
                            if (message.Equals("exit"))
                                iRedisSubscription.UnSubscribeFromChannels("Twelve");
                        });//blocking
                    }
                });
                using (RedisListService service = new RedisListService())
                {
                    Thread.Sleep(1000);

                    service.Publish("Eleven", "Eleven123");
                    service.Publish("Eleven", "Eleven234");
                    service.Publish("Eleven", "Eleven345");
                    service.Publish("Eleven", "Eleven456");

                    service.Publish("Twelve", "Twelve123");
                    service.Publish("Twelve", "Twelve234");
                    service.Publish("Twelve", "Twelve345");
                    service.Publish("Twelve", "Twelve456");
                    Console.WriteLine("**********************************************");

                    service.Publish("Eleven", "exit");
                    service.Publish("Eleven", "123Eleven");
                    service.Publish("Eleven", "234Eleven");
                    service.Publish("Eleven", "345Eleven");
                    service.Publish("Eleven", "456Eleven");

                    service.Publish("Twelve", "exit");
                    service.Publish("Twelve", "123Twelve");
                    service.Publish("Twelve", "234Twelve");
                    service.Publish("Twelve", "345Twelve");
                    service.Publish("Twelve", "456Twelve");
                }
                //观察者模式：微信订阅号---群聊天---数据同步--
                //MSMQ---RabbitMQ---ZeroMQ---RedisList:学习成本 技术成本
                #endregion
            }
        }
    }
}
