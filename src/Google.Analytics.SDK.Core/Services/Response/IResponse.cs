﻿// Copyright (c) Linda Lawton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Google.Analytics.SDK.Core.Services.Response
{
    public interface IResponse
    {
        bool IsValid();
        bool IsError();
    }
}