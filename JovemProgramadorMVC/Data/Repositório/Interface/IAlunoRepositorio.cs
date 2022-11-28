using JovemProgramadorMVC.Models;
using System.Collections.Generic;

namespace JovemProgramadorMVC.Data.Repositório.Interface
{
    public interface IAlunoRepositorio
    {
        void InserirAluno(AlunoModel alunos);
        List<AlunoModel> BuscarAlunos();
        AlunoModel BuscarId(int id);
        void EditarAluno(AlunoModel aluno);
        void ExcluirAluno(AlunoModel aluno);
    }
}
