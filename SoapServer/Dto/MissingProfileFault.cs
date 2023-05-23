using System.Runtime.Serialization;

namespace SoapServer.Dto;

[DataContract]
public class MissingProfileFault
{
    [DataMember] public string Message { get; set; }

    public MissingProfileFault(string message)
    {
        Message = message;
    }
}