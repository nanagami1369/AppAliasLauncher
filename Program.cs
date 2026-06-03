using System.Diagnostics;

// ヘルプページ表示
if (args.Length == 0)
{
    Console.Error.WriteLine("使い方: AppAliasLauncher.exe <エイリアス名.exe> [引数...]");
    return 1;
}


var ariasName = args[0];

// パスセパレータが含まれている場合は拒否
if (ariasName.Contains(Path.DirectorySeparatorChar) || ariasName.Contains(Path.AltDirectorySeparatorChar))
{
    Console.Error.WriteLine($"エラー: ファイル名のみ指定してください。パスは含められません。({ariasName})");
    return 1;
}
// .exe以外は拒否
if (!ariasName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
{
    Console.Error.WriteLine($"エラー: exeファイルのみ指定可能です。({ariasName})");
    return 1;
}

var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
var aliasPath = Path.Combine(localAppData, "Microsoft", "WindowsApps", ariasName);

if (!File.Exists(aliasPath))
{
    Console.Error.WriteLine($"エラー: アプリ実行エイリアスが見つかりません。({aliasPath})");
    return 1;
}

// args[1..] を引数として結合
var arguments = args.Length > 1
    ? string.Join(" ", args[1..])
    : string.Empty;

// UseShellExecute = true によりOSが再解析ポイントを解釈するため、
// コンソールウィンドウは表示されない
Process.Start(new ProcessStartInfo
{
    FileName = aliasPath,
    Arguments = arguments,
    UseShellExecute = true,
});

return 0;
