using api.SOAP.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.SOAP.Controllers;

[ApiController]
[Route("[controller]")]

public abstract class SOAPControllerBase : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IWebHostEnvironment _env;

    protected SOAPVersion SOAPVersion { get; init;  }

    protected SOAPControllerBase(ILogger logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;

        SOAPControllerAttributes? soapversionattributes = Attribute.GetCustomAttribute(GetType(), typeof(SOAPControllerAttributes)) as SOAPControllerAttributes;
        if (soapversionattributes is null)
            throw new Exception("SOAPControllerAttributes not found");
         else
            SOAPVersion = soapversionattributes.SOAPVersion;
            if (SOAPVersion == SOAPVersion.v1_1)
            {
                _logger.LogInformation("SOAP Version 1.1");
            }
            else if (SOAPVersion == SOAPVersion.v1_2)
            {
               throw new Exception("SOAP Version 1.2 not supported");
            }
            else
            {
                throw new Exception("Invalid SOAP Version");
            }
        
    }
    
    public virtual SOAPResponseEnvelope CreateSOAPResponseEnvelope(string Action, string Message, string recordID )
    {
        OTA_VehResNotifRS Response = new OTA_VehResNotifRS
            {
                TimeStamp = "2019-01-15T21:05:47.088-08:00",
                Target = "Production",
                Version = "1.0",
            };
        
        if (Action == "Success")
        {
            Response.Errors = null;
            Response.Success = new Success();
        } 
        else 
        {
            Response.Success = null;
            Response.Errors = new List<Error>
            {
                new Error
                {
                    Type = Action,
                    ShortText = Message,
                    Code = "500",
                    Status = "ERROR",
                    RecordID = recordID
                }
            };
        }
        
        return new OTA_VehResNotifRS_Envelope(Response);
    }
}