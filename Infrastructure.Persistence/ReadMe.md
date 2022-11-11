Create Migration
dotnet ef migrations add AddInitProject --project .\Infrastructure\Infrastructure.Persistence\Infrastructure.Persistence.csproj --startup-project .\Presentation\WebApi.Admin\WebApi.Admin.csproj --context AdminApplicationDbContext
Update Migration
dotnet ef database update  --project .\Infrastructure\Infrastructure.Persistence\Infrastructure.Persistence.csproj --startup-project .\Presentation\WebApi.Admin\WebApi.Admin.csproj --context AdminApplicationDbContext
Remove to Step One Prev
dotnet ef migrations remove --project .\Infrastructure\Infrastructure.Persistence\Infrastructure.Persistence.csproj --startup-project .\Presentation\WebApi.Admin\WebApi.Admin.csproj --context AdminApplicationDbContext


//start migration in linux
export ASPNETCORE_ENVIRONMENT=Production && dotnet exec --runtimeconfig WebApi.Admin.runtimeconfig.json --depsfile WebApi.Admin.deps.json "/root/.dotnet/tools/.store/dotnet-ef/5.0.7/dotnet-ef/5.0.7/tools/netcoreapp3.1/any/tools/netcoreapp2.0/any/ef.dll" --verbose database update --context AdminApplicationDbContext --assembly Infrastructure.Persistence.dll --startup-assembly WebApi.Admin.dll
