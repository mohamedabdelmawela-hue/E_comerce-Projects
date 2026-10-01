using myStore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using myStore.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using myStore.Middleware;
using myStore.ServiceLayer;
using myStore.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserServiceLayer, UserServiceLayer>();
builder.Services.AddScoped<IproductServices,ProductServices>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IproductRepository,productRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServiceLayer>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<ICartService, CartServiceLayer>();
builder.Services.AddScoped<ICartRepository, cartRepository>();

builder.Services.AddScoped<ICartItemService, cartItemServices>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

builder.Services.AddScoped<IOrderService, OrderServiceLayer>();
builder.Services.AddScoped<IOrderRepository, orderRepository>();

builder.Services.AddScoped<IOrderItemRepository, orderItemRepository>();

builder.Services.AddScoped<ICheckoutService, ChickOutService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<E_comerceContext>(options =>
    options.UseSqlServer(
     builder.Configuration.GetConnectionString("DefaultConnection"))); 
builder.Services.AddScoped<IJwtServices, JwtServices>();
/*

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters =
        new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]!))
        };
});*/
/*ممتاز، دي من أهم أجزاء الـ JWT. خلينا نفصصها سطر سطر.

السطر الأول
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)

معناه:

يا ASP.NET استخدم JWT Authentication
كنظام تسجيل دخول للمشروع

كأنك بتقول:

أي Request جاي ومعاه Token، افحصه.

السطر الثاني
.AddJwtBearer(options =>

معناه:

هنستخدم Bearer Token
ونحدد قواعد التحقق منه
الجزء ده
options.TokenValidationParameters =
    new TokenValidationParameters

معناه:

القوانين اللي هنتأكد منها قبل ما نثق في التوكن
ValidateIssuer
ValidateIssuer = true

معناه:

اتأكد مين اللي أنشأ التوكن

ولازم يطابق:

ValidIssuer = builder.Configuration["jwt:issuer"]

لو في التوكن:

Issuer = myStore

وفي appsettings:

Issuer = myStore

يبقى تمام ✅

غير كده ❌

ValidateAudience
ValidateAudience = true

معناه:

اتأكد التوكن معمول لمين

مثال:

Audience = MyStoreUser

لازم يطابق اللي في:

ValidAudience
ValidateLifetime
ValidateLifetime = true

معناه:

اتأكد إن التوكن لسه صالح

أنت كنت عامل:

Expires = DateTime.Now.AddHours(2)

بعد ساعتين:

التوكن ينتهي

ويرجع:

401 Unauthorized
ValidateIssuerSigningKey
ValidateIssuerSigningKey = true

معناه:

اتأكد إن التوقيع صحيح

يعني محدش عدّل التوكن بإيده.

الجزء ده
ValidIssuer = builder.Configuration["jwt:issuer"]

معناه:

هات Issuer من appsettings.json

مثال:

"Jwt": {
  "Issuer": "myStore"
}
الجزء ده
ValidAudience = builder.Configuration["jwt:audience"]

معناه:

هات Audience من appsettings.json
الجزء ده
IssuerSigningKey =
new SymmetricSecurityKey(
Encoding.UTF8.GetBytes(
builder.Configuration["Jwt:Key"]!))

ده أهم جزء.

معناه:

هات المفتاح السري من appsettings
واستخدمه للتحقق من التوقيع

مثال:

"Jwt": {
  "Key": "ThisIsMySecretKey123456789"
}

لما التوكن يتولد:

يتوقع بالمفتاح ده

ولما يجي Request:

المشروع يفحص التوقيع بنفس المفتاح

لو المفتاح مختلف:

401 Unauthorized
الخلاصة بالمصري
AddAuthentication
↓
شغل نظام JWT

AddJwtBearer
↓
قول للمشروع ازاي يتحقق من التوكن

ValidateIssuer
↓
مين أنشأ التوكن

ValidateAudience
↓
التوكن معمول لمين

ValidateLifetime
↓
لسه صالح ولا انتهى

ValidateIssuerSigningKey
↓
التوقيع صحيح ولا مزور

IssuerSigningKey
↓
المفتاح السري اللي بيتحقق بيه من التوقيع

لو قدرت تشرحها بالشكل ده، يبقى أنت فاهم الجزء ده من الـ JWT فهم ممتاز، ومش محتاج تحفظ كل سطر حرفيًا. 💪*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=> {
    options.TokenValidationParameters = new TokenValidationParameters { 
    
    ValidateIssuer=true,
    ValidateAudience=true,
    ValidateLifetime=true,
    ValidateIssuerSigningKey=true,
        ValidIssuer = builder.Configuration["jwt:issuer"],
        ValidAudience = builder.Configuration["jwt:audience"],
        IssuerSigningKey=
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
    }; });
//******************************************************************
/*      أنا ضفت
     * AddSecurityDefinition
و AddSecurityRequirement
جوه AddSwaggerGen
عشان Swagger يعرف إن المشروع بيستخدم JWT
ويظهر زر Authorize
ويبعت التوكن مع كل Request

*/
//******************************************************************
/*  لو سألك حد:

AddSecurityDefinition بتعمل إيه؟

قول:

بعرف Swagger نوع الحماية المستخدمة
وهي Bearer JWT*/
//*********************************************************************
/*
 AddSecurityRequirement بتعمل إيه؟

قول:

بقول لـ Swagger استخدم نظام الحماية ده 
على الـ APIs
 */
//********************************************************************
/*إزاي خليت Swagger يدعم JWT؟"

يكفي تقول:

"أضفت Security Definition و Security Requirement في AddSwaggerGen عشان يظهر Authorize ويبعت التوكن مع الطلبات."
*/
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
