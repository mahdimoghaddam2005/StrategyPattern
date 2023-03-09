namespace StrategyPattern.Domain.Services;
public interface ISmsSender
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string Receptor { get; set; }
    string Send(string receptor,string message);
}