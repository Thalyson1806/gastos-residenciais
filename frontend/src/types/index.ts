// Tipos que espelham os DTOs do backend
// Manter sincronizado com as respostas da API

export interface Person {
  id: string;
  name: string;
  age: number;
  isMinor: boolean;
}

export interface CreatePersonRequest {
  name: string;
  age: number;
}

export interface Category {
  id: string;
  description: string;
  purpose: number;
  purposeName: string;
}

export interface CreateCategoryRequest {
  description: string;
  purpose: number;
}

export interface Transaction {
  id: string;
  value: number;
  description: string;
  type: number;
  typeName: string;
  createdAt: string;
  personId: string;
  personName: string;
  categoryId: string;
  categoryDescription: string;
}

export interface CreateTransactionRequest {
  value: number;
  description: string;
  type: number;
  personId: string;
  categoryId: string;
}

export interface PersonTotals {
  personId: string;
  personName: string;
  age: number;
  totalIncome: number;
  totalExpense: number;
  balance: number;
}

export interface GeneralTotals {
  totalIncome: number;
  totalExpense: number;
  balance: number;
  personTotals: PersonTotals[];
}

// Enum helpers para exibição
export const PurposeLabels: Record<number, string> = {
  1: 'Despesa',
  2: 'Receita',
  3: 'Ambos'
};

export const TransactionTypeLabels: Record<number, string> = {
  1: 'Despesa',
  2: 'Receita'
};
