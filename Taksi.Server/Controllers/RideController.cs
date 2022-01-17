﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taksi.DTO.DTOs;
using Taksi.Server.BLL.Services.Interfaces;
using Taksi.Server.DAL.Entities;

namespace Taksi.Server.Controllers
{
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IRideService _service;

        public RideController(IRideService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("/rides/create-ride")]
        public async Task<RideDto> CreateRide(
            [FromQuery] Guid clientId,
            [FromQuery] double startX,
            [FromQuery] double startY,
            [FromQuery] double endX,
            [FromQuery] double endY)
        {
            var rideEntity = new RideEntity(
                new List<Point2dEntity>
                {
                    new Point2dEntity(startX, startY),
                    new Point2dEntity(endX, endY)
                },
                clientId);
            await _service.RegisterRide(rideEntity);

            return new RideDto(
                rideEntity.Id,
                rideEntity.Path.Select(p => p.GetDto()).ToList(),
                rideEntity.Status,
                rideEntity.AssignedClient,
                rideEntity.AssignedDriver);
        }

        [HttpGet]
        [Route("/rides/get-ride-for-client")]
        public async Task<IActionResult> FindRidesForClient([FromQuery] Guid clientId)
        {
            // TODO: Позже думаю можно сделать этот метод просто Find и искать поездки по любым заданным параметрам
            if (clientId != Guid.Empty)
            {
                var result = await _service.GetAllForClient(clientId);
                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound();
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }

        [HttpPatch]
        [Route("/rides/assign-driver")]
        public async Task<IActionResult> AssignDriver([FromQuery] Guid rideId, [FromQuery] Guid driverId)
        {
            if (rideId != Guid.Empty && driverId != Guid.Empty)
            {
                await _service.AssignDriver(rideId, driverId);
                return Ok(await _service.FindOneRide(rideId));
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        
        [HttpPatch]
        [Route("/rides/wait-for-client")]
        public async Task<IActionResult> WaitForClient([FromQuery] Guid rideId)
        {
            if (rideId != Guid.Empty)
            {
                await _service.WaitForClient(rideId);
                return Ok(await _service.FindOneRide(rideId));
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        
        [HttpPatch]
        [Route("/rides/start-ride")]
        public async Task<IActionResult> StartRide([FromQuery] Guid rideId)
        {
            if (rideId != Guid.Empty)
            {
                await _service.StartRide(rideId);
                return Ok(await _service.FindOneRide(rideId));
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        }
        
        [HttpPatch]
        [Route("/rides/end-ride")]
        public async Task<IActionResult> EndRide([FromQuery] Guid rideId)
        {
            if (rideId != Guid.Empty)
            {
                await _service.EndRide(rideId);
                return Ok(await _service.FindOneRide(rideId));
            }

            return StatusCode((int) HttpStatusCode.BadRequest);
        } 
    }
}