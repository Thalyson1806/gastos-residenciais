import axios from 'axios';
import type {
  Person,
  CreatePersonRequest,
  Category,
  CreateCategoryRequest,
  Transaction,
  CreateTransactionRequest,
  GeneralTotals
} from '../types';

// Configuração base do Axios
// URL da API backend - ajustar conforme ambiente
const api = axios.create({
  baseURL: 'http://localhost:5019/api',
  headers: {
    'Content-Type': 'application/json'
  }
});

// Serviços de Person
export const personService = {
  getAll: async (): Promise<Person[]> => {
    const response = await api.get<Person[]>('/persons');
    return response.data;
  },

  create: async (data: CreatePersonRequest): Promise<Person> => {
    const response = await api.post<Person>('/persons', data);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await api.delete(`/persons/${id}`);
  }
};

// Serviços de Category
export const categoryService = {
  getAll: async (): Promise<Category[]> => {
    const response = await api.get<Category[]>('/categories');
    return response.data;
  },

  create: async (data: CreateCategoryRequest): Promise<Category> => {
    const response = await api.post<Category>('/categories', data);
    return response.data;
  }
};

// Serviços de Transaction
export const transactionService = {
  getAll: async (): Promise<Transaction[]> => {
    const response = await api.get<Transaction[]>('/transactions');
    return response.data;
  },

  create: async (data: CreateTransactionRequest): Promise<Transaction> => {
    const response = await api.post<Transaction>('/transactions', data);
    return response.data;
  }
};

// Serviços de Reports
export const reportService = {
  getTotals: async (): Promise<GeneralTotals> => {
    const response = await api.get<GeneralTotals>('/reports/totals');
    return response.data;
  }
};

export default api;
