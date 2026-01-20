import { useState, useEffect } from 'react';
import type { Category, CreateCategoryRequest } from '../../types';
import { PurposeLabels } from '../../types';
import { categoryService } from '../../services/api';

// Página de gerenciamento de categorias
// Permite criar e listar categorias com seus propósitos
export function CategoriesPage() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const [formData, setFormData] = useState<CreateCategoryRequest>({
    description: '',
    purpose: 1
  });

  useEffect(() => {
    loadCategories();
  }, []);

  const loadCategories = async () => {
    try {
      setLoading(true);
      const data = await categoryService.getAll();
      setCategories(data);
      setError('');
    } catch (err) {
      setError('Erro ao carregar categorias');
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
      await categoryService.create(formData);
      setSuccess('Categoria criada com sucesso!');
      setFormData({ description: '', purpose: 1 });
      loadCategories();
    } catch (err: any) {
      const message = err.response?.data?.error || 'Erro ao criar categoria';
      setError(message);
    }
  };

  // Retorna a classe CSS baseada no propósito
  const getPurposeBadgeClass = (purpose: number): string => {
    switch (purpose) {
      case 1: return 'badge badge-expense';
      case 2: return 'badge badge-income';
      default: return 'badge';
    }
  };

  if (loading) {
    return <div className="loading">Carregando...</div>;
  }

  return (
    <div>
      <div className="card">
        <h2>Nova Categoria</h2>

        {error && <div className="error">{error}</div>}
        {success && <div className="success">{success}</div>}

        <form className="form" onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="description">Descrição</label>
            <input
              type="text"
              id="description"
              value={formData.description}
              onChange={(e) => setFormData({ ...formData, description: e.target.value })}
              placeholder="Ex: Alimentação, Salário, Lazer"
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="purpose">Finalidade</label>
            <select
              id="purpose"
              value={formData.purpose}
              onChange={(e) => setFormData({ ...formData, purpose: parseInt(e.target.value) })}
            >
              <option value={1}>Despesa</option>
              <option value={2}>Receita</option>
              <option value={3}>Ambos</option>
            </select>
          </div>

          <button type="submit" className="btn btn-primary">
            Cadastrar
          </button>
        </form>
      </div>

      <div className="card">
        <h2>Categorias Cadastradas</h2>

        {categories.length === 0 ? (
          <p>Nenhuma categoria cadastrada.</p>
        ) : (
          <table className="table">
            <thead>
              <tr>
                <th>Descrição</th>
                <th>Finalidade</th>
              </tr>
            </thead>
            <tbody>
              {categories.map((category) => (
                <tr key={category.id}>
                  <td>{category.description}</td>
                  <td>
                    <span className={getPurposeBadgeClass(category.purpose)}>
                      {PurposeLabels[category.purpose]}
                    </span>
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