using System.Xml.Serialization;
namespace api.SOAP.Model;

[XmlRoot(ElementName = "Envelope", Namespace = SOAPConstants.SOAP1_1Namespace)]

public partial class SOAP1_1RequestEnvelope : SOAPRequestEnvelope
{
    public SOAP1_1RequestEnvelope()
    {
        Body = new SOAPRequestBody();
    }
    
    [XmlElement(ElementName = "Body")]
    public SOAPRequestBody Body { get; set; }
}

public partial class SOAPRequestEnvelope
{
    
}