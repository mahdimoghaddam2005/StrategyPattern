using Kavenegar;
namespace StrategyPattern.Domain.Services;
public class KavehnegarSender : ISmsSender
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string Receptor { get; set; }
    public string Send(string receptor,string message)
    {
        // Send Sms With Kavenegar Service
        new Kavenegar.KavenegarApi("384C4C484F34465741356F59435A36537966584B642F714C524D51774B53534C6977483332536E7A5735303D").Send(message : message , receptor : receptor , sender : "1000596446");
        return $$"""
        {
            "Sender" : "{{this.GetType().Name}}",
            "Status" : "Success",
            "Receptor" : "{{receptor}}",
            "Message" : "{{message}}"
        }
        """;
    }
}