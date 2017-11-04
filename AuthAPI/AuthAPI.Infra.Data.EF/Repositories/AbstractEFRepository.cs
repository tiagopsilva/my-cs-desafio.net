using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Domain.ValueObjects;
using AuthAPI.Infra.Data.EF.EFContext;
using FluentValidation.Results;

namespace AuthAPI.Infra.Data.EF.Repositories
{
    public abstract class AbstractEFRepository<TEntity>
        where TEntity : class
    {
        protected readonly AuthAPIContext Context;
        protected readonly DbSet<TEntity> Set;

        protected AbstractEFRepository(AuthAPIContext context)
        {
            Context = context;
            Set = context.Set<TEntity>();
        }

        protected async Task<MethodResult> AddAsync(TEntity entity)
        {
            try
            {
                Set.Add(entity);
                await Context.SaveChangesAsync();
                return Success();
            }
            catch(DbEntityValidationException eve)
            {
                return Error(eve.EntityValidationErrors.SelectMany(validation => validation.ValidationErrors));
            }
        }

        protected async Task<MethodResult> UpdateAsync(TEntity entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                await Context.SaveChangesAsync();
                return Success();
            }
            catch (DbEntityValidationException eve)
            {
                return Error(eve.EntityValidationErrors.SelectMany(validation => validation.ValidationErrors));
            }
        }

        protected MethodResult Success()
        {
            return new MethodResult();
        }

        protected MethodResult Error(string errorMessage)
        {
            return new MethodResult(errorMessage);
        }

        protected MethodResult Error(IEnumerable<DbValidationError> dbValidationErrors)
        {
            var failures = new List<ValidationFailure>();

            foreach (var dbValidationError in dbValidationErrors)
                failures.Add(new ValidationFailure(dbValidationError.PropertyName?.Trim() ?? "", dbValidationError.ErrorMessage?.Trim()));

            return new MethodResult(failures);
        }
    }
}