using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace E_CommerceSystem
{
    internal class Program
    {
        static ApplicationDbContext context = new ApplicationDbContext();
        public static int currentUserId = 0;

        static void MainMenu()
        {
            while (true)
            {
                Console.WriteLine("===== Welcome to E-Commerce System =====");

                Console.WriteLine("1. Get User Details by ID");
                Console.WriteLine("2. Add New Product");
                Console.WriteLine("3. Update Product Details");
                Console.WriteLine("4. Get Products List");
                Console.WriteLine("5. Get Product Details by ID");
                Console.WriteLine("6. Place New Order");
                Console.WriteLine("7. Get All Orders for User");
                Console.WriteLine("8. Get Order Details by ID");
                Console.WriteLine("9. Add Review for Product");
                Console.WriteLine("10. Get All Reviews for Product");
                Console.WriteLine("11. Update Review");
                Console.WriteLine("12. Delete Review");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        UserDetails();
                        break;

                    case 2:
                        AddNewProduct();
                        break;

                    case 3:
                        UpdateProductDetails();
                        break;

                    case 4:
                        GetProductsList();
                        break;

                    case 5:
                        GetProductDetailsById(); 
                        break;

                    case 6:
                        PlaceNewOrder(); 
                        break;

                    case 7:
                        GetAllOrdersForUser();
                        break;

                    case 8:
                        GetOrderDetailsById();
                        break;

                    case 9:
                        AddReviewForProduct();
                        break;

                    case 10:
                        GetAllReviewsForProduct(); 
                        break;

                    case 11:
                        UpdateReview(); 
                        break;

                    case 12:
                        DeleteReview();
                        break;

                    case 0:
                        Console.WriteLine("Exiting the program...");
                        return;

                    default:
                        Console.WriteLine("Invalid Choice. Please try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void Register()
        {
            Console.WriteLine("Enter Name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            Console.WriteLine("Enter Phone: ");
            string phone = Console.ReadLine();

            Console.WriteLine("Enter Role: ");
            string role = Console.ReadLine();

            context.Users.Add(new User { Name = name, Email = email, Password = password, Phone = phone, Role = role, CreatedAt = DateTime.Now });
            context.SaveChanges();

            Console.WriteLine("User Registered Successfully");
        }
        static void UserDetails()  
        {
            Console.Write("Enter User ID: ");
            int userId = int.Parse(Console.ReadLine());

            User foundUser = context.Users.FirstOrDefault(u => u.U_Id == userId);

            if (foundUser != null)
            {
                Console.WriteLine($"ID: {foundUser.U_Id}");
                Console.WriteLine($"Name: {foundUser.Name}");
                Console.WriteLine($"Email: {foundUser.Email}");
                Console.WriteLine($"Phone: {foundUser.Phone}");
                Console.WriteLine($"Role: {foundUser.Role}");
                Console.WriteLine($"Created At: {foundUser.CreatedAt}");
            }

            else
            {
                Console.WriteLine("User Not Found");
            }
        } 
        static void AddNewProduct()
        {
            Console.WriteLine("Enter Product Name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter Stock: ");
            int stock = int.Parse(Console.ReadLine());

            context.Products.Add(new Product { Name = name ,Description = description, Price = price, Stock = stock, OverallRating = 0 });
            context.SaveChanges();

            Console.WriteLine("Product Added Successfully");
        }
        static void UpdateProductDetails()
        {
            Console.WriteLine("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Product product = context.Products.FirstOrDefault(p => p.P_Id == productId);
            if (product != null)
            {
                Console.WriteLine("Enter New Product Name: ");
                product.Name = Console.ReadLine();

                Console.WriteLine("Enter New Description: ");
                product.Description = Console.ReadLine();

                Console.WriteLine("Enter New Price: ");
                product.Price = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter New Stock: ");
                product.Stock = int.Parse(Console.ReadLine());

                context.SaveChanges();

                Console.WriteLine("Product Updated Successfully");
            }
            else
            {
                Console.WriteLine("Product Not Found");
            }
        }
        static void GetProductsList()
        {
            Console.Write("Enter product name to search or leave empty: ");
            string name = Console.ReadLine();

            Console.Write("Enter minimum price: ");
            decimal minPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Enter maximum price: ");
            decimal maxPrice = decimal.Parse(Console.ReadLine());

            Console.Write("Enter page number: ");
            int pageNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter page size: ");
            int pageSize = int.Parse(Console.ReadLine());

            var products = context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);

            var result = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            foreach (var product in result)
            {
                Console.WriteLine($"ID: {product.P_Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Description: {product.Description}");
                Console.WriteLine($"Price: {product.Price}");
                Console.WriteLine($"Stock: {product.Stock}");
                Console.WriteLine($"Overall Rating: {product.OverallRating}");
            }
        }
        static void GetProductDetailsById()
        {
            Console.WriteLine("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Product product = context.Products.FirstOrDefault(p => p.P_Id == productId);

            if (product != null)
            {
                Console.WriteLine($"ID: {product.P_Id}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Description: {product.Description}");
                Console.WriteLine($"Price: {product.Price}");
                Console.WriteLine($"Stock: {product.Stock}");
                Console.WriteLine($"Overall Rating: {product.OverallRating}");
            }

            else
            {
                Console.WriteLine("Product Not Found");
            }
        }
        static void AddReviewForProduct()
        {
            Console.WriteLine("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Product product = context.Products.FirstOrDefault(p => p.P_Id == productId);

            if (product != null)
            {
                Console.Write("Enter Rating (1 - 5): ");
                int rating = int.Parse(Console.ReadLine());

                Console.Write("Enter Comment: ");
                string comment = Console.ReadLine();
                
                context.Reviews.Add(new Review { P_Id = productId, U_Id = currentUserId , Rating = rating, Comment = comment , ReviewDate= DateTime.Now});
                context.SaveChanges();

                Console.WriteLine("Review Added Successfully");
            }
            else
            {
                Console.WriteLine("Product Not Found");
            }
        }
        static void GetAllReviewsForProduct()
        {
            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter Page Number: ");
            int pageNumber = int.Parse(Console.ReadLine());

            Console.Write("Enter Page Size: ");
            int pageSize = int.Parse(Console.ReadLine());

            var reviews = context.Reviews
                .Where(r => r.P_Id == productId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            if (reviews.Count > 0)
            {
                foreach (var review in reviews)
                {
                    Console.WriteLine($"Review ID: {review.R_Id}");
                    Console.WriteLine($"User ID: {review.U_Id}");
                    Console.WriteLine($"Product ID: {review.P_Id}");
                    Console.WriteLine($"Rating: {review.Rating}");
                    Console.WriteLine($"Comment: {review.Comment}");
                    Console.WriteLine($"Review Date: {review.ReviewDate}");
                }
            }
            else
            {
                Console.WriteLine("No Reviews Found");
            }
        }
        static void PlaceNewOrder()
        {
            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Product product = context.Products.FirstOrDefault(p => p.P_Id == productId);

            if (product == null)
            {
                Console.WriteLine("Product Not Found");
                return;
            }

            if (product.Stock < quantity)
            {
                Console.WriteLine("Not enough stock available");
                return;
            }

            decimal totalAmount = product.Price * quantity;

            Order newOrder = new Order
            {
                U_Id = currentUserId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount
            };

            context.Orders.Add(newOrder);
            context.SaveChanges();

            context.OrderProducts.Add(new OrderProduct
            {
                O_Id = newOrder.O_Id,
                P_Id = product.P_Id,
                Quantity = quantity
            });

            product.Stock = product.Stock - quantity;

            context.SaveChanges();

            Console.WriteLine("Order Placed Successfully");
            Console.WriteLine($"Total Amount: {totalAmount}");
        }
        static void GetAllOrdersForUser()
        {
            var orders = context.Orders
                .Where(o => o.U_Id == currentUserId)
                .ToList();

            if (orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.O_Id}");
                    Console.WriteLine($"User ID: {order.U_Id}");
                    Console.WriteLine($"Order Date: {order.OrderDate}");
                    Console.WriteLine($"Total Amount: {order.TotalAmount}");
                }
            }

            else
            {
                Console.WriteLine("No Orders Found");
            }
        }
        static void GetOrderDetailsById()
        {
            Console.Write("Enter Order ID: ");
            int orderId = int.Parse(Console.ReadLine());

            Order order = context.Orders.FirstOrDefault(o => o.O_Id == orderId && o.U_Id == currentUserId );

            if (order != null)
            {
                Console.WriteLine($"Order ID: {order.O_Id}");
                Console.WriteLine($"User ID: {order.U_Id}");
                Console.WriteLine($"Order Date: {order.OrderDate}");
                Console.WriteLine($"Total Amount: {order.TotalAmount}");
            }

            else
            {
                Console.WriteLine("Order Not Found");
            }
        } 
        static void UpdateReview()
        {
            Console.Write("Enter Review ID: ");
            int reviewId = int.Parse(Console.ReadLine());

            Review review = context.Reviews.FirstOrDefault( r => r.R_Id == reviewId && r.U_Id == currentUserId );

            if (review != null)
            {
                Console.Write("Enter New Rating: ");
                review.Rating = int.Parse(Console.ReadLine());

                Console.Write("Enter New Comment: ");
                review.Comment = Console.ReadLine();

                context.SaveChanges();

                Console.WriteLine("Review Updated Successfully");
            }

            else
            {
                Console.WriteLine("Review Not Found");
            }
        }
        static void DeleteReview()
        {
            Console.Write("Enter Review ID: ");
            int reviewId = int.Parse(Console.ReadLine());

            Review review = context.Reviews.FirstOrDefault(r => r.R_Id == reviewId && r.U_Id == currentUserId);

            if (review != null)
            {
                context.Reviews.Remove(review);

                context.SaveChanges();

                Console.WriteLine("Review Deleted Successfully");
            }

            else
            {
                Console.WriteLine("Review Not Found");
            }
        }
        static void Main(string[] args)
        {
             context.Database.EnsureCreated();

            while (true)
            {
                Console.WriteLine("===== Welcome =====");

                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                int firstChoice = int.Parse(Console.ReadLine());

                switch (firstChoice)
                {
                    case 1:
                        Register();
                        break;

                    case 2:

                        Console.Write("Enter User Name: ");
                        string loginName = Console.ReadLine();

                        Console.Write("Enter Password: ");
                        string loginPassword = Console.ReadLine();

                        var foundUser = context.Users.FirstOrDefault(u => u.Name == loginName && u.Password == loginPassword);

                        if (foundUser != null)
                        {
                            currentUserId = foundUser.U_Id;

                            Console.WriteLine("Login Successful");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();

                            MainMenu ();
                        }

                        else
                        {
                            Console.WriteLine("Invalid Username or Password");
                        }

                        break;

                    case 0:

                        Console.WriteLine("Exiting the program...");
                        return;

                    default:

                        Console.WriteLine("Invalid Choice");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        
        }
    }
}
