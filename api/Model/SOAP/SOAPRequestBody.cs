using System.Xml.Serialization;
namespace api.SOAP.Model;

[XmlType(Namespace = SOAPRequestBody.DefaultNamespace)]

public partial class SOAPRequestBody {
    public const string DefaultNamespacePrefix = "SOAP-ENV";
    public const string DefaultNamespace =  "http://schemas.xmlsoap.org/soap/envelope/";
    
}