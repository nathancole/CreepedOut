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
    builder.EntitySet<CreepyThing>("CreepyThings");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CreepyThingsController : ODataController
    {
        private CreepedOutEntities db = new CreepedOutEntities();

        // GET: odata/CreepyThings
        [EnableQuery]
        public IQueryable<CreepyThing> GetCreepyThings()
        {
            return db.CreepyThings;
        }

        // GET: odata/CreepyThings(5)
        [EnableQuery]
        public SingleResult<CreepyThing> GetCreepyThing([FromODataUri] int key)
        {
            return SingleResult.Create(db.CreepyThings.Where(creepyThing => creepyThing.CreepyThingID == key));
        }

        //// PUT: odata/CreepyThings(5)
        //public IHttpActionResult Put([FromODataUri] int key, Delta<CreepyThing> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    CreepyThing creepyThing = db.CreepyThings.Find(key);
        //    if (creepyThing == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Put(creepyThing);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CreepyThingExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(creepyThing);
        //}

        //// POST: odata/CreepyThings
        //public IHttpActionResult Post(CreepyThing creepyThing)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.CreepyThings.Add(creepyThing);
        //    db.SaveChanges();

        //    return Created(creepyThing);
        //}

        //// PATCH: odata/CreepyThings(5)
        //[AcceptVerbs("PATCH", "MERGE")]
        //public IHttpActionResult Patch([FromODataUri] int key, Delta<CreepyThing> patch)
        //{
        //    Validate(patch.GetEntity());

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    CreepyThing creepyThing = db.CreepyThings.Find(key);
        //    if (creepyThing == null)
        //    {
        //        return NotFound();
        //    }

        //    patch.Patch(creepyThing);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CreepyThingExists(key))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Updated(creepyThing);
        //}

        //// DELETE: odata/CreepyThings(5)
        //public IHttpActionResult Delete([FromODataUri] int key)
        //{
        //    CreepyThing creepyThing = db.CreepyThings.Find(key);
        //    if (creepyThing == null)
        //    {
        //        return NotFound();
        //    }

        //    db.CreepyThings.Remove(creepyThing);
        //    db.SaveChanges();

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CreepyThingExists(int key)
        {
            return db.CreepyThings.Count(e => e.CreepyThingID == key) > 0;
        }
    }
}
