
using Discord;
using Discord.WebSocket;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Nft
{
    public string Name;
    public string Url;
    public string ImageUrl;
    public string OpenSeaUrl;
    public string FloorPrice;
    public string SalesCount5;
    public string SalesCount30;
    public string SupplyCount;


    public void ReadAllOfThis()
    {
        Console.WriteLine("Name: {0}", Name);
        Console.WriteLine("Url: {0}", Url);
        Console.WriteLine("ImageUrl: {0}", ImageUrl);
        Console.WriteLine("OpenSeaUrl: {0}", OpenSeaUrl);
        Console.WriteLine("FloorPrice: {0}", FloorPrice);
        Console.WriteLine("SalesCount5: {0}", SalesCount5);
        Console.WriteLine("SalesCount30: {0}", SalesCount30);
        Console.WriteLine("SupplyCount: {0}", SupplyCount);
        Console.WriteLine("\n");
    }

}

public class EmbedWithRole
{
    public EmbedBuilder eb;
    public SocketRole role;


}

namespace Discord_BlaBot
{
    internal class Program
    {
        #region Values
        public static DiscordSocketClient _client;
        public static EdgeDriver driver;
        public static ulong GuildID;
        public static ulong LowFomoRoleID;
        public static ulong ModerateFomoRoleID;
        public static ulong ExtremeFomoRoleID;
        public static ulong NftLogChannelID;
        public static bool GetOpenSeaURLorNo;

        #endregion

        public static async Task RefreshPages()
        {
            driver.Navigate().GoToUrl("https://icy.tools/collections/trending");
            driver.FindElement(By.CssSelector("#headlessui-menu-button-3")).SendKeys(" ");
            await Task.Delay(1000);
            var test1 = driver.FindElement(By.CssSelector("#__next > main > div > div > div > div > div > div:nth-child(2) > div > div"));

            var test2 = test1.FindElements(By.XPath(".//*"));
            var test3 = test2.ToList().Find(m => m.TagName.ToLower().Contains("div"));
            var test4 = test3.FindElements(By.XPath(".//*"));
            test4.ToList().RemoveRange(0, test4.ToList().FindIndex(m => m.TagName.ToLower().Contains("div")));

            test4[7].SendKeys(" ");
            await Task.Delay(1000);
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            driver.Navigate().GoToUrl("https://icy.tools/collections/trending");
            driver.FindElement(By.CssSelector("#headlessui-menu-button-3")).SendKeys(" ");
            await Task.Delay(1000);
            var test12 = driver.FindElement(By.CssSelector("#__next > main > div > div > div > div > div > div:nth-child(2) > div > div"));

            var test22 = test12.FindElements(By.XPath(".//*"));
            var test32 = test22.ToList().Find(m => m.TagName.ToLower().Contains("div"));
            var test42 = test32.FindElements(By.XPath(".//*"));
            test42.ToList().RemoveRange(0, test42.ToList().FindIndex(m => m.TagName.ToLower().Contains("div")));

            test42[9].SendKeys(" ");
            await Task.Delay(1000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
        }
        public static async Task MainAsync()
        {

            EdgeOptions options = new EdgeOptions();
            options.AddArgument("--log-level=3");
            // Here you set the path of the profile ending with User Data not the profile folder
            var config = File.ReadAllLines(Environment.CurrentDirectory + "/Config.txt");
           
            Console.WriteLine(config[6].Substring(config[6].IndexOf(":") + 1).Trim());
            // Here you specify the actual profile folder
           
            driver = new EdgeDriver(options);

            IJavaScriptExecutor jscript = driver as IJavaScriptExecutor;
            driver.Navigate().GoToUrl("https://icy.tools/collections/trending");
            Console.Write("Did you logined? If logined type \"yes\": ");

            while (Console.ReadLine().ToLower().Trim() != "yes")
            {
                Console.Write("Please type \"yes\": ");

            }


            Console.WriteLine("\n");
            Console.Write("You want to get OpenSea URLs too? If yes type \"yes\", If no type \"no\": ");
            var whatsthat = Console.ReadLine().ToLower().Trim();
            while (whatsthat != "yes" && whatsthat != "no")
            {
                Console.Write("Please type \"yes\" or \"no\": ");
                whatsthat = Console.ReadLine().ToLower().Trim();
            }

            if (whatsthat == "yes")
            {

                GetOpenSeaURLorNo = true;
            }
            else
            {

                GetOpenSeaURLorNo = false;
            }




            driver.Navigate().GoToUrl("https://icy.tools/collections/trending");
            driver.FindElement(By.CssSelector("#headlessui-menu-button-3")).SendKeys(" ");
            await Task.Delay(1000);
            var test1 = driver.FindElement(By.CssSelector("#__next > main > div > div > div > div > div > div:nth-child(2) > div > div"));

            var test2 = test1.FindElements(By.XPath(".//*"));
            var test3 = test2.ToList().Find(m => m.TagName.ToLower().Contains("div"));
            var test4 = test3.FindElements(By.XPath(".//*"));
            test4.ToList().RemoveRange(0, test4.ToList().FindIndex(m => m.TagName.ToLower().Contains("div")));
           
            test4[7].SendKeys(" ");
            await Task.Delay(1000);
           
          
       
            jscript.ExecuteScript("window.open('{0}', '_blank');");
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            driver.Navigate().GoToUrl("https://icy.tools/collections/trending");
            driver.FindElement(By.CssSelector("#headlessui-menu-button-3")).SendKeys(" ");
            await Task.Delay(1000);
            var test12 = driver.FindElement(By.CssSelector("#__next > main > div > div > div > div > div > div:nth-child(2) > div > div"));

            var test22 = test12.FindElements(By.XPath(".//*"));
            var test32 = test22.ToList().Find(m => m.TagName.ToLower().Contains("div"));
            var test42 = test32.FindElements(By.XPath(".//*"));
            test42.ToList().RemoveRange(0, test42.ToList().FindIndex(m => m.TagName.ToLower().Contains("div")));
         
            test42[9].SendKeys(" ");
            await Task.Delay(1000);

            driver.SwitchTo().Window(driver.WindowHandles[0]);
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                GatewayIntents =
                GatewayIntents.Guilds |
                GatewayIntents.GuildMembers |
                GatewayIntents.GuildMessageReactions |
                GatewayIntents.GuildMessages |
                GatewayIntents.GuildVoiceStates | GatewayIntents.All

            });




