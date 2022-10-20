using APIAgendaMongo.Models;
using APIAgendaMongo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace APIAgendaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientServices _clientService;
        private readonly AdressServices _adressService;

        public ClientController(ClientServices clientService, AdressServices adressService)
        {
            _clientService = clientService;
            _adressService = adressService;
        }

        [HttpGet]
        public ActionResult<List<Client>> Get() => _clientService.Get();


        [HttpGet("{Id:length(24)}", Name = "GetClient")]
        public ActionResult<Client> Get(string id)
        {
            var client = _clientService.Get(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public ActionResult<Client> Post(Client client)
        {
            Adress adress = _adressService.Create(client.Adress);

            _clientService.Create(client);
            return CreatedAtRoute("GetClient", new { id = client.Id.ToString() }, client);
        }

        [HttpPut]
        public ActionResult<Client> Put(Client clientIn, string id)
        {
            var client = _clientService.Get(id);
            if (client == null) return NotFound("Não encontrado");

            client.Id = id;
            _clientService.Update(client.Id, clientIn);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult<Client> Delete(string id)
        {
            Client client = _clientService.Get(id);
            if (client == null) return NotFound();
            _clientService.Remove(client);
            return NoContent();
        }
    }
}
