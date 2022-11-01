# werd-funk

### scaffold
* already installed Azure Functions Core Tools
* GitHub : create new repo
* ensure GitHub auth token is good on kdev
* https://github.com/kellysieben/werd-funk.git
* dotnet new blazorwasm -o client
* dotnet new classlib -o common
* vscode
* Azure Static Web Apps: Create HTTP function…
* Add common project to other projects:
```xml
  <ItemGroup>
    <ProjectReference Include="..\common\common.csproj" />
  </ItemGroup>
```
* swa init
* appsettings.Development.json (capital 'D' is important!) to wwwroot
    + "API_Prefix": "http://localhost:7071"   <-- Azure Functions HTTP default local port is 7071
* added API_Prefix lookup to Client/Program.cs
```cs
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });
```
* add to .vscode/launch.json
```
"url": "http://localhost:5034"
```
* client/Properties/launchSettings.json
  + removed https url from Profiles:client
  + keep http://localhost:5034
* 