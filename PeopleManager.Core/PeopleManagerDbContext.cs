using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Model;
using System.Linq.Expressions;

namespace PeopleManager.Core
{
    public class PeopleManagerDbContext : IdentityDbContext
    {

        public PeopleManagerDbContext(DbContextOptions<PeopleManagerDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People => Set<Person>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.ResponsiblePerson)
                .WithMany(p => p.ResponsibleForVehicles)
                .HasForeignKey(v => v.ResponsiblePersonId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }

        public void Seed()
        {
			AddDefaultRoles();
			AddDefaultUser();

            var bavoPerson = new Person
            {
                FirstName = "Bavo",
                LastName = "Ketels",
                Email = "bavo.ketels@vives.be",
                Description = "Lector"
            };
            var wimPerson = new Person
            {
                FirstName = "Wim",
                LastName = "Engelen",
                Email = "wim.engelen@vives.be",
                Description = "Opleidingshoofd"
            };

            People.AddRange(new List<Person>
            {
                bavoPerson,
                new Person{FirstName = "Isabelle", LastName = "Vandoorne", Email = "isabelle.vandoorne@vives.be" },
                wimPerson,
                new Person{FirstName = "Ebe", LastName = "Deketelaere", Email = "ebe.deketelaere@vives.be" }
            });

            Vehicles.AddRange(new[] {
                new Vehicle{LicensePlate = "1-ABC-123", ResponsiblePerson = bavoPerson},
                new Vehicle{LicensePlate = "THE_BOSS", Brand= "Ferrari", Type="448", ResponsiblePerson = wimPerson},
                new Vehicle{LicensePlate = "SALES_GUY_1", Brand= "Audi", Type="e-tron", ResponsiblePerson = bavoPerson},
                new Vehicle{LicensePlate = "DESK_1", Brand= "Fiat", Type="Punto", ResponsiblePerson = bavoPerson}});

            SaveChanges();
        }            
        private void AddDefaultUser(){
            string email = "jordy.admin@outlook.com";
            string email2 = "jordy.normal@outlook.com";
            var AdminRole = Roles.SingleOrDefault(r => r.Name == "Administrator");

			IdentityUser defaultUser = new IdentityUser
            {
                AccessFailedCount = 0,
                EmailConfirmed = false,
                LockoutEnabled = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                UserName = email,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                NormalizedUserName = email.ToUpper(),
				PasswordHash = "AQAAAAIAAYagAAAAEHP2gmzTGx5N1QXzEsWy6MWuazVfSAjqP31a5gczgjHY27MzhGzGI5WLs9TclbBx3g=="
			};
            IdentityUser normalUser = new IdentityUser
            {
                AccessFailedCount = 0,
                EmailConfirmed = false,
                LockoutEnabled = false,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                UserName = email2,
                Email = email2,
                NormalizedEmail = email2.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                NormalizedUserName = email2.ToUpper(),
                PasswordHash = "AQAAAAIAAYagAAAAEHP2gmzTGx5N1QXzEsWy6MWuazVfSAjqP31a5gczgjHY27MzhGzGI5WLs9TclbBx3g=="
            };

            Users.Add(defaultUser);
            Users.Add(normalUser);
            SaveChanges();
			UserRoles.Add(new IdentityUserRole<string>(){ 
                RoleId = AdminRole.Id, 
                UserId = defaultUser.Id });
            SaveChanges();
        }
        private void AddDefaultRoles()
        {
            Roles.Add(new IdentityRole("Administrator"));
			Roles.Add(new IdentityRole("Manager"));

            SaveChanges();
		}
    }
}
