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
    /// 超卖：订单数超过商品
    /// 
    ///数据库：秒杀的时候，10件商品，100个人想买，假定大家一瞬间都来了，
    ///A 查询还有没有--有---1更新
    ///B 查询还有没有--有---1更新
    ///C 查询还有没有--有---1更新
    ///可能会卖出12  12甚至20件商品
    ///
    /// 微服务也有超卖的问题，异步队列
    /// 
    /// 
    /// Redis原子性操作--保证一个数值只出现一次--防止一个商品卖给多个人
    /// 
    /// 
    /// Redis是单线程，程序怎么又多线程操作Redis呢？ 这个是可以的，
    /// 打开多个链接，去提交任务，对程序而言，Redis是并发
    /// 
    /// 假如redis命令是拼装的，Decr---1 获取值  2 程序减1  3 再save结果回去
    ///     程序多线程并发一下，  A线程  1   2   3     初始值是10
    ///                           B线程    1 2 3       结果是9，减了2次但是结果是9
    ///                           
    ///     组合命令，Decr  Redis线程直接完成当前值-1并返回结果，原子性操作                     
    ///       程序多线程并发一下，A线程     123        初始值10
    ///                           B线程  123       
    ///                           C线程        123      得到3个结果，9/8/7
    ///  
    /// 假如库存只有1个(数据库)，三个人同时来下单，一检测>0,都会成功--超卖
    ///                          三个人同时来，lock/开启事务
    ///                          只有一个人能去检测>0    -1  save 
    ///                          然后第二个人来，==0 返回失败 
    ///                          然后第三个人来，==0 返回失败
    ///     因为这个等于是数据库单线程了，其他都要阻塞，各种超时
    ///     -1的时候除了操作库存，还得增加订单，等支付。。
    ///     10个商品秒杀，一次只能进一个？ 违背了业务
    ///     
    /// 所以用上了Redis，一方面保证绝对不会超卖，
    ///                 另一方面没有效率影响，数据库是可以为成功的人并发的
    ///                 还有撤单的时候增加库存，可以继续秒杀，
    ///                 限制秒杀的库存是放在redis，不是数据库，不会造成数据的不一致性
    ///            
    /// Redis能够拦截无效的请求，如果没有这一层，所有的请求压力都到数据库
    /// 
    /// 缓存击穿/穿透---缓存down掉，请求全部到数据库
    /// 缓存预热功能---缓存重启，数据丢失，多了一个初始化缓存数据动作(写代码去把数据读出来放入缓存)
    /// </summary>
    public class OversellTest
    {
        private static bool IsGoOn = true;//秒杀活动是否结束
        public static void Show()
        {
            using (RedisStringService service = new RedisStringService())
            {
                service.Set<int>("Stock", 10);//是库存
            }

            for (int i = 0; i < 5000; i++)
            {
                int k = i;
                Task.Run(() =>//每个线程就是一个用户请求
                {
                    using (RedisStringService service = new RedisStringService())
                    {
                        if (IsGoOn)
                        {
                            long index = service.Decr("Stock");//-1并且返回  
                            if (index >= 0)
                            {
                                Console.WriteLine($"{k.ToString("000")}秒杀成功，秒杀商品索引为{index}");
                                //service.Incr("Stock");//+1
                                //可以分队列，去数据库操作
                            }
                            else
                            {
                                if (IsGoOn)
                                {
                                    IsGoOn = false;
                                }
                                Console.WriteLine($"{k.ToString("000")}秒杀失败，秒杀商品索引为{index}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"{k.ToString("000")}秒杀停止......");
                        }
                    }
                });
            }
            Console.Read();
        }
    }


    public class OversellField
    {
        private static bool IsGoOn = true;//秒杀活动是否结束
        private static int Stock = 0;
        public static void Show()
        {
            Stock = 10;

            for (int i = 0; i < 5000; i++)
            {
                int k = i;
                Task.Run(() =>//每个线程就是一个用户请求
                {
                    if (IsGoOn)
                    {
                        long index = Stock;//-1并且返回 去数据库查一下当前的库存
                        Thread.Sleep(100);

                        if (index >= 1)
                        {
                            Stock = Stock - 1;//更新库存
                            Console.WriteLine($"{k.ToString("000")}秒杀成功，秒杀商品索引为{index}");
                            //可以分队列，去数据库操作
                        }
                        else
                        {
                            if (IsGoOn)
                            {
                                IsGoOn = false;
                            }
                            Console.WriteLine($"{k.ToString("000")}秒杀失败，秒杀商品索引为{index}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"{k.ToString("000")}秒杀停止......");
                    }
                });
            }
            Console.Read();
        }
    }
}
