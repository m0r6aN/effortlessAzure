using Microsoft.EntityFrameworkCore;

public static class Program
{
    public static void Main()
    {
        string cn = "Server=fh-sql2-eus.fh.otrcapital.com;Initial Catalog=OTRTableau;Persist Security Info=False;User ID=FH.User;password=L@ffyT@ffy007!;enlist=false;Encrypt=False;TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<DomainDbContext>()
           .UseSqlServer(cn)
           .Options;

        using var context = new DomainDbContext(options);

        var x = context.RateConDetailRate.Count();

        Console.WriteLine(x);
        Console.ReadLine();
    }
}