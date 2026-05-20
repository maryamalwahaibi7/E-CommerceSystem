using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace E_CommerceSystem
{
    internal class Program
    {
        static ApplicationDbContext context = new ApplicationDbContext();
        public static int currentUserId = 0;

        static bool CheckLogin()
        {
            if (currentUserId != 0)
            {
                return true;
            }

            Console.WriteLine("You must login or register first.");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("0. Cancel");

            Console.Write("Enter your choice: ");

            int choice;

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid Choice");
                return false;
            }

            switch (choice)
            {
                case 1:
                    Register();
                    return false;

                case 2:
                    Login();
                    return currentUserId != 0;

                case 0:
                    return false;

                default:
                    Console.WriteLine("Invalid Choice");
                    return false;
            }
        }
        static void Register()
        {
            Console.WriteLine("Enter Name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty");
                return;
            }

            Console.WriteLine("Enter Email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty");
                return;
            }

            if (context.Users.Any(u => u.Email == email))
            {
                Console.WriteLine("Email already exists");
                return;
            }

            Console.WriteLine("Enter Password: ");
            string password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Password cannot be empty");
                return;
            }

            SHA256 sha256 = SHA256.Create();

            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();

            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            string hashedPassword = builder.ToString();

            Console.WriteLine("Enter Phone: ");
            string phone = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Phone cannot be empty");
                return;
            }

            Console.WriteLine("Enter Role: ");
            string role = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(role))
            {
                Console.WriteLine("Role cannot be empty");
                return;
            }

            context.Users.Add(new User { Name = name, Email = email, Password = hashedPassword, Phone = phone, Role = role, CreatedAt = DateTime.Now });

            context.SaveChanges();

            Console.WriteLine("User Registered Successfully");
        }
        static void Login()
        {
            Console.Write("Enter User Name: ");
            string loginName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(loginName))
            {
                Console.WriteLine("User Name cannot be empty");
                return;
            }

            Console.Write("Enter Password: ");
            string loginPassword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(loginPassword))
            {
                Console.WriteLine("Password cannot be empty");
                return;
            }

            SHA256 sha256 = SHA256.Create();

            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(loginPassword));

            StringBuilder builder = new StringBuilder();

            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }

            string hashedLoginPassword = builder.ToString();

            var foundUser = context.Users.FirstOrDefault(u => u.Name == loginName && u.Password == hashedLoginPassword);

            if (foundUser != null)
            {
                currentUserId = foundUser.U_Id;

                Console.WriteLine("Login Successful");
            }

            else
            {
                Console.WriteLine("Invalid Username or Password");
            }
        }
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

                int choice;

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid Choice");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                switch (choice)
                {
                    case 1:

                        if (CheckLogin())
                        {
                            UserDetails();
                        }

                        break;

                    case 2:

                        if (CheckLogin())
                        {
                            AddNewProduct();
                        }

                        break;

                    case 3:

                        if (CheckLogin())
                        {
                            UpdateProductDetails();
                        }

                        break;

                    case 4:

                        GetProductsList();

                        break;

                    case 5:

                        GetProductDetailsById();

                        break;

                    case 6:

                        if (CheckLogin())
                        {
                            PlaceNewOrder();
                        }

                        break;

                    case 7:

                        if (CheckLogin())
                        {
                            GetAllOrdersForUser();
                        }

                        break;

                    case 8:

                        if (CheckLogin())
                        {
                            GetOrderDetailsById();
                        }

                        break;

                    case 9:

                        if (CheckLogin())
                        {
                            AddReviewForProduct();
                        }

                        break;

                    case 10:

                        GetAllReviewsForProduct();

                        break;

                    case 11:

                        if (CheckLogin())
                        {
                            UpdateReview();
                        }

                        break;

                    case 12:

                        if (CheckLogin())
                        {
                            DeleteReview();
                        }

                        break;

                    case 0:

                        Console.Write("Are you sure you want to exit? (yes/no): ");
                        string answer = Console.ReadLine();

                        if (answer.ToLower() == "yes")
                        {
                            Console.WriteLine("Exiting the program...");
                            return;
                        }

                        break;

                    default:

                        Console.WriteLine("Invalid Choice. Please try again.");
                        break;
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
        static void Main(string[] args)
        {
            context.Database.EnsureCreated();

            MainMenu();
        }
        static void UserDetails()
        {
            var foundUser = context.Users.FirstOrDefault(u => u.U_Id == currentUserId);

            Console.WriteLine($"ID: {foundUser.U_Id}");
            Console.WriteLine($"Name: {foundUser.Name}");
            Console.WriteLine($"Email: {foundUser.Email}");
            Console.WriteLine($"Phone: {foundUser.Phone}");
            Console.WriteLine($"Role: {foundUser.Role}");
            Console.WriteLine($"Created At: {foundUser.CreatedAt}");

        }
        static void AddNewProduct()
        {
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Product Name cannot be empty");
                return;
            }

            Console.Write("Enter Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter Price: ");

            decimal price;

            if (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
            {
                Console.WriteLine("Invalid Price");
                return;
            }

            Console.Write("Enter Stock: ");

            int stock;

            if (!int.TryParse(Console.ReadLine(), out stock) || stock < 0)
            {
                Console.WriteLine("Invalid Stock");
                return;
            }

            context.Products.Add(new Product { Name = name, Description = description, Price = price, Stock = stock, OverallRating = 0 });

            context.SaveChanges();

            Console.WriteLine("Product Added Successfully");
        }
        static void UpdateProductDetails()
        {
            var products = context.Products.ToList();

            if (products.Count == 0)
            {
                Console.WriteLine("No Products Found");
                return;
            }

            Console.WriteLine("===== Available Products =====");

            foreach (var item in products)
            {
                Console.WriteLine($"ID: {item.P_Id}");
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Price: {item.Price}");
                Console.WriteLine($"Stock: {item.Stock}");
                Console.WriteLine("----------------------");
            }

            Console.WriteLine("Enter Product ID: ");

            int productId;

            if (!int.TryParse(Console.ReadLine(), out productId))
            {
                Console.WriteLine("Invalid Product ID");
                return;
            }

            Product product = context.Products.FirstOrDefault(p => p.P_Id == productId);

            if (product != null)
            {
                Console.WriteLine("Enter New Product Name: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Product Name cannot be empty");
                    return;
                }

                product.Name = name;

                Console.WriteLine("Enter New Description: ");
                product.Description = Console.ReadLine();

                Console.WriteLine("Enter New Price: ");

                decimal price;

                if (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
                {
                    Console.WriteLine("Invalid Price");
                    return;
                }

                product.Price = price;

                Console.WriteLine("Enter New Stock: ");

                int stock;

                if (!int.TryParse(Console.ReadLine(), out stock) || stock < 0)
                {
                    Console.WriteLine("Invalid Stock");
                    return;
                }

                product.Stock = stock;

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
        static void PlaceNewOrder()
        {
            decimal totalAmount = 0;

            Order newOrder = new Order
            {
                U_Id = currentUserId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount
            };

            context.Orders.Add(newOrder);
            context.SaveChanges();

            while (true)
            {
                var products = context.Products.ToList();

                if (products.Count == 0)
                {
                    Console.WriteLine("No Products Found");
                    return;
                }

                Console.WriteLine("===== Available Products =====");

                foreach (var item in products)
                {
                    Console.WriteLine($"ID: {item.P_Id}");
                    Console.WriteLine($"Name: {item.Name}");
                    Console.WriteLine($"Description: {item.Description}");
                    Console.WriteLine($"Price: {item.Price}");
                    Console.WriteLine($"Stock: {item.Stock}");
                    Console.WriteLine("----------------------");
                }

                Console.Write("Enter Product ID: ");

                int productId;

                if (!int.TryParse(Console.ReadLine(), out productId))
                {
                    Console.WriteLine("Invalid Product ID");
                    return;
                }

                Console.Write("Enter Quantity: ");

                int quantity;

                if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Invalid Quantity");
                    return;
                }

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

                context.OrderProducts.Add(new OrderProduct
                {
                    O_Id = newOrder.O_Id,
                    P_Id = product.P_Id,
                    Quantity = quantity
                });

                product.Stock = product.Stock - quantity;

                totalAmount = totalAmount + (product.Price * quantity);

                Console.Write("Do you want to add another product? (yes/no): ");
                string answer = Console.ReadLine();

                if (answer.ToLower() != "yes")
                {
                    break;
                }
            }

            newOrder.TotalAmount = totalAmount;

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

            Order order = context.Orders.FirstOrDefault(o => o.O_Id == orderId && o.U_Id == currentUserId);

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
        static void AddReviewForProduct()
        {
            var products = context.Products.ToList();

            if (products.Count == 0)
            {
                Console.WriteLine("No Products Found");
                return;
            }

            Console.WriteLine("===== Available Products =====");

            foreach (var item in products)
            {
                Console.WriteLine($"ID: {item.P_Id}");
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Price: {item.Price}");
                Console.WriteLine($"Stock: {item.Stock}");
                Console.WriteLine("----------------------");
            }

            Console.WriteLine("Enter Product ID: ");

            int productId;

            if (!int.TryParse(Console.ReadLine(), out productId))
            {
                Console.WriteLine("Invalid Product ID");
                return;
            }

            Product product = context.Products.FirstOrDefault(p => p.P_Id == productId);

            if (product != null)
            {
                bool purchasedProduct = context.OrderProducts
                    .Any(op => op.P_Id == productId && op.Order.U_Id == currentUserId);

                if (!purchasedProduct)
                {
                    Console.WriteLine("You can only review products you purchased");
                    return;
                }

                bool alreadyReviewed = context.Reviews
                    .Any(r => r.P_Id == productId && r.U_Id == currentUserId);

                if (alreadyReviewed)
                {
                    Console.WriteLine("You already reviewed this product");
                    return;
                }

                Console.Write("Enter Rating (1 - 5): ");

                int rating;

                if (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
                {
                    Console.WriteLine("Invalid Rating");
                    return;
                }

                Console.Write("Enter Comment: ");
                string comment = Console.ReadLine();

                context.Reviews.Add(new Review
                {
                    P_Id = productId,
                    U_Id = currentUserId,
                    Rating = rating,
                    Comment = comment,
                    ReviewDate = DateTime.Now
                });

                context.SaveChanges();

                product.OverallRating = (decimal)context.Reviews
                    .Where(r => r.P_Id == productId)
                    .Average(r => r.Rating);

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
        static void UpdateReview()
        {
            Console.Write("Enter Review ID: ");

            int reviewId;

            if (!int.TryParse(Console.ReadLine(), out reviewId))
            {
                Console.WriteLine("Invalid Review ID");
                return;
            }

            Review review = context.Reviews.FirstOrDefault(r => r.R_Id == reviewId && r.U_Id == currentUserId);

            if (review != null)
            {
                Console.Write("Enter New Rating: ");

                int rating;

                if (!int.TryParse(Console.ReadLine(), out rating) || rating < 1 || rating > 5)
                {
                    Console.WriteLine("Invalid Rating");
                    return;
                }

                review.Rating = rating;

                Console.Write("Enter New Comment: ");
                review.Comment = Console.ReadLine();

                context.SaveChanges();

                Product product = context.Products.FirstOrDefault(p => p.P_Id == review.P_Id);

                product.OverallRating = (decimal)context.Reviews
                    .Where(r => r.P_Id == review.P_Id)
                    .Average(r => r.Rating);

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

            int reviewId;

            if (!int.TryParse(Console.ReadLine(), out reviewId))
            {
                Console.WriteLine("Invalid Review ID");
                return;
            }

            Review review = context.Reviews.FirstOrDefault(r => r.R_Id == reviewId && r.U_Id == currentUserId);

            if (review != null)
            {
                int productId = review.P_Id;

                context.Reviews.Remove(review);

                context.SaveChanges();

                Product product = context.Products.FirstOrDefault(p => p.P_Id == productId);

                var remainingReviews = context.Reviews
                    .Where(r => r.P_Id == productId)
                    .ToList();

                if (remainingReviews.Count > 0)
                {
                    product.OverallRating = (decimal)remainingReviews.Average(r => r.Rating);
                }
                else
                {
                    product.OverallRating = 0;
                }

                context.SaveChanges();

                Console.WriteLine("Review Deleted Successfully");
            }

            else
            {
                Console.WriteLine("Review Not Found");
            }
        }

    }
}
