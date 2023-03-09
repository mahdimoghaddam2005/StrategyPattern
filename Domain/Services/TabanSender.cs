namespace StrategyPattern.Domain.Services;
public class TabanSender : ISmsSender
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string Receptor { get; set; }
    public string Send(string receptor,string message)
    {
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