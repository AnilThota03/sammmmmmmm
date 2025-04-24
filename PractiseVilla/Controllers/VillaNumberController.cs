using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PractiseVilla.Models;
using PractiseVilla.Models.Dtos;
using PractiseVilla.Repository.IRepository;
using System.Net;

namespace PractiseVilla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly IVillaNumberRepository _dbVillaNumber;

        private readonly IMapper _mapper;

        protected APIResponse _response;

        public VillaNumberController(IVillaNumberRepository villaNumberRepository, IMapper mapper)
        {
            _dbVillaNumber = villaNumberRepository;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillas()
        {
            try
            {
                IEnumerable<VillaNumber> villas = await _dbVillaNumber.GetAll();
                _response.Result = _mapper.Map<List<VillaNumberDto>>(villas);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string> { ex.ToString() };
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            return _response;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetVillas(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var villa = await _dbVillaNumber.GetOne(v => v.VillaNo == id);
                if (villa == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<VillaNumberDto>(villa);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
            }

            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return _response;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaNumberCreateDto villaDto)
        {

            try
            {
                if (villaDto == null)
                {
                    return BadRequest("Invalid villa data.");
                }

                var check = await _dbVillaNumber.GetOne(v => v.VillaNo == villaDto.VillaNo);

                if (check != null)
                {
                    return BadRequest("Villa Already Exist");
                }

                var villa = _mapper.Map<VillaNumber>(villaDto);

                await _dbVillaNumber.Create(villa);
                _response.Result = villa;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            return _response;

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla([FromBody] VillaNumberUpdateDto updateDto, int id)
        {
            try
            {
                if (id != updateDto.VillaNo)
                {
                    return BadRequest("Url id and update data id is not same");
                }
                var check = await _dbVillaNumber.GetOne(v => v.VillaNo == updateDto.VillaNo);
                if (check == null)
                {
                    return BadRequest("There is not data to update");
                }
                else
                {


                    VillaNumber updatingVilla = _mapper.Map<VillaNumber>(updateDto);

                    await _dbVillaNumber.Update(updatingVilla);

                    _response.Result = updatingVilla;
                    _response.IsSuccess = true;
                    _response.StatusCode = HttpStatusCode.OK;

                }
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }
            return _response;

        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
        {
            try
            {
                var chek = await _dbVillaNumber.GetOne(v => v.VillaNo == id);


                if (chek != null)
                {
                    await _dbVillaNumber.Delete(chek);
                    _response.StatusCode = HttpStatusCode.NoContent;
                    _response.IsSuccess = true;

                }
                else
                {
                    return BadRequest("Data is not there");
                }


            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string> { ex.Message };
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            return _response;
        }
    }
}
