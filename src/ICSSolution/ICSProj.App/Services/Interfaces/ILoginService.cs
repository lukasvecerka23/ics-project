// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ICSProj.App.Services;

public interface ILoginService
{
    Guid CurrentUserId { get; set; }
}
