#!/usr/bin/env bash
set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "$SCRIPT_DIR/.." && pwd)"
PROJECT="$ROOT_DIR/src/Vessel/Vessel.csproj"
OUTPUT_DIR="$ROOT_DIR/publish"

# Defaults
RID=""
CONFIGURATION="Release"
BUNDLE=false
APP_NAME="Vessel"

usage() {
    echo "Usage: $0 [options]"
    echo ""
    echo "Options:"
    echo "  --rid <RID>        Runtime identifier (e.g., osx-arm64, osx-x64, win-x64, linux-x64)"
    echo "  --config <cfg>     Build configuration (default: Release)"
    echo "  --bundle           Create macOS .app bundle (requires osx RID)"
    echo "  --name <name>      App name (default: Vessel)"
    echo "  -h, --help         Show this help"
    exit 0
}

while [[ $# -gt 0 ]]; do
    case "$1" in
        --rid) RID="$2"; shift 2 ;;
        --config) CONFIGURATION="$2"; shift 2 ;;
        --bundle) BUNDLE=true; shift ;;
        --name) APP_NAME="$2"; shift 2 ;;
        -h|--help) usage ;;
        *) echo "Unknown option: $1"; exit 1 ;;
    esac
done

# Auto-detect RID if not specified
if [[ -z "$RID" ]]; then
    case "$(uname -s)-$(uname -m)" in
        Darwin-arm64) RID="osx-arm64" ;;
        Darwin-x86_64) RID="osx-x64" ;;
        Linux-x86_64) RID="linux-x64" ;;
        Linux-aarch64) RID="linux-arm64" ;;
        *) echo "Could not detect RID. Please specify --rid"; exit 1 ;;
    esac
    echo "Auto-detected RID: $RID"
fi

PUBLISH_DIR="$OUTPUT_DIR/$RID"

echo "Building $APP_NAME for $RID ($CONFIGURATION)..."
dotnet publish "$PROJECT" \
    -c "$CONFIGURATION" \
    -r "$RID" \
    --self-contained true \
    -o "$PUBLISH_DIR" \
    -p:PublishSingleFile=true \
    -p:IncludeNativeLibrariesForSelfExtract=true

echo "Published to: $PUBLISH_DIR"

# macOS .app bundle
if [[ "$BUNDLE" == true && "$RID" == osx-* ]]; then
    BUNDLE_DIR="$OUTPUT_DIR/$APP_NAME.app"
    CONTENTS="$BUNDLE_DIR/Contents"
    MACOS="$CONTENTS/MacOS"
    RESOURCES="$CONTENTS/Resources"

    echo "Creating macOS .app bundle..."
    rm -rf "$BUNDLE_DIR"
    mkdir -p "$MACOS" "$RESOURCES"

    # Copy binary
    cp "$PUBLISH_DIR/Vessel" "$MACOS/$APP_NAME"

    # Copy config if exists
    if [[ -f "$ROOT_DIR/config.json" ]]; then
        cp "$ROOT_DIR/config.json" "$RESOURCES/config.json"
    fi

    # Create Info.plist
    cat > "$CONTENTS/Info.plist" << PLIST
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
    <key>CFBundleName</key>
    <string>$APP_NAME</string>
    <key>CFBundleDisplayName</key>
    <string>$APP_NAME</string>
    <key>CFBundleIdentifier</key>
    <string>com.vessel.app</string>
    <key>CFBundleVersion</key>
    <string>1.0.0</string>
    <key>CFBundleShortVersionString</key>
    <string>1.0.0</string>
    <key>CFBundleExecutable</key>
    <string>$APP_NAME</string>
    <key>CFBundlePackageType</key>
    <string>APPL</string>
    <key>LSMinimumSystemVersion</key>
    <string>12.0</string>
    <key>NSHighResolutionCapable</key>
    <true/>
</dict>
</plist>
PLIST

    echo "Bundle created: $BUNDLE_DIR"
fi

echo "Done!"
