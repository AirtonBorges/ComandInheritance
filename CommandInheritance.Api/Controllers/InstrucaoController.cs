using CommandInheritanceApi.Infra;
using Microsoft.AspNetCore.Mvc;

namespace CommandInheritanceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstrucaoController : ControllerBase
    {
        private readonly ILogger<InstrucaoController> _logger;
        private readonly IKafkaProducer _producer;

        public InstrucaoController(ILogger<InstrucaoController> logger, IKafkaProducer producer)
        {
            _logger = logger;
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string pMensagem)
        {
            if (!ModelState.IsValid) return RedirectToAction();
            try
            {
                await _producer.PublicarInstrucao(pMensagem);
            }
            catch (Exception xException)
            {
                _logger.LogError("{Message}", xException);
                return RedirectToAction();
            }

            return Ok();
        }
    }
}