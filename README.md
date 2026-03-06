# Vessel (.NET)

A cross-platform site-specific browser built with [Photino.NET](https://www.tryphotino.io/). Wraps any web URL in a native window with keyboard shortcuts, zoom control, and always-on-top support.

Port of [vessel-mac](https://github.com/user/vessel-mac) (Swift/AppKit) to .NET for cross-platform use (macOS, Windows, Linux).

## Requirements

- .NET 10.0 SDK (or later)

## Quick Start

```bash
# Run with a URL
dotnet run --project src/Vessel -- --url https://example.com --title "My App"

# Run with config file (place config.json in CWD or next to binary)
dotnet run --project src/Vessel
```

## Configuration

### config.json

```json
{
  "url": "https://example.com",
  "title": "Vessel",
  "width": 1280,
  "height": 800,
  "alwaysOnTop": false
}
```

Config is loaded from (first found wins):
1. Next to the executable
2. macOS `.app` bundle `Resources/` directory
3. Current working directory

### CLI Flags

CLI flags override config file values:

| Flag | Description |
|---|---|
| `--url <url>` | Target URL |
| `--title <title>` | Window title |
| `--width <px>` | Window width |
| `--height <px>` | Window height |
| `--float` | Enable always-on-top |
| `--no-float` | Disable always-on-top |

## Keyboard Shortcuts

| Shortcut | Action |
|---|---|
| Cmd/Ctrl+R | Reload |
| Cmd/Ctrl+Shift+R | Hard Reload |
| Cmd/Ctrl+[ | Back |
| Cmd/Ctrl+] | Forward |
| Cmd/Ctrl++ | Zoom In |
| Cmd/Ctrl+- | Zoom Out |
| Cmd/Ctrl+0 | Reset Zoom |
| Ctrl+Cmd+F / F11 | Toggle Fullscreen |
| Cmd/Ctrl+Alt+T | Toggle Always on Top |
| Cmd/Ctrl+M | Minimize |
| Cmd/Ctrl+Q | Quit |

## Building

```bash
# Run tests
dotnet test tests/Vessel.Tests/

# Build self-contained binary for current platform
./scripts/build.sh

# Build macOS .app bundle
./scripts/build.sh --bundle

# Build for a specific platform
./scripts/build.sh --rid win-x64
./scripts/build.sh --rid linux-x64
./scripts/build.sh --rid osx-arm64 --bundle
```

## Architecture

Uses an **iframe bridge** pattern: the target URL is loaded in an iframe inside a bootstrap HTML page. A JavaScript bridge (`bridge.js`) handles keyboard shortcuts, title observation, and navigation. JS-to-C# communication uses `window.external.sendMessage()`.

Interfaces follow the **Interface Segregation Principle (ISP)** - each handler depends only on the narrow interface it needs (e.g., `IWindowStateController`, `IZoomController`).

### Known Limitation

Sites that set `X-Frame-Options: DENY` or `SAMEORIGIN` will not load in the iframe. This is acceptable for the primary use case (intranet/internal sites).
