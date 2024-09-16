using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RequestViewer.Domain.Models;
using RequestViewer.Domain.Queries;

namespace RequestViewer.WPF.Stores
{
    public class PeriodsStore
    {
        private readonly IGetAllPeriodsQuery _getAllPeriodsQuery;
        private readonly List<Period> _periods;

        public IEnumerable<Period> Periods => _periods;

        public event Action PeriodsLoaded;

        public PeriodsStore(IGetAllPeriodsQuery getAllPeriodsQuery)
        {
            _getAllPeriodsQuery = getAllPeriodsQuery;

            _periods = new List<Period>();
        }

        public async Task Load()
        {
            IEnumerable<Period> periods = await _getAllPeriodsQuery.Execute();

            _periods.Clear();
            _periods.AddRange(periods);

            PeriodsLoaded?.Invoke();
        }
    }
}