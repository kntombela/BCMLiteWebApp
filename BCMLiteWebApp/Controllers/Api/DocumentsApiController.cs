using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace BCMLiteWebApp.Controllers.Api
{
    [RoutePrefix("api/documents")]
    [Authorize]
    public class DocumentsApiController : ApiController
    {
        private BCMContext db = new BCMContext();

        // GET: api/processes/1/documents
        [Route("~/api/processes/{processId:int}/documents")]
        [ResponseType(typeof(DocumentViewModel))]
        public async Task<IHttpActionResult> GetSkills(int processId)
        {
            var documents = await GetDocumentsByProcess(processId);

            if (documents == null)
            {
                return NotFound();
            }

            return Ok(documents);
        }

        private async Task<List<DocumentViewModel>> GetDocumentsByProcess(int processId)
        {
            return await db.Documents.Where(d => d.ProcessID == processId)
                                                  .Select(d => new DocumentViewModel
                                                  {
                                                      DocumentID = d.DocumentID,        
                                                      Description = d.Description,
                                                      RTO = d.RTO,
                                                      ProcessID = d.ProcessID
                                                  }).ToListAsync();
        }
    }
}