using ASAP.Assets.Core;
using ASAP.Assets.Service;

using System.Web.Http;

namespace ASAP.Assets.WebApp.Controllers
{
    public class AssetsController : ApiController
    {
        private readonly AssetsService _service;

        public AssetsController()
        {

            _service = new AssetsService();
        }

        [HttpGet]
        public IHttpActionResult GetAssets()
        {
            var res = _service.GetAll();
            return Ok(res);
        }

        [HttpGet]
        public IHttpActionResult GetAsset(int id)
        {
            var res = _service.GetById(id);
            return Ok(res);
        }

        [HttpPost]
        public IHttpActionResult PostAsset(Asset asset)
        {
            var res = _service.Insert(asset);
            return Ok(res);
        }

        [HttpPut]
        public IHttpActionResult PutAsset(int id, Asset asset)
        {
            var res = _service.Update(asset);
            return Ok(res);
        }

        [HttpDelete]
        public IHttpActionResult PutAsset(int id)
        {
            var res = _service.Delete(id);
            return Ok(res);
        }
    }
}
