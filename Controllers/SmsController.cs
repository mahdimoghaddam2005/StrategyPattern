using Microsoft.AspNetCore.Mvc;
using StrategyPattern.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using StrategyPattern.Infrastructure;
using StrategyPattern.Entities;

namespace StrategyPattern.Controllers;

[ApiController]
[Route("[controller]")]
public class SmsController : ControllerBase
{
  // Get Service and Context
  private SmsContext context;
  private readonly SenderStrategy strategy;

  public SmsController(SenderStrategy strategy,SmsContext context)
  {
    this.context = context;
    this.strategy = strategy;
  }

  [HttpPost("[action]/{iSend}",Name = nameof(Send))]
  public async Task<IActionResult> Send([FromRoute] string iSend)
  {
    // Check Route For Existing Service
    var sender = strategy.Resolve(service: iSend);
    if (sender is null)
    {
        return BadRequest("This Service is Not Exist !");      
    }
    // Get Headers From Request For Use Message and Receptor Parameters
    var service = HttpContext.Request.Headers.ToList();
    if (
        string.IsNullOrEmpty
          (service.FirstOrDefault(k => k.Key == "Message").Value.ToString()) ||
        string.IsNullOrEmpty
        ( service.FirstOrDefault(k => k.Key == "Receptor").Value.ToString())
       )
        return BadRequest("The Message or Receptor was not Recived !");
    // Run Send Method For Sms and Get Status
    var send =sender
    .Send
    (
        service!.FirstOrDefault(h => h.Key == "Receptor").Value.ToString(),
        message : service!.FirstOrDefault(h => h.Key == "Message").Value.ToString()
    );
    // Convert Json to a Model For Access Propeties
    SmsModel? smsModel = JsonConvert.DeserializeObject<SmsModel>(send);
  // Add To DataBase
  context.ShortMessageServices.Add(new ShortMessageService()
  {
    Message = smsModel!.Message,
    Receptor = smsModel.Receptor,
    DateTime = DateTimeOffset.Now
  });
  await context.SaveChangesAsync();
  // Return 200
    return Ok($"The Message Has Been Sent to {smsModel.Receptor} With Message : {smsModel.Message} and By {smsModel.Sender}");
  }

  [HttpGet("[action]",Name = nameof(Get))]
  public async Task<IActionResult> Get()
  {
    // Get All of Smss;
    return Ok(await context.ShortMessageServices.ToListAsync());
  }
}
// Defined Model For Access Sms
internal class SmsModel
{
  public string Sender { get; set; }
  public string Receptor { get; set; }
  public string Message { get; set; }
}
