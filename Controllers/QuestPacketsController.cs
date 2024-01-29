using api_wov.Models.Entities;
using api_wov.Models.QuestPackets;
using api_wov.Repository.QuestPackets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_wov.Controllers
{
    [Route("api/questpackets")]
    [ApiController]
    public class QuestPacketsController : ControllerBase
    {
        private readonly IQuestPacketRepository<QuestPacket, OutputQuestPacket, Guid> _questPacketRepository;
        public QuestPacketsController(IQuestPacketRepository<QuestPacket, OutputQuestPacket, Guid> questPacketRepository)
        {
            _questPacketRepository = questPacketRepository;
        }

        //GET: /api/questpackets/{questId}
        [HttpGet("{QuestId}")]
        public async Task<IActionResult> GetPacketByQuestId(Guid QuestId)
        {
            return Ok(await _questPacketRepository.GetPacketByQuestId(QuestId));
        }
    }
}