            _client.Log += Log;
            var lines = config;

            var bottoken = lines[0].Substring(lines[0].IndexOf("BotToken:") + "BotToken:".Length).Trim();

            Console.WriteLine(bottoken);
            Console.WriteLine(lines[1]);
            GuildID =ulong.Parse(lines[1].Substring(lines[1].IndexOf(":") + 1).Trim());

            NftLogChannelID =ulong.Parse(lines[2].Substring(lines[2].IndexOf(":") + 1).Trim());
            Console.WriteLine(NftLogChannelID);
            ExtremeFomoRoleID =ulong.Parse(lines[3].Substring(lines[3].IndexOf(":") + 1).Trim());
            Console.WriteLine(ExtremeFomoRoleID);
            ModerateFomoRoleID =ulong.Parse(lines[4].Substring(lines[4].IndexOf(":") + 1).Trim());
            Console.WriteLine(ModerateFomoRoleID);
            LowFomoRoleID =ulong.Parse(lines[5].Substring(lines[5].IndexOf(":") + 1).Trim());
            Console.WriteLine(LowFomoRoleID);
            await _client.LoginAsync(TokenType.Bot, bottoken);
            await _client.StartAsync();




            _client.Ready += ReadyHandler;

            await Task.Delay(-1);




        }

        private static Task ReadyHandler()
        {
            _=StartLoop();
            return Task.CompletedTask;

        }

