﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SOHU.Data.Enum;
using SOHU.Data.Helpers;
using SOHU.Data.Models;
using SOHU.Data.Repositories;

namespace SOHU.MVC.Controllers
{
    public class InvoicePaymentController : BaseController
    {
        private readonly IInvoicePaymentRepository _invoicePaymentResposistory;

        public InvoicePaymentController(IInvoicePaymentRepository invoicePaymentResposistory)
        {
            _invoicePaymentResposistory = invoicePaymentResposistory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(int ID)
        {
            var model = _invoicePaymentResposistory.GetByID(ID) ?? new InvoicePayment();
            return View(model);
        }

        public IActionResult GetAllToList()
        {
            return Json(_invoicePaymentResposistory.GetAllToList());
        }

        public IActionResult GetByID(int ID)
        {
            return Json(_invoicePaymentResposistory.GetByID(ID));
        }

        public IActionResult Create(InvoicePayment model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Insert, RequestUserID);
            int result = _invoicePaymentResposistory.Create(model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
            }
            else
            {
                note = AppGlobal.Success + " - " + AppGlobal.CreateFail;
            }
            return Json(note);
        }

        public IActionResult Update(InvoicePayment model)
        {
            string note = AppGlobal.InitString;
            model.Initialization(InitType.Update, RequestUserID);
            int result = _invoicePaymentResposistory.Update(model.Id, model);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
            }
            else
            {
                note = AppGlobal.Success + " - " + AppGlobal.EditFail;
            }
            return Json(note);
        }

        public IActionResult Delete(int ID)
        {
            string note = AppGlobal.InitString;
            int result = _invoicePaymentResposistory.Delete(ID);
            if (result > 0)
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteSuccess;
            }
            else
            {
                note = AppGlobal.Success + " - " + AppGlobal.DeleteFail;
            }
            return Json(note);
        }

        public IActionResult SaveChange(InvoicePayment model)
        {
            string note = AppGlobal.InitString;
            int result = 0;
            if (model.Id > 0)
            {
                model.Initialization(InitType.Update, RequestUserID);
                result = _invoicePaymentResposistory.Update(model.Id, model);
                if (result > 0)
                {
                    note = AppGlobal.Success + " - " + AppGlobal.EditSuccess;
                }
                else
                {
                    note = AppGlobal.Success + " - " + AppGlobal.EditFail;
                }
            }
            else
            {
                model.Initialization(InitType.Insert, RequestUserID);
                result = _invoicePaymentResposistory.Create(model);
                if (result > 0)
                {
                    note = AppGlobal.Success + " - " + AppGlobal.CreateSuccess;
                }
                else
                {
                    note = AppGlobal.Success + " - " + AppGlobal.CreateFail;
                }
            }
            return Json(note);
        }
    }
}