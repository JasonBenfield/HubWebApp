namespace XTI_PublishTool
{
    internal sealed class IndexFile
    {
        private readonly string dir;

        public IndexFile(string dir)
        {
            this.dir = dir;
        }

        public void Write()
        {
            var indexFile = Path.Combine(dir, "main.ts");
            if (File.Exists(indexFile)) { throw new Exception("index.ts already exists"); }
            using var writer = new StreamWriter(indexFile, false);
            writeIndexDeclaration(writer, new string[0], dir);
        }

        private void writeIndexDeclaration(StreamWriter writer, string[] parentDirs, string dir)
        {
            var joinedParentDirs = parentDirs.Any()
                ? string.Join("/", parentDirs)
                : "";
            var location = parentDirs.Any()
                ? $"./{joinedParentDirs}/"
                : "./";
            var filePaths = Directory.GetFiles(dir, "*.ts")
                .Where
                (
                    f => !f.EndsWith(".d.ts", StringComparison.OrdinalIgnoreCase) &&
                        !Path.GetFileName(f).Equals("_references.ts", StringComparison.OrdinalIgnoreCase)
                );
            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                writer.WriteLine($"export * from \"{location}{fileName}\";");
            }
            foreach (var childDir in Directory.GetDirectories(dir))
            {
                var childParentDirs = parentDirs
                    .Union(new[] { new DirectoryInfo(childDir).Name })
                    .ToArray();
                writeIndexDeclaration(writer, childParentDirs, childDir);
            }
        }

    }
}
