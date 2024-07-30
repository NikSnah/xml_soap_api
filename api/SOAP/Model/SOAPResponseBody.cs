using System.Xml.Serialization;

namespace api.SOAP.Model;


public partial class OTA_VehResNotifRS_Body : SOAPResponseBody
{

    [XmlElement(ElementName = "OTA_VehResNotiRS", Namespace = SOAPConstants.OTA_Namespace )]
    public OTA_VehResNotifRS OTA_VehResNotifRS { get; set; }

    public OTA_VehResNotifRS_Body()
    {
       // OTA_VehResNotifRS = new OTA_VehResNotifRS();
    }

    public OTA_VehResNotifRS_Body(OTA_VehResNotifRS response)
    {
        OTA_VehResNotifRS = response;
    }
}

public partial class SOAP1_1ResponseBody : SOAPResponseBody
{
}

[XmlInclude(typeof(SOAP1_1ResponseBody))]
[XmlInclude(typeof(OTA_VehResNotifRS_Body))]

public partial class SOAPResponseBody
{

}