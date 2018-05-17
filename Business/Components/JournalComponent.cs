using Business.Components.Base;
using Business.Interfaces;
using Business.Views;
using DataAccess.Base.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Components
{
    public class JournalComponent : BaseComponent<Journal, JournalView, IJournalRepository>, IJournalComponent
    {
        public JournalComponent(ICatalog catalog) : base(catalog)
        {
        }

        protected override IJournalRepository Repository => Catalog.JournalRepository;

        protected override Journal ViewToEntity(JournalView item)
        {
            return JournalView.ToEntity(item);
        }

        protected override JournalView EntityToView(Journal item)
        {
            return JournalView.FromEntity(item);
        }

        public IEnumerable<JournalView> GetAllPublished()
        {
            try
            {
                var list = Repository.Filter(journal => journal.IsPublished == true);
                var result = list.ToArray().Select(EntityToView);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
        public IEnumerable<JournalView> GetAllUnpublished()
        {
            try
            {
                var list = Repository.Filter(journal => journal.IsPublished == false);
                var result = list.ToArray().Select(EntityToView);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
