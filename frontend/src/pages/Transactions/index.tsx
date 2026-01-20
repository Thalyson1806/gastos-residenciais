import { useState, useEffect } from 'react';
import type {
  Transaction,
  CreateTransactionRequest,
  Person,
  Category
} from '../../types';
import { TransactionTypeLabels } from '../../types';
import { transactionService, personService, categoryService } from '../../services/api';
// Página de gerenciamento de transações
// Aplica regras de negócio: menor não pode ter receita, categoria deve permitir o tipo
export function TransactionsPage() {
  const [transactions, setTransactions] = useState<Transaction[]>([]);
  const [persons, setPersons] = useState<Person[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const [formData, setFormData] = useState<CreateTransactionRequest>({
    value: 0,
    description: '',
    type: 1,
    personId: '',
    categoryId: ''
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setLoading(true);
      const [transactionsData, personsData, categoriesData] = await Promise.all([
        transactionService.getAll(),
        personService.getAll(),
        categoryService.getAll()
      ]);
      setTransactions(transactionsData);
      setPersons(personsData);
      setCategories(categoriesData);
      setError('');
    } catch (err) {
      setError('Erro ao carregar dados');
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
      await transactionService.create(formData);
      setSuccess('Transação criada com sucesso!');
      setFormData({
        value: 0,
        description: '',
        type: 1,
        personId: '',
        categoryId: ''
      });
      loadData();
    } catch (err: any) {
      // Erros de regra de negócio vêm do backend
      const message = err.response?.data?.error || 'Erro ao criar transação';
      setError(message);
    }
  };

  // Filtra categorias baseado no tipo de transação selecionado
  // Expense (1) = categorias com purpose 1 ou 3
  // Income (2) = categorias com purpose 2 ou 3
  const getFilteredCategories = (): Category[] => {
    return categories.filter((cat) => {
      if (formData.type === 1) {
        return cat.purpose === 1 || cat.purpose === 3;
      } else {
        return cat.purpose === 2 || cat.purpose === 3;
      }
    });
  };

  // Formata valor para exibição em Real brasileiro
  const formatCurrency = (value: number): string => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL'
    }).format(value);
  };

  // Formata data para exibição
  const formatDate = (dateString: string): string => {
    return new Date(dateString).toLocaleDateString('pt-BR', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  };

  if (loading) {
    return <div className="loading">Carregando...</div>;
  }

  return (
    <div>
      <div className="card">
        <h2>Nova Transação</h2>

        {error && <div className="error">{error}</div>}
        {success && <div className="success">{success}</div>}

        <form className="form" onSubmit={handleSubmit}>
          <div className="form-group">
            <label htmlFor="person">Pessoa</label>
            <select
              id="person"
              value={formData.personId}
              onChange={(e) => setFormData({ ...formData, personId: e.target.value })}
              required
            >
              <option value="">Selecione uma pessoa</option>
              {persons.map((person) => (
                <option key={person.id} value={person.id}>
                  {person.name} {person.isMinor ? '(Menor)' : ''}
                </option>
              ))}
            </select>
          </div>

          <div className="form-group">
            <label htmlFor="type">Tipo</label>
            <select
              id="type"
              value={formData.type}
              onChange={(e) => setFormData({
                ...formData,
                type: parseInt(e.target.value),
                categoryId: '' // Limpa categoria ao mudar tipo
              })}
            >
              <option value={1}>Despesa</option>
              <option value={2}>Receita</option>
            </select>
          </div>

          <div className="form-group">
            <label htmlFor="category">Categoria</label>
            <select
              id="category"
              value={formData.categoryId}
              onChange={(e) => setFormData({ ...formData, categoryId: e.target.value })}
              required
            >
              <option value="">Selecione uma categoria</option>
              {getFilteredCategories().map((category) => (
                <option key={category.id} value={category.id}>
                  {category.description}
                </option>
              ))}
            </select>
          </div>

          <div className="form-group">
            <label htmlFor="value">Valor (R$)</label>
            <input
              type="number"
              id="value"
              value={formData.value || ''}
              onChange={(e) => setFormData({ ...formData, value: parseFloat(e.target.value) || 0 })}
              placeholder="0,00"
              min="0.01"
              step="0.01"
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="description">Descrição</label>
            <input
              type="text"
              id="description"
              value={formData.description}
              onChange={(e) => setFormData({ ...formData, description: e.target.value })}
              placeholder="Ex: Compras do mês"
              required
            />
          </div>

          <button type="submit" className="btn btn-primary">
            Cadastrar
          </button>
        </form>
      </div>

      <div className="card">
        <h2>Transações Registradas</h2>

        {transactions.length === 0 ? (
          <p>Nenhuma transação registrada.</p>
        ) : (
          <table className="table">
            <thead>
              <tr>
                <th>Data</th>
                <th>Pessoa</th>
                <th>Categoria</th>
                <th>Descrição</th>
                <th>Tipo</th>
                <th>Valor</th>
              </tr>
            </thead>
            <tbody>
              {transactions.map((transaction) => (
                <tr key={transaction.id}>
                  <td>{formatDate(transaction.createdAt)}</td>
                  <td>{transaction.personName}</td>
                  <td>{transaction.categoryDescription}</td>
                  <td>{transaction.description}</td>
                  <td>
                    <span className={transaction.type === 1 ? 'badge badge-expense' : 'badge badge-income'}>
                      {TransactionTypeLabels[transaction.type]}
                    </span>
                  </td>
                  <td>{formatCurrency(transaction.value)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}