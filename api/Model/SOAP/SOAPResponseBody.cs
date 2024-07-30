using System.Xml.Serialization;
namespace api.SOAP.Model;

[XmlType(Namespace = SOAPResponseBody.DefaultNamespace)]

public partial class SOAPResponseBody {
    public const string DefaultNamespacePrefix = "ser";
    public const string DefaultNamespace =  "http://some.com/service/";

}
