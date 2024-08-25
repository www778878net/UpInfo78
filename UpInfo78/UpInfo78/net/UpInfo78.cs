using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace www778878net.net
{
    /// <summary>
    /// Webapi 传递信息类
    /// </summary>
    public class UpInfo78
    {

        /// <summary>
        /// 用于修改的行idpk 
        /// </summary>
        public int Midpk { get; set; }
        /// <summary>
        /// sid
        /// </summary>
        public string sid { get; set; }

        /// <summary>
        /// 公司表中的id 可设置为cidmy cidvps cidguest
        /// </summary>
        public string cid { get; set; }
        public string bcid { get; set; }

        public string uname { get; set; } = "";
        /// <summary>
        /// 用于修改的行id 
        /// </summary>
        public string mid { get; set; } = "";

        /// <summary>
        /// 获取记录条数
        /// </summary>
        public int getnumber { get; set; }
        /// <summary>
        /// 从哪条记录开始
        /// </summary>
        public int getstart { get; set; }
        /// <summary>
        /// 直接用数组方式传参
        /// </summary>
        public string[] pars { get; set; }
        /// <summary>
        /// 获取列名
        /// </summary>
        public string[] cols { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 电脑名 
        /// </summary>
        public static string pcid { get; set; } = "";
        /// <summary>
        /// 是否倒序
        /// </summary>

        public int desc { get; set; }
        /// <summary>
        /// 平台默认ali
        /// </summary>
        public string server { get; set; }

        public int v { get; set; }
        public string cache { get; set; }
        public UpInfo78()
        {
            server = "ali";
            sid = "";
            cid = "";
            bcid = "";
            pars = new string[0] { };         
            getnumber = 1000;
            getstart = 0;
            order = "idpk";
            desc = 0;
            cache = "";
            cols = new string[] { "all" };
            v = 24;
            Midpk = 0;
        }
        /// <summary>
        /// 转换UrlEncode
        /// </summary>
        /// <returns></returns>
        public string ToUrlEncode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("sid=" + Uri.EscapeDataString(sid));
            if (uname != "") sb.Append("&uname=" + Uri.EscapeDataString(uname));

            switch (cid)
            {
                case "cidguest":
                    cid = "";
                    break;
                case "cidmy":
                    cid = "d4856531-e9d3-20f3-4c22-fe3c65fb009c";
                    break;
                case "cidvps":
                    cid = "28401227-bd00-a20f-c561-ddf0def881d9";
                    break;
            }
            switch (bcid)
            {
                case "cidguest":
                    bcid = "GUEST000-8888-8888-8888-GUEST00GUEST";
                    break;
                case "cidmy":
                    bcid = "";
                    break;
                case "cidvps":
                    bcid = "28401227-bd00-a20f-c561-ddf0def881d9";
                    break;
            }
            if (bcid != "") sb.Append("&bcid=" + Uri.EscapeDataString(bcid));

            if (cid != "") sb.Append("&cid=" + Uri.EscapeDataString(cid));

            
            if (cache != "") sb.Append("&cache=" + cache);
            if (mid != "") sb.Append("&mid=" + Uri.EscapeDataString(mid));


            if (pcid != "") sb.Append("&pcid=" + pcid);
            string orderdesc = order;
            if (desc == 1)
                orderdesc = order + " desc";
            if (orderdesc != "idpk") sb.Append("&order=" + Uri.EscapeDataString(orderdesc));
            if (getnumber != 1000)
                sb.Append("&getnumber=" + getnumber.ToString());
            if (getstart != 0)
                sb.Append("&getstart=" + getstart.ToString());
            if (v != 24)sb.Append("&v=" + v);
            if (Midpk != 0) sb.Append("&midpk=" + Midpk);



            if (pars.Length > 0)
            {
                string tmp = "";
                if (v == 22)
                {
                    bool isini = true;
                    for (int i = 0; i < pars.Length; i++)
                    {
                        if (isini)
                        {
                            isini = false;
                            tmp += pars[i];
                        }
                        else
                        {
                            tmp += ",~" + pars[i];
                        }
                    }
                    byte[] bs = Encoding.UTF8.GetBytes(tmp);
                    tmp = Convert.ToBase64String(bs);
                    tmp = tmp.Replace('+', '*').Replace('/', '-').Replace('=', '.');
                }
                else
                {
                    tmp = JsonConvert.SerializeObject(pars);
                }

                sb.Append("&pars=" + Uri.EscapeDataString(tmp));


            }
            if (cols.Length > 0 && cols[0] != "all")
                sb.Append("&cols=" + JsonConvert.SerializeObject(cols));

            return sb.ToString();
        }
        private void AddPar(string[] addpar)
        {
            if (pars.Length == 0)
            {
                pars = addpar;
                return;
            }
            string[] result = new string[pars.Length + addpar.Length];

            pars.CopyTo(result, 0);
            addpar.CopyTo(result, pars.Length);
            pars = result;
        }
        /// <summary>
        /// 数据参数转换为字符串 |
        /// </summary>
        /// <param name="sInfo"></param>
        public void setPar(params string[] sInfo)
        {
            AddPar(sInfo);
        }



    }
}