        public static async Task StartLoop()
        {

            Console.WriteLine("XD");
            List<Nft> nfts = new List<Nft>();
            List<EmbedWithRole> embeds = new List<EmbedWithRole>();
            try
            {
                for (int c = 1; c < 6; c++)
                {
                    var nft = new Nft();



                    var URLOfNFT = driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div > div.flex.flex-col > div > div > div > table > tbody > tr:nth-child({c}) > td:nth-child(1) > a"));
                    if (URLOfNFT.Count > 0)
                    {

                        nft.Url = URLOfNFT[0].GetDomProperty("href").Trim();

                    }

                    var ImageOfNFT = driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div > div.flex.flex-col > div > div > div > table > tbody > tr:nth-child({c}) > td:nth-child(1) > a > div > img"));

                    if (ImageOfNFT.Count > 0)
                    {

                        nft.ImageUrl = ImageOfNFT[0].GetDomProperty("src");


                    }
                    else
                    {
                        nft.ImageUrl = "https://pbs.twimg.com/profile_images/715852271389655041/s-VdeDI5_400x400.jpg";
                    }

                    var NameOfNFT = driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div > div.flex.flex-col > div > div > div > table > tbody > tr:nth-child({c}) > td:nth-child(1) > a > div > div > p.font-body.font-bold.text-base.text-dark.dark\\:text-light.overflow-ellipsis.overflow-hidden"));

                    if (NameOfNFT.Count > 0)
                    {

                        nft.Name = NameOfNFT[0].Text.ToString();
                    }
                    else
                    {
                        nft.Name = "Not defined.";
                    }

                    var SupplyCountOfNFT = driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div > div.flex.flex-col > div > div > div > " +
                        $"table > tbody > tr:nth-child({c}) > td:nth-child(1) > a > div > div > p.font-body.font-normal.text-sm.text-dim > strong"));

                    try
                    {
                        if (SupplyCountOfNFT.Count > 0)
                        {

                            if (!string.IsNullOrEmpty(SupplyCountOfNFT[0].Text.ToString()))
                            {
                                if (SupplyCountOfNFT[0].Text.ToCharArray().Length > 0)
                                {
                                    if (!string.IsNullOrWhiteSpace(SupplyCountOfNFT[0].Text.ToString()))
                                    {

                                        nft.SupplyCount =SupplyCountOfNFT[0].Text.ToString();
                                    }
                                }


                            }




                        }
                        else
                        {
                            nft.SupplyCount = "Not defined.";
                        }
                    }
                    catch
                    {
                        nft.SupplyCount = "Not defined.";
                    }



                    var floor = driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div > div.flex.flex-col > div > div > div > table > tbody > tr:nth-child({c}) > td:nth-child(2) > a > p"));

                    if (floor.Count > 0)
                    {

                        nft.FloorPrice = floor[0].Text.Substring(2);

                        if (nft.FloorPrice.IndexOf("-") != -1)
                        {
                            nft.FloorPrice = nft.FloorPrice.Remove(nft.FloorPrice.IndexOf("-"));
                        }
                        if (nft.FloorPrice.IndexOf("+") != -1)
                        {
                            nft.FloorPrice = nft.FloorPrice.Remove(nft.FloorPrice.IndexOf("+"));
                        }

                        if (nft.FloorPrice.IndexOf("-") != -1)
                        {
                            nft.FloorPrice = nft.FloorPrice.Remove(nft.FloorPrice.IndexOf("-"));
                        }

                        nft.FloorPrice = nft.FloorPrice.Replace("\n", "");
                        nft.FloorPrice = nft.FloorPrice.Replace("\r", "");
                        nft.FloorPrice = nft.FloorPrice.Replace("-", "");
                        nft.FloorPrice = nft.FloorPrice.Replace("+", "");
                        nft.FloorPrice = nft.FloorPrice.Replace("%", "");
                        nft.FloorPrice = nft.FloorPrice.Trim();

                        nft.FloorPrice += " ETH";


                    }
                    else
                    {
                        nft.FloorPrice = "Not defined.";
                    }


                    var fiveminsales = driver.FindElements(By.XPath($"/html/body/div[1]/main/div/div/div/div/div/div[3]/div/div/div/table/tbody/tr[{c}]/td[5]/a/p"));


                    if (fiveminsales.Count > 0)
                    {

                        string myText = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].firstChild.textContent;", fiveminsales[0]);

                        nft.SalesCount5 = myText;

                        if (nft.SalesCount5.IndexOf("-") != -1)
                        {
                            nft.SalesCount5 = nft.SalesCount5.Remove(nft.SalesCount5.IndexOf("-"));
                        }

                        if (nft.SalesCount5.IndexOf("+") != -1)
                        {

                            nft.SalesCount5 = nft.SalesCount5.Remove(nft.SalesCount5.IndexOf("+"));
                        }

                        if (nft.SalesCount5.IndexOf("-") != -1)
                        {

                            nft.SalesCount5 = nft.SalesCount5.Remove(nft.SalesCount5.IndexOf("-"));
                        }

                        nft.SalesCount5 = nft.SalesCount5.Replace("\n", "");
                        nft.SalesCount5 = nft.SalesCount5.Replace("\r", "");
                        nft.SalesCount5 = nft.SalesCount5.Trim();

                        nft.SalesCount5 = nft.SalesCount5.Replace("–", "");


                    }
                    else
                    {
                        nft.SalesCount5 = "Not defined.";
                    }


