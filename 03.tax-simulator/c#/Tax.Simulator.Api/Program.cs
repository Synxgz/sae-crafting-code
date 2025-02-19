using Tax.Simulator;
using Tax.Simulator.Api;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapGet("/api/tax/calculate",
        (string situationFamiliale, decimal salaireMensuel, decimal salaireMensuelConjoint, int nombreEnfants) =>
        {
            Resultat<decimal, string> resultat = Simulateur.CalculerImpotsAnnuel(
                        situationFamiliale,
                        salaireMensuel,
                        salaireMensuelConjoint,
                        nombreEnfants);
            IResult reponse;
            if(resultat.EstEchec())
            {
                reponse = Results.BadRequest(resultat.SiEchec());
            }
            else
            {
                reponse = Results.Ok(resultat.SiSucces());
            }
            return reponse;
        })
    .WithName("CalculateTax");

await app.RunAsync();

public partial class Program;