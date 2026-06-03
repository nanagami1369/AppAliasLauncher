using System.Diagnostics;
using System.Windows.Forms;

// ヘルプページ表示
if (args.Length == 0)
{
    MessageBox.Show("使い方: AppAliasLauncher.exe <エイリアス名.exe> [引数...]", "AppAliasLauncher", MessageBoxButtons.OK, MessageBoxIcon.Information);
    return 1;
}

var ariasName = args[0];

// パスセパレータが含まれている場合は拒否
if (ariasName.Contains(Path.DirectorySeparatorChar) || ariasName.Contains(Path.AltDirectorySeparatorChar))
{
    MessageBox.Show($"エラー: ファイル名のみ指定してください。パスは含められません。({ariasName})", "AppAliasLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
    return 1;
}
// .exe以外は拒否
if (!ariasName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
{
    MessageBox.Show($"エラー: exeファイルのみ指定可能です。({ariasName})", "AppAliasLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
    return 1;
}

var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
var aliasPath = Path.Combine(localAppData, "Microsoft", "WindowsApps", ariasName);

if (!File.Exists(aliasPath))
{
    MessageBox.Show($"エラー: アプリ実行エイリアスが見つかりません。({aliasPath})", "AppAliasLauncher", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
