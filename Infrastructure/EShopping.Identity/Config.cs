﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace EShopping.Identity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalogapi"),
                new ApiScope("basketapi"),

            };
        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                //Listado de microservicio
                new ApiResource("Catalog", "Catalogo.API")
                {
                    //machine to machine flow
                    Scopes={ "catalogapi" }
                },
                new ApiResource("Basket", "Basket.API")
                {
                    //machine to machine flow
                    Scopes={ "basketapi" }
                },
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                //machine to machine flow
                new Client
                {
                    ClientName="Catalog API Client",
                    ClientId="CatalogApiClient",
                    ClientSecrets = {new Secret("5c6eb3b4-61a7-4668-ac57-2b4591ec26d2".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"catalogapi", "basketapi" }
                },
            };
    }
}