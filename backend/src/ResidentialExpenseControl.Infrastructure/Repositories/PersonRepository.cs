using Microsoft.EntityFrameworkCore;
using ResidentialExpenseControl.Domain.Entities;
using ResidentialExpenseControl.Domain.Interfaces;
using ResidentialExpenseControl.Infrastructure.Data;

namespace ResidentialExpenseControl.Infrastructure.Repositories
{
    // Implementação concreta do repositório
    // O Domain define a interface, Infrastructure implementa
    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext _context;

        public PersonRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public async Task DeleteAsync(Person person)
        {
            _context.Persons.Remove(person);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}