﻿using RequestViewer.Domain.Models;
using System.Threading.Tasks;

namespace RequestViewer.Domain.Commands
{
    public interface IUpdateRequestCommand
    {
        Task Execute(Request request);
    }
}
