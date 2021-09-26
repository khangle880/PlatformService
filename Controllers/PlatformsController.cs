using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting platforms...");

            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetAllPlatforms")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var item = _repository.GetPlatformById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(item));
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto){
            var platform = _mapper.Map<Platform>(platformCreateDto);

            _repository.CreatePlatform(platform);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);

            return CreatedAtAction(nameof(GetPlatformById), new {Id = platformReadDto.Id}, platform);
        }
    }
}