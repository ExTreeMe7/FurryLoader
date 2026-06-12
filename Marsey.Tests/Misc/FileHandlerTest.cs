using Marsey.Misc;

namespace Marsey.MiscTests;

[TestFixture]
public sealed class FileHandlerTest
{
    private string? _tempDirectory;

    [SetUp]
    public void SetUp()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), "MarseyFileHandlerTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempDirectory);
    }

    [TearDown]
    public void TearDown()
    {
        if (_tempDirectory == null)
            return;

        try
        {
            Directory.Delete(_tempDirectory, recursive: true);
        }
        catch
        {
            // Best-effort cleanup. Test failure should be about behavior, not temp file cleanup.
        }
    }

    [Test]
    public void CreateShadowCopy_CreatesNewSnapshot_WhenSourceChangesWhileOldShadowIsLocked()
    {
        string sourcePath = Path.Combine(_tempDirectory!, "Patch.dll");
        File.WriteAllText(sourcePath, "version-one");

        string firstShadowPath = FileHandler.CreateShadowCopy(sourcePath);

        Assert.That(firstShadowPath, Is.Not.EqualTo(Path.GetFullPath(sourcePath)));
        Assert.That(File.ReadAllText(firstShadowPath), Is.EqualTo("version-one"));

        using FileStream lockedShadow = new FileStream(firstShadowPath, FileMode.Open, FileAccess.Read, FileShare.None);

        File.WriteAllText(sourcePath, "version-two");

        string secondShadowPath = FileHandler.CreateShadowCopy(sourcePath);

        Assert.That(secondShadowPath, Is.Not.EqualTo(firstShadowPath));
        Assert.That(File.ReadAllText(secondShadowPath), Is.EqualTo("version-two"));
    }
}
