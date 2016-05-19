using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using CreepedOut;

namespace CreepedOut.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using CreepedOut;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<CreepyThingImage>("CreepyThingImages");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CreepyThingImagesController : ODataController
    {
        private CreepedOutEntities db = new CreepedOutEntities();

        // GET: odata/CreepyThingImages
        [EnableQuery]
        public IQueryable<CreepyThingImage> GetCreepyThingImages()
        {
            return db.CreepyThingImages;
        }

        // GET: odata/CreepyThingImages(5)
        [EnableQuery]
        public SingleResult<CreepyThingImage> GetCreepyThingImage([FromODataUri] int key)
        {
            return SingleResult.Create(db.CreepyThingImages.Where(creepyThingImage => creepyThingImage.CreepyThingImageID == key));
        }

        // PUT: odata/CreepyThingImages(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<CreepyThingImage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreepyThingImage creepyThingImage = db.CreepyThingImages.Find(key);
            if (creepyThingImage == null)
            {
                return NotFound();
            }

            patch.Put(creepyThingImage);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreepyThingImageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(creepyThingImage);
        }

        // POST: odata/CreepyThingImages
        public IHttpActionResult Post(CreepyThingImage creepyThingImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CreepyThingImages.Add(creepyThingImage);
            db.SaveChanges();

            return Created(creepyThingImage);
        }

        // PATCH: odata/CreepyThingImages(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<CreepyThingImage> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreepyThingImage creepyThingImage = db.CreepyThingImages.Find(key);
            if (creepyThingImage == null)
            {
                return NotFound();
            }

            patch.Patch(creepyThingImage);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreepyThingImageExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(creepyThingImage);
        }

        // DELETE: odata/CreepyThingImages(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            CreepyThingImage creepyThingImage = db.CreepyThingImages.Find(key);
            if (creepyThingImage == null)
            {
                return NotFound();
            }

            db.CreepyThingImages.Remove(creepyThingImage);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CreepyThingImageExists(int key)
        {
            return db.CreepyThingImages.Count(e => e.CreepyThingImageID == key) > 0;
        }
    }
}
