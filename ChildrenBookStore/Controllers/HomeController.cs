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
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginSignupPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginSignupPage(Registration registration)
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Register (Name, Email, Password) Values ('{registration.Name}', '{registration.Email}','{registration.Password}')";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            ViewBag.Result = 1;
            return View();
        }
        public IActionResult Login(Registration register)
        {
            ViewBag.Query = 0;
            var check = 1;
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select Email,Password From Register where Email = '{register.LoginEmail}' and Password = '{register.LoginPassword}'";
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
                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.Query = 1;
            }
            return RedirectToAction("LoginSignupPage");

        }
        public IActionResult UserDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserDetails(UserDetails user)
        {

            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into UserDetails (userdetails_name,userdetais_email,userdetails_address,userdetails_state,userdetails_pincode) Values ('{user.FullName}', '{user.Email}','{user.Address}','{user.State}','{user.Pincode}' )";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            return RedirectToAction("Success");
        }
        public IActionResult Success()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Success(int id)
        {
            string connectionString = Configuration["ConnectionStrings:MyConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Truncate table Cart";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult ActivityBooks()
        {
            List<Books> BooksList = new List<Books>();

            string connectionString = Configuration["ConnectionStrings:Myconnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "select * from Books where Book_Type='Activity Books'";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Books Books = new Books();
                        Books.BookId = Convert.ToInt32(dataReader["Book_Id"]);
                        Books.ImageUrl = Convert.ToString(dataReader["Url"]);
                        Books.Name = Convert.ToString(dataReader["Name"]);
                        Books.Price = Convert.ToDecimal(dataReader["Price"]);

                        BooksList.Add(Books);
                    }
                }

                connection.Close();
            }
            return View(BooksList);
        }
        public IActionResult StoryBooks()
        {
            List<Books> BooksList = new List<Books>();

            string connectionString = Configuration["ConnectionStrings:Myconnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "select * from Books where Book_Type='Story Books'";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Books Books = new Books();
                        Books.BookId = Convert.ToInt32(dataReader["Book_Id"]);
                        Books.ImageUrl = Convert.ToString(dataReader["Url"]);
                        Books.Name = Convert.ToString(dataReader["Name"]);
                        Books.Price = Convert.ToDecimal(dataReader["Price"]);

                        BooksList.Add(Books);
                    }
                }

                connection.Close();
            }
            return View(BooksList);
        }
        public IActionResult StudyBooks()
        {
            List<Books> BooksList = new List<Books>();

            string connectionString = Configuration["ConnectionStrings:Myconnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "select * from Books where Book_Type='Subject Books'";
                SqlCommand command = new SqlCommand(sql, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Books Books = new Books();
                        Books.BookId = Convert.ToInt32(dataReader["Book_Id"]);
                        Books.ImageUrl = Convert.ToString(dataReader["Url"]);
                        Books.Name = Convert.ToString(dataReader["Name"]);
                        Books.Price = Convert.ToDecimal(dataReader["Price"]);

                        BooksList.Add(Books);
                    }
                }

                connection.Close();
            }
            return View(BooksList);
        }
        public IActionResult Details(int id)
        {
            Books Books = new Books();
            string connectionString = Configuration["ConnectionStrings:Myconnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * from Books where Book_Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Books.BookId = Convert.ToInt32(dataReader["Book_Id"]);
                        Books.ImageUrl = Convert.ToString(dataReader["Url"]);
                        Books.Name = Convert.ToString(dataReader["Name"]);
                        Books.BookType = Convert.ToString(dataReader["Book_Type"]);
                        Books.Author = Convert.ToString(dataReader["Author"]);
                        Books.Price= Convert.ToDecimal(dataReader["Price"]);
                    }
                }

                connection.Close();

            }
            return View(Books);
        }
        [HttpPost]
        public IActionResult Details(int id, Books books)
        {
            string connectionString = Configuration["ConnectionStrings:Myconnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Insert Into Cart (Book_image,Book_name, CartPrice,Id) Values ('{books.ImageUrl}','{books.Name}','{books.Price}','{books.BookId}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            ViewBag.Result = "Book is added to the Cart";
            return RedirectToAction("Cart");
        }
        public IActionResult Cart()
        {
            decimal t = 0;
            List<Cart> cartList = new List<Cart>();
            string connectionString = Configuration["ConnectionStrings:Myconnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "select * from Cart";
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Cart products = new Cart();
                        products.Cart_Id = Convert.ToInt32(dataReader["Cart_id"]);
                        products.BookImage = Convert.ToString(dataReader["Book_image"]);
                        products.BookName = Convert.ToString(dataReader["Book_name"]);
                        products.Price = Convert.ToDecimal(dataReader["CartPrice"]);
                        t = t + products.Price;
                        cartList.Add(products);
                    }
                }
                connection.Close();
                ViewBag.counter = cartList.Count();
                ViewBag.Total = t;
            }
            return View(cartList);
        }
    }
}
