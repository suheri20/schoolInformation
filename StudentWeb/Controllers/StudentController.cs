using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using StudentWeb.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentWeb.Controllers
{
    public class StudentController : BaseController
    {
        public readonly string studentUrl = "/student";
        // GET: Student
        public ActionResult Index()
        {
            var client = CreateClient();
            var request = new RestRequest(studentUrl,Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            var studentList = JsonConvert.DeserializeObject<List<StudentModel>>(response.Content);

            return View(studentList);
        }

        // GET: Student/Details/5
        public ActionResult Details(long id)
        {
            var client = CreateClient();
            var request = new RestRequest(studentUrl + "/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            var studentList = JsonConvert.DeserializeObject<StudentModel>(response.Content);

            return View(studentList);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            //ViewData["DropdownGender"] = new SelectList(ConstructDropDownGender(), "id", "value");
            return View(new StudentModel());
        }

        // POST: Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentModel student)
        {
            try
            {
                if(student == null)
                {
                    return View();
                }

                var client = CreateClient();
                var request = new RestRequest(studentUrl, Method.POST);
                var jsonData = JsonConvert.SerializeObject(student);
                request.AddParameter("text/json", jsonData, ParameterType.RequestBody);
                var response = new RestResponse();
                Task.Run(async () =>
                {
                    response = await GetResponseContentAsync(client, request) as RestResponse;
                }).Wait();
                var studentList = JsonConvert.DeserializeObject<StudentModel>(response.Content);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Edit/5
        public ActionResult Edit(long id)
        {
            var client = CreateClient();
            var request = new RestRequest(studentUrl + "/{id}", Method.GET);
            request.AddUrlSegment("id", id.ToString());
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            var studentList = JsonConvert.DeserializeObject<StudentModel>(response.Content);


            //ViewBag.ShowDropDown = new SelectList(ConstructDropDownGender(), "id", "value");

            return View(studentList);
        }


        public List<MyDrop> ConstructDropDownGender()
        {
            var GenreLst = new List<MyDrop>();
            MyDrop a = new MyDrop();
            a.id = 1;
            a.value = "Male";

            MyDrop b = new MyDrop();
            b.id = 2;
            b.value = "Female";

            GenreLst.Add(a);
            GenreLst.Add(b);

            return GenreLst;
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentModel student)
        {
            try
            {
                if (student == null)
                    return RedirectToAction("Index");

                var client = CreateClient();
                var request = new RestRequest(studentUrl + "/{id}", Method.PUT);
                request.AddUrlSegment("id", student.Id.ToString());
                var jsonData = JsonConvert.SerializeObject(student);
                request.AddParameter("text/json", jsonData, ParameterType.RequestBody);
                var response = new RestResponse();
                Task.Run(async () =>
                {
                    response = await GetResponseContentAsync(client, request) as RestResponse;
                }).Wait();
                var studentList = JsonConvert.DeserializeObject<StudentModel>(response.Content);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            var client = CreateClient();
            var request = new RestRequest(studentUrl + "/{id}", Method.DELETE);
            request.AddUrlSegment("id", id.ToString());
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            var studentList = JsonConvert.DeserializeObject<StudentModel>(response.Content);
            return RedirectToAction("Index");
        }


        public class MyDrop
        {
            public int id { get; set; }
            public string value { get; set; }
        }
        //// POST: Student/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}