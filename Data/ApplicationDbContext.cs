using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private DataAutogenerator _dataAutogenerator = new DataAutogenerator();
        private Random _rand = new Random();

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Cms-Commercial;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<Subsubcategory> Subsubcategories { get; set; }
        public DbSet<Marka> Marka { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PhotoProduct> PhotoProducts { get; set; }
        public DbSet<RejestratorLogowania> RejestratorLogowania { get; set; }
        public DbSet<LogException> LogExceptions { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {

            /*
                        builder.Entity<ApplicationUser> ()
                             .HasMany (h => h.RejestratorLogowania).WithOne (w => w.User).HasForeignKey (f => f.UserId).OnDelete (DeleteBehavior.ClientNoAction);

                        builder.Entity<ApplicationUser> ()
                            .HasMany (h => h.Products).WithOne (w => w.User).HasForeignKey (f => f.UserId).OnDelete (DeleteBehavior.ClientNoAction);

                        builder.Entity<ApplicationUser> ()
                            .HasMany (h => h.LogExceptions).WithOne (w => w.User).HasForeignKey (f => f.UserId).OnDelete (DeleteBehavior.ClientNoAction);


                        builder.Entity<ApplicationRole> ()
                             .HasMany (h => h.Users).WithOne (w => w.Role).HasForeignKey (f => f.RoleId).OnDelete (DeleteBehavior.ClientNoAction);



                        builder.Entity<Category> ()
                            .HasMany (h => h.Products).WithOne (w => w.Category).HasForeignKey (f => f.CategoryId).OnDelete (DeleteBehavior.ClientNoAction);

                        builder.Entity<Category> ()
                            .HasMany (h => h.Subcategories).WithOne (w => w.Category).HasForeignKey (f => f.CategoryId).OnDelete (DeleteBehavior.ClientNoAction);

                        builder.Entity<Category> ()
                            .HasMany (h => h.Subsubcategories).WithOne (w => w.Category).HasForeignKey (f => f.CategoryId).OnDelete (DeleteBehavior.ClientNoAction);



                        builder.Entity<Marka> ()
                            .HasMany (h => h.Products).WithOne (w => w.Marka).HasForeignKey (f => f.MarkaId).OnDelete (DeleteBehavior.ClientNoAction);



                        builder.Entity<Product> ()
                            .HasMany (h => h.PhotosProduct).WithOne (w => w.Product).HasForeignKey (f => f.ProductId).OnDelete (DeleteBehavior.ClientNoAction);



                        builder.Entity<Subcategory> ()
                            .HasMany (h => h.Products).WithOne (w => w.Subcategory).HasForeignKey (f => f.SubcategoryId).OnDelete (DeleteBehavior.ClientNoAction);

                        builder.Entity<Subcategory> ()
                            .HasMany (h => h.Subsubcategories).WithOne (w => w.Subcategory).HasForeignKey (f => f.SubcategoryId).OnDelete (DeleteBehavior.ClientNoAction);



                        builder.Entity<Subsubcategory> ()
                            .HasMany (h => h.Products).WithOne (w => w.Subsubcategory).HasForeignKey (f => f.SubsubcategoryId).OnDelete (DeleteBehavior.ClientNoAction);
            */







            // Dodanie ról   
            ApplicationRole adminRole = new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "Administrator",
                NormalizedName = "Administrator"
            };
            ApplicationRole personelRole = new ApplicationRole()
            {
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "User",
            };
            builder.Entity<ApplicationRole>().HasData(adminRole, personelRole);



            // Dodanie użytkowników  

            string photo = "https://th.bing.com/th?q=User+ICO&w=120&h=120&c=1&rs=1&qlt=90&cb=1&dpr=1.6&pid=InlineBlock&mkt=pl-PL&cc=PL&setlang=pl&adlt=moderate&t=1&mw=247";

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();

            ApplicationUser administratorUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@admin.pl",
                UserName = "admin@admin.pl",
                Imie = "sdg",
                Nazwisko = "sdg",
                Ulica = "sdg",
                NumerUlicy = "15",
                Miejscowosc = "sdg",
                KodPocztowy = "12-222",
                Kraj = "Polska",
                Telefon = "235235235",
                DataUrodzenia = DateTime.Now.AddYears(-_rand.Next(20, 50)).ToString(),
                EmailConfirmed = true,
                NormalizedUserName = "admin@admin.pl".ToUpper(),
                NormalizedEmail = "admin@admin.pl".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RoleId = adminRole.Id,
                DataDodania = DateTime.Now.ToString(),
            };
            administratorUser.PasswordHash = passwordHasher.HashPassword(administratorUser, "SDG%$@5423sdgagSDert");
            IdentityUserRole<string> identityUserRoleAdmin = new IdentityUserRole<string>()
            {
                UserId = administratorUser.Id,
                RoleId = adminRole.Id
            };


            ApplicationUser user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = "user@user.pl",
                UserName = "user@user.pl",
                Imie = "sdg",
                Nazwisko = "sdg",
                Ulica = "sdg",
                NumerUlicy = "15",
                Miejscowosc = "sdg",
                KodPocztowy = "12-222",
                Kraj = "Polska",
                Telefon = "235235235",
                DataUrodzenia = DateTime.Now.AddYears(-_rand.Next(20, 50)).ToString(),
                EmailConfirmed = true,
                NormalizedUserName = "user@user.pl".ToUpper(),
                NormalizedEmail = "user@user.pl".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RoleId = personelRole.Id,
                DataDodania = DateTime.Now.ToString(),
            };
            user.PasswordHash = passwordHasher.HashPassword(user, "SDG%$@5423sdgagSDert");
            IdentityUserRole<string> identityUserRoleUser = new IdentityUserRole<string>()
            {
                UserId = user.Id,
                RoleId = personelRole.Id
            };

            builder.Entity<ApplicationUser>().HasData(administratorUser, user);
            builder.Entity<IdentityUserRole<string>>().HasData(identityUserRoleAdmin, identityUserRoleUser);






            // POSTS

            List<string> photos = new List<string>()
            {
                "https://www.protenis.com.pl/cache/files/240437348/pure-aero-98---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/240437348/rafa-290-2023---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/240437348/pure-aero-2023---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/928233567/1042a224-400-asics---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/928233567/1042a224-101-asics-2023---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/928233567/1042a165-404as-buty-tenisowe---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/928233567/1042a254-400-asics-buty-tenisowe-damskie---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/928233567/1042a217-400-3-9-2023---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/928233567/1042a134-104-3-2023-protenis-2---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/928233567/1042a198-101-asics-1.2023-1---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1421207455/pilki-tenisowe-dunlop-atp-2019-oficial-ball-1---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1421207455/wersje/pilki-tenisowe-dunlop-fort-clay-2019_16---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1421207455/dunlop-autralian-open-pilki-tenisowe-puszka_1---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1175563111/dunlop-stage-3-red-12---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/717107113/tsgx-3-gosen-2022---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/717107113/goseng-tour3_2---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/717107113/83naciag-tenisowy-gosen-micros-super-220m-16g---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/780022970/283672-blge-head-2022-junior-1---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1489540560/wr8017802-lime-wilson-junior---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/334667241/prince_durapro_2---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1174085877/karton-puszek-tenisowych-tecnifibre-club-nowe-logo---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1167813859/granatowe-owijki-toalson-ultra-grip-navy-3-szt---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1167813859/owijki-tenisowe-zewnetrzne-toalson-zielone_1---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/693346931/1041a458-001.asics-2023---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/693346931/1041a330-101-asics-but-2023-1_1---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/693346931/2023-asics-buty---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/693346931/1041a358-102---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/693346931/1041a224-401-asics---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/693346931/1041a330-400-asics-2023-asics-tenis---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/996149710/asics-gel-resolution-8-clay-1041a346-960-wyprzedaz---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/693346931/1041a299-001-1-asics-2022---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/318610289/1041a343-960-asics-1---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/237343331/okulary_squash_tourna_2---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1014600267/head-evo-speed-23-new_1---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1014600267/710100180-gravity-elite---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1804069569/koszyk-na-pilki-tenisowe-gamma-75_1---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1891842834/head-new-ball-trolley-additional-bag-2022---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1804069569/wozek-na-pilki-tenisowe---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/267313233/zestaw-trenerski-head-nowy---w-225-h-225-wo-225-ho-225.jpg",
                "https://www.protenis.com.pl/cache/files/1891842834/p176-ball-cart-czarny---w-225-h-225-wo-225-ho-225.jpg"
            };


            List<string> marki = new List<string>()
            {
                "Babolat",
                "Head",
                "Lacoste",
                "Pacific",
                "Prince",
                "Pro Kennex",
                "Pro's Pro",
            };
            List<string> markiId = new List<string>();
            List<string> kolory = new List<string>() { "Biały", "Niebieski", "Zielony", "Czarny", "Szary" };
            List<string> rozmiary = new List<string>() { "33", "33.5", "34", "34.5", "35", "35.5" };
            List<string> categories = new List<string>() { "Jeden", "Dwa", "Trzy", "Cztery", "Piec", "Szesc", "Siedem", "Osiem", "Dziewiec", "Dziesiec", "Jedenascie" };
            List<string> subcategories = new List<string>() { "Jeden", "Dwa", "Trzy", "Cztery", "Piec", "Szesc", "Siedem", "Osiem", "Dziewiec", "Dziesiec", "Jedenascie" };





            foreach (var marka in marki)
            {
                Marka m = new Marka()
                {
                    MarkaId = Guid.NewGuid().ToString(),
                    Name = marka
                };
                builder.Entity<Marka>().HasData(m);
                markiId.Add(m.MarkaId);
            }




            List<string> categoriesId = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                Category category = new Category()
                {
                    CategoryId = Guid.NewGuid().ToString(),
                    Name = _dataAutogenerator.Nazwisko(),
                    FullName = _dataAutogenerator.Nazwisko(),
                    IloscOdwiedzin = _rand.Next(10, 100),
                };
                builder.Entity<Category>().HasData(category);
                categoriesId.Add(category.CategoryId);
            }



            List<string> subcategoriesId = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                Subcategory subcategory = new Subcategory()
                {
                    SubcategoryId = Guid.NewGuid().ToString(),
                    Name = _dataAutogenerator.Nazwisko(),
                    FullName = _dataAutogenerator.Nazwisko(),
                    IloscOdwiedzin = _rand.Next(10, 100),
                    CategoryId = categoriesId[_rand.Next(0, categoriesId.Count)]
                };
                builder.Entity<Subcategory>().HasData(subcategory);
                subcategoriesId.Add(subcategory.SubcategoryId);
            }





            List<string> subsubcategoriesId = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                Subsubcategory subsubcategory = new Subsubcategory()
                {
                    SubsubcategoryId = Guid.NewGuid().ToString(),
                    Name = _dataAutogenerator.Nazwisko(),
                    FullName = _dataAutogenerator.Nazwisko(),
                    IloscOdwiedzin = 0,
                    SubcategoryId = subcategoriesId[_rand.Next(0, subcategoriesId.Count)],
                    CategoryId = categoriesId[_rand.Next(0, categoriesId.Count)]
                };
                builder.Entity<Subsubcategory>().HasData(subsubcategory);
                subsubcategoriesId.Add(subsubcategory.SubsubcategoryId);
            }



            for (var i = 0; i < 20; i++)
            {
                Product product = new Product()
                {
                    ProductId = Guid.NewGuid().ToString(),
                    Name = _dataAutogenerator.Nazwisko(),
                    Description = _dataAutogenerator.Description(10),
                    Price = _dataAutogenerator.Price(40, 250).ToString(),
                    Quantity = _dataAutogenerator.Number(),
                    Kolor = kolory[_rand.Next(0, kolory.Count)],
                    Rozmiar = rozmiary[_rand.Next(0, rozmiary.Count)],
                    DataDodania = _dataAutogenerator.RandomDateTime().ToString(),
                    IloscOdwiedzin = 0,
                    UserId = administratorUser.Id,
                    MarkaId = markiId[_rand.Next(0, markiId.Count)],
                    CategoryId = categoriesId[_rand.Next(0, categoriesId.Count)],
                    SubcategoryId = subcategoriesId[_rand.Next(0, subcategoriesId.Count)],
                    SubsubcategoryId = subsubcategoriesId[_rand.Next(0, subsubcategoriesId.Count)]
                };
                builder.Entity<Product>().HasData(product);


                for (var j = 0; j < _rand.Next(1, 4); j++)
                {
                    PhotoProduct photoProduct = new PhotoProduct()
                    {
                        PhotoProductId = Guid.NewGuid().ToString(),
                        PhotoData = GetImageBytesAsync(photos[_rand.Next(0, photos.Count)]),
                        ProductId = product.ProductId
                    };
                    builder.Entity<PhotoProduct>().HasData(photoProduct);
                }
            }





            base.OnModelCreating(builder);
        }




        /// <summary>
        /// Zamienia zdjęcie pobrane z sieci na byte[]
        /// </summary>
        private byte[] GetImageBytesAsync(string imageUrl)
        {
            using (var httpClient = new HttpClient())
            {
                byte[] imageBytes;

                using (var response = httpClient.GetAsync(imageUrl).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        imageBytes = response.Content.ReadAsByteArrayAsync().Result;
                    }
                    else
                    {
                        imageBytes = new byte[0];
                    }
                }

                return imageBytes;
            }
        }






    }
}
