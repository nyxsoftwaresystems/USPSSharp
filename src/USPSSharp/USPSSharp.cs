using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using USPSSharp.Models;

namespace USPSSharp
{
    public class USPSSharp
    {
        /// <summary>
        /// 
        /// </summary>
        private const string BASE_API_URL = "https://secure.shippingapis.com";

        /// <summary>
        /// 
        /// </summary>
        private const string ADDRESS_API_PATH = "ShippingAPI.dll";

        /// <summary>
        /// 
        /// </summary>
        private const string TRACKING_API_PATH = "";

        /// <summary>
        /// The API key used to access the USPS API.
        /// </summary>
        private readonly string _userId;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        public USPSSharp(string UserID)
        {
            _userId = UserID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addresses"></param>
        /// <returns></returns>
        public async Task<List<AddressVerificationResponse>> VerifyAddress(List<Address> addresses)
        {
            //The USPS address verification API accepts a maximum of 5 addresses for verification and a minimum of 1.
            if(addresses.Count == 0 || addresses.Count > 5)
            {
                throw new Exception("A maximum of 5 addresses is allowed and a minimum of 1 is required");
            }

            var client = new HttpClient(); //Our HTTP client.
            var request = new HttpRequestMessage(); //Our Request Message to use with our client.

            //We will loop through each address and assign an ID to each address object.
            StringBuilder sb = new StringBuilder();
            int id = 0;
            foreach(Address a in addresses)
            {
                a.Id = id++;
                sb.Append(a.ToString());
            }

            //Obtain the formatted URI.
            request.RequestUri = FormatAPIUri(ADDRESS_API_PATH, "Verify", "AddressValidateRequest", "<Revision>1</Revision>" + sb.ToString());

            //Execute the HTTP request with our URI.
            HttpResponseMessage response = await client.SendAsync(request);

            //Validate the response code. Throw exception if it is anything other than HttpStatusCode.OK
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var rawResponseString = await response.Content.ReadAsStringAsync();
                return AddressVerificationResponse.Parse(rawResponseString);                
            }
            else
            {
                throw new Exception("Received status code " + response.StatusCode.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiPath"></param>
        /// <param name="apiName"></param>
        /// <param name="rootAPIObject"></param>
        /// <param name="XMLbody"></param>
        /// <returns></returns>
        private Uri FormatAPIUri(string apiPath, string apiName, string rootAPIObject, string XMLbody)
        {
            //Format the final XML
            StringBuilder finalXML = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(finalXML);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement(rootAPIObject);
            xmlWriter.WriteAttributeString("UserID", _userId);
            finalXML.Append(XMLbody);
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            //Now create the final Uri and return.
            StringBuilder sb = new StringBuilder();
            return new Uri(sb.Append($"{BASE_API_URL}/{apiPath}?API={apiName}&XML=").Append(finalXML.ToString()).ToString());
        }
    }
}
