using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;

namespace USPSSharp.Models
{
    public class AddressVerificationResponse : Address
    {
		/// <summary>
		/// Prevent instantiation. Objects of this type must be created through the parse static method.
		/// </summary>
		private AddressVerificationResponse() {}

		/// <summary>
		/// 
		/// </summary>
		public string Error { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CityAbbreviation { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string DeliveryPoint { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CarrierRoute { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool? Footnotes { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool? DPVConfirmation { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool? DPVCMRA { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string DPVFootnotes { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool? Business { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool? CentralDeliveryPoint { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public bool? Vacant { get; set; }

		/// <summary>
		/// Parses an Address Verification API response.
		/// </summary>
		/// <param name="xmlResponse">The raw XML string returned by the API.</param>
		/// <returns>List of verified addresses with additional fields in the response.</returns>
		public static List<AddressVerificationResponse> Parse(string xmlResponse)
		{
			List<AddressVerificationResponse> addressVerificationResponses = new List<AddressVerificationResponse>();

			XElement addressVerificationResponseElement = XElement.Parse(xmlResponse);

			return (from addressElement in addressVerificationResponseElement.Elements("Address")
					select new AddressVerificationResponse
					{
						Id = Int32.Parse(addressElement.Attribute("Id")?.Value),
						Address1 = addressElement.Element("Address1")?.Value,
						Address2 = addressElement.Element("Address1")?.Value,
						Business = Boolean.Parse(addressElement.Element("Business")?.Value),
						CarrierRoute = addressElement.Element("CarrierRoute")?.Value,
						CentralDeliveryPoint = Boolean.Parse(addressElement.Element("CentralDeliveryPoint")?.Value),
						City = addressElement.Element("City")?.Value,
						CityAbbreviation = addressElement.Element("CityAbbreviation")?.Value,
						DeliveryPoint = addressElement.Element("DeliveryPoint")?.Value,
						DPVCMRA = Boolean.Parse(addressElement.Element("DPVCMRA")?.Value),
						DPVConfirmation = Boolean.Parse(addressElement.Element("DPVConfirmation")?.Value),
						DPVFootnotes = addressElement.Element("DPVFootnotes")?.Value,
						Error = addressElement.Element("Error")?.Value,
						FirmName = addressElement.Element("FirmName")?.Value,
						Footnotes = Boolean.Parse(addressElement.Element("Footnotes")?.Value),
						State = addressElement.Element("State")?.Value,
						Urbanization = addressElement.Element("Urbanization")?.Value,
						Vacant = Boolean.Parse(addressElement.Element("Vacant")?.Value),
						Zip4 = addressElement.Element("Zip4")?.Value,
						Zip5 = addressElement.Element("Zip5")?.Value,
					}).ToList();

		}
	}
}
