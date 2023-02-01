using System.Net;
using System.Net.Sockets;

namespace SharpMason.Extensions.Utils
{
    public class NetWorkUtil
    {

        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetHostIp()
        {
            var ipv4S = "";
            var reserve = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var curAdd in reserve.AddressList)
            {
                if (curAdd.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipv4S += $"{curAdd},";
                }
            }


            if (ipv4S.Length == 0)
            {
                throw new Exception("获取本机IP地址出错！");
            }

            var ips = ipv4S.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (ips.Length == 1)
            {
                return ips[0];
            }

            //排除docker默认
            var realIPs = ips.Where(w => w != "172.17.0.1").ToArray();
            if (realIPs.Length <= 0) return ips[0];
            {
                //优先k8s
                var localIPs = realIPs.Where(w => w.StartsWith("10")).ToArray();
                return localIPs.Any() ? localIPs.First() : realIPs[0];
            }

        }
    }
}
