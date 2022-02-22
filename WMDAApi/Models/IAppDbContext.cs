using Microsoft.EntityFrameworkCore;

namespace WMDAApi.Models
{
    public interface IAppDbContext
    {
        DbSet<Patient> Patients { get; set; }
    }
}
