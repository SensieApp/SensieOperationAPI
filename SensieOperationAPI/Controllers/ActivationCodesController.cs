using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SensieOperationAPI.Models;
using System;

namespace SensieOperationAPI.Controllers
{
    public class ActivationCodesController : ApiController
    {
        private OperationContext db = new OperationContext();

        // GET: api/ActivationCodes
        //public IQueryable<ActivationCode> GetActivationCodes()
        //{
        //    return db.ActivationCodes;
        //}

        // GET: api/ActivationCodes/5
        [ResponseType(typeof(ActivationCode))]
        public async Task<IHttpActionResult> GetActivationCode(string acId, string email)
        {
            ActivationCode activationCode = await db.ActivationCodes.FirstOrDefaultAsync(a => a.Code == acId); //.FindAsync(acId);
            if (activationCode == null)
            {
                return Content(HttpStatusCode.NotFound, "Activation Code does not exist."); //NotFound();
            }

            User user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return Content(HttpStatusCode.NotFound, "User does not exist."); //NotFound();
            }

            var activationCodeId = activationCode.ActivationCodeId;
            var uId = user.UserId;

            Activation activation = await db.Activations.FirstOrDefaultAsync(a => a.ActivationCodeId == activationCodeId && a.UserId == uId);

            if (activation == null)
            {
                return Content(HttpStatusCode.NotFound, "Activation does not exist."); //NotFound();
            }

            var fechaExpiracion = activation.ExpirationDate;
            string status;
            var today = DateTime.Now;
            if (today <= fechaExpiracion)
                status = "Active";
            else
                status = "Expired";
            return Ok(status);
        }

        // PUT: api/ActivationCodes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivationCode(int id, ActivationCode activationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != activationCode.ActivationCodeId)
            {
                return BadRequest();
            }

            db.Entry(activationCode).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivationCodeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ActivationCodes
        [ResponseType(typeof(ActivationCode))]
        public async Task<IHttpActionResult> PostActivationCode(ActivationCode activationCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ActivationCodes.Add(activationCode);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = activationCode.ActivationCodeId }, activationCode);
        }

        // DELETE: api/ActivationCodes/5
        [ResponseType(typeof(ActivationCode))]
        public async Task<IHttpActionResult> DeleteActivationCode(int id)
        {
            ActivationCode activationCode = await db.ActivationCodes.FindAsync(id);
            if (activationCode == null)
            {
                return NotFound();
            }

            db.ActivationCodes.Remove(activationCode);
            await db.SaveChangesAsync();

            return Ok(activationCode);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivationCodeExists(int id)
        {
            return db.ActivationCodes.Count(e => e.ActivationCodeId == id) > 0;
        }
    }
}