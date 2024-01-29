using api_wov.Models.Entities;
using api_wov.Models.RegisterQuests;
using api_wov.Repository.RegisterQuests;
using api_wov.Services.RequestResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_wov.Controllers
{
    [Route("api/registers")]
    [ApiController]
    public class RegisterQuestsController : ControllerBase
    {
        private readonly IRegisterQuestsRepository<RegisterQuest, OutputRegisterQuest, Guid> _registerQuestsRepository;
        public RegisterQuestsController(IRegisterQuestsRepository<RegisterQuest, OutputRegisterQuest, Guid> registerQuestsRepository)
        {
            _registerQuestsRepository = registerQuestsRepository;
        }

        //GET: /api/registers
        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] RequestResult request)
        {
            return Ok(await _registerQuestsRepository.WoVListAllAsync(request));
        }


        //POST: /api/registers
        [HttpPost("")]
        public async Task<IActionResult> Create(InputRegisterQuest input)
        {
            var item = new RegisterQuest();
            item.Name = input.Name;
            item.Links = JsonConvert.SerializeObject(new Links { Facebook = input.Facebook, Instagram = input.Instagram });
            item.Quests = JsonConvert.SerializeObject(input.Quests);
            item.Totals = JsonConvert.SerializeObject(new Totals
            {
                Gold = input.TotalGold,
                Gem = input.TotalGem,
                Money = input.TotalMoney,
                Exp = input.TotalExp,
            });
            item.Payments = JsonConvert.SerializeObject(new Payments
            {
                Name = input.Payments,
                PaymentId = Guid.NewGuid(),
                Status = "active"
            });

            return Ok(await _registerQuestsRepository.WoVAddAsync(item));
        }
    }
}