                    driver.SwitchTo().Window(driver.WindowHandles[1]);

                    for (int i = 1; i < 100; i++)
                    {
                        if (driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div > div.flex.flex-col > div > div > div > table > tbody > tr:nth-child({i}) > td:nth-child(1) > a > div > div > p.font-body.font-bold.text-base.text-dark.dark\\:text-light.overflow-ellipsis.overflow-hidden")).Count > 0)
                        {

                            var elementt = driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div > div.flex.flex-col > div > div > div > table > tbody > tr:nth-child({i}) > td:nth-child(1) > a > div > div > p.font-body.font-bold.text-base.text-dark.dark\\:text-light.overflow-ellipsis.overflow-hidden"))[0];

                            if (nft.Name.ToLower().Trim().Contains(elementt.Text.ToLower()))
                            {


                                var thirtyminsales = driver.FindElements(By.XPath($"/html/body/div[1]/main/div/div/div/div/div/div[3]/div/div/div/table/tbody/tr[{i}]/td[5]/a/p"));

                                if (thirtyminsales.Count > 0)
                                {
                                    string myText = (string)((IJavaScriptExecutor)driver).ExecuteScript("return arguments[0].firstChild.textContent;", thirtyminsales[0]);
                                    nft.SalesCount30 = myText;

                                    if (nft.SalesCount30.IndexOf("-") != -1)
                                    {
                                        nft.SalesCount30 = nft.SalesCount30.Remove(nft.SalesCount30.IndexOf("-"));
                                    }

                                    if (nft.SalesCount30.IndexOf("+") != -1)
                                    {

                                        nft.SalesCount30 = nft.SalesCount30.Remove(nft.SalesCount30.IndexOf("+"));
                                    }

                                    if (nft.SalesCount30.IndexOf("-") != -1)
                                    {

                                        nft.SalesCount30 = nft.SalesCount30.Remove(nft.SalesCount30.IndexOf("-"));
                                    }

                                    nft.SalesCount30 = nft.SalesCount30.Replace("\n", "");
                                    nft.SalesCount30 = nft.SalesCount30.Replace("\r", "");
                                    nft.SalesCount30 = nft.SalesCount30.Trim();

                                    nft.SalesCount30 = nft.SalesCount30.Replace("–", "");

                                    i = 500;

                                }
                                else
                                {
                                    nft.SalesCount30 = "Not defined.";
                                }
                            }
                        }
                    }

                    driver.SwitchTo().Window(driver.WindowHandles[0]);
                    if (GetOpenSeaURLorNo)
                    {
                        IJavaScriptExecutor jscript = driver as IJavaScriptExecutor;
                        jscript.ExecuteScript("window.open('{0}', '_blank');");

                        driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
                        nft.OpenSeaUrl = nft.Url;
                        driver.Navigate().GoToUrl(nft.Url);
                        for (int test = 0; test < 55; test++)
                        {
                            if (driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div.flex.flex-col > div > div.flex.flex-row.flex-wrap.justify-between > div > p:nth-child({test}) > a")).Count > 0)
                            {
                                try
                                {
                                    if (driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div.flex.flex-col > div > div.flex.flex-row.flex-wrap.justify-between > div > p:nth-child({test}) > a"))[0].GetDomProperty("href") != null)
                                    {
                                        if (c == 3)
                                        {
                                            Console.WriteLine(driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div.flex.flex-col > div > div.flex.flex-row.flex-wrap.justify-between > div > p:nth-child({test}) > a"))[0].GetDomProperty("href"));
                                        }
                                        if (driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div.flex.flex-col > div > div.flex.flex-row.flex-wrap.justify-between > div > p:nth-child({test}) > a"))[0].GetDomProperty("href").Contains("opensea.io"))
                                        {
                                            nft.OpenSeaUrl = driver.FindElements(By.CssSelector($"#__next > main > div > div > div > div > div.flex.flex-col > div > div.flex.flex-row.flex-wrap.justify-between > div > p:nth-child({test}) > a"))[0].GetDomProperty("href").Trim();
                                            test = 100;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                            }
                        }
                        driver.Close();
                        driver.SwitchTo().Window(driver.WindowHandles[0]);
                    }
                    else
                    {
                        nft.OpenSeaUrl = nft.Url;
                    }






                    nfts.Add(nft);

                    var eb = new EmbedBuilder();
                    eb.Url = nft.OpenSeaUrl;
                    if (!nft.Name.ToLower().Contains("collection"))
                    {
                        eb.Title = nft.Name + " Collection";
                    }
                    else
                    {
                        eb.Title = nft.Name;
                    }

                    int bla = 0;
                    var ebwithrole = new EmbedWithRole();
                    if (int.TryParse(nft.SalesCount5, out bla))
                    {
                        int level = int.Parse(nft.SalesCount5);
                        if (level < 20)
                        {

                            ebwithrole.role =  _client.GetGuild(GuildID).GetRole(LowFomoRoleID);

                            eb.AddField("Level", ebwithrole.role.Mention, true);
                            eb.Color = ebwithrole.role.Color;
                           

                        }
                        else if (level >= 20 && level < 40)
                        {
                            ebwithrole.role =  _client.GetGuild(GuildID).GetRole(ModerateFomoRoleID);

                            eb.AddField("Level", ebwithrole.role.Mention, true);
                            eb.Color = ebwithrole.role.Color;
                           
                        }
                        else if (level >= 40)
                        {
                            ebwithrole.role =  _client.GetGuild(GuildID).GetRole(ExtremeFomoRoleID);

                            eb.AddField("Level", ebwithrole.role.Mention, true);

                            eb.Color = ebwithrole.role.Color;
                           
                        }

                    }
                    else
                    {

                        eb.AddField("Level", "Undefined", true);
                        eb.Color = Color.DarkerGrey;

                    }


                    eb.AddField("Floor", nft.FloorPrice, true);
                    eb.AddField("5m Sales", nft.SalesCount5, true);
                    eb.AddField("30m Sales", nft.SalesCount30, true);
                    eb.AddField("Supply", nft.SupplyCount, true);

                    eb.ThumbnailUrl = nft.ImageUrl.Trim();
                    var sign = new EmbedFooterBuilder();
                    sign.Text = "Made by LindaMosep";
                    sign.IconUrl = "https://c.tenor.com/KChHVc7BktYAAAAM/discord-loading.gif";

                    eb.Footer = sign;
                    ebwithrole.eb = eb;
                    embeds.Add(ebwithrole);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            foreach (var m in embeds)
            {
                if (m.role != null)
                {
                    await _client.GetGuild(GuildID).GetTextChannel(NftLogChannelID).SendMessageAsync(m.role.Mention, false, m.eb.Build());
                }
                else
                {
                    await _client.GetGuild(GuildID).GetTextChannel(NftLogChannelID).SendMessageAsync("", false, m.eb.Build());
                }

            }
            await RefreshPages();
            await Task.Delay(new TimeSpan(0, 4, 45));
            _=StartLoop();
        }

        static void Main(string[] args)
        {
            MainAsync().Wait();

        }

        static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }


}
