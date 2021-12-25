using ChildrenBookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrenBookStore.Controllers
{
    public class AdminController : Controller
    {
        public IConfiguration Configuration { get; }
        public AdminController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            List<Books> booksList = new List<Books>();

            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Books";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Books book = new Books();
                        book.BookId = Convert.ToInt32(dataReader["Book_Id"]);
                        book.Name = Convert.ToString(dataReader["Name"]);
                        book.Author = Convert.ToString(dataReader["Author"]);
                        book.BookType = Convert.ToString(dataReader["Book_Type"]);
                        book.Price = Convert.ToDecimal(dataReader["Price"]);
                        book.ImageUrl = Convert.ToString(dataReader["Url"]);
                        booksList.Add(book);
                    }
                }

                connection.Close();
            }
            return View(booksList);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Admin admin)
        {
            var check = 1;
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select username,Password From Admin where username = '{admin.Username}' and Password = '{admin.Password}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    var validate = command.ExecuteScalar();
                    //var validate = command.ExecuteScalar();
                    if (validate != null)
                    {
                        check = 0;

                    }
                    connection.Close();
                }

            }
            if (check == 0)
            {
                ViewBag.Res = 1;
                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.Query = 1;
            }
            return RedirectToAction("Login");

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Books book)
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Books (Name,Author,Book_Type, Price, Url) Values ('{book.Name}', '{book.Author}','{book.BookType}','{book.Price}','{book.ImageUrl}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            ViewBag.Result = "Success";
            return View();
        }
        public IActionResult Edit()
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            Books book = new Books();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Books";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        //book.BookId = Convert.ToInt32(dataReader["Book_Id"]);
                        book.Name = Convert.ToString(dataReader["Name"]);
                        book.Author = Convert.ToString(dataReader["Author"]);
                        book.BookType = Convert.ToString(dataReader["Book_Type"]);
                        book.Price = Convert.ToDecimal(dataReader["Price"]);
                        book.ImageUrl = Convert.ToString(dataReader["Url"]);
                        //booksList.Add(book);
                    }
                }

                connection.Close();
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Books book, int id)
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Books SET Name='{book.Name}',Author='{book.Author}', Price='{book.Price}', Book_Type='{book.BookType}',Url='{book.ImageUrl}' Where Book_Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult Delete()
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            Books book = new Books();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Books";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        book.BookId = Convert.ToInt32(dataReader["Book_Id"]);
                        book.Name = Convert.ToString(dataReader["Name"]);
                        book.Author = Convert.ToString(dataReader["Author"]);
                        book.BookType = Convert.ToString(dataReader["Book_Type"]);
                        book.Price = Convert.ToDecimal(dataReader["Price"]);
                        book.ImageUrl = Convert.ToString(dataReader["Url"]);
                        //booksList.Add(book);
                    }
                }

                connection.Close();
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Books Where Book_Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult Details()
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            Books book = new Books();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "Select * From Books";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        book.BookId = Convert.ToInt32(dataReader["Book_Id"]);
                        book.Name = Convert.ToString(dataReader["Name"]);
                        book.Author = Convert.ToString(dataReader["Author"]);
                        book.BookType = Convert.ToString(dataReader["Book_Type"]);
                        book.Price = Convert.ToDecimal(dataReader["Price"]);
                        book.ImageUrl = Convert.ToString(dataReader["Url"]);
                        //booksList.Add(book);
                    }
                }

                connection.Close();
            }
            return View(book);
        }
    }
}