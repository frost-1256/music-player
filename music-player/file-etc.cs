using System;
using System.IO;
using System.Linq;

namespace file_search {
    // made by claude sonnet 4.6 extended 
    public class file_search {
                /// <summary>スキャン対象のルートパス</summary>
        public string RootPath { get; private set; }

        /// <summary>取得したファイルパスの配列</summary>
        public string[] Files { get; private set; } = Array.Empty<string>();

        /// <summary>取得したディレクトリパスの配列</summary>
        public string[] Directories { get; private set; } = Array.Empty<string>();

        public file_search(string rootPath)
        {
            if (string.IsNullOrWhiteSpace(rootPath))
                throw new ArgumentException("パスを指定してください。", nameof(rootPath));

            RootPath = rootPath;
        }

        /// <summary>
        /// 指定ディレクトリをスキャンしてファイル・ディレクトリを取得する
        /// </summary>
        /// <param name="searchPattern">検索パターン（例: "*", "*.txt"）</param>
        /// <param name="recursive">サブディレクトリを再帰的に検索するか</param>
        public void Scan(string searchPattern = "*", bool recursive = false)
        {
            if (!Directory.Exists(RootPath))
                throw new DirectoryNotFoundException($"ディレクトリが見つかりません: {RootPath}");

            var option = recursive
                ? SearchOption.AllDirectories
                : SearchOption.TopDirectoryOnly;

            Files       = Directory.GetFiles(RootPath, searchPattern, option);
            Directories = Directory.GetDirectories(RootPath, searchPattern, option);
        }

        /// <summary>
        /// ファイルとディレクトリをまとめて返す
        /// </summary>
        public string[] GetAll() => Files.Concat(Directories).ToArray();

        /// <summary>結果を整形して表示する</summary>
        public void PrintResults()
        {
            Console.WriteLine($"\n📁 スキャン対象: {RootPath}");
            Console.WriteLine(new string('─', 50));

            Console.WriteLine($"\n【ディレクトリ】 {Directories.Length} 件");
            foreach (var dir in Directories)
                Console.WriteLine($"  📂 {dir}");

            Console.WriteLine($"\n【ファイル】 {Files.Length} 件");
            foreach (var file in Files)
                Console.WriteLine($"  📄 {file}");

            Console.WriteLine($"\n合計: {GetAll().Length} 件\n");
        }
    }
};