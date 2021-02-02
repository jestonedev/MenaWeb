using MenaWeb.Models;
using MenaWeb.Models.Entities;
using MenaWeb.ViewModels;
using MenaWeb.ViewOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MenaWeb.DataServices
{
    public class DocumentIssuedService
    {
        private readonly DatabaseContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DocumentIssuedService( DatabaseContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public DocumentIssuedVM<DocumentIssued> GetViewModel(PageOptions pageOptions)
        {
            var viewModel = new DocumentIssuedVM<DocumentIssued>
            {
                PageOptions = pageOptions ?? new PageOptions()
            };
            var query = db.DocumentIssueds.AsQueryable();
            viewModel.PageOptions.TotalRows = query.Count();
            var count = query.Count();
            viewModel.PageOptions.Rows = count;
            viewModel.PageOptions.TotalPages = Math.Max((int)Math.Ceiling(count / (double)viewModel.PageOptions.SizePage), 1);

            if (viewModel.PageOptions.TotalPages < viewModel.PageOptions.CurrentPage)
                viewModel.PageOptions.CurrentPage = 1;
            viewModel.DocumentIssueds = GetQueryPage(query, viewModel.PageOptions).ToList();
            return viewModel;
        }

        public List<DocumentIssued> GetQueryPage(IQueryable<DocumentIssued> query, PageOptions pageOptions)
        {
            var result = query;
            result = result.Skip((pageOptions.CurrentPage - 1) * pageOptions.SizePage);
            result = result.Take(pageOptions.SizePage);
            return result.ToList();
        }

        public DocumentIssuedVM<DocumentIssued> GetDocumentIssuerView(DocumentIssued documentIssuer)
        {
            var docIssuerVM = new DocumentIssuedVM<DocumentIssued>()
            {
                DocumentIssue = documentIssuer
            };
            return docIssuerVM;
        }

        internal void Create(DocumentIssued documentIssuer)
        {
            db.DocumentIssueds.Add(documentIssuer);
            db.SaveChanges();
        }

        internal void Edit(DocumentIssued documentIssuer)
        {
            db.DocumentIssueds.Update(documentIssuer);
            db.SaveChanges();
        }

        internal void Delete(DocumentIssued documentIssuer)
        {
            db.DocumentIssueds.Remove(documentIssuer);
            db.SaveChanges();
        }
        
    }
}
