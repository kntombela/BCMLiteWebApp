using BCMLiteWebApp.DAL;
using BCMLiteWebApp.Models;
using BCMLiteWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        // GET: api/documents/1/details
        [ResponseType(typeof(DocumentViewModel))]
        [Route("~/api/documents/{id:int}/details")]
        public async Task<IHttpActionResult> GetDocument(int id)
        {
            var documents = await GetDocumentById(id);

            if (documents == null)
            {
                return NotFound();
            }

            return Ok(documents);
        }

        // POST: api/documents    
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddEditDocument(Document document)
        {
            string status = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Edit or add depending on if id exists
            if (!DocumentExists(document.DocumentID))
            {
                db.Documents.Add(document);
                await db.SaveChangesAsync();
                status = "created";
            }
            else
            {
                db.Entry(document).State = EntityState.Modified;

                //When value is not specified for model DateTime property, the value defaults to 0001-01-01
                //which is outside of the range of SQL Server's DATETIME
                document.DateModified = DateTime.Now;

                try
                {
                    await db.SaveChangesAsync();
                    status = "updated";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.DocumentID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            string message = $"Document successfully { status }!";

            return Ok(new PostResponseViewModel { Ids = new List<int>() { document.DocumentID }, Message = message });
        }

        // DELETE: api/documents/delete
        [Route("delete")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> Delete(int[] ids)
        {
            foreach (int i in ids)
            {
                Document document = await db.Documents.FindAsync(i);
                if (document == null)
                {
                    return NotFound();
                }

                db.Documents.Remove(document);
            }
            await db.SaveChangesAsync();

            string message = "Documents deleted successfully!";

            return Ok(new PostResponseViewModel { Ids = null, Message = message });
        }

        // POST: api/documents    
        [Route("import")]
        [HttpPost]
        [ResponseType(typeof(PostResponseViewModel))]
        public async Task<IHttpActionResult> AddMultipleDocuments(List<Document> documentList)
        {
            var documentIds = new List<int>();
            string status = "";

            if (!ModelState.IsValid)
            {
                status = "unsuccessful";
                return BadRequest(ModelState);
            }

            foreach (var data in documentList)
            {
                db.Documents.Add(data);
            }
            await db.SaveChangesAsync();

            foreach (var data in documentList)
            {
                documentIds.Add(data.DocumentID);
            }

            status = "successful";

            string message = $"Document import { status }!";

            return Ok(new PostResponseViewModel { Ids = documentIds, Message = message });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Helpers
        private bool DocumentExists(int id)
        {
            return db.Documents.Count(s => s.DocumentID == id) > 0;
        }

        private async Task<List<DocumentViewModel>> GetDocumentsByProcess(int processId)
        {
            return await db.Documents.Where(s => s.ProcessID == processId)
                                                  .Select(s => new DocumentViewModel
                                                  {
                                                      DocumentID = s.DocumentID,
                                                      Description = s.Description,
                                                      RTO = s.RTO,
                                                      ProcessID = s.ProcessID,
                                                      DateModified = s.DateModified
                                                  }).ToListAsync();
        }

        private async Task<DocumentViewModel> GetDocumentById(int id)
        {
            return await db.Documents.Where(s => s.DocumentID == id)
                                                  .Select(s => new DocumentViewModel
                                                  {
                                                      DocumentID = s.DocumentID,
                                                      Description = s.Description,
                                                      RTO = s.RTO,
                                                      ProcessID = s.ProcessID,
                                                      DateModified = s.DateModified
                                                  }).FirstOrDefaultAsync();
        }

        #endregion
    }
}