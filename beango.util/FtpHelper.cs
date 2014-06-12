using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace beango.util
{
    public class FtpHelper
    {
        #region "Atributos/Propriedades"

        #region "Atributos"

        FtpWebRequest ftpWebRequest;
        FileInfo fileInfo;
        FileStream fs;
        FtpWebResponse response;
        Uri uriRequest;

        #endregion

        #region "Propriedades"

        public string uri { get; set; }
        public string user { get; set; }
        public string pass { get; set; }

        #endregion

        #endregion

        #region "Construtores"
        public FtpHelper(string uri, string user, string pass)
        {
            try
            {
                this.uri = uri;
                this.user = user;
                this.pass = pass;
                createFTP();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region "Enumeradores"

        private enum MethodFtp
        {
            DeleteFile,
            DownloadFile,
            ListDirectory,
            MakeDirectory,
            RemoveDirectory,
            UploadFile, 
            GetFileSize
        }

        #endregion

        #region "Métodos"

        // Métodos divididos por visibilidade e organizados em ordem alfabética.

        #region "Private"

        /// <summary>Remove os caracteres não necessários na string (\n \r)
        /// </summary>
        /// <param name="text">Texto que deverá ser retirado os caracteres indesejáveis.</param>
        /// <returns>Retorna um texto sem os caractere4s indesejados.</returns>
        private string clearString(string text)
        {
            return text.Replace("\n", "").Replace("\r", "");
        }

        /// <summary>Completa com barra (/) o caminho atual configurado no FTP.
        /// </summary>
        private void completeUri()
        {
            if (!Path.HasExtension(uri))
                if (!uri.EndsWith("/"))
                    uri += "/";
        }

        /// <summary>Método geral para realizar o download de arquivos.
        /// </summary>
        private void generalDownload(FileStream fileStream)
        {
            setMethodFtp(MethodFtp.DownloadFile);
            response = (FtpWebResponse)ftpWebRequest.GetResponse();
            Stream ftpStream = response.GetResponseStream();
            const int bufferSize = 2048;
            byte[] buffer = new byte[bufferSize];

            if (ftpStream != null)
            {
                int readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    fileStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
            }

            if (ftpStream != null) ftpStream.Close();
            response.Close();
        }

        /// <summary>Método geral para realizar o upload de arquivos.
        /// </summary>
        private void generalUpload()
        {
            setMethodFtp(MethodFtp.UploadFile);
            ftpWebRequest.ContentLength = fileInfo.Length;
            const int bufferSize = 2048;
            byte[] buffer = new byte[bufferSize];

            fs = fileInfo.OpenRead();
            Stream ftpStream = ftpWebRequest.GetRequestStream();
            int readCount = fs.Read(buffer, 0, bufferSize);
            while (readCount != 0)
            {
                ftpStream.Write(buffer, 0, readCount);
                readCount = fs.Read(buffer, 0, bufferSize);
            }
            fs.Close();
            ftpStream.Close();
        }

        /// <summary>List Diretórios e Arquivos incluídos no caminho configurado no FTP.
        /// </summary>
        /// <returns>Retorna uma lista de textos com informações dos arquivos e diretórios encontrados.</returns>
        private string[] listDirectoriesAndFiles()
        {
            createFTP();
            setMethodFtp(MethodFtp.ListDirectory);
            List<string> result = new List<string>();
            WebResponse webResponse = ftpWebRequest.GetResponse();
            StreamReader reader = new StreamReader(webResponse.GetResponseStream());
            string line = reader.ReadToEnd();
            Regex regex = new Regex("(?<name>.*\r\n)", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(line);
            foreach (Match m in matches)
                result.Add(m.Groups["name"].ToString());

            return result.ToArray();
        }

        /// <summary>Remove o nome do arquivo em um caminho configurado no FTP.
        /// </summary>
        /// <param name="path">Caminho completo </param>
        private void removeNameFile()
        {
            if (Path.HasExtension(this.uri))
            {
                uri = uri.Remove(uri.LastIndexOf("/"));
                completeUri();
                createFTP();
            }
        }

        /// <summary>Configurado o método FTP utilizado no objeto FtpWebRequest.
        /// </summary>
        /// <param name="method">Método que traz o tipo que se deseja configurar (enumerador Method)</param>
        private void setMethodFtp(MethodFtp method)
        {
            string strMethod = "";
            if (method == MethodFtp.DownloadFile) strMethod = WebRequestMethods.Ftp.DownloadFile;
            else if (method == MethodFtp.MakeDirectory) strMethod = WebRequestMethods.Ftp.MakeDirectory;
            else if (method == MethodFtp.RemoveDirectory) strMethod = WebRequestMethods.Ftp.RemoveDirectory;
            else if (method == MethodFtp.UploadFile) strMethod = WebRequestMethods.Ftp.UploadFile;
            else if (method == MethodFtp.ListDirectory) strMethod = WebRequestMethods.Ftp.ListDirectory;
            else if (method == MethodFtp.DeleteFile) strMethod = WebRequestMethods.Ftp.DeleteFile;
            else if (method == MethodFtp.GetFileSize) strMethod = WebRequestMethods.Ftp.GetFileSize;

            if (ftpWebRequest.Method != strMethod)
            {
                ftpWebRequest.Method = strMethod;
            }
        }

        #endregion

        #region "Public"

        /// <summary>Retorna o diretóriod atual configurado no FTP.
        /// </summary>
        /// <returns>Retorna texto com o caminho do endereço atual.</returns>
        public string actualDirectory()
        {
            string uri = ((Uri)ftpWebRequest.RequestUri).ToString();
            return uri;
        }

        /// OK
        /// <summary>Altera o diretório atual para o diretório específicado na mesma estrutura.
        /// </summary>
        /// <param name="path">Caminho completo para o novo diretório</param>
        /// <returns>Retorna verdadeiro se conseguir acessar o diretório e falso caso não consiga acessar o diretório.</returns>
        public bool alterActualDirectory(string folder)
        {
            try
            {
                removeNameFile();
                completeUri();
                string oldFolder = getNameFolder(this.uri);
                backDirectory();
                if (existsFolder(folder))
                {
                    this.uri += folder;
                    completeUri();
                    createFTP();
                    return true;
                }
                else
                {
                    this.uri += oldFolder;
                    completeUri();
                    createFTP();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
        }

        /// OK
        /// <summary>
        /// Volta um diretório referente ao diretório atual configurado no FTP.
        /// </summary>
        public void backDirectory()
        {
            removeNameFile();
            if (this.uri.EndsWith("/"))
                this.uri = this.uri.Substring(0, this.uri.Length - 1);

            this.uri = this.uri.Remove(uri.LastIndexOf('/'));
            completeUri();
            createFTP();
        }

        /// OK
        /// <summary>Cria um diretório no diretório configurado no FTP.
        /// </summary>
        /// <param name="nameFolder">Texto com o nome do diretório que deseja criar.</param>
        /// <returns>Retorna verdadeiro se o diretório for criado e falso caso o diretório não consiga ser criado.</returns>
        public bool createFolder(string nameFolder)
        {
            if (!existsFolder(nameFolder) && !string.IsNullOrEmpty(nameFolder))
            {
                completeUri();
                this.uri += nameFolder;
                completeUri();
                createFTP();
                setMethodFtp(MethodFtp.MakeDirectory);
                response = (FtpWebResponse)ftpWebRequest.GetResponse();
                response.Close();
                backDirectory();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// OK
        /// <summary>Excluir um arquivo do diretório configurado no FTP.
        /// </summary>
        /// <param name="nameFile">Texto com o nome do arquivo que deseja excluir.</param>
        /// <returns>Retorna verdadeiro caso o arquivo seja excluído e falso caso não consiga excluir o arquivo.</returns>
        public bool deleteFile(string nameFile)
        {
            removeNameFile();
            if (existsFile(nameFile))
            {
                this.uri = this.uri + nameFile;
                createFTP();
                setMethodFtp(MethodFtp.DeleteFile);
                response = (FtpWebResponse)ftpWebRequest.GetResponse();
                removeNameFile();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// OK
        /// <summary>Exclui um diretório no diretório configurado no FTP.
        /// </summary>
        /// <param name="nameFolder">Texto com o nome do diretório a ser excluído.</param>
        /// <returns>Retorna verdadeiro se o diretório for excluído e falso caso o diretório não seja excluído.</returns>
        public bool deleteFolder(string nameFolder)
        {
            if (existsFolder(nameFolder))
            {
                completeUri();
                this.uri += nameFolder;
                completeUri();
                createFTP();
                setMethodFtp(MethodFtp.RemoveDirectory);
                response = (FtpWebResponse)ftpWebRequest.GetResponse();
                backDirectory();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// OK
        /// <summary>Realiza o download de arquivos no servidor remoto para o computador local.
        /// </summary>
        /// <param name="nameFileWithExtension">Nome do arquivo com extensão no diretório atual do FTP</param>
        /// <param name="fileStream">FileStream com arquivo para download</param>
        /// <returns>Retorna valor boolean indicando se o Download foi realizado com sucesso.</returns>
        public bool download(string nameFileWithExtension, ref FileStream fileStream)
        {
            removeNameFile();
            completeUri();
            if (existsFile(nameFileWithExtension))
            {
                uri = uri + nameFileWithExtension;
                createFTP();
                generalDownload(fileStream);
                return true;
            }
            else
                return false;
        }

        /// OK
        /// <summary>Realiza o download de arquivos no servidor remoto para o computador local.
        /// </summary>
        /// <param name="nameFileWithExtension">Nome do arquivo com extensão no diretório atual do FTP</param>
        /// <param name="pathLocal">Caminho do diretório no qual será salvo o arquivo.</param>
        /// <returns>Retorna valor boolean indicando se o Download foi realizado com sucesso.</returns>
        public bool download(string nameFileWithExtension, string pathLocal)
        {
            try
            {
                removeNameFile();
                completeUri();
                if (existsFile(nameFileWithExtension))
                {
                    uri = uri + nameFileWithExtension;
                    createFTP();

                    pathLocal = pathLocal.Replace(@"\", @"/");
                    if (!Path.HasExtension(pathLocal))
                        pathLocal.Remove(pathLocal.LastIndexOf("/"));
                    if (!pathLocal.EndsWith("/"))
                        pathLocal += "/";

                    pathLocal += nameFileWithExtension;

                    fs = new FileStream(pathLocal, FileMode.Create);
                    generalDownload(fs);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                removeNameFile();
            }
        }

        /// OK
        /// <summary>Verifica a existência de um determinado diretório no diretório configurado no FTP.
        /// </summary>
        /// <param name="nameFolder">Texto com o nome do diretório no qual deseja verificar a existência.</param>
        /// <param name="stringComparison">Parametro para especificar qual comparação deve ser realizada</param>
        /// <returns>Retorna verdadeiro se encontrar o diretório e falso se não encontrar.</returns>
        public bool existsFolder(string nameFolder, StringComparison stringComparison)
        {
            string[] folders = listFolders();
            foreach (string f in folders)
                if (clearString(f).Equals(nameFolder, stringComparison)) return true;

            return false;
        }

        /// OK
        /// <summary>Verifica a existência de um determinado diretório no diretório configurado no FTP.
        /// </summary>
        /// <param name="nameFolder">Texto com o nome do diretório no qual deseja verificar a existência.</param>
        /// <returns>Retorna verdadeiro se encontrar o diretório e falso se não encontrar.</returns>
        public bool existsFolder(string nameFolder)
        {
            string[] folders = listFolders();
            foreach (string f in folders)
                if (clearString(f).Equals(nameFolder)) return true;

            return false;
        }

        /// OK
        /// <summary>Verifica a existência de um determinado arquivo no diretório configurado no FTP.
        /// </summary>
        /// <param name="nameFolder">Texto com o nome do arquivo no qual deseja verificar a existência.</param>
        /// <param name="stringComparison">Parametro para especificar o tipo de comparação que será realizada.</param>
        /// <returns>Retorna verdadeiro se encontrar o arquivo e falso se não encontrar.</returns>
        public bool existsFile(string nameFileWithExtension, StringComparison stringComparison)
        {
            string[] files = listFiles();
            foreach (string file in files)
                if (clearString(file).Equals(nameFileWithExtension, stringComparison)) return true;

            return false;
        }

        /// OK
        /// <summary>Verifica a existência de um determinado arquivo no diretório configurado no FTP.
        /// </summary>
        /// <param name="nameFolder">Texto com o nome do arquivo no qual deseja verificar a existência.</param>
        /// <returns>Retorna verdadeiro se encontrar o arquivo e falso se não encontrar.</returns>
        public bool existsFile(string nameFileWithExtension)
        {
            string[] files = listFiles();
            foreach (string file in files)
                if (clearString(file) == nameFileWithExtension) return true;

            return false;
        }

        /// OK
        /// <summary>Cria um objeto do tipo FTP para iniciarmos o gerenciamento.
        /// </summary>
        /// <returns>Retorna um objeto FtpWebRequest</returns>
        public FtpWebRequest createFTP()
        {
            try
            {
                if (this.uri.Contains("%"))
                {
                    this.uri = this.uri.Replace("%", Uri.HexEscape('%'));
                }
                if (this.uri.Contains("#"))
                {
                    this.uri = this.uri.Replace("#", Uri.HexEscape('#'));
                }
                if (this.uri.Contains("&"))
                {
                    this.uri = this.uri.Replace("&", Uri.HexEscape('&'));
                }

                uriRequest = new Uri(this.uri);
                ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(uriRequest);
                ftpWebRequest.Credentials = new NetworkCredential(user, pass);
                ftpWebRequest.UsePassive = true;
                ftpWebRequest.UseBinary = true;
                ftpWebRequest.KeepAlive = false;
                ftpWebRequest.Proxy = null;

                if (ftpWebRequest == null) throw new Exception("Objeto FTP não pode ser inicializado!");

                return ftpWebRequest;
            }
            catch (Exception ex)
            {
                throw;
                return null;
            }
        }

        /// OK
        /// <summary>Retorna o nome do arquivo no caminho mencionado.
        /// </summary>
        /// <param name="path">Caminho completo com o nome do arquivo.</param>
        /// <returns>Retorna texto com o nome do arquivo, caso encontrado.</returns>
        public string getNameFile(string path)
        {
            if (Path.HasExtension(path))
                return Path.GetFileName(path);
            else
                return "";
        }

        public long getSizeFile(string path)
        {
            this.uri = this.uri + path;
            completeUri();
            createFTP();
            setMethodFtp(MethodFtp.GetFileSize);
            response = (FtpWebResponse)ftpWebRequest.GetResponse();
            long cl = response.ContentLength;
            response.Close();
            return cl;
        }

        /// OK
        /// <summary>Retorna o nome do arquivo no caminho mencionado.
        /// </summary>
        /// <returns>Retorna texto com o nome do arquivo, caso encontrado.</returns>
        public string getNameFile()
        {
            if (Path.HasExtension(this.uri))
                return Path.GetFileName(this.uri);
            else
                return "";
        }

        /// OK
        /// <summary>Retorna o nome referente ao nome do diretório do caminho passado.</summary>
        /// <param name="path">Caminho completo.</param>
        /// <returns>Retorna string com o nome do diretório.</returns>
        public string getNameFolder(string path)
        {
            string folderPath = path.Replace(@"\", @"/");
            if (System.IO.Path.HasExtension(folderPath) || folderPath.EndsWith("/"))
                folderPath = path.Remove(folderPath.LastIndexOf('/')).Replace(@"\", @"/");

            int lengthSubstring = (folderPath.Length - folderPath.LastIndexOf('/')) - 1;
            string folder = folderPath.Substring(folderPath.LastIndexOf('/') + 1, lengthSubstring);
            return folder;
        }

        /// OK
        /// <summary>Retorna o nome referente ao nome do diretório do caminho passado.</summary>
        /// <param name="path">Caminho completo.</param>
        /// <returns>Retorna string com o nome do diretório.</returns>
        public string getNameFolder()
        {
            string folderPath = this.uri.Replace(@"\", @"/");
            if (System.IO.Path.HasExtension(folderPath) || folderPath.EndsWith("/"))
                folderPath = this.uri.Remove(folderPath.LastIndexOf('/')).Replace(@"\", @"/");

            int lengthSubstring = (folderPath.Length - folderPath.LastIndexOf('/')) - 1;
            string folder = folderPath.Substring(folderPath.LastIndexOf('/') + 1, lengthSubstring);
            return folder;
        }

        /// OK
        /// <summary>Lista somente diretórios referente ao caminho configurado no FTP.
        /// </summary>
        /// <returns>Retorna uma lista de diretórios em formato texto.</returns>
        public string[] listFolders()
        {
            string[] lista = listDirectoriesAndFiles();
            List<string> result = new List<string>();
            foreach (string item in lista)
                if (!Path.HasExtension(clearString(item)))
                    result.Add(item);

            return result.ToArray();
        }

        /// OK
        /// <summary>Lista somente arquivos referente ao caminho configurado no FTP.
        /// </summary>
        /// <returns>Retorna lista de arquivos em formato texto referente ao diretório configurado no FTP.</returns>
        public string[] listFiles()
        {
            string[] lista = listDirectoriesAndFiles();
            List<string> result = new List<string>();
            foreach (string item in lista)
                result.Add(clearString(item));

            return result.ToArray();
        }

        /// OK
        /// <summary>
        /// Abrir diretório no diretório atual
        /// </summary>
        /// <param name="folder">Nome do diretório a ser aberto</param>
        /// <returns>Retorna verdadeiro se o diretório foi encontrado e aberto ou falso caso contrário.</returns>
        public bool openFolder(string folder)
        {
            removeNameFile();
            completeUri();
            if (existsFolder(folder))
            {
                this.uri += folder;
                completeUri();
                createFTP();
                return true;
            }
            else
                return false;
        }

        /// OK
        /// <summary>Realiza o upload de arquivos locais para o servidor remoto.</summary>
        /// <param name="pathRemote">Caminho completo com o nome do arquivo.</param>
        /// <param name="pathLocal">Caminho completo com o nome do arquivo.</param>
        /// <returns>Retorna valor boolean indicando se o Upload foi realizado com sucesso.</returns>
        public bool upload(string completeURIWithNameFileAndExtension, string pathLocalWithNameFileAndExtension)
        {
            try
            {
                this.uri = completeURIWithNameFileAndExtension;
                createFTP();
                fileInfo = new FileInfo(pathLocalWithNameFileAndExtension);
                generalUpload();
                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
            finally
            {
                removeNameFile();
            }
        }

        /// OK
        /// <summary>Realiza o upload de arquivos recebendo um FileStream
        /// </summary>
        /// <param name="fileStream">Arquivo FileStream</param>
        /// <returns>Retorna valor boolean indicando se o Upload foi realizado com sucesso.</returns>
        public bool upload(FileStream fileStream)
        {
            try
            {
                fileInfo = new FileInfo(fileStream.Name);
                completeUri();
                this.uri += fileInfo.Name;
                createFTP();
                generalUpload();
                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
            finally
            {
                removeNameFile();
            }
        }

        /// OK
        /// <summary></summary>
        /// <param name="pathLocalWithNameFileAndExtension">Caminho completo com o nome do arquivo.</param>
        /// <returns>Retorna valor boolean indicando se o Upload foi realizado com sucesso.</returns>
        public bool upload(string pathLocalWithNameFileAndExtension)
        {
            try
            {
                fileInfo = new FileInfo(pathLocalWithNameFileAndExtension);
                completeUri();
                this.uri += fileInfo.Name;
                createFTP();
                generalUpload();
                return true;
            }
            catch (Exception ex)
            {
                throw;
                return false;
            }
            finally
            {
                removeNameFile();
            }
        }

        #endregion

        #endregion
    }
}
