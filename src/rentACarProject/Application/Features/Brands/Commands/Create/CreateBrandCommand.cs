using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Brands.Commands.Create;

// Burada Create klasik mimarideki Add() metoduna karşılık geliyor.
// kullanıcı tarafından yapılan requestin CQRS request olduğunu IRequest interface'inden implemente ederek yapıyoruz.
// dönüş tipi ise CreatedBrandResponse'tur.
// bir metot kendine ait request ve responselara sahip olmalıdır, keza operasyonları kesinlikle birbirinden ayırmamız gereklidir.
// burada da aslında aynı şeyi söylüyoruz.
// Add() metodundaki requestimizin buradaki hali CreateBrandCommand iken
// response umuz ise IRequest<CreatedBrandResponse> kısmıdır diyebiliriz.
// Bütün istekler CreateBrandCommand nesnesi üzerinden yapılacak.

// Not: CQRS (Command,  Query, Responsibility, Segregation) Komutların sorgularının sorumluluklarının birbirinden ayrıştırılmasıdır. 
// Örn klasik mimaride Add() -->Command-Create'tir; GetAll() ise Query-Read'tir.
// Kısacası artık her bir operasyonumuz bir Command ya da Query'dir.
// Klasik mimaride metot olan şey CQRS te Class'ın kendisidir.
public class CreateBrandCommand : IRequest<CreatedBrandResponse>
{
    public string Name { get; set; }


    // Her command ya da Query'nin bir handler'ı vardır. 
    // Handler demek onu ele alan, işleten demektir.
    // bir commandi sadece bir handler la işletebilirsin.
    // zaten aşağıdaki de sen bir handlersın (CreateBrandCommandHandler : IRequestHandler) request'in bu (CreateBrandCommand), response'un bu (CreatedBrandResponse)diyor.
    // standart bir koddur.
    // normalde bizim neye ihtiyacımız varsa burada da aynıdır.
    // yani bunu bir repository'e ihtiyacı var, maplemeye ihtiyacımız var, iş kurallarına ihtiyacı var. 
    // Bunları da aşağıda inject ediyoruz.

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;

        public CreateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository,
                                         BrandBusinessRules brandBusinessRules)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _brandBusinessRules = brandBusinessRules;
        }

        // aşağısını yukarıdan implemente edebiliriz burada yazmasa da.
        // burada gelen command'i bir brand nesnesine çeviriyoruz.
        public async Task<CreatedBrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = _mapper.Map<Brand>(request);
            // burada autoMapper ile hızlıca mapleyip veritabanına ekledik. 

            await _brandRepository.AddAsync(brand);

            CreatedBrandResponse response = _mapper.Map<CreatedBrandResponse>(brand);
            return response;
        }
    }
}