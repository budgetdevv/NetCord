﻿using NetCord.JsonModels;

namespace NetCord;

public class RoleMenu(JsonComponent jsonModel) : EntityMenu(jsonModel)
{
    public IReadOnlyList<ulong> DefaultValues { get; } = jsonModel.DefaultValues.SelectOrEmpty(d => d.Id).ToArray();
}
