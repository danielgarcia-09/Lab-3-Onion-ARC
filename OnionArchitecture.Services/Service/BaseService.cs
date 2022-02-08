using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionArchitecture.Bl.Dto;
using OnionArchitecture.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Services.Service
{
    public interface IBaseService<T, TDto, TContext>
        where T : IBaseEntity
        where TDto : BaseDto
        where TContext : DbContext
    {
        Task<List<TDto>> Get();

        Task<TDto> GetById(int id);

        Task<TDto> Create(TDto dto);

        Task<TDto> Update(int id, TDto dto);

        Task<bool> Delete(int id);
    }
    public class BaseService<T, TDto, TContext> : IBaseService<T, TDto, TContext>
        where T : BaseEntity 
        where TDto : BaseDto 
        where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly IMapper _mapper;
        protected readonly DbSet<T> _dbSet;

        public BaseService( TContext context, IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<T>();
        }

        public async Task<List<TDto>> Get()
        {
            var all = await _dbSet.AsQueryable().ToListAsync();
            return _mapper.Map<List<TDto>>(all);
        }


        public async Task<TDto> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> Create(TDto dto)
        {
            var entity = _mapper.Map<T>(dto);

            _dbSet.Add(entity);

            await _context.SaveChangesAsync();

            return _mapper.Map(entity, dto);
        }

        public async Task<TDto> Update(int id, TDto dto)
        {
            var entity = await _dbSet.FindAsync(id);

            if( entity is not null )
            {
                entity = _mapper.Map(dto, entity);

                _dbSet.Update(entity);

                await _context.SaveChangesAsync();

                return _mapper.Map<TDto>(entity);
            }
            return null;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if( entity is not null )
            {
                entity.Deleted = true;

                await _context.SaveChangesAsync();

                return true;
            }
            return false;
        }

    }
}
