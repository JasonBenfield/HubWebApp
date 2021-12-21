namespace XTI_PublishTool
{
    internal sealed class IndexDeclarationFile
    {
        private readonly string baseDir;

        public IndexDeclarationFile(string baseDir)
        {
            this.baseDir = baseDir;
        }

        public void Write()
        {
            string indexContent = "";
            var indexDeclFile = Path.Combine(baseDir, "index.d.ts");
            if (File.Exists(indexDeclFile))
            {
                using (var reader = new StreamReader(indexDeclFile))
                {
                    indexContent = reader.ReadToEnd();
                }
            }
            using (var writer = new StreamWriter(indexDeclFile, false))
            {
                writeIndexDeclaration(writer, new string[0], baseDir);
                writer.WriteLine(indexContent);
            }
        }

        private void writeIndexDeclaration(StreamWriter writer, string[] parentDirs, string dir)
        {
            var joinedParentDirs = parentDirs.Any()
                ? string.Join("/", parentDirs)
                : "";
            var location = parentDirs.Any()
                ? $"./{joinedParentDirs}/"
                : "./";
            var filePaths = Directory.GetFiles(dir, "*.d.ts")
                .Where
                (
                    f => 
                        !f.Equals(Path.Combine(baseDir, "index.d.ts"), StringComparison.OrdinalIgnoreCase) &&
                        !f.Equals(Path.Combine(baseDir, "main.d.ts"), StringComparison.OrdinalIgnoreCase) &&
                        !f.Equals(Path.Combine(baseDir, "_references.d.ts"), StringComparison.OrdinalIgnoreCase)
                );
            foreach (var filePath in filePaths)
            {
                var fileName = Path.GetFileName(filePath);
                writer.WriteLine($"/// <reference path=\"{location}{fileName}\" />");
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
