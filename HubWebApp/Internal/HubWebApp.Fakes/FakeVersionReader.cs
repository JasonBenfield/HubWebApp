using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTI_Hub.Abstractions;
using XTI_HubSetup;

namespace HubWebApp.Fakes;

public sealed class FakeVersionReader : IVersionReader
{
    private readonly List<XtiVersionModel> versions = new();

    public FakeVersionReader(params XtiVersionModel[] versions)
    {
        this.versions.AddRange(versions);
    }

    public Task<XtiVersionModel[]> Versions() => Task.FromResult(versions.ToArray());
}
