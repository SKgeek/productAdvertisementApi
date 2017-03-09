using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using AmazonProductAdvtApi;
using System.Xml;
using System.Data;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.IO;
using Demo123.Models;

namespace ClassesForAll
{
    class AmazonDATA
    {
       static WebClient WCAmazon = new WebClient();
   public static List<Product> getAmazonDatabykeyword(string key)
        {
            SignedRequestHelper s = new SignedRequestHelper("//Your Associate Key Id //",
                                                     "//Your Associate Secret Key //",
                                                     "webservices.amazon.in");
            IDictionary<string, string> r1 = new Dictionary<string, string>();
            r1["Service"] = "AWSECommerceService";
            r1["Operation"] = "ItemSearch";
            r1["AssociateTag"] = "//Your Associate Tag ";
            r1["SearchIndex"] = "All";
            r1["Keywords"] = key;
            r1["ResponseGroup"] = "Images,ItemAttributes,Offers";
            r1["Version"] = "2011-08-01";
            r1["ItemPage"] = "1";
            string signedUrl = s.Sign(r1);
          
            string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";

            WebRequest request = HttpWebRequest.Create(signedUrl);
            WebResponse response = request.GetResponse();

            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());
            List<Product> lisAmazon = new List<Product>();
           
            for (int i = 0; i < 10; i++)
            {
                Product p = new Product();
                try
                {
                    XmlNode idNode = doc.GetElementsByTagName("ASIN", NAMESPACE).Item(i);
                    XmlNode productTypeNode = doc.GetElementsByTagName("ProductGroup", NAMESPACE).Item(i);
                    XmlNode titleNode = doc.GetElementsByTagName("Title", NAMESPACE).Item(i);
                    XmlNode ImageUrlNode = doc.GetElementsByTagName("ImageSets", NAMESPACE).Item(i);
                    XmlNode priceNodeRaw = doc.GetElementsByTagName("LowestNewPrice", NAMESPACE).Item(i);
                    XmlNode PRICENODE = priceNodeRaw.LastChild;

                    XmlNode urlNode = doc.GetElementsByTagName("DetailPageURL", NAMESPACE).Item(i);
                    XmlNode ImageNode = ImageUrlNode.FirstChild.LastChild.FirstChild;

                    XmlNode priceNode = priceNodeRaw.FirstChild;
                    var id = idNode.InnerText;
                    p.id = id;

                    var title = titleNode.InnerText;
                    p.title = title;

                    var imgsrc = ImageNode.InnerText;
                    p.imgsrc = imgsrc;

                    var price = PRICENODE.InnerText;
                    p.price = price;

                    var producttype = productTypeNode.InnerText;
                    p.productType = producttype;

                    var website = "Amazon";
                    p.website = website;

                    var url = urlNode.InnerText;
                    p.url = url;

                    //p.id = idNode.InnerText;
                    //p.title = titleNode.InnerText;
                    //p.imgsrc = ImageNode.InnerText;
                    //p.price =Convert.ToInt32( PRICENODE.InnerText.Replace("INR","").Trim());
                    //p.productType = productTypeNode.InnerText;
                    //p.website = "Amazon";
                    //p.url = urlNode.InnerText;

                    lisAmazon.Add(p);
                    p.Equals(null);
                }
                catch { }
            }
            return lisAmazon;
        }



        public static Product getAmazonDataById(string id)
        {
           
            


            Product p = new Product();

            SignedRequestHelper s = new SignedRequestHelper("//Your Associate Key Id //",
                                                      "//Your Associate Secret Key //",
                                                      "webservices.amazon.in");
            IDictionary<string, string> r1 = new Dictionary<string, string>();
            r1["Service"] = "AWSECommerceService";

            r1["Operation"] = "ItemLookup";
            r1["AssociateTag"] = "//Your Associate Tag ";

            r1["ItemId"] = id;
            r1["IdType"] = "ASIN";
            r1["ResponseGroup"] = "Images,ItemAttributes";
            r1["Condition"] = "All";
            r1["Version"] = "2011-08-01";


      

            string signedUrl = s.Sign(r1);
            WebRequest request = HttpWebRequest.Create(signedUrl);
            WebResponse response = request.GetResponse();

            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());


