using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TodoList
{

  public class Product
  {
    public Product()
    {

    }
    public Product(int id, string name, decimal price, decimal lastPrice, string description, int categoryId)
    {
      Id = id;
      Name = name;
      Price = price;
      LastPrice = lastPrice;
      Description = description;
      CategoryId = categoryId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal LastPrice { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }

  }
  public class Category
  {
    public Category()
    {

    }
    public Category(int id, string name, string description)
    {
      Id = id;
      Name = name;
      Description = description;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Product> ProductItems { get; set; } = new List<Product>();
  }

  internal class Program
  {
    // declare table
    static int tableWidth = 73;
    // declare lst product
    public static List<Product> products = new List<Product>();
    // declare lst category
    public static List<Category> categories = new List<Category>();

    static void PrintRow(params string[] columns)
    {
      int width = (tableWidth - columns.Length) / columns.Length;
      string row = "|";

      foreach (string column in columns)
      {
        row += AlignCentre(column, width) + "|";
      }

      Console.WriteLine(row);
    }
    static void PrintLine()
    {
      Console.WriteLine(new string('-', tableWidth));
    }

    static string AlignCentre(string text, int width)
    {
      text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

      if (string.IsNullOrEmpty(text))
      {
        return new string(' ', width);
      }
      else
      {
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
      }
    }
    static void Main(string[] args)
    {
      //var products = new List<Product>();
      //for(int i = 1; i <= 10; i++)
      //{
      //    products.Add(new Product() { Id = i, Name = $"Product{i}", Price = i, LastPrice = i, Description = $"Product{i}" });
      //}
      //DrawlColumnProduct(products);
      Console.InputEncoding = Encoding.UTF8;
      Console.OutputEncoding = Encoding.UTF8;
      Console.WriteLine("Chọn bảng muốn quản lý");
      // declare manager table 

      bool statusManagerTable = true;
      bool statusManagerTableProduct = false;
      bool statusManagerTableCategory = false;
      while (statusManagerTable)
      {
        Console.WriteLine("Nhập 1 quản lý product");
        Console.WriteLine("Nhập 2 quản lý category");
        Console.WriteLine("Nhập 3 thoát chương trình");
        Console.WriteLine("Mời bạn nhập");
        int managerTable = int.Parse(Console.ReadLine());
        switch (managerTable)
        {
          case 1:
            {
              statusManagerTableProduct = true;
              ManagerProduct(statusManagerTableProduct);
              break;
            }

          case 2:
            {
              statusManagerTableCategory = true;
              ManagerCategory(statusManagerTableCategory);
              break;
            }
          case 3:
            {
              statusManagerTable = false;
              break;
            }

        }
      }
      Console.WriteLine("Bạn đã thoát chương trình");
      Console.ReadLine();
    }

    static void ManagerCategory(bool statusManagerTableCategory)
    {
      while (statusManagerTableCategory)
      {
        Console.WriteLine("Nhập 1 để tạo danh mục");
        Console.WriteLine("Nhập 2 để sửa danh mục");
        Console.WriteLine("Nhập 3 để xóa danh mục");
        Console.WriteLine("Nhập 4 tìm kiếm theo tên danh mục");
        Console.WriteLine("Nhập 5 tìm kiếm chi tiết theo Id danh mục");
        Console.WriteLine("Nhập 6 để thoát");
        int managerCategory = int.Parse(Console.ReadLine());
        switch (managerCategory)
        {
          case 1:
            {
              Console.Write("Nhập tên danh mục : Name = ");
              var name = Console.ReadLine();
              Console.Write("Nhập mô tả danh mục : Description = ");
              var description = Console.ReadLine();
              var newId = SelectIdOfCategory();
              var category = new Category(newId, name, description);
              categories.Add(category);
              DrawlColumnCategory(categories);
              break;
            }

          case 2:
            {
              Console.Write("Nhập Id danh mục cần sửa : Id = ");
              var idUpdate = Console.ReadLine();
              idUpdate = ProcessCheckId(idUpdate);
              DrawlColumnCategory(categories);
              var findCategoryById = FindCategoryById(int.Parse(idUpdate));
              if (findCategoryById == null)
              {
                Console.WriteLine("Id danh mục không tồn tại");
              }
              else
              {
                Console.WriteLine("Bắt đầu chỉnh sửa");
                Console.Write("Nhập tên danh mục sửa : Name = ");
                var name = Console.ReadLine();
                Console.Write("Nhập mô tả danh mục sửa : Description = ");
                var description = Console.ReadLine();
                name = name ?? "";
                description = description ?? "";
                var categoryUpdate = new Category(findCategoryById.Id, name, description);
                UpdateCategory(categoryUpdate);
                Console.WriteLine("Chỉnh sửa danh mục thành công");
                DrawlColumnCategory(categories);
              }
              break;
            }

          case 3:
            {
              Console.Write("Nhập Id danh mục cần xóa : Id = ");
              var idDelete = Console.ReadLine();
              idDelete = ProcessCheckId(idDelete);
              DrawlColumnCategory(categories);
              var findCategoryById = FindCategoryById(int.Parse(idDelete));
              if (findCategoryById == null)
              {
                Console.WriteLine("Id danh mục không tồn tại");
              }
              else
              {
                RemoveCategory(findCategoryById.Id);
                Console.WriteLine("Xóa thành công");
                DrawlColumnCategory(categories);
              }
              break;
            }
          case 4:
            {
              Console.Write("Nhập tên danh mục cần tìm kiếm : Name = ");
              var name = Console.ReadLine();
              var lstCategory = FindCategoryByName(name);
              if (lstCategory.Count() > 0)
              {
                foreach (var category in lstCategory)
                {
                  if (category.ProductItems.Count() > 0)
                  {
                    DrawlColumnProduct(category.ProductItems);
                  }
                }
                DrawlColumnCategory(lstCategory);
              }
              else
              {
                Console.WriteLine($"Không tìm thấy sản phẩm với {name}");
              }
              break;
            }
          case 5:
            {

              break;
            }
          case 6:
            {
              statusManagerTableCategory = false;
              break;
            }

        }

      }
    }
    static void ManagerProduct(bool statusManagerTableProduct)
    {
      while (statusManagerTableProduct)
      {
        Console.WriteLine("Nhập 1 để tạo sản phẩm");
        Console.WriteLine("Nhập 2 để sửa sản phẩm");
        Console.WriteLine("Nhập 3 để xóa sản phẩm");
        Console.WriteLine("Nhập 4 tìm kiếm chi tiết sản phẩm");
        Console.WriteLine("Nhập 5 để thoát");
        int managerProduct = int.Parse(Console.ReadLine());
        switch (managerProduct)
        {
          case 1:
            {
              var id = SelectIdOfProduct();
              Console.Write("Nhập tên sản phẩm : Name = ");
              var name = Console.ReadLine();
              Console.Write("Nhập giá sản phẩm : Price = ");
              var price = Console.ReadLine();
              price = ProcessCheckPriceLastPriceOfProduct(price);
              Console.Write("Nhập giá bán ra sản phẩm : LastPrice = ");
              var lastPrice = Console.ReadLine();
              lastPrice = ProcessCheckPriceLastPriceOfProduct(lastPrice);
              Console.Write("Nhập mô tả sản phẩm : Description = ");
              var des = Console.ReadLine();
              Console.Write("Nhập danh mục cho sản phẩm : CategoryId = ");
              var categoryId = Console.ReadLine();
              DrawlColumnCategory(categories);
              categoryId = CheckCategoryIdToCreateProduct(categoryId);
              if (categoryId == null)
              {
                Console.WriteLine("Không tồn tại category. Sản phẩm sẽ không thuộc danh mục nào");
                categoryId = "0";
              }
              var product = new Product(id, name, decimal.Parse(price), decimal.Parse(lastPrice), des, int.Parse(categoryId));
              var lstProductRes = AddProduct(product);
              DrawlColumnProduct(lstProductRes);
              break;
            }
          case 2:
            {
              Console.Write("Nhập Id sản phẩm để chỉnh sửa : Id = ");
              var id = Console.ReadLine();
              id = ProcessCheckId(id);
              var product = FindProductById(int.Parse(id));
              if (product == null)
              {
                Console.WriteLine($"Không có sản phẩm nào ứng với Id = {id}");
              }
              else
              {
                Console.Write("Nhập tên chỉnh sửa : Name = ");
                var name = Console.ReadLine();
                Console.Write("Nhập giá chỉnh sửa : Price = ");
                var price = Console.ReadLine();
                price = ProcessCheckPriceLastPriceOfProduct(price);
                Console.Write("Nhập giá bán ra sản phẩm : LastPrice = ");
                var lastPrice = Console.ReadLine();
                lastPrice = ProcessCheckPriceLastPriceOfProduct(lastPrice);
                Console.Write("Nhập mô tả sản phẩm : Description = ");
                var des = Console.ReadLine();
                Console.Write("Nhập danh mục sản phẩm : CategoryId = ");
                var categoryId = Console.ReadLine();
                categoryId = CheckCategoryIdToCreateProduct(categoryId);
                if (categoryId == null)
                {
                  Console.WriteLine("Cập nhập thất bại");
                }
                else
                {
                  var productUpdate = new Product(int.Parse(id), name, decimal.Parse(price), decimal.Parse(lastPrice), des, int.Parse(categoryId));
                  var products = UpdateProduct(productUpdate);
                  DrawlColumnProduct(products);
                }
              }
              break;
            }
          case 3:
            {
              Console.Write("Nhập Id sản phẩm cần xóa : Id = ");
              var id = Console.ReadLine();
              id = ProcessCheckId(id);
              var product = FindProductById(int.Parse(id));
              if (product != null)
              {
                var products = RemoveProduct(product.Id);
                DrawlColumnProduct(products);
              }
              else
              {
                Console.WriteLine($"Không có sản phẩm nào ứng với Id = {id}");
              }
              break;
            }
          case 4:
            {
              Console.Write("Nhập tên sản phẩm cần tìm kiếm : Name = ");
              var name = Console.ReadLine();
              var productRes = FindProductByName(name);
              DrawlColumnProduct(productRes);
              break;
            }
          case 5:
            {
              statusManagerTableProduct = false;
              break;
            }
        }
      }
      Console.WriteLine("Đã thoát khỏi quản lý sản phẩm");
    }


    /// <summary>
    /// Manager Product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>



    public static string CheckCategoryIdToCreateProduct(string categoryId)
    {
      var categoryIdAfterCheck = ProcessCheckId(categoryId);
      var category = categories.Where(x => (int)x.Id == int.Parse(categoryIdAfterCheck)).FirstOrDefault();
      if (category == null)
      {
        Console.WriteLine($"Không có category ứng với Id = {categoryId}");
        return null;
      }
      else
      {
        return categoryIdAfterCheck;
      }
    }
    public static List<Product> AddProduct(Product product)
    {
      products.Add(product);
      return products;
    }
    public static List<Product> UpdateProduct(Product productUpdate)
    {
      foreach (var product in products.ToList())
      {
        if (product.Id == productUpdate.Id)
        {
          product.Name = productUpdate.Name;
          product.Price = productUpdate.Price;
          product.LastPrice = productUpdate.LastPrice;
          product.Description = productUpdate.Description;
        }
      }
      return products;
    }
    public static List<Product> RemoveProduct(int Id)
    {
      var product = products.Where(x => x.Id == Id).FirstOrDefault();
      if (product != null)
      {
        products.Remove(product);
      }
      return products;
    }
    public static Product FindProductById(int Id)
    {
      var product = products.Where(x => x.Id == Id).FirstOrDefault();
      return product;
    }
    public static List<Product> FindProductByName(string name)
    {
      var productRes = products.Where(x => x.Name.Contains(name)).ToList();
      if (productRes != null)
      {

        return productRes;
      }
      return new List<Product>();
    }
    /// <summary>
    /// Manager category
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>

    public static Category FindCategoryById(int Id)
    {
      var category = categories.Where(x => x.Id == Id).FirstOrDefault();
      return category;
    }
    public static List<Category> UpdateCategory(Category category)
    {
      foreach (var categoryUpdate in categories)
      {
        if (categoryUpdate.Id == category.Id)
        {
          categoryUpdate.Name = category.Name;
          categoryUpdate.Description = category.Description;
        }
      }
      return categories;
    }
    public static List<Category> RemoveCategory(int Id)
    {
      foreach (var category in categories.ToList())
      {
        if (category.Id == Id)
        {
          categories.Remove(category);
        }
      }
      return categories;
    }
    public static List<Category> FindCategoryByName(string name)
    {
      var lstCategory = new List<Category>();
      if (categories.Count() > 0)
      {
        lstCategory = categories.Where(x => x.Name.Contains(name)).ToList();
        foreach (var category in lstCategory)
        {
          if (products.Count > 0)
          {
            var productItems = products.Where(x => x.CategoryId == category.Id).ToList();
            category.ProductItems = productItems;
          }
        }
      }
      else
      {
        lstCategory = new List<Category>();
      }
      return lstCategory;

    }

    /// <summary>
    /// Select Id
    /// </summary>
    /// <returns></returns>
    static int SelectIdOfCategory()
    {
      if (categories.Count() > 0)
      {
        var newIdCategory = categories.Select(x => x.Id).Max() + 1;
        return newIdCategory;
      }
      else
      {
        return 1;
      }

    }
    static int SelectIdOfProduct()
    {
      if (products.Count() > 0)
      {
        var newId = products.Select(x => x.Id).Max() + 1;
        return newId;
      }
      else
      {
        return 1;
      }
    }

    static bool CheckId(string id)
    {
      try
      {
        var idCheck = int.Parse(id);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
    static bool CheckPriceLastPriceOfProduct(string price)
    {
      try
      {
        var idCheck = decimal.Parse(price);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
    static string ProcessCheckPriceLastPriceOfProduct(string price)
    {
      bool checkPrice = true;
      while (checkPrice)
      {
        var checkPriceResponse = CheckPriceLastPriceOfProduct(price);
        if (checkPriceResponse == true)
        {
          checkPrice = false;
        }
        else
        {
          Console.Write("Nhập lại giá sản phẩm : Price =");
          price = Console.ReadLine();
        }
      }
      return price;
    }
    static string ProcessCheckId(string Id)
    {
      bool checkId = true;
      while (checkId)
      {
        var checkIdResponse = CheckId(Id);
        if (checkIdResponse == true)
        {
          checkId = false;
        }
        else
        {
          Console.Write("Nhập lại id : Id = ");
          Id = Console.ReadLine();
        }
      }
      return Id;
    }

    /// <summary>
    /// Drawl Table
    /// </summary>
    /// <param name="products"></param>
    static void DrawlColumnProduct(List<Product> products)
    {
      var nameTypeOfProducts = new Product().GetType().GetProperties().Where(x => (x.Name.ToString() != "Id" && !x.Name.ToString().EndsWith("Id")) || x.Name.ToString() == "Id").ToList();
      string[] typeOfProduct = new string[nameTypeOfProducts.Count];
      for (int i = 0; i < typeOfProduct.Length; i++)
      {
        typeOfProduct[i] = nameTypeOfProducts[i].Name;
      }
      PrintLine();
      PrintRow(typeOfProduct);
      PrintLine();
      foreach (var product in products)
      {
        PrintRow(product.Id.ToString(), product.Name.ToString(), product.Price.ToString(), product.LastPrice.ToString(), product.Description.ToString());
      }
      PrintLine();
    }
    static void DrawlColumnCategory(List<Category> categories)
    {
      var nameTypeOfCategories = new Category().GetType()
          .GetProperties()
          .Where(x => (x.Name.ToString() != "Id" && !x.Name.ToString().EndsWith("Id") && x.Name.ToString() != "ProductItems") || (x.Name.ToString() == "Id" && x.Name.ToString() != "ProductItems")).ToList();
      string[] typeOfCategrory = new string[nameTypeOfCategories.Count];
      for (int i = 0; i < typeOfCategrory.Length; i++)
      {
        typeOfCategrory[i] = nameTypeOfCategories[i].Name;
      }
      PrintLine();
      PrintRow(typeOfCategrory);
      PrintLine();
      foreach (var categorie in categories)
      {
        PrintRow(categorie.Id.ToString(), categorie.Name.ToString(), categorie.Description);
      }
      PrintLine();
    }
  }

}
