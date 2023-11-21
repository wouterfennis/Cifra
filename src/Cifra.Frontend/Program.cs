using Cifra.Api.Client.Extensions;

var builder = WebApplication.CreateBuilder(args);

var isUrlValid = Uri.TryCreate(builder.Configuration["CifraApiBaseUrl"], UriKind.Absolute, out Uri? cifraApiBaseUrl);

if (!isUrlValid)
{
    throw new InvalidOperationException("The Cifra API base URL is invalid.");
}

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddCifraApiClient(cifraApiBaseUrl!);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
