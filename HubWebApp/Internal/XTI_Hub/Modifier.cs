﻿using Microsoft.EntityFrameworkCore;
using XTI_App.Abstractions;
using XTI_HubDB.Entities;

namespace XTI_Hub;

public sealed class Modifier : IModifier
{
    private readonly HubFactory factory;
    private readonly ModifierEntity record;

    internal Modifier(HubFactory factory, ModifierEntity record)
    {
        this.factory = factory;
        this.record = record ?? new ModifierEntity();
        ID = this.record.ID;
    }

    public int ID { get; }
    public ModifierKey ModKey() => new ModifierKey(record.ModKey);
    public string TargetKey { get => record.TargetKey; }
    public int TargetID() => int.Parse(TargetKey);

    public bool IsForCategory(ModifierCategory modCategory) => modCategory.ID == record.CategoryID;

    public Task SetDisplayText(string displayText)
    {
        return factory.DB.Modifiers.Update(record, r =>
        {
            r.DisplayText = displayText;
        });
    }

    public async Task<Modifier> DefaultModifier()
    {
        Modifier? defaultModifier;
        if (ModKey().Equals(ModifierKey.Default))
        {
            defaultModifier = this;
        }
        else
        {
            var appID = await factory.DB
                .ModifierCategories
                .Retrieve()
                .Where(modCat => modCat.ID == record.CategoryID)
                .Select(modCat => modCat.AppID)
                .FirstAsync();
            var app = await factory.Apps.App(appID);
            defaultModifier = await app.DefaultModifier();
        }
        return defaultModifier;
    }

    public ModifierModel ToModel() => new ModifierModel
    {
        ID = ID,
        CategoryID = record.CategoryID,
        ModKey = ModKey().DisplayText,
        TargetKey = TargetKey,
        DisplayText = record.DisplayText
    };

    public override string ToString() => $"{nameof(Modifier)} {ID}";
}