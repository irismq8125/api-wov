using api_wov.Models.Entities;
using api_wov.Models.QuestPackets;
using api_wov.Models.Quests;
using api_wov.Repository.QuestPackets;
using api_wov.Repository.Quests;
using api_wov.Services;
using api_wov.Services.RequestResults;
using Microsoft.AspNetCore.Mvc;

namespace api_wov.Controllers
{
    [Route("api/quests")]
    [ApiController]
    public class QuestsController : ControllerBase
    {
        private readonly IQuestsRepository<Quest, OutputQuest, Guid> _questRepository;
        private readonly IQuestPacketRepository<QuestPacket, OutputQuestPacket, Guid> _questPacketRepository;

        public QuestsController(IQuestsRepository<Quest, OutputQuest, Guid> questRepository, 
                                IQuestPacketRepository<QuestPacket, OutputQuestPacket, Guid> questPacketRepository)
        {
            _questRepository = questRepository;
            _questPacketRepository = questPacketRepository;
        }

        //GET: /api/quests
        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] RequestResult request)
        {
            return Ok(await _questRepository.WoVListAllAsync(request));
        }

        //GET: /api/quests/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(Guid id)
        {
            return Ok(await _questRepository.WoVGetByIdAsync(id));
        }

        //POST: /api/quests
        [HttpPost("")]
        public async Task<IActionResult> Create(InputQuest input)
        {
            var item = new Quest();
            item.Id = Guid.NewGuid();
            item.Name = input.Name;
            item.ShortDescription = input.ShortDescription;
            item.Url = input.Url;
            item.Status = input.Status;

            if(input.Packet is not null)
            {
                if(input.Packet == "all") 
                {
                    _questPacketRepository.AddAll(item.Id.ToString());
                }
                else
                {
                    _questPacketRepository.AddQuestPacket(item.Id.ToString(), input.Packet);
                }
            }

            return Ok(await _questRepository.WoVAddAsync(item));
        }

        //PUT: /api/quests/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, InputQuest input)
        {
            var item = await _questRepository.WoVGetByIdAsync(id);
            item.Name = input.Name;
            item.ShortDescription = input.ShortDescription;
            item.Url = input.Url;
            item.Status = input.Status;
            return Ok(await _questRepository.WoVUpdateAsync(item));
        }

        //DELETE: /api/quests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _questRepository.WoVDeleteAsync(id));
        }
    }
}
