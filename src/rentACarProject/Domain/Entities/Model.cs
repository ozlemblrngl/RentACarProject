using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;
public class Model : Entity<Guid>
{
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string UmageUrl { get; set; }

    public virtual Brand? Brand { get; set; }

    //public Model()
    //{

    //}

    //public Model (Guid id, Guid brandId, string name, decimal dailyPrice, string umageUrl) :this()
    //{
    //    Id = id;
    //    BrandId = brandId;
    //    Name = name;
    //    DailyPrice = dailyPrice;
    //    UmageUrl = umageUrl;
    //}



}
