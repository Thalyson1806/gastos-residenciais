import { useState, useEffect } from 'react';
import type { Person, CreatePersonRequest } from '../../types';
import { personService } from '../../services/api';

// Página de gerenciamento de pessoas
// Permite criar, listar e excluir pessoas
export function PersonsPage() {
  const [persons, setPersons] = useState<Person[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  // Estado do formulário
  const [formData, setFormData] = useState<CreatePersonRequest>({
    name: '',
    age: 0
  });

  // Carrega a lista de pessoas ao montar o componente
  useEffect(() => {
    loadPersons();
  }, []);

  const loadPersons = async () => {
    try {
      setLoading(true);
      const data = await personService.getAll();
      setPersons(data);
      setError('');
    } catch (err) {
      setError('Erro ao carregar pessoas');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');
    setSuccess('');

    try {
      await personService.create(formData);
      setSuccess('Pessoa criada com sucesso!');
      setFormData({ name: '', age: 0 });
      loadPersons();
    } catch (err: any) {
      // Captura mensagem de erro do backend (DomainException)
      const message = err.response?.data?.error || 'Erro ao criar pessoa';
      setError(message);
    }
  };

  const handleDelete = async (id: string, name: string) => {
    if (!confirm(`Deseja excluir "${name}"? Todas as transações serão removidas.`)) {
      return;
    }

    try {
      await personService.delete(id);
      setSuccess('Pessoa excluída com sucesso!');
      loadPersons();
    } catch (err: any) {
      const message = err.response?.data?.error || 'Erro ao excluir pessoa';
      setError(message);
    }
  };

  if (loading) {
    return <div className="loading">Carregando...</div>;
  }

  return (
    <div>
      <div className="card">
        <h2>Nova Pessoa</h2>

        {error && <div className="error">{error}</div>}
        {success && <div className="success">{success}</div>}

        <form className="form" onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="name">Nome</label>
            <input
              type="text"
              id="name"
              value={formData.name}
              onChange={(e) => setFormData({ ...formData, name: e.target.value })}
              placeholder="Digite o nome"
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="age">Idade</label>
            <input
              type="number"
              id="age"
              value={formData.age || ''}
              onChange={(e) => setFormData({ ...formData, age: parseInt(e.target.value) || 0 })}
              placeholder="Digite a idade"
              min="1"
              required
            />
          </div>

          <button type="submit" className="btn btn-primary">
            Cadastrar
          </button>
        </form>
      </div>

      <div className="card">
        <h2>Pessoas Cadastradas</h2>

        {persons.length === 0 ? (
          <p>Nenhuma pessoa cadastrada.</p>
        ) : (
          <table className="table">
            <thead>
              <tr>
                <th>Nome</th>
                <th>Idade</th>
                <th>Status</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {persons.map((person) => (
                <tr key={person.id}>
                  <td>{person.name}</td>
                  <td>{person.age} anos</td>
                  <td>
                    {person.isMinor ? (
                      <span className="badge badge-minor">Menor de idade</span>
                    ) : (
                      <span className="badge badge-income">Maior de idade</span>
                    )}
                  </td>
                  <td>
                    <button
                      className="btn btn-danger"
                      onClick={() => handleDelete(person.id, person.name)}
                    >
                      Excluir
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}