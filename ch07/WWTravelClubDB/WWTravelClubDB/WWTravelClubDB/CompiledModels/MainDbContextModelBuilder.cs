﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable enable

namespace WWTravelClubDB.CompiledModels
{
    public partial class MainDbContextModel
    {
        partial void Initialize()
        {
            var destination = DestinationEntityType.Create(this);
            var package = PackageEntityType.Create(this);

            PackageEntityType.CreateForeignKey1(package, destination);

            DestinationEntityType.CreateAnnotations(destination);
            PackageEntityType.CreateAnnotations(package);

            AddAnnotation("ProductVersion", "7.0.9");
            AddAnnotation("Relational:MaxIdentifierLength", 128);
            AddAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
        }
    }
}
