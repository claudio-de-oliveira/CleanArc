namespace CleanArc
{
    public abstract class FileGenerator
    {
        private readonly string path;
        private readonly string filename;

        public FileGenerator(string path, string filename)
        {
            this.path = path;
            this.filename = filename;
            CreateDirectory(path);
        }

        protected abstract List<string> Generate();

        private static void CreateDirectory(string path)
        {
            string[] folders = path.Split('\\');
            string p = folders[0];
            for (int i = 1; i < folders.Length; i++)
            {
                if (!string.IsNullOrEmpty(p))
                {
                    if (!Directory.Exists(p))
                        Directory.CreateDirectory(p);
                    p += $"\\{folders[i]}";
                }
            }
            if (!Directory.Exists(p) && !string.IsNullOrEmpty(p))
                Directory.CreateDirectory(p);
        }

        public bool CreateFile()
        {
            var text = Generate();

            if (text == null)
                return false;

            CreateDirectory(path);

            var fname = Path.Combine(path, filename);
            if (File.Exists(fname))
                return false;

            var textWriter = new StreamWriter(fname);

            foreach (var line in text)
                textWriter.WriteLine(line);

            textWriter.Close();

            return true;
        }
    }
}
