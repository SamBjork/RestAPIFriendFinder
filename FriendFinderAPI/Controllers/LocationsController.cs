using AutoMapper;
using Castle.Core.Internal;
using FriendFinderAPI.Dtos;
using FriendFinderAPI.FilterAuthentication;
using FriendFinderAPI.Models;
using FriendFinderAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendFinderAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationsController(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }


        //-----------------------------------------------------------------------------
        // GetLocations
        //-----------------------------------------------------------------------------					
        [HttpGet(Name = "GetLocations")]
        public async Task<ActionResult<LocationDto[]>> GetLocations()
        {
            try
            {
                var results = await _locationRepository.GetLocations();
                var mappedResults = _mapper.Map<LocationDto[]>(results);

                if (mappedResults.IsNullOrEmpty())
                {
                    return NotFound();
                }

                for (int i = 0; i < mappedResults.Length; i++)
                {
                    mappedResults[i].Links = CreateLinksForLocations(mappedResults[i]);
                }

                return Ok(mappedResults);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {exception.Message}");
            }
        }
        #region Old Version

        // //GET:      api/v1.0/locations
        // [HttpGet(Name ="GetLocations")]
        // public async Task<ActionResult<LocationDto[]>> GetLocations()
        // {
        //     try
        //     {
        //         var results = await _locationRepository.GetLocations();
        //         var mappedResults = _mapper.Map<LocationDto[]>(results);
        //          for(int i = 0; i<mappedResults.Length;i++)
        //         {
        //             mappedResults[i].Links = CreateLinksGetAllLocations(mappedResults[i]);
        //         };
        //         return Ok(mappedResults);
        //     }
        //     catch(Exception e)
        //     {
        //         return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
        //     }
        // }

        #endregion




        //-----------------------------------------------------------------------------
        // GetLocation
        //-----------------------------------------------------------------------------							
        [HttpGet("{locationId}", Name = "GetLocation")]
        public async Task<ActionResult<LocationDto>> GetLocation(int locationId)
        {
            try
            {
                var result = await _locationRepository.GetLocation(locationId);
                var mappedResult = _mapper.Map<LocationDto>(result);

                if (mappedResult == null)
                {
                    return NotFound();
                }

                mappedResult.Links = CreateLinksForLocations(mappedResult);

                return Ok(mappedResult);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {exception.Message}");
            }
        }
        #region Old Version
        //GET:      api/v1.0/locations/n
        // [HttpGet("{id}", Name ="GetLocation")]
        // public async Task<ActionResult<LocationDto>> GetLocation(int id)
        // {
        //     try
        //     {
        //         var result = await _locationRepository.GetLocation(id);
        //         
        //         if(result == null)
        //             return NotFound();
        //         
        //         var mappedResult = _mapper.Map<LocationDto>(result);
        //         mappedResult.Links = CreateLinksGetAllLocations(mappedResult);
        //         return Ok(mappedResult);            
        //         }
        //     catch(Exception e)
        //     {
        //         return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
        //     }
        // }
        #endregion





        //-----------------------------------------------------------------------------
        // GetLocationByHobby
        //-----------------------------------------------------------------------------							
        [HttpGet("searchhobby", Name = "GetLocationsByHobby")]
        public async Task<ActionResult<LocationDto[]>> GetLocationsByHobby(string hobbyName)
        {
            try
            {
                var results = await _locationRepository.GetLocationsByHobby(hobbyName);
                var mappedResults = _mapper.Map<LocationDto[]>(results);

                if (mappedResults.IsNullOrEmpty())
                {
                    return NotFound();
                }

                for (int i = 0; i < mappedResults.Length; i++)
                {
                    mappedResults[i].Links = CreateLinksForLocations(mappedResults[i]);
                }

                return Ok(mappedResults);
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {exception.Message}");
            }
        }
        #region Old
        // [HttpGet("hobby/{id}", Name ="GetLocationsByHobby")]
        // public async Task<ActionResult<LocationDto[]>> GetLocationsByHobby(int id)
        // {
        //     try
        //     {
        //         var results = await _locationRepository.GetLocationsByHobby(id);
        //         var mappedResults = _mapper.Map<LocationDto[]>(results);
        //          for(int i = 0; i<mappedResults.Length;i++)
        //         {
        //             mappedResults[i].Links = CreateLinksGetAllLocations(mappedResults[i]);
        //         };
        //         return Ok(mappedResults);            }
        //     catch(Exception e)
        //     {
        //         return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
        //     }
        // }
        #endregion







        //-----------------------------------------------------------------------------
        // PostLocation
        //-----------------------------------------------------------------------------							
        //POST:      api/v1.0/locations
        [HttpPost(Name = "PostLocation")]
        public async Task<ActionResult<LocationDto>> PostLocation(LocationDto locationDto)
        {
            try
            {
                var mappedEntity = _mapper.Map<Location>(locationDto);
                _locationRepository.Add(mappedEntity);

                if (await _locationRepository.Save())
                    return Created($"/api/v1.0/cities/{mappedEntity.LocationId}", _mapper.Map<LocationDto>(mappedEntity));
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }




        //-----------------------------------------------------------------------------
        // PutLocation
        //-----------------------------------------------------------------------------							
        //PUT:      api/v1.0/locations/n
        [HttpPut("{locationId}", Name = "PutLocation")]
        public async Task<ActionResult<LocationDto>> PutLocation(int locationId, LocationDto locationDto)
        {
            try
            {
                var oldLocation = await _locationRepository.GetLocation(locationId);
                if (oldLocation == null)
                    return NotFound($"We could not find a location with that id: {locationId}");

                var newLocation = _mapper.Map(locationDto, oldLocation);
                _locationRepository.Update(newLocation);

                if (await _locationRepository.Save())
                    return NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }





        //-----------------------------------------------------------------------------
        // DeleteLocation
        //-----------------------------------------------------------------------------							
        //DELETE:       api/v1.0/locations/n
        [HttpDelete("{locationId}", Name = "DeleteLocation")]
        public async Task<ActionResult> DeleteLocation(int locationId)
        {
            try
            {
                var location = await _locationRepository.GetLocation(locationId);
                if (location == null)
                    return NotFound($"We could not find a location with that id: {locationId}");

                _locationRepository.Delete(location);
                if (await _locationRepository.Save())
                    return NoContent();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
            }
            return BadRequest();
        }
        
        private IEnumerable<LinkDto> CreateLinksForLocations(LocationDto locationDto)
        {
            var links = new[]
            {
                new LinkDto
                {
                    Href = Url.Link("GetLocation", new {locationId = locationDto.LocationId}).ToLower(),
                    Rel = "self",
                    Method = "GET"
                },
                new LinkDto
                {
                    Href = Url.Link("PutLocation", new {locationId = locationDto.LocationId}).ToLower(),
                    Rel = "update location",
                    Method = "PUT"
                },
                new LinkDto
                {
                    Href = Url.Link("DeleteLocation", new {locationId = locationDto.LocationId}).ToLower(),
                    Rel = "delete location",
                    Method = "DELETE"
                }
            };
            return links;
        }        
    }
}