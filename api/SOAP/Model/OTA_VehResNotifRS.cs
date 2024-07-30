using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace api.SOAP.Model;

[XmlRoot(ElementName = "OTA_VehResNotifRS", Namespace = SOAPConstants.OTA_Namespace)]
public class OTA_VehResNotifRS : SOAPResponseEnvelope
{

    public OTA_VehResNotifRS()
    {
        ns.Add(string.Empty, "http://www.opentravel.org/OTA/2003/05");

    }

    [XmlAttribute(AttributeName = "TimeStamp")]
    public string? TimeStamp { get; set; }

    [XmlAttribute(AttributeName = "Target")]
    public string? Target { get; set; }

    [XmlAttribute(AttributeName = "Version")]
    public string? Version { get; set; }

    [XmlElement(ElementName = "Success")]
    public Success? Success { get; set; }

    [XmlArray(ElementName = "Errors")]
    [XmlArrayItem(ElementName = "Error")]

    public List<Error>? Errors { get; set;}
}

public class Success
{
}

public class Error
{

    [XmlAttribute(AttributeName = "Type")]
    public string?  Type { get; set; }

    [XmlAttribute(AttributeName = "ShortText")]
    public string? ShortText { get; set; }

    [XmlAttribute(AttributeName = "Code")]
    public string? Code { get; set; }

    [XmlAttribute(AttributeName = "Status")]

    public string? Status { get; set; }

    [XmlAttribute(AttributeName = "RecordID")]

    public string? RecordID { get; set; }

}

