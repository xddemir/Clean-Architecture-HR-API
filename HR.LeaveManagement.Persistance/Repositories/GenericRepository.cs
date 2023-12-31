﻿using ClassLibrary1HR.LeaveManagement.Persistance.DatabaseContext;
using HR.LeaveManagement.Application.Contracts.Persistance;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1HR.LeaveManagement.Persistance.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly HrDatabaseContext _context;

    public GenericRepository(HrDatabaseContext context)
    {
        _context = context;
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int Id)
    {
        return (await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(q => q.Id == Id));
    }
}