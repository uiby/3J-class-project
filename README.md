
Ecoris
=======

3Jのクラス企画

Features
--------

- 直感的な操作による3Dテトリス
- 手の動きを検出するためにLeap Motionを使用
- ゲームの作成にはUnityを使用
- エコの要素を追加したテトリス (eco + tetris = Ecoris)
- 缶のブロックは潰したり、生ゴミのブロックは降ったりすることで、小さいブロックにすることができる

Draft
-----

3Dテトリスに関する設計案

- [3Dテトリス設計草案](https://github.com/13J-Programmers/3J_class_project/blob/master/doc/overview.md)
- [3Dテトリス詳細設計 (latest version)](https://github.com/13J-Programmers/3J_class_project/blob/master/doc/design.md)

Ecoris Projectのドキュメント（自動生成）

- [Ecoris - documentation](http://tex2e.sakura.ne.jp/3j-class-project/html/index.html)
	

to Committer
------------

#### Ignore

Unity内で生成される以下のファイルは中間ファイルであり、管理する必要がないので、コミットしないことにします。(.gitignoreに追加)
	
	# temporary files

	/[Ll]ibrary/
	/[Tt]emp/
	/[Oo]bj/
	/[Bb]uild/
	
	# Autogenerated VS/MD solution and project files
	*.csproj
	*.unityproj
	*.sln
	*.suo
	*.tmp
	*.user
	*.userprefs
	*.pidb
	*.booproj
	
	# Unity3D generated meta files
	*.pidb.meta
	
	# Unity3D Generated File On Crash Reports
	sysinfo.txt

Unityが自動生成するmetaファイルは、必ずコミットすることにします。

#### Commit

変更の意図がわかるように、commitの精度を心がけましょう。

例えば、メソッド1つを追加する作業だけでも、追加すべき箇所やクラスを特定した後に、
開発者は次のようなことを日頃から行っているのではないでしょうか。

- 近くのコードのインデントの修正
- 変数の単語の間違いを修正
- 今回の作業として追加すべきメソッドを追加

これらの一連の流れを1つのcommitとするのは、望ましいcommitの精度とは言えません。
それぞれ3つのcommitとして分けたほうが、それぞれの差分の意図が伝わるはずです。

[Software Design 2015年6月号 第1特集 楽しく始めるGit & GitHub入門] より

#### Pull Request

Pull Requestがまだ作業中の場合、`[WIP]`（Work In Progress）をタイトル冒頭に付けるようにしましょう。
こうすることにより、誤ってmergeすることを防ぐことができます。

#### Work Flow (GitHub Flow)

理想的なワークフローの流れ

1. 新しい作業をするときはmasterブランチから、これから行う作業の特徴を表した名前でブランチを作成する
2. 作成したローカルリポジトリのブランチにcommitする
3. 同名のブランチをGitHubのリポジトリに作成し、定期的にpushする
4. 助けて欲しいときや、フィードバックが欲しいときはPull Requestを作成し、Pull Requestでやりとりする
5. ほかの開発者がレビューし、作業終了を確認したらmasterブランチにマージする

Contributing
------------

1. Fork it ( https://github.com/13J-Programmers/3J_class_project )
2. Create your feature branch (git checkout -b my-new-feature)
3. Commit your changes (git commit -am 'Add some feature')
4. Push to the branch (git push origin my-new-feature)
5. Create a new Pull Request

Contact us
----------

If you have any questions, please ask us (slack, issues, class mail, line)


