using System;
using System.Web.Http;
using IFCurrenciesApi.Managers;
using IFCurrenciesApi.Services;

namespace IFCurrenciesApi.Controllers
{
    public class BanksController : ApiController
    {
        private readonly IBankService _bankService;

        public BanksController(IBankService bankService)
        {
            _bankService = bankService;
        }

        [HttpGet]
        public IHttpActionResult GetAllBanks()
        {
            var banks = _bankService.GetAllBanksWithLatestCurrencyRates();

            if (banks == null)
            {
                return NotFound();
            }

            return Ok(banks);
        }

        [HttpGet]
        [Route("api/banks/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var bank = _bankService.GetBankWithRelatedEntities(id);

            if (bank == null)
            {
                return NotFound();
            }

            return Ok(bank);
        }

        [HttpGet]
        [Route("api/banks/updaterates")]
        public IHttpActionResult UpdateRates()
        {
            var externalService = new ApiService();

            var manager = new FinanceManager(_bankService, externalService);

            try
            {
                manager.UpdateCurrencyRates();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok("Rates Updated");
        }

    }
}