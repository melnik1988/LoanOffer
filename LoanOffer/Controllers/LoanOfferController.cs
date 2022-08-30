using LoanOffer.Moduls.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LoanOffer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanOfferController : ControllerBase
    {
        //private IFile fileService;
        private IConfiguration configuration;
        public LoanOfferController(IConfiguration _configuration)
        {
            configuration = _configuration;
        }


        [HttpGet("{value}")]
        public async Task<string> GetLoans(string value)
        {
            var _loans = JsonConvert.DeserializeObject<LoanApplication>(value);

            try
            {
                // Parallel
                var tasks = new List<Task>();
                foreach (var s in subs)
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        await GetGiphys(apiKey, giphys_json, s);
                    }));
                }
                await Task.WhenAll(tasks);

            }
            catch (Exception ex)
            {
                throw new Exception("Exception in SearchGiphy", ex);
            }

            return JsonConvert.SerializeObject(giphys_json);
        }
    }
}
