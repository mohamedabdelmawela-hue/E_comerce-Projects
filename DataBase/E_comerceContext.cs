using Microsoft.EntityFrameworkCore;
using myStore.Models;

namespace myStore.DataBase
{
    public class E_comerceContext:DbContext
    {
        public E_comerceContext(DbContextOptions<E_comerceContext> options)
        : base(options)
        {
        }
        public DbSet<UsersModel> Users { get; set; }
        public DbSet<productModel> Products { get; set; }
        public DbSet<orderModel> Orders { get; set; }
        public DbSet<orderItemModel> OrderItems { get; set; }
        public DbSet<categoryModel> Category { get; set; }
        public DbSet<cartModel> Carts { get; set; }
        public DbSet<cartItemModel> CartItems { get; set; }
        

    }
}
