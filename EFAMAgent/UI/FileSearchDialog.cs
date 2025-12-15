using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace Link.EFAM.Agent.UI
{
    using Link.EFAM.Agent.Properties;
    using Link.EFAM.Core;

    /// <summary>
    /// 네트워크 드라이브에서 파일을 검색하는 대화 상자
    /// </summary>
    public partial class FileSearchDialog : KryptonForm
    {
        private BackgroundWorker searchWorker = null;
        private bool isSearching = false;
        private List<SearchResult> searchResults = new List<SearchResult>();

        /// <summary>
        /// FileSearchDialog 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        public FileSearchDialog()
        {
            InitializeComponent();
            InitializeSearchWorker();
        }

        /// <summary>
        /// 백그라운드 검색 작업자를 초기화합니다.
        /// </summary>
        private void InitializeSearchWorker()
        {
            searchWorker = new BackgroundWorker();
            searchWorker.WorkerReportsProgress = true;
            searchWorker.WorkerSupportsCancellation = true;
            searchWorker.DoWork += SearchWorker_DoWork;
            searchWorker.ProgressChanged += SearchWorker_ProgressChanged;
            searchWorker.RunWorkerCompleted += SearchWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// 검색 버튼 클릭 이벤트 핸들러
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchKeyword.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                KryptonMessageBox.Show("검색어를 입력해주세요.", "파일 검색",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearchKeyword.Focus();
                return;
            }

            if (!chkSearchFileName.Checked && !chkSearchFileContent.Checked)
            {
                KryptonMessageBox.Show("검색 옵션을 선택해주세요.", "파일 검색",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isSearching)
            {
                // 검색 중지
                searchWorker.CancelAsync();
                btnSearch.Text = "검색";
                isSearching = false;
            }
            else
            {
                // 검색 시작
                lstSearchResults.Items.Clear();
                searchResults.Clear();

                SearchParameters parameters = new SearchParameters
                {
                    Keyword = keyword,
                    SearchFileName = chkSearchFileName.Checked,
                    SearchFileContent = chkSearchFileContent.Checked
                };

                btnSearch.Text = "중지";
                isSearching = true;
                progressBar.Visible = true;
                lblStatus.Text = "검색 중...";

                searchWorker.RunWorkerAsync(parameters);
            }
        }

        /// <summary>
        /// 취소 버튼 클릭 이벤트 핸들러
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isSearching)
            {
                searchWorker.CancelAsync();
            }
            this.Close();
        }

        /// <summary>
        /// 백그라운드 검색 작업
        /// </summary>
        private void SearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            SearchParameters parameters = e.Argument as SearchParameters;
            List<SearchResult> results = new List<SearchResult>();

            try
            {
                // 네트워크 드라이브 목록 가져오기
                foreach (SharedDrive drive in AgentStore.Store.SharedDrives)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (drive.IsConnected && !string.IsNullOrEmpty(drive.DriveName))
                    {
                        string drivePath = drive.DriveName;
                        if (!drivePath.EndsWith("\\"))
                            drivePath += "\\";

                        worker.ReportProgress(0, "검색 중: " + drivePath);

                        SearchDirectory(drivePath, parameters, results, worker);
                    }
                }

                e.Result = results;
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        /// <summary>
        /// 디렉토리를 재귀적으로 검색합니다.
        /// </summary>
        private void SearchDirectory(string directory, SearchParameters parameters,
            List<SearchResult> results, BackgroundWorker worker)
        {
            if (worker.CancellationPending)
                return;

            try
            {
                // 파일 검색
                string[] files = Directory.GetFiles(directory);
                foreach (string file in files)
                {
                    if (worker.CancellationPending)
                        return;

                    try
                    {
                        FileInfo fileInfo = new FileInfo(file);
                        bool match = false;

                        // 파일명 검색
                        if (parameters.SearchFileName)
                        {
                            if (fileInfo.Name.IndexOf(parameters.Keyword,
                                StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                match = true;
                            }
                        }

                        // 파일 내용 검색 (텍스트 파일만)
                        if (!match && parameters.SearchFileContent)
                        {
                            match = SearchFileContent(file, parameters.Keyword);
                        }

                        if (match)
                        {
                            SearchResult result = new SearchResult
                            {
                                FileName = fileInfo.Name,
                                FilePath = fileInfo.FullName,
                                FileSize = fileInfo.Length,
                                ModifiedDate = fileInfo.LastWriteTime
                            };

                            results.Add(result);
                            worker.ReportProgress(results.Count, result);
                        }
                    }
                    catch
                    {
                        // 접근 권한이 없거나 오류 발생한 파일은 건너뜀
                    }
                }

                // 하위 디렉토리 검색
                string[] subdirectories = Directory.GetDirectories(directory);
                foreach (string subdirectory in subdirectories)
                {
                    if (worker.CancellationPending)
                        return;

                    SearchDirectory(subdirectory, parameters, results, worker);
                }
            }
            catch
            {
                // 접근 권한이 없거나 오류 발생한 디렉토리는 건너뜀
            }
        }

        /// <summary>
        /// 텍스트 파일의 내용을 검색합니다.
        /// </summary>
        private bool SearchFileContent(string filePath, string keyword)
        {
            try
            {
                // 텍스트 파일 확장자만 검색
                string extension = Path.GetExtension(filePath).ToLower();
                string[] textExtensions = { ".txt", ".log", ".csv", ".xml", ".json",
                    ".cs", ".cpp", ".h", ".java", ".py", ".js", ".html", ".css",
                    ".sql", ".md", ".ini", ".config", ".bat", ".ps1" };

                bool isTextFile = false;
                foreach (string ext in textExtensions)
                {
                    if (extension == ext)
                    {
                        isTextFile = true;
                        break;
                    }
                }

                if (!isTextFile)
                    return false;

                // 파일 크기 제한 (10MB 이하만 검색)
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 10 * 1024 * 1024)
                    return false;

                // 파일 내용 검색
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string content = reader.ReadToEnd();
                    return content.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 검색 진행 상황 업데이트
        /// </summary>
        private void SearchWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState is string)
            {
                lblStatus.Text = e.UserState.ToString();
            }
            else if (e.UserState is SearchResult)
            {
                SearchResult result = e.UserState as SearchResult;
                AddSearchResult(result);
                lblStatus.Text = string.Format("검색 중... ({0}개 발견)", e.ProgressPercentage);
            }
        }

        /// <summary>
        /// 검색 결과를 리스트뷰에 추가합니다.
        /// </summary>
        private void AddSearchResult(SearchResult result)
        {
            ListViewItem item = new ListViewItem(result.FileName);
            item.SubItems.Add(result.FilePath);
            item.SubItems.Add(FormatFileSize(result.FileSize));
            item.SubItems.Add(result.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss"));
            item.Tag = result;

            lstSearchResults.Items.Add(item);
        }

        /// <summary>
        /// 파일 크기를 읽기 쉬운 형식으로 변환합니다.
        /// </summary>
        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;

            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }

        /// <summary>
        /// 검색 작업 완료
        /// </summary>
        private void SearchWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Visible = false;
            btnSearch.Text = "검색";
            isSearching = false;

            if (e.Cancelled)
            {
                lblStatus.Text = "검색이 취소되었습니다.";
            }
            else if (e.Error != null)
            {
                lblStatus.Text = "검색 중 오류가 발생했습니다.";
                KryptonMessageBox.Show("검색 중 오류가 발생했습니다: " + e.Error.Message,
                    "파일 검색", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Result is Exception)
            {
                Exception ex = e.Result as Exception;
                lblStatus.Text = "검색 중 오류가 발생했습니다.";
                KryptonMessageBox.Show("검색 중 오류가 발생했습니다: " + ex.Message,
                    "파일 검색", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<SearchResult> results = e.Result as List<SearchResult>;
                lblStatus.Text = string.Format("검색 완료: {0}개의 파일을 찾았습니다.",
                    results != null ? results.Count : 0);
            }
        }

        /// <summary>
        /// 검색 결과 더블클릭 시 파일이 있는 폴더를 엽니다.
        /// </summary>
        private void lstSearchResults_DoubleClick(object sender, EventArgs e)
        {
            if (lstSearchResults.SelectedItems.Count > 0)
            {
                SearchResult result = lstSearchResults.SelectedItems[0].Tag as SearchResult;
                if (result != null && File.Exists(result.FilePath))
                {
                    try
                    {
                        // 파일이 있는 폴더를 열고 파일을 선택
                        Process.Start("explorer.exe", "/select, \"" + result.FilePath + "\"");
                    }
                    catch (Exception ex)
                    {
                        KryptonMessageBox.Show("파일을 열 수 없습니다: " + ex.Message,
                            "파일 검색", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 폼이 닫힐 때 검색 작업을 중지합니다.
        /// </summary>
        private void FileSearchDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSearching && searchWorker.IsBusy)
            {
                searchWorker.CancelAsync();

                // 작업이 취소될 때까지 잠시 대기
                for (int i = 0; i < 10 && searchWorker.IsBusy; i++)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
            }
        }

        /// <summary>
        /// 검색 매개변수 클래스
        /// </summary>
        private class SearchParameters
        {
            public string Keyword { get; set; }
            public bool SearchFileName { get; set; }
            public bool SearchFileContent { get; set; }
        }

        /// <summary>
        /// 검색 결과 클래스
        /// </summary>
        private class SearchResult
        {
            public string FileName { get; set; }
            public string FilePath { get; set; }
            public long FileSize { get; set; }
            public DateTime ModifiedDate { get; set; }
        }
    }
}
