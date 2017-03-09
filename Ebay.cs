using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;

using Demo123.Models;

namespace ClassesForAll
{
    class Ebay
    {
        public static List<Product> getEbayDataByKeyword(string key)
        {
            WebClient WCEbay = new WebClient();

            List<Product> lisEbay = new List<Product>();
            string NAMESPACE = "http://www.ebay.com/marketplace/search/v1/services";
            string EbayXml = WCEbay.DownloadString("http://svcs.ebay.com/services/search"
            + "/FindingService/v1?REST-PAYLOAD&OPERATION-NAME=findItemsByKeywords"
            + "&GLOBAL-ID=EBAY-IN&SERVICE-VERSION=1.12.0"
            //+ "&affiliate.networkId=9"
            //+ "&affiliate.trackingId=5337955844"

            + "&SECURITY-APPNAME=//Your Security App Name//"
            + "&RESPONSE-DATA-FORMAT=XML&paginationInput.entriesPerPage=10&keywords=" + key);

         //   Console.WriteLine("Ebay dATA SUCCEED");

            XmlDocument xmlEbay = new XmlDocument();
            xmlEbay.LoadXml(EbayXml);
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    Product p = new Product();
                    XmlNode idNode = xmlEbay.GetElementsByTagName("itemId", NAMESPACE).Item(i);
                    XmlNode productTypeNode = xmlEbay.GetElementsByTagName("categoryName", NAMESPACE).Item(i);
                    XmlNode titleNode = xmlEbay.GetElementsByTagName("title", NAMESPACE).Item(i);
                    XmlNode ImageUrlNode = xmlEbay.GetElementsByTagName("galleryURL", NAMESPACE).Item(i);
                    XmlNode priceNodeRaw = xmlEbay.GetElementsByTagName("convertedCurrentPrice", NAMESPACE).Item(i);
                    XmlNode urlNode = xmlEbay.GetElementsByTagName("viewItemURL", NAMESPACE).Item(i);

                    p.id = idNode.InnerText;
                    p.productType = productTypeNode.InnerText;
                    p.title = titleNode.InnerText;
                    p.imgsrc = ImageUrlNode.InnerText;
                    p.price = priceNodeRaw.InnerText;
                    p.url = urlNode.InnerText;
                    p.website = "ebay";
                    lisEbay.Add(p);
                    p.Equals(null);
                }
                catch { }
               


            }

            return lisEbay;

        }

        


        //public static Product getEbayDataByID(string id)
        //{
        //    WebClient WCEbay = new WebClient();

        //    List<Product> lisEbay = new List<Product>();
        //    string NAMESPACE = "http://www.ebay.com/marketplace/search/v1/services";
        //    string EbayXml = WCEbay.DownloadString("http://svcs.ebay.com/services/search/FindingService/v1?"
        //        + "OPERATIONOPERATION-NAME=findItemsByProduct&SERVICE-VERSION=1.0.0&"
        //        + "SECURITY-APPNAME=//Your Security App Name//&"
        //        + "RESPONSE-DATA-FORMAT=XML&"
        //        + "REST-PAYLOAD&"
        //        + "paginationInput.entriesPerPage=2&"
        //        + "productId.@type=ReferenceID&"
        //        + "productId=53039031"
        //        + "&affiliate.networkId=9"
        //        + "&GLOBAL-ID=EBAY-IN&SERVICE-VERSION=1.12.0"
        //        + "&affiliate.trackingId="+id);



        // //   Console.WriteLine("Ebay dATA SUCCEED");

        //    XmlDocument xmlEbay = new XmlDocument();
        //    xmlEbay.LoadXml(EbayXml);
        //    for (int i = 0; i < 10; i++)
        //    {
        //        Product p = new Product();
        //        XmlNode idNode = xmlEbay.GetElementsByTagName("itemId", NAMESPACE).Item(i);
        //        XmlNode productTypeNode = xmlEbay.GetElementsByTagName("categoryName", NAMESPACE).Item(i);
        //        XmlNode titleNode = xmlEbay.GetElementsByTagName("title", NAMESPACE).Item(i);
        //        XmlNode ImageUrlNode = xmlEbay.GetElementsByTagName("galleryURL", NAMESPACE).Item(i);
        //        XmlNode priceNodeRaw = xmlEbay.GetElementsByTagName("convertedCurrentPrice", NAMESPACE).Item(i);
        //        XmlNode urlNode = xmlEbay.GetElementsByTagName("viewItemURL", NAMESPACE).Item(i);

        //        p.id = idNode.InnerText;
        //        p.productType = productTypeNode.InnerText;
        //        p.title = titleNode.InnerText;
        //        p.imgsrc = ImageUrlNode.InnerText;
        //        p.price = priceNodeRaw.InnerText;
        //        p.url = urlNode.InnerText;
        //        p.website = "ebay";
        //        lisEbay.Add(p);
        //        p.Equals(null);


        //    }

        //    return lisEbay[0];

        }









    }

