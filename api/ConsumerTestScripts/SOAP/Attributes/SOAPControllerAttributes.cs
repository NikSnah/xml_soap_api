using Microsoft.AspNetCore.Mvc;
using api.SOAP.Model;

namespace api.SOAP;

public class SOAPControllerAttributes : ProducesAttribute 
{
    public SOAPVersion SOAPVersion { get; }

    public SOAPControllerAttributes(SOAPVersion soapVersion) : base(System.Net.Mime.MediaTypeNames.Text.Xml)
    {
        SOAPVersion = soapVersion;
    }
}

