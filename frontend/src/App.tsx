import { useState } from 'react';
import { PersonsPage } from './pages/Persons';
import { CategoriesPage } from './pages/Categories';
import { TransactionsPage } from './pages/Transactions';
import { DashboardPage } from './pages/Dashboard';
import './App.css';

// Tipos das páginas disponíveis na navegação
type Page = 'dashboard' | 'persons' | 'categories' | 'transactions';

// Componente principal da aplicação
// Gerencia a navegação entre as páginas usando estado
function App() {
  const [currentPage, setCurrentPage] = useState<Page>('dashboard');

  // Renderiza a página atual baseado no estado
  const renderPage = () => {
    switch (currentPage) {
      case 'dashboard':
        return <DashboardPage />;
      case 'persons':
        return <PersonsPage />;
      case 'categories':
        return <CategoriesPage />;
      case 'transactions':
        return <TransactionsPage />;
      default:
        return <DashboardPage />;
    }
  };

  return (
    <div className="app">
      <h1 style={{ textAlign: 'center', marginBottom: '20px', color: '#2c3e50' }}>
        💰 Controle de Gastos Residenciais
      </h1>

      {/* Navegação entre as páginas */}
      <nav className="nav">
        <button
          className={currentPage === 'dashboard' ? 'active' : ''}
          onClick={() => setCurrentPage('dashboard')}
        >
          📊 Dashboard
        </button>
        <button
          className={currentPage === 'persons' ? 'active' : ''}
          onClick={() => setCurrentPage('persons')}
        >
          👤 Pessoas
        </button>
        <button
          className={currentPage === 'categories' ? 'active' : ''}
          onClick={() => setCurrentPage('categories')}
        >
          🗂️ Categorias
        </button>
        <button
          className={currentPage === 'transactions' ? 'active' : ''}
          onClick={() => setCurrentPage('transactions')}
        >
          💳 Transações
        </button>
      </nav>

      {/* Conteúdo da página atual */}
      <main>
        {renderPage()}
      </main>
    </div>
  );
}

export default App;