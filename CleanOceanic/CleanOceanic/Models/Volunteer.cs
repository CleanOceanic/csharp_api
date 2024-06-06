using Microsoft.AspNetCore.Mvc;

namespace CleanOceanic.Models;

public class Volunteer
{
    [HiddenInput]
    public int IdUsuario { get; set; }
    public required string Nome { get; set; }
    public required DateOnly DtNascimento { get; set; }
    public required Genero Genero { get; set; }
    public required string Email { get; set; }
    public required string Senha { get; set; }
    public required string Telefone { get; set; }

    public required Address Address { get; set; }

    public required DateTime CreatedAt { get; set; } = DateTime.Now;
}

public enum Genero
{
    Masculino, Feminino, Transgênero, Intersexo, Outro
}