using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CORE.API.Controllers
{
    [Route("api/validation")]
    [ApiController]
    public class ValidationController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private IMapper _mapper;
        public ValidationController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repository = repositoryWrapper;

            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var owners = _repository.Validation.GetAll();

                return Ok(owners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var owner = _repository.Validation.FindByCondition(x=>x.ID==id).FirstOrDefault();
                Core.Validations.Engine.Processor processor = new Core.Validations.Engine.Processor(_repository);
                     var result =processor.Validate(null, null, "1", "1");
                
                if (owner == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(owner);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}