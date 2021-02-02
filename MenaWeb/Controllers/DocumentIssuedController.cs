using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MenaWeb.DataServices;
using MenaWeb.Models;
using MenaWeb.Models.Entities;
using MenaWeb.SecurityServices;
using MenaWeb.ViewModels;
using MenaWeb.ViewOptions;
using Microsoft.AspNetCore.Html;

namespace MenaWeb.Controllers
{
    
    public class DocumentIssuedController : Controller
    {
        private readonly DatabaseContext db;
        private readonly DocumentIssuedService dataService;

        public DocumentIssuedController(DocumentIssuedService dataService, DatabaseContext db)
        {
            this.db = db;
            this.dataService = dataService;
        }

        public IActionResult Index(PageOptions pageOptions)
        {
            return View(dataService.GetViewModel(pageOptions));
        }

        public IActionResult Create()
        {
            ViewBag.Action = "Create";
            return View("DocumentIssue", dataService.GetDocumentIssuerView(new DocumentIssued()));
        }

        [HttpPost]
        public IActionResult Create(DocumentIssued documentIssue)
        {
            if (documentIssue == null)
                return NotFound();
            if(ModelState.IsValid)
            {
                dataService.Create(documentIssue);
                return RedirectToAction("Index");
            }
            return View("DocumentIssue", dataService.GetDocumentIssuerView(documentIssue));
        }

        public IActionResult Edit(int? idDocumentIssue)
        {
            ViewBag.Action = "Edit";
            if (idDocumentIssue == null)
                return NotFound();
            var docIssued = db.DocumentIssueds.FirstOrDefault(c => c.IdDocumentIssued == idDocumentIssue);
            if (docIssued == null)
                return NotFound();
            return View("DocumentIssue", dataService.GetDocumentIssuerView(docIssued));

        }

        [HttpPost]
        public IActionResult Edit(DocumentIssued documentIssue)
        {
           
            if (documentIssue == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                dataService.Edit(documentIssue);
                return RedirectToAction("Index");
            }
            return View("DocumentIssue", dataService.GetDocumentIssuerView(documentIssue));

        }
        [HttpGet]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? idDocumentIssue)
        {
            ViewBag.Action = "Delete";
            if (idDocumentIssue == null)
                return NotFound();
            var documentIssue = db.DocumentIssueds.FirstOrDefault(c=> c.IdDocumentIssued == idDocumentIssue);
            if (documentIssue == null)
                return NotFound();
            return View("DocumentIssue", dataService.GetDocumentIssuerView(documentIssue));
        }
        [HttpPost]
        public IActionResult Delete(DocumentIssued documentIssue)
        {
            if (documentIssue == null)
                return NotFound();

            var dependentRecords = db.People.FirstOrDefault(c => c.IdDocumentIssued == documentIssue.IdDocumentIssued);
            if (dependentRecords == null)
            {
                var docIssueDel = db.DocumentIssueds.FirstOrDefault(c => c.IdDocumentIssued == documentIssue.IdDocumentIssued);
                if (docIssueDel != null)
                {
                    dataService.Delete(docIssueDel);
                    return RedirectToAction("Index");
                }
            }

            ViewData["Controller"] = "DocumentIssued";
            ViewData["TextError"] = "Нельзя удалить орган, так как есть зависимые записи. Сначала удалите их!";
            return View("Error");
        }
    }
}
