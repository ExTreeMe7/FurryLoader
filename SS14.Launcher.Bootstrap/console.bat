@echo off
if exist "FurryLoader.exe" (
    "FurryLoader.exe" --debug
) else (
    "MusyaLoader.exe" --debug
)

PAUSE
