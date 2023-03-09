namespace StrategyPattern.Entities;
public class ShortMessageService
{
    public int Id { get; set; }
    public string Message { get; set; }
    public string Receptor { get; set; }
    public DateTimeOffset DateTime { get; set; }
}