using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ruanmou.Redis.Service;
using System.Threading;

namespace MyRedis.BackService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceStackProcessor.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.Read();
            }
        }
    }
}
