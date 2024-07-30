using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;
namespace api.SOAP.Model;




[XmlRoot("Envelope", Namespace = SOAPConstants.SOAP1_1Namespace)]

public partial class OTA_VehResNotifRS_Envelope : SOAPResponseEnvelope
{
    public OTA_VehResNotifRS_Envelope()
    {
        ns.Add(SOAPConstants.DefaultSOAPEnvelopeNamespacePrefix, SOAPConstants.SOAP1_1Namespace);
    }

    public OTA_VehResNotifRS_Envelope(OTA_VehResNotifRS response) : this()
    {
        _body = new OTA_VehResNotifRS_Body(response);
    }

    [NotNull]
    [XmlElement("Body")]

    public OTA_VehResNotifRS_Body? Body_Typed
    {
        get
        {
            if (_body is null)
            {
                _body = (OTA_VehResNotifRS_Body)CreateBody();
            }
            return (OTA_VehResNotifRS_Body)_body;
        }
        set { 
            throw new NotImplementedException();
        }
    }


}


[XmlRoot("Envelope", Namespace = SOAPConstants.SOAP1_1Namespace)]

public partial class SOAP1_1ResponseEnvelope : SOAPResponseEnvelope
{
    public SOAP1_1ResponseEnvelope()
    {
        ns.Add(SOAPConstants.DefaultSOAPEnvelopeNamespacePrefix, SOAPConstants.SOAP1_1Namespace);
    }
    [NotNull]
    [XmlElement("Body")]

    public SOAP1_1ResponseBody? Body_Typed
    {
        get
        {
            if (_body is null)
            {
                _body = (SOAP1_1ResponseBody)CreateBody();
            }
            return (SOAP1_1ResponseBody)_body;
        }
        set { 
            throw new NotImplementedException();
        }
    }


}

public abstract partial class SOAPResponseEnvelope 
{

    [XmlNamespaceDeclarations]
    public XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

    protected SOAPResponseBody? _body;

    [NotNull]
    [XmlIgnore]
    public SOAPResponseBody? Body
    {
        get
        {
            if (_body is null)
            {
                _body = CreateBody();
            }
            return _body;
        }
        set { _body = value; }
    }

    protected virtual SOAPResponseBody CreateBody() =>  new SOAP1_1ResponseBody();

}