using System;
using System.Collections.Generic;
using Business.Components.Base;
using Business.Views;

namespace Business.Interfaces
{
    public interface IJournalComponent : IBaseComponent<JournalView>
    {
        IEnumerable<JournalView> GetAllPublished();
        IEnumerable<JournalView> GetAllUnpublished();
    }
}

