using Microsoft.EntityFrameworkCore;
using RequestViewer.Domain.Commands;
using RequestViewer.Domain.Models;
using RequestViewer.EntityFramework.DTOs;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace RequestViewer.EntityFramework.Commands
{
    public class ApproveRequestCommand : IApproveRequestCommand
    {
        private readonly RequestViewerDbContextFactory _contextFactory;

        public ApproveRequestCommand(RequestViewerDbContextFactory requestViewerDbContextFactory)
        {
            _contextFactory = requestViewerDbContextFactory;
        }

        public async Task Execute(Request request)
        {
            using (var context = _contextFactory.Create())
            {
                foreach (var day in request.Dates)
                {
                    var requestDto = new RequestDTO()
                    {
                        Id = day.RequestId,
                        IsApproved = true,
                        PeriodId = request.Period.PeriodId
                    };

                    context.Update(requestDto);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
