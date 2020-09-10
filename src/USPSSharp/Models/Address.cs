using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace USPSSharp.Models
{
    public class Address
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirmName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Urbanization { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Zip5 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Zip4 { get; set; }

        public override string ToString()
        {
            XmlWriterSettings settings = new XmlWriterSettings {
                OmitXmlDeclaration = true
            };
            StringBuilder sb = new StringBuilder();
            XmlWriter xmlWriter = XmlWriter.Create(sb, settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Address"); // <Address ID=X>
            xmlWriter.WriteAttributeString("ID", Id.ToString());

            xmlWriter.WriteStartElement("FirmName");
            xmlWriter.WriteString(FirmName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Address1");
            xmlWriter.WriteString(Address1);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Address2");
            xmlWriter.WriteString(Address2);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("City");
            xmlWriter.WriteString(City);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("State");
            xmlWriter.WriteString(State);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Urbanization");
            xmlWriter.WriteString(Urbanization);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Zip5");
            xmlWriter.WriteString(Zip5);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Zip4");
            xmlWriter.WriteString(Zip4);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement(); // </Address>
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            return sb.ToString();
        }

    }
}
