# CliHelper

A C# library to help build console applications (command line interface).

[![NuGet Version][nuget-badge]][nuget]
[![GitHub Release Version][tag-badge]][releases]
[![License][license-badge]][license]
![C# Version][cs-ver-badge]
![Framework Version][framework-ver-badge]
![.NETFramework Version][net-ver-badge]

---

## Usage Example

```C#
using System;
using CliHelper;

namespace PrintApp
{
    public static class PrintAppMain
    {
        public static void Main(params string[] args)
        {
            var app = new CliApplication("PrintApp");

            var runCommand = app.RegisterCommand("print", 
                "Print something in the console.", Run);

            runCommand.RegisterParameter("message", "Write  message.", 'm');
            runCommand.RegisterFlag("error", "Print in red.", 'e');
        }

        private static void Run(ParameterWithValues[] parameters, Flag[] flags)
        {
            var error = flags.HasFlag("error");

            if (error) {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            var message = parameters.GetParameterByName("message");

            Console.WriteLine(message?.Values[0]);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
```

After build, do something like:

```bash
./PrintApp print --message "Hello!"

# or

./PrintApp print -m "Error!" --error
```

[nuget]: https://www.nuget.org/packages/CliHelper

[releases]: https://github.com/brunurd/CliHelper/releases
[changelog]: CHANGELOG.md
[license]: LICENSE

[nuget-badge]: https://img.shields.io/nuget/v/CliHelper
[license-badge]: https://img.shields.io/github/license/brunurd/CliHelper
[tag-badge]: https://img.shields.io/github/v/tag/brunurd/CliHelper?sort=semver
[cs-ver-badge]: https://img.shields.io/badge/C%23-7.3-621ee5
[framework-ver-badge]: https://img.shields.io/badge/framework-netstandard2.0-621ee5
[net-ver-badge]: https://img.shields.io/badge/.NET_Framework-4.6.1-621ee5
