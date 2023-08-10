using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularySheet.Application.Commons.Interfaces;
using VocabularySheet.Application.Words.Commands;
using VocabularySheet.Infrastructure.Data;

namespace VocabularySheet.Maui;

public class DebugService
{
    private readonly IMediator _mediator;

    public DebugService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Foo()
    {
        await _mediator.Send(new SynchronizeWordsCommand());
    }
}
