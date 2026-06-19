using Microsoft.EntityFrameworkCore;
using NUREM.Auth;

var builder = WebApplication.CreateBuilder(args);

// Web API denetleyicilerini (Controllers) sisteme ekliyoruz
builder.Services.AddControllers();

// Veri tabanı bağlantımızı sisteme tanıtıyoruz
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger (Test ekranı) için gerekli ayarlar
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseDefaultFiles(); // index.html'i ana sayfa olarak kabul etmesini sağlar
app.UseStaticFiles();  // wwwroot klasörünü dış dünyaya açar

// Eğer geliştirme modundaysak Swagger test ekranını aç
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();