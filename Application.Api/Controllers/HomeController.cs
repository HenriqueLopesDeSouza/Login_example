using Application.Api.ViewModels;
using Application.Business.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.Api.Controllers
{
    [Route("api/teste")]
    public class HomeController : Controller
    {
        private readonly IUserService _personService;
        private readonly IMapper _mapper;

        public HomeController(IUserService personService,
                                      IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }


        
        [HttpGet]
        [Authorize]
        public  List<UserViewModel> Teste()
        {
             var lista = _mapper.Map<List<UserViewModel>>(_personService.Get());

            return lista;
        }
    }
}
