using CleanOceanic.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanOceanic.Controllers;

public class CompanyController : Controller
{
    // Lista estática de empresas e identificador de empresa
    private static List<Company> _listCompanies = new List<Company>();
    private static long _companyId = 0;

    // Método para exibir a página de cadastro de empresa
    [HttpGet]
    public IActionResult Cadastrar()
    {
        return View();
    }

    // Método para cadastrar uma nova empresa
    [HttpPost]
    public IActionResult Cadastrar(Company company)
    {
        // Verifica se o CNPJ já está cadastrado
        if (_listCompanies.Any(c => c.Cnpj == company.Cnpj))
        {
            TempData["msg"] = "CNPJ já cadastrado!";
            return View();
        }

        // Atribui um novo ID à empresa e a adiciona à lista
        company.IdEmpresa = ++_companyId;
        _listCompanies.Add(company);
        TempData["msg"] = "Empresa cadastrada com sucesso!";
        return RedirectToAction("Cadastrar");
    }

    // Método para exibir a lista de empresas
    public IActionResult Index()
    {
        return View(_listCompanies);
    }

    // Método para pesquisar empresas por nome
    [HttpGet]
    public IActionResult PesquisaNome(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            // Se a string de pesquisa estiver vazia, redireciona para a lista de empresas
            return RedirectToAction("Index");
        }

        // Procura empresas que correspondam ao termo de pesquisa (insensível a maiúsculas e minúsculas)
        var companies = _listCompanies.Where(c => c.RazaoSocial!.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList();

        if (companies.Count == 0)
        {
            // Caso nenhuma empresa seja encontrada
            TempData["msg"] = "Nenhuma empresa encontrada!";
            return RedirectToAction("Index");
        }

        // Se as empresas forem encontradas, envie-as para a visualização de índice
        return View("Index", companies);
    }

    // Método para visualizar detalhes de uma empresa
    [HttpGet]
    public IActionResult Visualizar(int id)
    {
        var company = _listCompanies.FirstOrDefault(c => c.IdEmpresa == id);
        if (company == null)
        {
            TempData["msg"] = "Empresa não encontrada!";
            return RedirectToAction("Index");
        }

        return View(company);
    }

    // Método para editar detalhes de uma empresa
    [HttpPost]
    public IActionResult Editar(Company company)
    {
        var existingCompany = _listCompanies.FirstOrDefault(c => c.IdEmpresa == company.IdEmpresa);
        if (existingCompany == null)
        {
            TempData["msg"] = "Empresa não encontrada!";
            return RedirectToAction("Index");
        }

        // Atualiza as propriedades da empresa conforme necessário
        existingCompany.RazaoSocial = company.RazaoSocial;
        existingCompany.Telefone = company.Telefone;
        existingCompany.Email = company.Email;
        existingCompany.QtdFuncionarios = company.QtdFuncionarios;
        existingCompany.Address = company.Address;

        TempData["msg"] = "Empresa atualizada com sucesso!";
        return RedirectToAction("Index");
    }

    // Método para remover uma empresa
    [HttpPost]
    public IActionResult Remover(int id)
    {
        var companyToRemove = _listCompanies.FirstOrDefault(c => c.IdEmpresa == id);
        if (companyToRemove == null)
        {
            TempData["msg"] = "Empresa não encontrada!";
            return RedirectToAction("Index");
        }

        // Remove a empresa da lista
        _listCompanies.Remove(companyToRemove);
        TempData["msg"] = "Empresa removida com sucesso!";
        return RedirectToAction("Index");
    }
}
