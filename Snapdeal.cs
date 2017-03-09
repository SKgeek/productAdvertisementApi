using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Demo123.Models;
using HtmlAgilityPack;

namespace ClassesForAll
{
    class Snapdeal
    {

        public static Product getSnapdealProductById(string id) {
            WebClient wcSnapdeal = new WebClient();
            Product lisSnapdeal = new Product();
            wcSnapdeal.Headers.Add("Snapdeal-Affiliate-Id", "//Your Affiliate Id//");
            wcSnapdeal.Headers.Add("Snapdeal-Token-Id", "//Your Affiliate Token//");
            var SnapdealJson = wcSnapdeal.DownloadString("http://affiliate-feeds.snapdeal.com/feed/product?id="+id);

            JObject jobject = (JObject)JsonConvert.DeserializeObject(SnapdealJson);
            var title = jobject["title"].ToString();
            var description = jobject["description"].ToString();
            var link = jobject["link"].ToString();
            var imageLink = jobject["imageLink"].ToString();
            var subCategoryId = jobject["subCategoryId"].ToString();
            var subCategoryName = jobject["subCategoryName"].ToString();
            var categoryId = jobject["categoryId"].ToString();
            var offerPrice = jobject["offerPrice"].ToString();

            Product SnapdealProduct = new Product();
            SnapdealProduct.id = id;
            SnapdealProduct.title = title;
            SnapdealProduct.url = link;
            SnapdealProduct.productType = subCategoryName;
            SnapdealProduct.imgsrc = imageLink;
            SnapdealProduct.price =offerPrice;
            SnapdealProduct.website = "Snapdeal";
            

            



            return SnapdealProduct;
            
        }
        public static List<Product> getSnapdealByKeyword(string keyword) {
            List<Product> lis = new List<Product>();
           
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load("https://www.snapdeal.com/search?sort=rlvncy&noOfResults=10&utm_source=aff_prog&utm_campaign=afts&offer_id=17&aff_id=//Your Affiliate Id//&keyword=" + keyword);

                var divs = document.DocumentNode.Descendants("div")
                    .Where(node => node.GetAttributeValue("class", "").Equals("col-xs-6  product-tuple-listing js-tuple "))
                    .ToList();
            int c = 0;
            try
            {
                foreach (var div in divs)
                {
                    Product p = new Product();
                    p.title = div.Descendants("p").FirstOrDefault().InnerText;
                    p.price = div.Descendants("span")
                                   .Where(node => node.GetAttributeValue("class", "").Equals("lfloat product-price")).FirstOrDefault().InnerText;
                   p.id = div.Descendants("a").FirstOrDefault().ChildAttributes("pogid").FirstOrDefault().Value;


                    c++;
                    if (c < 4)
                    {

                        p.imgsrc = div.Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;
                       
                    }
                    else
                    {
                        p.imgsrc = div.Descendants("source")
                                      .Where(node => node.GetAttributeValue("class", "").Equals("product-image"))
                                      .FirstOrDefault().ChildAttributes("srcset").FirstOrDefault().Value;
                    }

                    lis.Add(p);
                    p.Equals(null);
                }
              }
            catch { }

            return lis;
        }
        
             public static List<Deals> getSnapdealGetOffers()
        {
            List<Deals> lis = new List<Deals>();
            WebClient wcSnapdeal = new WebClient();
            Product lisSnapdeal = new Product();
            wcSnapdeal.Headers.Add("Snapdeal-Affiliate-Id", "//Your Affiliate Id//");
            wcSnapdeal.Headers.Add("Snapdeal-Token-Id", "//Your Affiliate Token//");
            var SnapdealJson = wcSnapdeal.DownloadString("http://affiliate-feeds.snapdeal.com/feed/api/dod/offer");
            try
            {
                JObject jObject = (JObject)JsonConvert.DeserializeObject(SnapdealJson);
                var jobject = jObject["products"];
                var length = jObject["products"].Count();

                for (int i = 0; i < length; i++)
                {
                    Product p = new Product();
                    var o = jobject[i]["id"].ToString();
                    p = getSnapdealProductById(o);

                    Deals d = new Deals();
                    d.id = o;
                    d.title = p.title;
                    d.imgurl_default = p.imgsrc;
                    d.price = p.price.ToString();
                    d.url = p.url;
                    d.category = p.productType;
                    d.imgsrc = d.imgurl_default;
                    d.website = p.website;
                    lis.Add(d);
                    d.Equals(null);

                }
            }
            catch { }

            return lis;




     

        }
    }
}
