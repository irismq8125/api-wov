using api_wov.Models.Entities;
using api_wov.Models.Packets;
using api_wov.Models.Quests;
using api_wov.Repository;
using api_wov.Repository.Packets;
using api_wov.Services;
using api_wov.Services.RequestResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_wov.Controllers
{
    [Route("api/packets")]
    [ApiController]
    public class PacketsController : ControllerBase
    {
        private readonly IPacketsRepository<Packet, OutputPacket, Guid> _packetRepository;
        public PacketsController(IPacketsRepository<Packet, OutputPacket, Guid> packetRepository)
        {
            _packetRepository = packetRepository;
        }

        //GET: /api/packets
        [HttpGet("")]
        public async Task<IActionResult> GetAll([FromQuery] RequestResult request)
        {
            return Ok(await _packetRepository.WoVListAllAsync(request));
        }

        //GET: /api/packets/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(Guid id)
        {
            return Ok(await _packetRepository.WoVGetByIdAsync(id));
        }

        //POST: /api/packets
        [HttpPost("")]
        public async Task<IActionResult> Create(InputPacket input)
        {
            var item = new Packet();
            item.Name = input.Name;
            item.Gem = input.Gem;
            item.Gold = input.Gold;
            item.Money = input.Money;
            item.Exp = input.Exp;
            item.Status = input.Status;

            return Ok(await _packetRepository.WoVAddAsync(item));
        }

        //PUT: /api/packets/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, InputPacket input)
        {
            var item = await _packetRepository.WoVGetByIdAsync(id);
            item.Name = input.Name;
            item.Gem = input.Gem;
            item.Gold = input.Gold;
            item.Money = input.Money;
            item.Exp = input.Exp;
            item.Status = input.Status;

            return Ok(await _packetRepository.WoVUpdateAsync(item));
        }

        //DELETE: /api/packets/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await _packetRepository.WoVDeleteAsync(id));
        }
    }
}
