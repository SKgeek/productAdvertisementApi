using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Demo123.Models;


namespace ClassesForAll
{
    class Flipkart
    {
        public static List<Product> getFlipkartData(string key)
        {
            WebClient WCFlipkart = new WebClient();
            WCFlipkart.Headers.Add("Fk-Affiliate-Id", "//Your Affiliate Id //");
            WCFlipkart.Headers.Add("Fk-Affiliate-Token", "//Your Affiliate Token //");
            string Flipkartjson = WCFlipkart.DownloadString("https://affiliate-api.flipkart.net/affiliate/1.0/search.json?query="
                                                            + key + "&resultCount=10");
            List<Product> lisFlipkart = new List<Product>();
            JObject jobject = (JObject)JsonConvert.DeserializeObject(Flipkartjson);

            var jproductData = jobject["productInfoList"];
          
            foreach (var item in jproductData)
            {
                try
                {
                    Product p = new Product();
                    p.id = item["productBaseInfoV1"]["productId"].ToString();
                    p.title = item["productBaseInfoV1"]["title"].ToString();
                    p.imgsrc = item["productBaseInfoV1"]["imageUrls"]["400x400"].ToString();
                    p.price = item["productBaseInfoV1"]["flipkartSellingPrice"]["amount"].ToString();
                    p.url = item["productBaseInfoV1"]["productUrl"].ToString();
                    p.website = "Flipkart";
                    p.productType = item["productBaseInfoV1"]["categoryPath"].ToString();

                    lisFlipkart.Add(p);
                    p.Equals(null);
                }
                catch { }
                //Category = lisFlipkart.GroupBy(X => X.productType).Select(group => new { productType = group.Key, count = group.Count() }).OrderByDescending(x => x.count).First().ToString();
            }
            return lisFlipkart;

        }
        public static Product getFlipkartDataById(string id)
        {
            WebClient WCFlipkartid = new WebClient();
            WCFlipkartid.Headers.Add("Fk-Affiliate-Id", "//Your Affiliate Id //");
            WCFlipkartid.Headers.Add("Fk-Affiliate-Token", "//Your Affiliate Token //");
            string Flipkartjson = WCFlipkartid.DownloadString("https://affiliate-api.flipkart.net/affiliate/1.0/product.json?id="+ id);
            
            JObject jobject = (JObject)JsonConvert.DeserializeObject(Flipkartjson);

            var jproductData = jobject["productInfoList"];
            Product p = new Product();
           

                p.id = jobject["productBaseInfoV1"]["productId"].ToString();
                p.title = jobject["productBaseInfoV1"]["title"].ToString();
                p.imgsrc = jobject["productBaseInfoV1"]["imageUrls"]["400x400"].ToString();
                p.price = jobject["productBaseInfoV1"]["flipkartSpecialPrice"]["amount"].ToString();
                p.url = jobject["productBaseInfoV1"]["productUrl"].ToString();
                p.website = "Flipkart";
                p.productType = jobject["productBaseInfoV1"]["categoryPath"].ToString();

               

            
            return p;

        }

        public static List< Deals> getFlipkartDealsofDay()
        {
            WebClient WCFlipkartid = new WebClient();
            WCFlipkartid.Headers.Add("Fk-Affiliate-Id", "//Your Affiliate Id //");
            WCFlipkartid.Headers.Add("Fk-Affiliate-Token", "//Your Affiliate Token //");
            string Flipkartjson = WCFlipkartid.DownloadString("https://affiliate-api.flipkart.net/affiliate/offers/v1/dotd/json");

            JObject jobject = (JObject)JsonConvert.DeserializeObject(Flipkartjson);
            List<Deals> lisFlipkart = new List<Deals>();
            var jproductData = jobject["dotdList"];
            int i = 0;
            while (i<10 )
            {
                try
                {
                    Deals d = new Deals();
                    d.title = jobject["dotdList"][i]["title"].ToString();
                    d.description = jobject["dotdList"][i]["description"].ToString();
                    d.url = jobject["dotdList"][i]["url"].ToString();
                    d.imgurl_default = jobject["dotdList"][i]["imageUrls"][1]["url"].ToString();
                    d.imgsrc = d.imgurl_default;
                    d.website = "Flipkart";
                    lisFlipkart.Add(d);
                    i++;
                }
                catch { }
            }
            return lisFlipkart;
        }

        //public static List<Deals> getFlipkartOffers()
        //{
        //    WebClient WCFlipkartid = new WebClient();
        //    WCFlipkartid.Headers.Add("Fk-Affiliate-Id", "//Your Affiliate Id //");
        //    WCFlipkartid.Headers.Add("Fk-Affiliate-Token", "//Your Affiliate Token //");
        //    string Flipkartjson = WCFlipkartid.DownloadString("https://affiliate-api.flipkart.net/affiliate/offers/v1/all/json");

        //    JObject jobject = (JObject)JsonConvert.DeserializeObject(Flipkartjson);
        //    List<Deals> lisFlipkart = new List<Deals>();
        //    var jproductData = jobject["allOffersList"];
        //    int i = 0;
        //    while (jobject["allOffersList"].HasValues)
        //    {

        //        Deals d = new Deals();
        //        d.title = jobject["allOffersList"][i]["title"].ToString();
        //        d.description = jobject["allOffersList"][i]["description"].ToString();
        //        d.url = jobject["allOffersList"][i]["url"].ToString();
        //        d.imgurl_default = jobject["allOffersList"][i]["imageUrls"][1]["url"].ToString();

        //        lisFlipkart.Add(d);

        //    }
        //    return lisFlipkart;
        //}






    }

}

