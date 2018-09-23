using AktywniWWW.Manager;
using AktywniWWW.Manager.User;
using AktywniWWW.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AktywniWWW.Commands.Home
{
    public class PaypalController : Controller
    {
        private PayPal.Api.Payment payment;

        private Payment CreatePayment2(string redirectUrl, int? orderID)
        {
            var details = new Details();
            var amount = new Amount();
            decimal subtotal = 0M;
            List<Item> itemsList = new List<Item>();

            Item item = new Item();
            item.name = "Abonament na 1 miesiąc";
            item.currency = "PLN";
            item.price = ((1 + 0.23) * 10).ToString("0.00");
            subtotal += (decimal)((1 + 0.23) * 10 * 1);
            item.price = item.price.Replace(",", ".");
            item.quantity = "1";
            itemsList.Add(item);

            details.tax = ((1 + 0.23) * 0).ToString("0.00");
            details.tax = details.tax.Replace(",", ".");
            details.shipping = ((decimal)((1 + 0) * 0)).ToString("0.00");
            details.shipping = details.shipping.Replace(",", ".");

            details.subtotal = subtotal.ToString("0.00");
            details.subtotal = details.subtotal.Replace(",", ".");
            amount.currency = "PLN";
            amount.total = ((1 + 0.23) * 10).ToString("0.00");
            amount.total = amount.total.Replace(",", ".");
            amount.details = details;

            ItemList itemList = new ItemList();
            itemList.items = itemsList;

            var payer = new Payer() { payment_method = "paypal" };

            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "/Paypal/FailureView",
                return_url = redirectUrl + "/Paypal/SuccessView?orderID=" + orderID
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Zakupy w sklepie internetowym",
                invoice_number = "numer",
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            var apiContext = Models.Configuration.GetAPIContext();

            return this.payment.Create(apiContext);
        }

        public ActionResult PaymentWithPaypal1(int orderID)
        {
            orderID = AddOrderToDatabase();
            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority;
            var payment = CreatePayment2(baseURI, orderID);

            return Redirect(payment.GetApprovalUrl());
        }

        private int AddOrderToDatabase()
        {
            Abonaments newAbonament = new Abonaments();
            Abonaments lastAbonament = PaymentManager.GetLastAbonament(User.Identity.Name);
            using (AktywniDBEntities db = new AktywniDBEntities())
            {
                newAbonament.DateStart = DateTime.Now;
                if (lastAbonament == null || lastAbonament.DateEnd <= DateTime.Now)
                    newAbonament.DateEnd = DateTime.Now.AddMonths(1);
                else
                    newAbonament.DateEnd = lastAbonament.DateEnd.AddMonths(1);

                newAbonament.UserId = UserManager.GetUserId(User.Identity.Name);
                newAbonament.Price = 12.30M;
                newAbonament.IsPaid = false;
                newAbonament.OrderId = PaymentManager.GetLastIdOrder() + 1;
                db.Abonaments.Add(newAbonament);
                db.SaveChanges();
            }
            return (int)newAbonament.OrderId;
        }

        public ActionResult FailureView()
        {
            // TODO: Handle cancelled payment
            return View();
        }

        public ActionResult SuccessView(int orderID, string paymentId, string token, string PayerID)
        {
            using (AktywniDBEntities db = new AktywniDBEntities())
            {
                Abonaments order = db.Abonaments.Where(x => x.OrderId == orderID).OrderByDescending(x=>x.AbonamentId).FirstOrDefault();
                order.IsPaid = true;
                db.SaveChanges();
            }
            return View();
        }


        private static RedirectUrls GetReturnUrls(string baseUrl, string intent)
        {
            var returnUrl = intent == "sale" ? "/Home/PaymentSuccessful" : "/Home/AuthorizeSuccessful";
            return new RedirectUrls()
            {
                cancel_url = baseUrl + "/Home/PaymentCancelled",
                return_url = baseUrl + returnUrl
            };
        }

        public static Payment ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = Models.Configuration.GetAPIContext();

            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };

            var executedPayment = payment.Execute(apiContext, paymentExecution);

            return executedPayment;
        }
    }
}