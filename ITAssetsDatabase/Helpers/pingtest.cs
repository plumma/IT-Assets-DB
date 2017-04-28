using System.Net.NetworkInformation;


namespace ITAssetsDatabase.HtmlHelpers
{
    public class pingtest
    {
        
        public static bool IsPingable(string hostname)
        {
                //string to hold our return messge
                string returnMessage = string.Empty;

                Ping pingreq = new Ping();

                bool result = false;

                
                try
                {
                    PingReply reply = pingreq.Send(hostname);
                                        
                    if (reply.Status == IPStatus.Success || reply.Status == IPStatus.TimedOut)
                        return true;
                }
                catch { }

                //Uri url = new Uri(hostname);
                //string pingurl = string.Format("{0}", url.Host);
                //string host = pingurl;
                //bool result = false;
                //Ping p = new Ping();
                //try
                //{
                //    PingReply reply = p.Send(host, 3000);
                //    if (reply.Status == IPStatus.Success)
                //        return true;
                //}
                //catch { }
                return result;
            }
        


    }
}