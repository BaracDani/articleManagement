using System;
using System.Collections.Generic;
using System.Linq;
using Business.Extensions;
using Business.Views;
using DataAccess.Base.Interfaces;
using DataAccess.Entities;

namespace Business.Components.Base
{
    public abstract class BaseComponent<TE, TV, TR> : IBaseComponent<TV> where TR : IBaseEntityRepository<TE> where TE : IBaseEntity where TV : BaseView, new()
    {
        protected BaseComponent(ICatalog catalog)
        {
            this.Catalog = catalog;
        }

        public ICatalog Catalog { get; }

        protected virtual TR Repository { get; set; }

        protected abstract TE ViewToEntity(TV entity);

        protected abstract TV EntityToView(TE entity);

        public TV Create(TV param)
        {
            var entity = ViewToEntity(param);

            entity = Repository.Create(entity);
            Catalog.SaveChanges();

            var log = new ActivityLog
            {
                Active = true,
                Operation = Operation.Create,
                Table = nameof(TV),
                EntityId = entity.Id,
                OnTime = DateTime.Now.ToShortDateTime(),
                NewValue = entity.JsonSerialize(),
            };

            Catalog.ActivityLogRepository.Create(log);
            Catalog.SaveChanges();

            return EntityToView(entity);
        }

        public bool Delete(int id)
        {
            Repository.LogicalDelete(id);
            return true;
        }

        public IEnumerable<TV> GetAll()
        {
            try
            {
                var list = Repository.All();
                var result = list.ToArray().Select(EntityToView);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public TV GetById(int id)
        {
            var item = Repository.GetById(id);
            return EntityToView(item);
        }

        public bool Update(TV param)
        {
            //var item = Repository.GetById(param.Id);
            //if (item == null)
                //return false;

            var entity = ViewToEntity(param);
            Repository.Update(entity);

            Catalog.SaveChanges();
            return true;
        }
    }
}
