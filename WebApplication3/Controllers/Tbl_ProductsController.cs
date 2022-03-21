using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class Tbl_ProductsController : Controller
    {
        private dbReadNLearnEntities db = new dbReadNLearnEntities();

        // GET: Tbl_Products
        public ActionResult Index()
        {
            var tbl_Products = db.Tbl_Products.Include(t => t.Tbl_Categories);
            return View(tbl_Products.ToList());
        }

        // GET: Tbl_Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Products tbl_Products = db.Tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Products);
        }

        // GET: Tbl_Products/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.Tbl_Categories, "category_id", "category_name");
            return View();
        }

        // POST: Tbl_Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "product_id,product_name,category_id,product_price,product_qty,product_image")] Tbl_Products tbl_Products)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Products.Add(tbl_Products);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.Tbl_Categories, "category_id", "category_name", tbl_Products.category_id);
            return View(tbl_Products);
        }


        // add w/ img start

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.category_id = new SelectList(db.Tbl_Categories, "category_id", "category_name");
            return View();
        }

        [HttpPost]
        public ActionResult Add(Tbl_Products productModel)
        {
            //if (ModelState.IsValid)
            //{
                string fileName = Path.GetFileNameWithoutExtension(productModel.ImageFile.FileName);
                string extension = Path.GetExtension(productModel.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                productModel.product_image = "~/ProductImg/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/ProductImg/"), fileName);
                productModel.ImageFile.SaveAs(fileName);
                using (dbReadNLearnEntities db = new dbReadNLearnEntities())
                {
                    db.Tbl_Products.Add(productModel);
                    db.SaveChanges();
                //return RedirectToAction("Index");
            }
            //}
            ViewBag.category_id = new SelectList(db.Tbl_Categories, "category_id", "category_name", productModel.category_id);
            ModelState.Clear();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult ViewDetails(int id)
        {
            Tbl_Products productModel = new Tbl_Products();

            using (dbReadNLearnEntities db = new dbReadNLearnEntities())
            {
                productModel = db.Tbl_Products.Where(x => x.product_id == id).FirstOrDefault();
            }

            return View(productModel);
        }


        // add w/ img end



        // GET: Tbl_Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Products tbl_Products = db.Tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.Tbl_Categories, "category_id", "category_name", tbl_Products.category_id);
            return View(tbl_Products);
        }

        // POST: Tbl_Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "product_id,product_name,category_id,product_price,product_qty,product_image,ImageFile")] Tbl_Products tbl_Products)
        {

            //if (ModelState.IsValid)
            //{
                string fileName = Path.GetFileNameWithoutExtension(tbl_Products.ImageFile.FileName);
                string extension = Path.GetExtension(tbl_Products.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                tbl_Products.product_image = "~/ProductImg/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/ProductImg/"), fileName);
                tbl_Products.ImageFile.SaveAs(fileName);
                using (dbReadNLearnEntities db = new dbReadNLearnEntities())
                {
                    db.Entry(tbl_Products).State = EntityState.Modified;
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                //db.Entry(tbl_Products).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            //}
            ModelState.Clear();
            ViewBag.category_id = new SelectList(db.Tbl_Categories, "category_id", "category_name", tbl_Products.category_id);
            return RedirectToAction("Index");


        }

        // GET: Tbl_Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_Products tbl_Products = db.Tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Products);
        }

        // POST: Tbl_Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tbl_Products tbl_Products = db.Tbl_Products.Find(id);
            db.Tbl_Products.Remove(tbl_Products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
