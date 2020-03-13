using Ruanmou.Redis.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyRedis
{
    ///<summary>
    /// 1 Cache和NoSql，Redis原理
    /// 2 String
    /// 3 Hashtable
    /// 4 Set
    /// 5 ZSet
    /// 6 List
    /// 7 分布式异步队列
    /// 
    /// 能听见我说话的，能看到我桌面的小伙伴儿，刷个1
    /// 准备好学习的小伙伴儿，给Eleven老师刷一个专属字母E，然后课程就正式开始了！！！
    /// 
    /// 本地缓存，当前进程的内存，把数据存起来，下次直接用，可以提升效率
    /// 1 容量有限，window下面，32位一个进程最多2G，或者3G  64位最多也就4G
    /// 2 多服务器直接需要缓存共享
    /// 需要分布式缓存---远程服务器内存管理数据，提供读写接口，效率高
    /// 分布式缓存：Memcached 最早流行
    ///             Nosql--Redis 主流方案
    /// 
    /// NoSql:非关系型数据库，Not Only Sql
    ///       web1.0--服务端提供数据客户端看，只能看新闻
    ///       web2.0--客户端可以向服务端互动了，还能评论了（。。。。）
    ///       数据的关系复杂：好友关系(张三有100个好友，映射表，其实就冗余了，关系型数据开始累赘) 
    ///       再就是数据读取和写入压力，硬盘的速度满足不了，尤其是一些大数据量
    ///       所以产生了NoSql了，
    ///       特点：基于内存；
    ///             没有严格的数据格式，不是一行数据的列必须一样
    ///             丰富的类型，满足web2.0的需求
    /// 
    /// Redis:REmote DIctionary Server 远程字典服务器
    ///       基于内存管理(数据存在内存)，实现了5种数据结构(分别应对各种具体需求)，单线程模型的应用程序(单进程单线程)，对外提供插入-查询-固化-集群功能
    /// 
    /// Redis-----SqlServer
    /// redis-cli---SqlClient
    /// REDIS支持N多个命令---Sql语句
    /// RDM---可视化SQLClient
    /// ServiceStack(1小时3600次请求--可破解)----Ado.Net
    /// StackExchange 免费
    /// 其实更像ORM，封装了链接+命令
    /// 
    /// 
    /// 基于内存管理：速度快！不能当数据库；  Redis还有个固化数据的功能，VitualMemory,把一些不经常访问是会存在硬盘  可以配置的；down掉会丢失数据，snapshot可以保存到硬盘，
    ///  AOF：数据变化记录日志，很少用
    ///  Redis毕竟不是数据库，只能用来提升性能，不能作为数据的最终依据
    ///  
    /// 5种数据结构，分别应对各种具体需求，下文详细分解
    /// 
    /// 
    /// 多线程模型：.Net应用都是的，尤其网站，可以更好的发挥硬件的能力，
    ///             但是也有线程冲突的问题和调度的成本
    /// 单线程模型：nodejs单线程，整个进程只有一个线程，线程就是执行流，性能低？实际上并非如此！一次网络请求操作==正则解析请求+加减乘除计算+数据库操作(发命令--等结果)+读文件(发命令--等结果)+调用接口(发命令---等结果)，单线程都是事件驱动，发起命令就做下一件事儿，这个线程是完全不做等待的，一直在计算，单线程非常高；
    /// 单线程多进程的模式来提供集群服务
    /// 
    /// 单线程最大的好处就是原子性操作，就是要么都成功，要么都失败，不会出现中间状态
    /// redis每个命令都是原子性(因为单线程)，不用考虑并发，不会出现中间状态
    ///   
    /// Redis就是为开发而生，会为各种开发需求提供对应的解决方案
    /// 只是为了提升性能，不做数据标准！ 任何的数据固化都是数据库完成的，不能代替数据库
    /// 
    /// 
    /// 五大结构理解：
    /// String： key-value的缓存，支持过期  value不超过512M
    ///          Redis是单线程的，比如SetAll&AppendToValue&GetValues&GetAndSetValue&IncrementValue&IncrementValueBy，这些看上去是组合命令，但实际上是一个具体的命令，是一个原子性的命令，不可能出现中间状态，可以应对一些并发情况
    /// 
    /// 
    /// 1 Hashtable
    /// 2 Set
    /// 3 ZSet
    /// 
    /// Hash：key--Dictionary,1 节约空间（zipmap的紧密摆放的存储模式）  
    ///                       2 更新/访问方便(hashid+key)
    ///                       Hash数据很像关系型数据库的表的一行数据，
    ///                       但是字段是可以随意定制的，没有严格约束的,非常灵活
    /// 
    /// Set：就是数据集合，无序，去重
    /// ZSet：是一个有序集合，去重
    /// 
    /// 
    /// List 分页
    ///      队列，生产者消费者，一个数据，只有一个消费对象
    ///      一个程序写入，一个程序即时读取消费
    ///                 还可以多个程序读取消费
    ///                 按照时间顺序，数据失败了还可以放回去下次重试
    ///                 这种东西，在项目中有什么价值呢？！
    ///                 
    ///      发布订阅：发布一个数据，全部的订阅者都能收到
    ///                
    /// 
    /// 1 List结构
    /// 2 分布式异步队列
    /// 3 发布订阅
    /// 
    /// Redis可以提一些问题
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                {
                    Console.WriteLine("****************ServiceStackTest***************");
                    //BlogPageList.Show();
                    ServiceStackTest.Show();
                }
                {
                    //UserInfoTest.Show();
                }
                {
                    //FriendManager.Show();
                }
                {
                    //RankManager.Show();
                }
                {
                    //Console.WriteLine("****************StackExchangeTest***************");
                    //StackExchangeTest.Show();
                }
                {
                    //OversellTest.Show();
                    //OversellField.Show();
                }
                {
                    //SellAsyncTest.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
