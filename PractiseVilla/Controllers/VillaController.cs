using Microsoft.AspNetCore.Mvc;
using PractiseVilla.Data;
using PractiseVilla.Models.Dtos;
using PractiseVilla.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using PractiseVilla.Repository.IRepository;
using System.Net;

[Route("api/Villa")]
[ApiController]
public class VillaController : ControllerBase
{
    //private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    private readonly IVillaRepository _dbVilla;
    protected APIResponse _response;
    public VillaController(IVillaRepository dbVilla, IMapper mapper)
    {
        _dbVilla = dbVilla;
        _mapper = mapper;
        this._response = new APIResponse();
    }

    [HttpGet]
    public async Task<ActionResult<APIResponse>> GetVillas()
    {
        try
        {
            IEnumerable<Villa> villas = await _dbVilla.GetAll();
            _response.Result = _mapper.Map<List<VillaDto>>(villas);
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


        //if (villaList == null || villaList.Count == 0)
        //{
        //    return NotFound();
        //}

        //var dtoList = villaList.Select(v => new VillaDto
        //{
        //    Id = v.Id,
        //    Name = v.Name,
        //    rating = v.rating,
        //    price = v.price
        //});



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

            var villa = await _dbVilla.GetOne(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _response.Result = _mapper.Map<VillaDto>(villa);
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
        }

        catch(Exception ex)
        {
            _response.ErrorMessages = new List<string> { ex.Message};
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
        }
        return _response;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]

    public async Task<ActionResult<APIResponse>> CreateVilla([FromBody] VillaCreateDto villaDto)
    {

        try
        {
            if (villaDto == null || string.IsNullOrEmpty(villaDto.Name))
            {
                return BadRequest("Invalid villa data.");
            }

            var check = await _dbVilla.GetOne(v => v.Name.ToLower() == villaDto.Name.ToLower());

            if (check != null)
            {
                return BadRequest("Villa Already Exist");
            }

            var villa = _mapper.Map<Villa>(villaDto);

            await _dbVilla.Create(villa);
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
    public async  Task<ActionResult<APIResponse>> UpdateVilla([FromBody] VillaUpdateDto updateDto, int id)
    {
        try
        {
            if (id != updateDto.Id)
            {
                return BadRequest("Url id and update data id is not same");
            }
            var check = await _dbVilla.GetOne(v => v.Id == updateDto.Id);
            if (check == null)
            {
                return BadRequest("There is not data to update");
            }
            else
            {


                Villa updatingVilla = _mapper.Map<Villa>(updateDto);

                await _dbVilla.Update(updatingVilla);

                _response.Result = updatingVilla;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;

            }
        }
        catch(Exception ex)
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
            var chek = await _dbVilla.GetOne(v => v.Id == id);


            if (chek != null)
            {
                await _dbVilla.Delete(chek);
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
