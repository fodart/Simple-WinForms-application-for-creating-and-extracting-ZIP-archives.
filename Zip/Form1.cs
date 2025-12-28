using System.IO.Compression;


namespace Zip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Zip(object sender, EventArgs e)
        {
            string tempPath = Path.GetTempPath();

            string uniqueFolderName = "Temp_App" + Guid.NewGuid().ToString();
            string fullTempFolderPath = Path.Combine(tempPath, uniqueFolderName);
            string destinationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CompressedFile.zip");

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Select a file that you want to compress",
                Filter = "All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = true
            };
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    Directory.CreateDirectory(fullTempFolderPath);
                    foreach (string file in openFileDialog.FileNames)
                    {
                        string fileName = Path.GetFileName(file);
                        string destinationFilePath = Path.Combine(fullTempFolderPath, fileName);
                        MessageBox.Show("Selected file: " + file);

                        File.Copy(file, destinationFilePath, overwrite: true);
                    }
                    ZipFile.CreateFromDirectory(fullTempFolderPath, destinationFolder);
                    Directory.Delete(fullTempFolderPath, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
        }

        private void Unzip(object sender, EventArgs e)
        {
            string tempPath = Path.GetTempPath();

            string uniqueFolderName = "Temp_App" + Guid.NewGuid().ToString();
            string fullTempFolderPath = Path.Combine(tempPath, uniqueFolderName);
            string destinationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "CompressedFile.zip");

            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Select a Zip file",
                Filter = "Zip files (*.zip)|*.zip",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                CheckFileExists = true,
                CheckPathExists = true,
            };
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                ZipFile.ExtractToDirectory(openFileDialog.FileName, openFileDialog.InitialDirectory);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Zip(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Unzip(sender, e);
        }
    }
}