            string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";


            XmlNode productTypeNode = doc.GetElementsByTagName("ProductGroup", NAMESPACE)[0];
            XmlNode titleNode = doc.GetElementsByTagName("Title", NAMESPACE)[0];
            XmlNode ImageUrlNode = doc.GetElementsByTagName("ImageSets", NAMESPACE)[0];
            XmlNode urlNode = doc.GetElementsByTagName("DetailPageURL", NAMESPACE)[0];
            XmlNode ImageNode = ImageUrlNode.FirstChild.LastChild.FirstChild;

            p.url = urlNode.InnerText;
            p.imgsrc = ImageNode.InnerText;
            p.title = titleNode.InnerText;
            p.productType = productTypeNode.InnerText;


            p.price = "N/A";
            p.website = "Amazon";


            response.Close();







         
            return p;
        }

        public static List<Deals> getAmazonOffers() {
            string NAMESPACE = "http://webservices.amazon.com/AWSECommerceService/2011-08-01";


            SignedRequestHelper s = new SignedRequestHelper("//Your Associate Key Id //",
                                                     "//Your Associate Secret Key //",
                                                     "webservices.amazon.in");
            IDictionary<string, string> r1 = new Dictionary<string, string>();
            r1["Service"] = "AWSECommerceService";
           
            r1["Operation"] = "ItemSearch";
            r1["AssociateTag"] = "//Your Associate Tag ";
           
            r1["SearchIndex"] = "All";
           
            r1["ResponseGroup"] = "Images,ItemAttributes,Offers,PromotionSummary";
            r1["Keywords"] = " ";
            r1["Version"] = "2011-08-01";



            // http://webservices.amazon.in/onca/xml?Service=AWSECommerceService


            //   AssociateTag = buyhatk - 21 &
            //Product p = new Product();

            string signedUrl = s.Sign(r1);
            WebRequest request = HttpWebRequest.Create(signedUrl);
            WebResponse response = request.GetResponse();

            XmlDocument doc = new XmlDocument();
            doc.Load(response.GetResponseStream());
            var nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("app", "http://webservices.amazon.com/AWSECommerceService/2011-08-01");
            List<Deals> lisAmazonOffer = new List<Deals>();
            for (int i = 0; i < 9; i++)

            {
                
                    Deals p = new Deals();

                    XmlNode idNode = doc.GetElementsByTagName("ASIN", NAMESPACE).Item(i);
                    XmlNode productTypeNode = doc.GetElementsByTagName("ProductGroup", NAMESPACE).Item(i);
                    XmlNode titleNode = doc.GetElementsByTagName("Title", NAMESPACE).Item(i);
                XmlNode ImageUrlNode = doc.GetElementsByTagName("ImageSets", NAMESPACE).Item(i);
                XmlNode priceNodeRaw = doc.GetElementsByTagName("LowestNewPrice", NAMESPACE).Item(i);
                    XmlNode PRICENODE = priceNodeRaw.LastChild;

                    XmlNode urlNode = doc.GetElementsByTagName("DetailPageURL", NAMESPACE).Item(i);
                XmlNode ImageNode = ImageUrlNode.FirstChild.LastChild.FirstChild;

                XmlNode priceNode = priceNodeRaw.FirstChild;
                p.id = idNode.InnerText;
                    p.title = titleNode.InnerText;
                    p.url = urlNode.InnerText;
                    p.imgurl_default = ImageNode.InnerText;
                    p.price = PRICENODE.InnerText;
                    p.category = productTypeNode.InnerText;
                    p.website = "Amazon";
                p.imgsrc = p.imgurl_default;

                lisAmazonOffer.Add(p);
                    p.Equals(null);
               

               
            }


            return lisAmazonOffer;

            }
    }
}
