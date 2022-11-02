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

### debugging in vs code
* SWA: Run werd-funk (swa-cli.config.json)
* client, razor, blazor
  + add this in api/Properties/local.setttings.json?
  ```json
    "Host": {
        "LocalHttpPort": 7071,
        "CORS": "*",
        "CORSCredentials": false
    }
  ```
  + api --> func start
  + use vscode debugger --> Launch and Debug Standalone Blazor WebAssembly App
* api, azure funcs
  + use vscode debugger --> Attach to .NET Functions
  + vscode REST client extension (*.http) to GET/POST/PUT/etc to api

## when stuff craps out

### inotify issue
```
System.IO.FileSystem.Watcher: The configured user limit (128) on the number of inotify instances has been reached, or the per-process limit on the number of open file descriptors has been reached.
```
* http://blog.travisgosselin.com/configured-user-limit-inotify-instances/
  + echo fs.inotify.max_user_watches=524288 | sudo tee -a /etc/sysctl.conf && sudo sysctl -p
* https://stackoverflow.com/questions/43469400/asp-net-core-the-configured-user-limit-128-on-the-number-of-inotify-instance
  + echo fs.inotify.max_user_instances=524288 | sudo tee -a /etc/sysctl.conf && sudo sysctl -p
* https://www.appsloveworld.com/entity-framework-core/100/1/asp-net-core-the-configured-user-limit-128-on-the-number-of-inotify-instances
  + echo fs.inotify.max_user_instances=524288 | sudo tee -a /etc/sysctl.conf && sudo sysctl -p
* https://anthonyliriano.com/2021/04/08/21-configured-user-limit-128-on-the-number-of-inotify-instances-has-been-reached.html
  + echo fs.inotify.max_user_instances=524288 | sudo tee -a /etc/sysctl.conf && sudo sysctl -p
* https://github.com/dotnet/aspnetcore/issues/8449
  + echo fs.inotify.max_user_instances=524288 | sudo tee -a /etc/sysctl.conf && sudo sysctl -p

### address already in use
```
System.IO.IOException: Failed to bind to address http://127.0.0.1:5002: address already in use.
```
* https://techstrology.com/unhandled-exception-system-io-ioexception-failed-to-bind-to-address-http-127-0-0-15000-address-already-in-use/
  + lsof -i:<port>
  + kill -9 <pid>