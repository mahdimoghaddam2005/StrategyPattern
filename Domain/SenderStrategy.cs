using StrategyPattern.Domain.Services;
namespace StrategyPattern.Domain;
public class SenderStrategy
{
    private readonly List<ISmsSender> senders;
    public SenderStrategy(List<ISmsSender> sender)
    {
        this.senders = sender;
    }

    public ISmsSender Resolve(string service)
    {
        _= service ?? throw new ArgumentNullException("Null Service String");
        foreach (var sender in senders)
        {
            if(sender.GetType().Name == service)
            return sender;
        }
        return null;
    }
}