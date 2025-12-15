WiX v3 -> v4 quick setup (generated)

Files created:
- Product_v4.wxs : Converted WiX v4 schema
- Bundle_v4.wxs  : Converted WiX v4 schema
- EFAMAgent_v4.wixproj      : MSI project (SDK-style)
- EFAM_Bundle_v4.wixproj    : Bootstrapper project (SDK-style)

Build (Developer Command Prompt):
  msbuild EFAMAgent_v4.wixproj -t:Build -p:Configuration=Release
  msbuild EFAM_Bundle_v4.wixproj -t:Build -p:Configuration=Release

Notes:
- If build errors mention unknown elements/attributes, compare with v4 docs and adjust.
- Ensure required wixext packages are referenced (UI/Util/Bal). Add more as needed:
    WixToolset.Firewall.wixext, WixToolset.Com.wixext, etc.
- Update FinalOutputName in each .wixproj to your desired filenames.
