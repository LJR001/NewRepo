using APIAgendaMongo.Models;
using APIAgendaMongo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace APIAgendaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : ControllerBase
    {
        private readonly AdressServices _adressService;
       // private readonly ClientServices _clientService;

        public AdressController(AdressServices adressServices, ClientServices clientServices) 
        {
            _adressService = adressServices;
         //   _clientService = clientServices;
        } 

        [HttpGet]
        public ActionResult<List<Adress>> Get() => _adressService.Get();

        [HttpGet("{Id:length(24)}", Name = "GetAdress")]
        public ActionResult<Adress> Get(string id)
        {
            var adress = _adressService.Get(id);
            if (adress == null) return NotFound();

         //   var client = _clientService.Get();
         
       //     adress.Client = client.FirstOrDefault(x=>x.Adress.Id==id); 
            
            return Ok(adress);
        }

        [HttpPost]
        public ActionResult<Adress> Post(Adress adress)
        {
            _adressService.Create(adress);
            return CreatedAtRoute("GetAdress", new { id = adress.Id.ToString() }, adress);
        }

        [HttpPut]
        public ActionResult<Adress> Put(Adress adressIn, string id)
        {
            var adress = _adressService.Get(id);
            if (adress == null) return NotFound("Não encontrado");

            adressIn.Id = id;
            _adressService.Update(adress.Id, adressIn);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult<Adress> Delete(string id)
        {
            Adress adress = _adressService.Get(id);
            if (adress == null) return NotFound();
            _adressService.Remove(adress);
            return NoContent();
        }
    }
}
