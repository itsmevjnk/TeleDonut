# TeleDonut

A Telnet server written in C# that beams [`donut.c`](https://www.a1k0n.net/2021/01/13/optimizing-donut.html) to its connected clients.

## Installation

[.NET](https://dotnet.microsoft.com/en-us/download) is required to build this program.

```
git clone https://github.com/itsmevjnk/TeleDonut.git
cd TeleDonut
dotnet build
```

## Usage

Start this program as you would start any .NET program, either by using `dotnet run`, or executing the compiled executable (found in `bin`).

The server will listen from any interfaces on port 23, with the ability to specify the listening IP and port to be added in the future.

## Contributions

Contributions via pull requests or issues are welcome.

## License

The `Donut.cs` source code, which contains the C# port of Andy Sloane's `donut.c`, is in the public domain under Unlicense, whose terms is described in the `LICENSE.donut` file.

The remaining files in this repository are licensed under the MIT license; refer to `LICENSE` for more information.

