using System.Xml.Serialization;
namespace api.SOAP.Model;

[XmlRoot(ElementName = "Body")]
public partial class SOAPRequestBody
{
    public SOAPRequestBody()
    {
        OTA_VehResNotifRQ = new OTA_VehResNotifRQ();
    }

    [XmlElement(ElementName = "OTA_VehResNotiRQ", Namespace = "http://www.opentravel.org/OTA/")]
    public OTA_VehResNotifRQ OTA_VehResNotifRQ { get; set; }
}
