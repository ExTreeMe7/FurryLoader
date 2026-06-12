using Marsey.Patches;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Marsey.Config;
using Marsey.Misc;

namespace Marsey.PatchAssembly;

/// <summary>
/// Manages patch lists.
/// </summary>
public static class PatchListManager
{
    private static readonly List<IPatch> _patches = new List<IPatch>();
    private static List<PatchFileSnapshot> _lastPatchSnapshot = new List<PatchFileSnapshot>();
    private static bool _hasPatchSnapshot;

    /// <summary>
    /// Checks if the patch folder contents match the cached patch list.
    /// If not - resets the lists.
    /// </summary>
    public static void RecheckPatches()
    {
        List<PatchFileSnapshot> currentSnapshot = FileHandler
            .GetPatches(new[] { MarseyVars.MarseyPatchFolder })
            .Select(PatchFileSnapshot.FromPath)
            .OrderBy(patch => patch.Path)
            .ToList();

        if (!_hasPatchSnapshot || !currentSnapshot.SequenceEqual(_lastPatchSnapshot))
        {
            ResetList();
        }

        _lastPatchSnapshot = currentSnapshot;
        _hasPatchSnapshot = true;
    }

    /// <summary>
    /// Adds a patch to the list if it is not already present.
    /// </summary>
    /// <param name="patch">The patch to add.</param>
    public static void AddPatchToList(IPatch patch)
    {
        if (_patches.Any(p => p.Asmpath == patch.Asmpath)) return;

        string fileName = Path.GetFileName(patch.Asmpath);
        MarseyLogger.Log(MarseyLogger.LogType.TRCE, $"Loaded {patch.Name} ({fileName})");
        _patches.Add(patch);
    }

    /// <summary>
    /// Returns the list of patches of a specific type.
    /// </summary>
    public static List<T> GetPatchList<T>() where T : IPatch
    {
        return _patches.OfType<T>().ToList();
    }

    /// <summary>
    /// Clears the list of patches.
    /// </summary>
    public static void ResetList()
    {
        _patches.Clear();
        _lastPatchSnapshot = new List<PatchFileSnapshot>();
        _hasPatchSnapshot = false;
    }

    private sealed record PatchFileSnapshot(string Path, long Length, long LastWriteTimeUtcTicks, string Sha256)
    {
        public static PatchFileSnapshot FromPath(string path)
        {
            string fullPath = System.IO.Path.GetFullPath(path);
            FileInfo info = new FileInfo(fullPath);
            return new PatchFileSnapshot(
                fullPath,
                info.Exists ? info.Length : -1,
                info.Exists ? info.LastWriteTimeUtc.Ticks : -1,
                info.Exists ? ComputeSha256(fullPath) : string.Empty);
        }

        private static string ComputeSha256(string path)
        {
            try
            {
                using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
                byte[] hash = SHA256.HashData(stream);
                return System.Convert.ToHexString(hash);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
