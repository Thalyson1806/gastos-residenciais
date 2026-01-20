import { useState, useEffect } from 'react';
import type { GeneralTotals } from '../../types';
import { reportService } from '../../services/api';


// Dashboard com resumo financeiro
// Exibe totais gerais e por pessoa
export function DashboardPage() {
  const [totals, setTotals] = useState<GeneralTotals | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    loadTotals();
  }, []);

  const loadTotals = async () => {
    try {
      setLoading(true);
      const data = await reportService.getTotals();
      setTotals(data);
      setError('');
    } catch (err) {
      setError('Erro ao carregar totais');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  // Formata valor para exibição em Real brasileiro
  const formatCurrency = (value: number): string => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL'
    }).format(value);
  };

  if (loading) {
    return <div className="loading">Carregando...</div>;
  }

  if (error) {
    return <div className="error">{error}</div>;
  }

  if (!totals) {
    return <div className="loading">Sem dados</div>;
  }

  return (
    <div>
      <div className="card">
        <h2>Resumo Geral</h2>

        <div className="dashboard-cards">
          <div className="dashboard-card income">
            <h3>Total de Receitas</h3>
            <div className="value">{formatCurrency(totals.totalIncome)}</div>
          </div>

          <div className="dashboard-card expense">
            <h3>Total de Despesas</h3>
            <div className="value">{formatCurrency(totals.totalExpense)}</div>
          </div>

          <div className="dashboard-card">
            <h3>Saldo Geral</h3>
            <div className="value">{formatCurrency(totals.balance)}</div>
          </div>
        </div>
      </div>

      <div className="card">
        <h2>Totais por Pessoa</h2>

        {totals.personTotals.length === 0 ? (
          <p>Nenhuma pessoa cadastrada.</p>
        ) : (
          <table className="table">
            <thead>
              <tr>
                <th>Pessoa</th>
                <th>Idade</th>
                <th>Receitas</th>
                <th>Despesas</th>
                <th>Saldo</th>
              </tr>
            </thead>
            <tbody>
              {totals.personTotals.map((person) => (
                <tr key={person.personId}>
                  <td>{person.personName}</td>
                  <td>{person.age} anos</td>
                  <td style={{ color: '#27ae60' }}>{formatCurrency(person.totalIncome)}</td>
                  <td style={{ color: '#e74c3c' }}>{formatCurrency(person.totalExpense)}</td>
                  <td style={{ 
                    color: person.balance >= 0 ? '#27ae60' : '#e74c3c',
                    fontWeight: 'bold'
                  }}>
                    {formatCurrency(person.balance)}
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
