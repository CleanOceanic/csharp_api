using CleanOceanic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanOceanic.Controllers;

public class VolunteerController : Controller
{
    private static List<Volunteer> _listVolunteers = new List<Volunteer>();
    private static int _volunteerId = 0;

    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cadastrar(Volunteer volunteer)
    {
        if (_listVolunteers.Any(c => c.Email == volunteer.Email))
        {
            TempData["msg"] = "E-mail já cadastrado!";
            return View();
        }

        volunteer.IdUsuario = ++_volunteerId;
        _listVolunteers.Add(volunteer);
        TempData["msg"] = "Voluntário(a) cadastrado(a) com sucesso!";
        return RedirectToAction("Cadastrar");
    }

    public IActionResult Index()
    {
        return View(_listVolunteers);
    }

    [HttpGet]
    public IActionResult PesquisaNome(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            return RedirectToAction("Index");
        }

        var volunteers = _listVolunteers.Where(c => c.Nome.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

        if (volunteers.Count == 0)
        {
            TempData["msg"] = "Nenhum Voluntário(a) Encontrado(a)!!";
            return RedirectToAction("Index");
        }

        return View("Index", volunteers);
    }

    [HttpGet]
    public IActionResult Visualizar(int id)
    {
        var volunteer = _listVolunteers.FirstOrDefault(c => c.IdUsuario == id);
        if (volunteer == null)
        {
            TempData["msg"] = "Voluntário(a) não encontrado(a)!";
            return RedirectToAction("Index");
        }

        return View(volunteer);
    }

    [HttpPost]
    public IActionResult Editar(Volunteer volunteer)
    {
        var existingVolunteer = _listVolunteers.FirstOrDefault(c => c.IdUsuario == volunteer.IdUsuario);
        if (existingVolunteer == null)
        {
            TempData["msg"] = "Voluntário(a) não encontrado(a)!";
            return RedirectToAction("Index");
        }

        // Atualizar outras propriedades conforme necessário
        existingVolunteer.Nome = volunteer.Nome;
        existingVolunteer.Genero = volunteer.Genero;
        existingVolunteer.Email = volunteer.Email;
        existingVolunteer.Senha = volunteer.Senha;
        existingVolunteer.Telefone = volunteer.Telefone;
        existingVolunteer.Address = volunteer.Address;

        TempData["msg"] = "Voluntário(a) atualizado(a) com sucesso!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remover(int id)
    {
        var volunteerToRemove = _listVolunteers.FirstOrDefault(c => c.IdUsuario == id);
        if (volunteerToRemove == null)
        {
            TempData["msg"] = "Voluntário(a) não encontrado(a)!";
            return RedirectToAction("Index");
        }

        _listVolunteers.Remove(volunteerToRemove);
        TempData["msg"] = "Voluntário(a) removido(a) com sucesso!";
        return RedirectToAction("Index");
    }
}
