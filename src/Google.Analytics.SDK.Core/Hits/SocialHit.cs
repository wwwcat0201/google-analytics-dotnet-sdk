﻿// Copyright (c) Linda Lawton. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
namespace Google.Analytics.SDK.Core.Hits
{
    public class SocialHit : SocialHitBase
    {
        public SocialHit(string socialNetwork, string socialAction, string socialActionTarget) : base(socialNetwork, socialAction, socialActionTarget)
        {
            
        }
    }
}